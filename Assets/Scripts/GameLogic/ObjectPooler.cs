using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;


public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<ObjectPoolItem> m_itemsToPool;
    private List<GameObject> m_pooledObjects;

    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject m_objectToPool;
        public int m_amountToPool;
        public bool m_shouldExpand = true;
    }



    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        m_pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in m_itemsToPool)
        {
            for (int i = 0; i < item.m_amountToPool; i++)
            {
                InstantiatePoolObject(item.m_objectToPool);
            }
        }
    }

    public GameObject GetPooledObject(Type componentType)
    {
        for (int i = 0; i < m_pooledObjects.Count; i++)
        {
            if (!m_pooledObjects[i].activeInHierarchy && m_pooledObjects[i].GetComponent(componentType))
            {
                return m_pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in m_itemsToPool)
        {
            if (item.m_objectToPool.GetComponent(componentType))
            {
                if (item.m_shouldExpand)
                {
                    return InstantiatePoolObject(item.m_objectToPool);
                }
            }
        }
        return null;
    }

    GameObject InstantiatePoolObject(GameObject gameObjectToPool)
    {
        GameObject obj = Instantiate(gameObjectToPool);
        obj.SetActive(false);
        m_pooledObjects.Add(obj);
        obj.transform.SetParent(transform);
        return obj;
    }
}
