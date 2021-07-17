using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // Using Singleton Design Pattern to reach this script everywhere.
    public static ObjectPooler instance;

    private void Awake()
    {
        instance = this;
    }

    // Creating pool class to initialize our needs.
    [System.Serializable]
    public class Pool
    {
        public string type;
        public GameObject prefab;
        public int size;
    }

    // Creating pool List & Dictionary
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictonary;
    private GameObject objectToSpawn;

    // Prefabs are instantiating when game start and then they are queuing up.
    private void Start()
    {
        poolDictonary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictonary.Add(pool.type, objectPool);
        }
    }

    // In SpawnFromPool method, Prefabs will be active and visible one by one.
    public GameObject SpawnFromPool(string type, Vector3 position, Quaternion rotation)
    {
        if (!poolDictonary.ContainsKey(type))
        {
            Debug.LogWarning("Pool with type: " + type + " doesn't exist.");
            return null;
        }

        objectToSpawn = poolDictonary[type].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        poolDictonary[type].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}