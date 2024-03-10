using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public float effectDuration = 10;
    public float cooldown = 0.1f;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().ChangeCooldown(cooldown, effectDuration);
            transform.position = GameObject.Find("Inventory").transform.position;
            Destroy(gameObject, effectDuration);
        }
    }   
}
