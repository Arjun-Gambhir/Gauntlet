using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum pooledObjectType
{
    player,
    projectile,
    pickup,
    enemy
}

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}

public class ObjectPooler : MonoBehaviour
{
    // Singleton
    public static ObjectPooler Instance;

    // Object Pooling
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool; // List of objects to be pooled, along with whether they should be exxpandable and quantity, edited via inspector

    // Parent objects (empty gameobjects)
    public Transform playerParent;
    public Transform projectileParent;
    public Transform itemParent;
    public Transform enemiesParent;


    private void Awake()
    {
        // Set single instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = GetComponent<ObjectPooler>();
    }

    // object pool

    private void Start()
    {

        // empty list and create all objects set to be added from the inspector
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
                setParent(obj);
            }
        }

    }


    // Returns an inactive object via tag. If there is no such inactive object of that tag, create one from the list<itemsToPool> if it has one.
    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    setParent(obj);
                    return obj;
                }
            }
        }
        return null;
    }


    // 'Checks the tag of the objects (called when they are created) and then assigns them a parent in the heirarchy so it can be kept clean
    private void setParent(GameObject child)
    {
        //switch (pooledObjectType)
        //{
        //    case pooledObjectType.player:
        //        break;
        //    case pooledObjectType.projectile:
        //        break;
        //    case pooledObjectType.pickup:
        //        break;
        //    case pooledObjectType.enemy:
        //        break;
        //    default:
        //        break;
        //}

        //switch (child.tag)
        //{
        //    case "Spread":
        //        child.transform.SetParent(projectileParent);
        //        break;
        //    case "Blast":
        //        child.transform.SetParent(projectileParent);
        //        break;
        //    case "EnemyNormal":
        //        child.transform.SetParent(shipParent);
        //        break;
        //    case "EnemyFast":
        //        child.transform.SetParent(shipParent);
        //        break;
        //    case "EnemyBezier":
        //        child.transform.SetParent(shipParent);
        //        break;
        //    case "EnemyShield":
        //        child.transform.SetParent(shipParent);
        //        break;
        //    case "EnemyCarrier":
        //        child.transform.SetParent(shipParent);
        //        break;
        //    case "PowerUpBlaster":
        //        child.transform.SetParent(powerUpParent);
        //        break;
        //    case "PowerUpSpread":
        //        child.transform.SetParent(powerUpParent);
        //        break;
        //    case "PowerUpShield":
        //        child.transform.SetParent(powerUpParent);
        //        break;
        //    default:
        //        Debug.Log("1");
        //        break;
        //}

    }

}
