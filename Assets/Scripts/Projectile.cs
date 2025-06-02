using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static ShipFire shipFire;
    private Rigidbody2D rb;
    private ParticleSystem particleSystem;
    
    private void Start()
    {
        if (shipFire == null) shipFire = Player.instance.GetComponent<ShipFire>();
        particleSystem = transform.parent.GetComponentInChildren<ParticleSystem>();
    }

    private void StopAllMovement()
    {
        rb.totalForce = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    private void OnDisable()
    {
        StopAllMovement();
    }

    public void Shoot(Transform shipTransform, int projectileSpeed)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        transform.up = shipTransform.up;
        transform.position = shipTransform.position + shipTransform.up;

        gameObject.SetActive(true);
        rb.AddForce(transform.up * projectileSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player.instance.GetComponent<ShipFire>().AddToPool(GetComponent<Projectile>());
        particleSystem.transform.localPosition = transform.localPosition;
        particleSystem.Stop();
        particleSystem.Play();
        gameObject.SetActive(false);
    }
}
