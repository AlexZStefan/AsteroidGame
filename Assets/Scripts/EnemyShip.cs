using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 20f;
    public float projectileSpeed = 5f;

    private float fireCooldown = 5;
    private bool goUp = false;
    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            goUp = !goUp;
            Shoot();
            fireCooldown = 5;
        }

        if (goUp)
        {
            float y = transform.position.y + 0.01f;
            transform.position = new Vector2(transform.position.x, y);
        }
        else
        {
            float y = transform.position.y - 0.01f;
            transform.position = new Vector2(transform.position.x, y);
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.up * projectileSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
