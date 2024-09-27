using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace com.ethnicthv.Script
{
    public class ForeignObjectEmitter : MonoBehaviour
    {
        public GameObject[] foreignObjectPrefab;

        private Pool<ForeignObjectController> _pool;

        private List<ForeignObjectController> _objects = new();
        
        private RectTransform _rectTransform;

        private void Awake()
        {
            var objects = new List<ForeignObjectController>();
            foreach (var prefab in foreignObjectPrefab)
            {
                for (int i = 0; i < 10; i++)
                {
                    var obj = Instantiate(prefab, transform).GetComponent<ForeignObjectController>();
                    obj.gameObject.SetActive(false);
                    objects.Add(obj);
                }
            }
            
            // shuffle the list
            objects = objects.OrderBy(x => Random.value).ToList();

            _pool = new Pool<ForeignObjectController>(() =>
            {
                var obj = Instantiate(foreignObjectPrefab[Random.Range(0, foreignObjectPrefab.Length)], transform)
                    .GetComponent<ForeignObjectController>();
                obj.gameObject.SetActive(false);
                return obj;
            }, objects);
        }

        private void OnEnable()
        {
            StartCoroutine(EmittingCoroutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator EmittingCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                _rectTransform = (RectTransform)transform;
                var rect = _rectTransform.rect;
                var boundX = rect.width/2;
                var boundY = rect.height/2;
                var direction = Random.Range(0, 2) == 0 ? 1 : -1;
                var a = new Vector2(boundX * direction, Random.Range(-boundY, boundY));
                Debug.Log("Emitting object " + rect + " " + a);
                EmitObject(a, direction);
                
                _rectTransform = (RectTransform)transform;
                for (var i = 0; i < _objects.Count; i++)
                {
                    try
                    {
                        var oRectTransform = (RectTransform)_objects[i].transform;
                        if (oRectTransform.anchoredPosition.x < -_rectTransform.rect.width / 2 * 1.1f) ReturnObject(_objects[i]);
                        if (oRectTransform.anchoredPosition.x > _rectTransform.rect.width / 2 * 1.1f) ReturnObject(_objects[i]);
                    }
                    catch (InvalidOperationException)
                    {
                    }
                }
            }
        }

        public void EmitObject(Vector2 position, float direction = 1f)
        {
            var obj = _pool.Get();
            var rectTransform = (RectTransform)obj.transform;
            rectTransform.anchoredPosition = position;
            obj.gameObject.SetActive(true);
            obj.direction = direction;
            _objects.Add(obj);
        }

        public void ReturnObject(ForeignObjectController obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Return(obj);
            _objects.Remove(obj);
        }
    }
}