using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float speed = 5;
    public float cooldown = 0.1f;

    private Action shoot;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector3 shootInput;
    private float lastShot = 0;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shoot = ShootBullet;
    }
    
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        shootInput = new Vector3();
        shootInput.x = Input.GetAxis("HorizontalArrow");
        shootInput.y = Input.GetAxis("VerticalArrow");

        if(shootInput != Vector3.zero && lastShot + cooldown <= Time.time)
        {
            lastShot = Time.time;

            shoot();
        }
    }

    void ShootBullet()
    {
        var bullet = Instantiate(bulletPrefab, transform.position + shootInput.normalized, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = shootInput.normalized * speed * 3;

        Destroy(bullet, 2);
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * speed * Time.fixedDeltaTime);
    }

    public void ChangeShoot(Action newShoot, float duration)
    {
        StartCoroutine(ChangeShootCoroutine(newShoot, duration));
    }

    IEnumerator ChangeShootCoroutine(Action newShoot, float duration)
    {
        shoot = newShoot;
        yield return new WaitForSeconds(duration);
        shoot = ShootBullet;
    }
}
