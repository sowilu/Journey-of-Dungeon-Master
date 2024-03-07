using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    public GameObject bulletPrefab;
    public float cooldown = 0.5f;

    [HideInInspector] public Vector3 shootInput;
    
    Rigidbody2D rb;
    Vector2 moveInput;
    float lastShot = 0;
    Action shoot;
    float originalSpeed;


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
        shootInput.x = Input.GetAxisRaw("HorizontalArrow");
        shootInput.y = Input.GetAxisRaw("VerticalArrow");

        if (shootInput != Vector3.zero && lastShot + cooldown <= Time.time)
        {
            lastShot = Time.time;

            shoot();
        }
    }

    public void ShootBullet()
    {
        var bullet = Instantiate(bulletPrefab, transform.position + shootInput, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = shootInput * 15;
        Destroy(bullet, 2);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * speed * Time.fixedDeltaTime);
    }

    public void ChangeGun(Action newShoot, float duration)
    {
        StartCoroutine(ChangeGunCoroutine(newShoot, duration));
    }

    IEnumerator ChangeGunCoroutine(Action newShoot, float duration)
    {
        shoot = newShoot;
        yield return new WaitForSeconds(duration);
        shoot = ShootBullet;
    }
    
    public void ChangeSpeed(float newSpeed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(newSpeed, duration));
    }
    
    IEnumerator ChangeSpeedCoroutine(float newSpeed, float duration)
    {
        var oldSpeed = speed;
        speed = newSpeed;
        yield return new WaitForSeconds(duration);
        speed = oldSpeed;
    }

    
}
