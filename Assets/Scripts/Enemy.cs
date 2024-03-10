using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5;
    [Range(0, 1f)]
    public float lootChance = 0.5f;
    public GameObject[] loot;
    
    Transform target;
    private Rigidbody2D rb;
    
    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        var realTarget = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(realTarget);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Die();
        }
    }

    public void Die(bool dropLoot = true)
    {
        Splasher.instance.SpawnSplash(transform.position);
        if (dropLoot && Random.value < lootChance)
        {
            var randomLoot = loot[Random.Range(0, loot.Length)];
            Instantiate(randomLoot, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
