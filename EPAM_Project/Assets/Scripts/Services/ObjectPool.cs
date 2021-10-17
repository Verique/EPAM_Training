using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class ObjectPool : MonoBehaviour, IService
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
            public Transform parent;
        }

        private Dictionary<string, Queue<GameObject>> poolDict;
        public List<Pool> poolList;

        private void Start()
        {
            poolDict = new Dictionary<string, Queue<GameObject>>();

            foreach (var pool in poolList)
            {
                var objectPool = new Queue<GameObject>();

                for (var i = 0; i < pool.size; i++)
                {
                    var obj = Instantiate(pool.prefab, pool.parent);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDict.Add(pool.tag, objectPool);
            }
        }

        public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
        {
            if (poolDict == null) 
            {
                Debug.LogError($"Dict is null");
                return null;
            } 

            if (!poolDict.ContainsKey(tag))
            {
                Debug.LogError($"There are no {tag} objects in pool");
                return null;
            } 

            var obj = poolDict[tag].Dequeue();

            obj.SetActive(false);
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;

            poolDict[tag].Enqueue(obj);

            return obj;
        }
    }
}