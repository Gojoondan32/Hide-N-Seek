using System.Collections.Generic;
using UnityEngine;

public class Object_Pool<T> where T : Component
{
    private readonly T prefab;
    private readonly Transform parentTransform;
    private readonly Queue<T> pool = new Queue<T>();

    public Object_Pool(T prefab, Transform parentTransform, int initialSize = 0)
    {
        this.prefab = prefab;
        this.parentTransform = parentTransform;

        for (int i = 0; i < initialSize; i++)
        {
            AddObjectToPool();
        }
    }

    private void AddObjectToPool()
    {
        T newObject = GameObject.Instantiate(prefab, parentTransform);
        newObject.gameObject.SetActive(false);
        pool.Enqueue(newObject);
    }

    public T GetObjectFromPool()
    {
        if (pool.Count == 0)
        {
            AddObjectToPool();
        }

        T objectFromPool = pool.Dequeue();
        objectFromPool.gameObject.SetActive(true);
        return objectFromPool;
    }

    public void ReturnObjectToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        pool.Enqueue(objectToReturn);
    }
}