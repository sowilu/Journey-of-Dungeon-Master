using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2;
    [Range(0, 1)]public float lootChance = 0.5f;
    public GameObject[] loot;

    Transform target;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        var realTarget = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(realTarget);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Die();
        }
    }

    public void Die(bool dropLoot = true)
    {
        Splasher.instance.SpawnSplash(transform.position);
        if(dropLoot && Random.value < lootChance)
        {
            var lootIndex = Random.Range(0, loot.Length);
            Instantiate(loot[lootIndex], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
