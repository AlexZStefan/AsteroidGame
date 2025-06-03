using Unity.VisualScripting;
using UnityEngine;

public enum AsteroidSize
{
    SMALL,
    BIG,
}

public class Asteroid : MonoBehaviour
{
    public AsteroidSize asteroidSize;

    [SerializeField] private int maxSpeed = 150;
    [SerializeField] private int minSpeed = 50;
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        ApplyForce();
    }

    private void OnDisable()
    {
        StopAllMovement();
    }

    private void StopAllMovement() {
        rb.totalForce = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            SoundManager.Instance.PlaySFX("SFX_asteroid_blow");
            Destroy();
            switch (asteroidSize)
            {
                case AsteroidSize.SMALL:
                    ScoreTracker.AddScore(1);
                    break;
                case AsteroidSize.BIG:
                    ScoreTracker.AddScore(3);
                    break;
            }
        }
    }

    public void Destroy()
    {
        switch (asteroidSize)
        {
            case AsteroidSize.BIG:
                AsteroidManager.SpawnSmallerAsteroids(transform.position);
                break;
        }

        AsteroidManager.AddToPool(this);
        gameObject.SetActive(false);
    }

    internal void ApplyForce()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        float speeOffset = Random.Range(minSpeed, maxSpeed);
        Vector3 force = (Vector3)Random.insideUnitCircle.normalized * speeOffset;

        force.x = Mathf.Clamp(force.x, -maxSpeed, maxSpeed);
        force.y = Mathf.Clamp(force.y, -maxSpeed, maxSpeed);

        rb.AddForce(force);
    }
}