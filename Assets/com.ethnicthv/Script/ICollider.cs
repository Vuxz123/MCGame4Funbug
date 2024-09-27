using UnityEngine;

namespace com.ethnicthv.Script
{
    public interface ICollider
    {
        float BoundRadius { get; }
        Transform Transform { get; }
    }
}