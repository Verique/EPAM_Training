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

        public List<GameObject> GetPooledObjects(string poolTag) => poolDict[poolTag];

        public GameObject SpawnDisabled(string poolTag, Vector3 position, Quaternion rotation)
        {
            if (!poolDict.ContainsKey(poolTag))
            {
                return null;
            }

            var obj = poolDict[poolTag].FirstOrDefault(go => !go.activeInHierarchy);

            if (obj is null)
            {
                obj = poolActiveDict[poolTag][0];
                poolActiveDict[poolTag].RemoveAt(0);
                obj.SetActive(false);
            }
            
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            poolActiveDict[poolTag].Add(obj);
            
            return obj;
        }
        
        public GameObject Spawn(string poolTag, Vector3 position, Quaternion rotation)
        {
            var obj = SpawnDisabled(poolTag, position, rotation);
            
            obj.SetActive(true);

            return obj;
        }

        public T Spawn<T>(string poolTag, Vector3 position, Quaternion rotation) where T : Component
        {
            var go = Spawn(poolTag, position, rotation);
            
            if (go.TryGetComponent(out T component)) return component;
            
            throw new InvalidOperationException($"No component {nameof(T)} at {poolTag} object");
        }
        
        public T SpawnDisabled<T>(string poolTag, Vector3 position, Quaternion rotation) where T : Component
        {
            var go = SpawnDisabled(poolTag, position, rotation);
            
            if (go.TryGetComponent(out T component)) return component;
            
            throw new InvalidOperationException($"No component {nameof(T)} at {poolTag} object");
        }
    }
}