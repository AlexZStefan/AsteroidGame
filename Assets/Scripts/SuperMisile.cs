using System;
using UnityEngine;

public class SuperMisile : MonoBehaviour
{
    [SerializeField] int speed = 5;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyInitialForce(Vector3 direction)
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * 1500);
    }

    public void ApplyForce(Vector3 direction)
    {
        Vector3 worldPos = transform.parent.TransformPoint(transform.localPosition);
        transform.up = (direction - worldPos).normalized;
        if (!rb) rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        particleSystem.Stop();
        particleSystem.transform.localPosition = transform.localPosition;
        particleSystem.Play();
    }

    internal void AdjustDamping()
    {
        rb.linearDamping = 1;
        rb.angularDamping = .5f;
    }
}
