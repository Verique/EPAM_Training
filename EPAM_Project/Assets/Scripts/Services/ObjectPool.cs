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
                    var obj = Instantiate(pool.prefab, pool.parent);
                    obj.SetActive(false);
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

        public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDict.ContainsKey(tag))
            {
                return null;
            }

            var obj = poolDict[tag].FirstOrDefault(go => !go.activeInHierarchy);

            if (obj is null)
            {
                obj = poolActiveDict[tag][0];
                poolActiveDict[tag].RemoveAt(0);
                obj.SetActive(false);
            }
            
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);

            poolActiveDict[tag].Add(obj);

            return obj;
        }

        public void Despawn(string tag, GameObject obj)
        {
            obj.SetActive(false);
            poolActiveDict[tag].Remove(obj);
        }

        private GameObject GetNonActiveInQueue(IEnumerable<GameObject> list)
        {
            return list.FirstOrDefault(go => !go.activeInHierarchy);
        }
    }
}