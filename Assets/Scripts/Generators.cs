using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generators : MonoBehaviour
{
    public GameObject enemyType;
    public int maxQuantity = 10;

    public float currentTime = 0f;
    public float spawnTimer = 3f;

    public Transform enemyParent;

    // all enemies spawned from this object
    public List<GameObject> children;

    private void Awake()
    {
        enemyParent = GameObject.FindGameObjectWithTag("EnemyManager").transform;
    }

    private void Update()
    {
        // checks if there is room to spawn more, if so, do so on a timer
        if (children.Count < maxQuantity)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= spawnTimer)
            {
                SpawnEnemy();
                currentTime = 0f;
            }
        }
    }

    public void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyType, transform.position, Quaternion.identity, enemyParent);
        newEnemy.GetComponent<Enemies>().myGenerator = this.gameObject;
        children.Add(newEnemy);
    }

}
