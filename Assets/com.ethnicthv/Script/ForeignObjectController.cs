using System;
using DG.Tweening;
using UnityEngine;

namespace com.ethnicthv.Script
{
    public class ForeignObjectController : MonoBehaviour, ICollider, IResettable
    {
        public ForeignObjectEmitter foreignObjectEmitter;
        public float speed = 1.0f;
        public float boundRadius = 1.0f;

        public float maxX;
        
        public float direction = 1.0f;

        private void OnEnable()
        {
            CollideManager.Instance.RegisterCollision(this);
        }

        private void Update()
        {
            var rectTransform = (RectTransform)transform;
            rectTransform.anchoredPosition += Vector2.left * (speed * Time.deltaTime * direction);
        }

        private void OnDisable()
        {
            CollideManager.Instance.UnregisterCollision(this);
        }

        public void ResetObject()
        {
            
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, boundRadius);
        }

        public float BoundRadius => boundRadius;
        public Transform Transform => transform;
    }
}
