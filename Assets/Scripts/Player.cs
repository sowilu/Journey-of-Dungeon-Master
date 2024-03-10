using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float speed = 5;
    public float cooldown = 0.1f;

    [HideInInspector]
    public Vector3 shootInput;

    [Header("UI")] 
    public TextMeshPro coinText;
    public TextMeshPro liveText;

    [Header("Animations")]
    public Animator body;
    public Animator legs;
    
    public int Lives
    {
        get { return lives; }
        set
        {
            lives = value;
            liveText.text = "x " + lives;
        } 
    }
    
    public int Coins
    {
        get { return coins; }
        set
        {
            coins = value;
            coinText.text = "x " + coins;
        }
    }
    
    private int coins = 0;
    private int lives = 3;
    private Action shoot;
    private Rigidbody2D rb;
    private Vector2 moveInput;
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
        shootInput.x = Input.GetAxisRaw("HorizontalArrow");
        shootInput.y = Input.GetAxisRaw("VerticalArrow");

        if(shootInput != Vector3.zero && lastShot + cooldown <= Time.time)
        {
            lastShot = Time.time;
            shoot();
        }
        
        legs.SetFloat("speed", moveInput.magnitude);
        body.SetInteger("upDown", (int)shootInput.y);
        body.SetInteger("leftRight", (int)shootInput.x);
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
        StartCoroutine(ChangeShootCoroutine(newShoot, duration));
    }

    IEnumerator ChangeShootCoroutine(Action newShoot, float duration)
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
    
    public void ChangeCooldown(float newCooldown, float duration)
    {
        StartCoroutine(ChangeCooldownCoroutine(newCooldown, duration));
    }
    
    IEnumerator ChangeCooldownCoroutine(float newCooldown, float duration)
    {
        var oldCooldown = cooldown;
        cooldown = newCooldown;
        yield return new WaitForSeconds(duration);
        cooldown = oldCooldown;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Coins++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Lives--;
            if (Lives > 0)
            {
                //TODO: respawn animation
                //find all enemies
                var enemies = GameObject.FindObjectsOfType<Enemy>();
                foreach (var enemy in enemies)
                {
                    enemy.Die(false);
                }
                body.Play("Die");
            }
            else
            {
                //TODO: game over screen
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            
        }
        else if (other.gameObject.CompareTag("Live"))
        {
            Lives++;
            Destroy(other.gameObject);
        }
    }
}
