using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    public float effectDuration = 10;
    public float speed = 7;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().ChangeSpeed(speed, effectDuration);
            transform.position = GameObject.Find("Inventory").transform.position;
            Destroy(gameObject, effectDuration);
        }
    }
}
