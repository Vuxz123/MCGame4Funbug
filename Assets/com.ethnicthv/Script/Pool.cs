using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.ethnicthv.Script
{
    public class Pool<T> where T : IResettable
    {
        private readonly Queue<T> _pool = new();
        private readonly Func<T> _factory;

        public Pool(Func<T> factory)
        {
            _factory = factory;
        }
        
        public Pool(Func<T> factory, List<T> preloadObjects)
        {
            _factory = factory;
            Preload(preloadObjects);
        }
        
        public void Preload(List<T> objects)
        {
            foreach (var obj in objects)
            {
                _pool.Enqueue(obj);
            }
        }

        public T Get()
        {
            return _pool.TryDequeue(out var obj) ? obj : _factory();
        }

        public void Return(T obj)
        {
            obj.ResetObject();
            _pool.Enqueue(obj);
        }
    }
}