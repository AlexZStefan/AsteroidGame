using System;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private int maxSpeed = 5;
    [SerializeField] private float rotationTorque = 1f;

    private float velocity;
    private int speed = 10;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Forward/Backward thrust
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            velocity += Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            velocity -= Time.deltaTime * speed;
        }
        else
        {
            velocity = 0;
        }

        // Rotation via torque
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(rotationTorque, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(-rotationTorque,  ForceMode2D.Force);
        }
        else
        {
            rb.angularVelocity = 0f;
        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    public void StopAllMovement()
    {
        velocity = 0;
        rb.totalForce = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    public void Move()
    {
        velocity = Mathf.Clamp(velocity, -maxSpeed, maxSpeed);
        rb.AddForce(transform.up * velocity, ForceMode2D.Force);
    }
}
