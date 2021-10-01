using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Simpleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    private Dictionary<string, Queue<GameObject>> poolDict;
    public List<Pool> poolList;

    private void Start()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in poolList)
        {
            var objectPool = new Queue<GameObject>();

            var parent = new GameObject(pool.tag + "s");

            for (var i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.prefab, parent.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDict.Add(pool.tag, objectPool);
        }
    }

    public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
    {
        if (poolDict == null) return null;

        if (!poolDict.ContainsKey(tag))
        {
            return null;
        }

        var obj = poolDict[tag].Dequeue();

        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        poolDict[tag].Enqueue(obj);

        return obj;
    }
}