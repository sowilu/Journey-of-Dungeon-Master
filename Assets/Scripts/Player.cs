using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    public GameObject bulletPrefab;
    public float cooldown = 0.5f;

    [Header("UI")]
    public TextMeshPro coinText;
    public TextMeshPro liveText;

    [HideInInspector] public Vector3 shootInput;
    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
            liveText.text = "x " + lives;
        }
    }

    public int Coins
    {
        get
        {
            return coins;
        }
        set
        {
            coins = value;
            coinText.text = "x " + coins;
        }
    }

    Rigidbody2D rb;
    Vector2 moveInput;
    float lastShot = 0;
    Action shoot;
    float originalSpeed;
    int lives = 3;
    int coins = 0;


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
        originalSpeed = speed;
        speed = newSpeed;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
    }

    public void ChangeCooldown(float newCooldown, float duration)
    {
        StartCoroutine(ChangeCooldownCoroutine(newCooldown, duration));
    }

    IEnumerator ChangeCooldownCoroutine(float newCooldown, float duration)
    {
        var originalCooldown = cooldown;
        cooldown = newCooldown;
        yield return new WaitForSeconds(duration);
        cooldown = originalCooldown;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Coins++;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Live"))
        {
            Lives++;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Lives--;
            //TODO: respawn
        }
    }
}
