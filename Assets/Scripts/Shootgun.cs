using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootgun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 15;
    public float effectDuration = 10;

    protected Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public virtual void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, player.transform.position + player.shootInput, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = player.shootInput * bulletSpeed;
        Destroy(bullet, 2);

        var angle = Quaternion.Euler(0, 0, 30) * player.shootInput;
        var bullet2 = Instantiate(bulletPrefab, player.transform.position + angle, Quaternion.identity);
        bullet2.GetComponent<Rigidbody2D>().velocity = angle * bulletSpeed;
        Destroy(bullet2, 2);

        angle = Quaternion.Euler(0, 0, -30) * player.shootInput;
        var bullet3 = Instantiate(bulletPrefab, player.transform.position + angle, Quaternion.identity);
        bullet3.GetComponent<Rigidbody2D>().velocity = angle * bulletSpeed;
        Destroy(bullet3, 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.ChangeGun(Shoot, effectDuration);
            transform.position = GameObject.Find("Inventory").transform.position;
            Destroy(gameObject, effectDuration);
        }
    }
}
