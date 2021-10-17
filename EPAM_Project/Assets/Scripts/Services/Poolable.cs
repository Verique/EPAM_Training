using System;
using UnityEngine;

namespace Services
{
    public class Poolable : MonoBehaviour
    {
        [SerializeField] private string poolTag;
        public string PoolTag => poolTag;
        private ObjectPool pool;

        private void Start()
        {
            pool = ServiceLocator.Instance.Get<ObjectPool>();
        }

        private void OnDisable()
        {
            if (pool != null)
                pool.Despawn(poolTag, gameObject);
        }
    }
}