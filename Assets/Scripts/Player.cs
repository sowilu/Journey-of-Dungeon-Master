using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float speed = 5;
    
    Rigidbody2D rb;
    private Vector2 moveInput;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        var shootInput = new Vector3();
        shootInput.x = Input.GetAxis("HorizontalArrow");
        shootInput.y = Input.GetAxis("VerticalArrow");

        if(shootInput != Vector3.zero)
        {
            Instantiate(bulletPrefab, transform.position + shootInput, Quaternion.identity);
        }

    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * speed * Time.fixedDeltaTime);
    }
}
