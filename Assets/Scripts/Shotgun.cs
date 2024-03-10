using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public float speed = 15;
    public float effectTime = 5;
    public GameObject bulletPrefab;

    private Player player;

    private void Start()
    {
       player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, player.transform.position + player.shootInput.normalized, Quaternion.Euler(0, 0, 0));
        bullet.GetComponent<Rigidbody2D>().velocity = player.shootInput.normalized * speed;
        Destroy(bullet, 2);

        var angle = Quaternion.Euler(0, 0, 45) * player.shootInput.normalized;
        var bullet2 = Instantiate(bulletPrefab, player.transform.position + angle, Quaternion.identity);
        bullet2.GetComponent<Rigidbody2D>().velocity = angle * speed;
        Destroy(bullet2, 2);

        angle = Quaternion.Euler(0, 0, -45) * player.shootInput.normalized;
        var bullet3 = Instantiate(bulletPrefab, player.transform.position + angle, Quaternion.identity);
        bullet3.GetComponent<Rigidbody2D>().velocity = angle * speed;
        Destroy(bullet3, 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.ChangeGun(Shoot, effectTime);

            transform.position = GameObject.Find("Inventory").transform.position;

            Destroy(gameObject, effectTime);
        }
    }
}
