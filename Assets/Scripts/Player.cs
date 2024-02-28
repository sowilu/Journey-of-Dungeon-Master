using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    public GameObject bulletPrefab;

    Rigidbody2D rb;
    Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        var shootInput = new Vector3();
        shootInput.x = Input.GetAxisRaw("HorizontalArrow");
        shootInput.y = Input.GetAxisRaw("HorizontalArrow");

        if (shootInput != Vector3.zero)
        {
            Instantiate(bulletPrefab, transform.position + shootInput, Quaternion.identity);
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * speed * Time.fixedDeltaTime);
    }
}
