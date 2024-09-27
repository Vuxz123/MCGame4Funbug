using com.ethnicthv.Script.EventImpl;
using com.ethnicthv.Script.Input;
using UnityEngine;
using UnityEngine.UI;

namespace com.ethnicthv.Script
{
    public class AntibodyController : MonoBehaviour, ICollider
    {
        public ForeignObjectEmitter foreignObjectEmitter;

        public RectTransform canvas;
        public float speed = 1.0f;
        public float boundRadius = 1.0f;

        private Player _player;

        private int _listener1Id;
        private int _listener2Id;
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
        }

        private void Awake()
        {
            _player = new Player();
        }

        private void OnEnable()
        {
            CollideManager.Instance.RegisterCollision(this);
            _listener1Id = EventSystem.instance.RegisterListener<ObjectCollisionEvent>(OnCollision);
            _listener2Id = EventSystem.instance.RegisterListener<ObjectCollisionExitEvent>(OnColExit);
        }

        private void Update()
        {
            var rectTransform = (RectTransform)transform;
            if (UnityEngine.Input.GetKey(KeyCode.W) || UnityEngine.Input.GetKey(KeyCode.UpArrow))
            {
                rectTransform.anchoredPosition += new Vector2(0, speed * Time.deltaTime);
            }

            if (UnityEngine.Input.GetKey(KeyCode.S) || UnityEngine.Input.GetKey(KeyCode.DownArrow))
            {
                rectTransform.anchoredPosition += new Vector2(0, -speed * Time.deltaTime);
            }

            if (UnityEngine.Input.GetKey(KeyCode.A) || UnityEngine.Input.GetKey(KeyCode.LeftArrow))
            {
                rectTransform.anchoredPosition += new Vector2(-speed * Time.deltaTime, 0);
            }

            if (UnityEngine.Input.GetKey(KeyCode.D) || UnityEngine.Input.GetKey(KeyCode.RightArrow))
            {
                rectTransform.anchoredPosition += new Vector2(speed * Time.deltaTime, 0);
            }

            rectTransform.anchoredPosition = new Vector2(
                Mathf.Clamp(rectTransform.anchoredPosition.x, -canvas.rect.width / 2, canvas.rect.width / 2),
                Mathf.Clamp(rectTransform.anchoredPosition.y, -canvas.rect.height / 2, canvas.rect.height / 2));
        }

        private void OnDisable()
        {
            CollideManager.Instance.UnregisterCollision(this);
            EventSystem.instance.UnregisterListener<ObjectCollisionEvent>(_listener1Id);
            EventSystem.instance.UnregisterListener<ObjectCollisionExitEvent>(_listener2Id);
        }

        private void OnCollision(ObjectCollisionEvent ev)
        {
            if (ev.BaseCollider.Transform.gameObject != gameObject &&
                ev.TargetCollider.Transform.gameObject != gameObject) return;
            _image.color = Color.red;
            var foreignObjectController =
                (gameObject == ev.BaseCollider.Transform.gameObject
                    ? ev.TargetCollider.Transform.gameObject
                    : ev.BaseCollider.Transform.gameObject).GetComponent<ForeignObjectController>();
            foreignObjectEmitter.ReturnObject(foreignObjectController);
            GameManager.Instance.Eat();
        }

        private void OnColExit(ObjectCollisionExitEvent ev)
        {
            if (ev.BaseCollider.Transform.gameObject != gameObject &&
                ev.TargetCollider.Transform.gameObject != gameObject) return;
            _image.color = Color.white;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, boundRadius);
        }

        public float BoundRadius => boundRadius;
        public Transform Transform => transform;
    }
}