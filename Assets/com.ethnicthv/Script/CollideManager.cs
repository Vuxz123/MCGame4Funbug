using System;
using System.Collections.Generic;
using com.ethnicthv.Script.EventImpl;
using UnityEngine;

namespace com.ethnicthv.Script
{
    public class CollideManager : MonoBehaviour
    {
        public static CollideManager Instance { get; private set; }
        
        private readonly List<ICollider> _colliders = new();
        
        private readonly List<ICollider> _collidingColliders = new();
        
        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            for (var i = 0; i < _colliders.Count; i++)
            {
                for (var j = i + 1; j < _colliders.Count; j++)
                {
                    var collider1 = _colliders[i];
                    var collider2 = _colliders[j];
                    var distance = Vector3.Distance(collider1.Transform.position, collider2.Transform.position);
                    if (distance < collider1.BoundRadius + collider2.BoundRadius)
                    {
                        if (!_collidingColliders.Contains(collider1) || !_collidingColliders.Contains(collider2))
                        {
                            _collidingColliders.Add(collider1);
                            _collidingColliders.Add(collider2);
                            EventSystem.instance.TriggerEvent(new ObjectCollisionEnterEvent(collider1, collider2));
                        }
                        
                        EventSystem.instance.TriggerEvent(new ObjectCollisionEvent(collider1, collider2));
                    }
                    else
                    {
                        if (_collidingColliders.Contains(collider1) && _collidingColliders.Contains(collider2))
                        {
                            _collidingColliders.Remove(collider1);
                            _collidingColliders.Remove(collider2);
                            EventSystem.instance.TriggerEvent(new ObjectCollisionExitEvent(collider1, collider2));
                        }
                    }
                }
            }
        }

        public void RegisterCollision(ICollider col)
        {
            _colliders.Add(col);
        }
        
        public void UnregisterCollision(ICollider col)
        {
            _colliders.Remove(col);
        }
    }
}