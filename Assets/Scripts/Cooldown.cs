using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public float cooldown = 0.1f;
    public float effectDuration = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().ChangeCooldown(cooldown, 5);
            transform.position = GameObject.Find("Inventory").transform.position;
            Destroy(gameObject, effectDuration);
        }
    }
}
