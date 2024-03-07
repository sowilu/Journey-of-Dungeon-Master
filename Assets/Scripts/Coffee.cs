using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    public float speed = 7;
    public float effectDuration = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().ChangeSpeed(speed, 5);
            transform.position = GameObject.Find("Inventory").transform.position;
            Destroy(gameObject, effectDuration);
        }
    }
}
