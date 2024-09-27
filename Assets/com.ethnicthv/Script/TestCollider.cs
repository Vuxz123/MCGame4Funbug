using System;
using UnityEngine;

namespace com.ethnicthv.Script
{
    public class TestCollider : MonoBehaviour, ICollider
    {
        public float boundRadius;

        private void OnEnable()
        {
            CollideManager.Instance.RegisterCollision(this);
        }
        
        private void OnDisable()
        {
            CollideManager.Instance.UnregisterCollision(this);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, boundRadius);
        }

        public float BoundRadius => boundRadius;
        public Transform Transform => transform;
    }
}