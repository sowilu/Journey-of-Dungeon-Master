using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemies;
    public float minSpawnTime = 0.5f;
    public float maxSpawnTime = 2f;
    public float spawnDelay = 1f;
    
    void Start()
    {
        StartCoroutine(Spawn());
    }
    
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        while (true)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            int enemyIndex = Random.Range(0, enemies.Length);
            Instantiate(enemies[enemyIndex], spawnPoints[spawnPointIndex].position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
}
