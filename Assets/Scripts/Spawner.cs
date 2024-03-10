using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemies;
    public float minSpawnTime = 0;
    public float maxSpawnTime = 2;
    public float spawnDelay = 3;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDelay);

        while (true)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            int enemyIndex = Random.Range(0, enemies.Length);

            Instantiate(enemies[enemyIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
            
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }
    
}
