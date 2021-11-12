using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services
{
    public class ObjectPool : MonoBehaviour, IService
    {
        [Serializable]
        public class Pool
        {
            public GameObject prefab;
            public int size;
            public Transform parent;
        }

        private Dictionary<string, List<GameObject>> poolDict;
        private Dictionary<string, List<GameObject>> poolActiveDict;
        public List<Pool> poolList;
        
        public IEnumerable<GameObject> GetPooledObjects(string poolTag) => poolDict[poolTag];

        private void Awake()
        {
            poolDict = new Dictionary<string, List<GameObject>>();
            poolActiveDict = new Dictionary<string, List<GameObject>>();

            foreach (var pool in poolList)
            {
                var objectPool = new List<GameObject>();
                var objectActiveList = new List<GameObject>();

                for (var i = 0; i < pool.size; i++)
                {
                    pool.prefab.SetActive(false);
                    var obj = Instantiate(pool.prefab, pool.parent);
                    objectPool.Add(obj);
                }

                var poolableComponent = pool.prefab.GetComponent<Poolable>();

                if (poolableComponent is null)
                {
                    Debug.LogError($"There is no Poolable component on prefab {pool.prefab.name}");
                    continue;
                }

                poolDict.Add(poolableComponent.PoolTag, objectPool);
                poolActiveDict.Add(poolableComponent.PoolTag, objectActiveList);
            }
        }

        private GameObject GetNextPoolableObject(string poolTag)
        {
            var obj = poolDict[poolTag].FirstOrDefault(go => !go.activeInHierarchy);

            if (obj is {}) return obj;
            
            obj = poolActiveDict[poolTag][0];
            poolActiveDict[poolTag].RemoveAt(0);
            obj.SetActive(false);

            return obj;
        }

        private void ActivateObject(GameObject obj, string poolTag)
        {
            obj.SetActive(true);
            poolActiveDict[poolTag].Add(obj);
        }

        private GameObject SpawnObject(string poolTag, Vector3 position, Quaternion rotation)
        {
            if (!poolDict.ContainsKey(poolTag))
            {
                throw new InvalidOperationException($"No {poolTag} tag in pool");
            }
            
            var obj = GetNextPoolableObject(poolTag);
            obj.transform.position = position;
            obj.transform.rotation = rotation;

            return obj;
        }
        
        public GameObject SpawnWithSetup(string poolTag, Vector3 position, Quaternion rotation, Action<GameObject> setup)
        {
            var obj = SpawnObject(poolTag, position, rotation);
            
            setup?.Invoke(obj);
            
            ActivateObject(obj, poolTag);
            
            return obj;
        }
        
        public T SpawnWithSetup<T>(string poolTag, Vector3 position, Quaternion rotation, Action<T> setup) where T:Component
        {
            var obj = SpawnObject(poolTag, position, rotation);

            if (!obj.TryGetComponent(out T component))
            {
                throw new InvalidOperationException($"No component {nameof(T)} at {poolTag} object");
            }
            
            setup?.Invoke(component);
            
            ActivateObject(obj, poolTag);
            
            return component;
        }
        
        public GameObject Spawn(string poolTag, Vector3 position, Quaternion rotation)
            => SpawnWithSetup(poolTag, position, rotation, null);

        public T Spawn<T>(string poolTag, Vector3 position, Quaternion rotation) where T : Component
        {
            var go = Spawn(poolTag, position, rotation);
            
            if (go.TryGetComponent(out T component)) return component;
            
            throw new InvalidOperationException($"No component {nameof(T)} at {poolTag} object");
        }
    }
}