using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region singelton

    private static ObjectPooler _instance;

    public static ObjectPooler instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObjectPooler>();
            } 
            return _instance;
        }
    }

    #endregion
    
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int poolSize;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private GameObject pooledObjects;
    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        GameObject poolObjectHolder = new GameObject("Holder");
        poolObjectHolder.transform.parent = transform;
        pooledObjects = new GameObject("Holder");
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            
            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, pooledObjects.transform, true);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);  
        }
    }
    
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
    
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, Action OnSpawnObject)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        OnSpawnObject?.Invoke();
        
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

}





/*


public class PoolObject : MonoBehaviour
{
    public virtual void OnObjectReuse()
    {
        
    }

    protected void Destroy()
    {
        gameObject.SetActive(false);
    }
}

    Sebastian Lague method : 
public class PoolManager : MonoBehaviour
{
 private Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();

    private static PoolManager _instance;

    public static PoolManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PoolManager>();
            } 
            return _instance;
        }
    }
    
    public void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();

        GameObject poolHolder = new GameObject(prefab.name + " pool");
        poolHolder.transform.parent = transform;
        
        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<ObjectInstance>());

            for (int i = 0; i < poolSize; i++)
            {
                ObjectInstance newObject = new ObjectInstance(Instantiate(prefab));
                poolDictionary[poolKey].Enqueue(newObject);
                newObject.SetParent(poolHolder.transform);
            }
        }
    }

    public void ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolKey))
        {
            ObjectInstance objectToReuse = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToReuse);
            objectToReuse.Reuse(position, rotation);
        }
    }
    
    public class ObjectInstance
    {
        private GameObject refGameObject;
        private Transform refTransform;

        private bool hasPoolObjectComponent;
        private PoolObject poolObjectScript;

        public ObjectInstance(GameObject objectInstance)
        {
            refGameObject = objectInstance;
            refTransform = refGameObject.transform;
            refGameObject.SetActive(false);
            
            if (refGameObject.GetComponent<PoolObject>())
            {
                hasPoolObjectComponent = true;
                poolObjectScript = refGameObject.GetComponent<PoolObject>();
            }
        }

        public void Reuse(Vector3 position, Quaternion rotation)
        {
            if (hasPoolObjectComponent)
            {
                poolObjectScript.OnObjectReuse();
            }
            refGameObject.SetActive(true);
            refGameObject.transform.position = position;
            refGameObject.transform.rotation = rotation;
        }

        public void SetParent(Transform parent)
        {
            refTransform.parent = parent;
        }
    }
    
    }
    */
    
    