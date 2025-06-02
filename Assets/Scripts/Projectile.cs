using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static ShipFire shipFire;
    private Rigidbody2D rb;

    private void Start()
    {
        if (shipFire == null) shipFire = Player.instance.GetComponent<ShipFire>();
    }

    private void OnEnable()
    {
        Menu.onRestart += Restart;
    }

    private void OnDisable()
    {
        Menu.onRestart -= Restart;
    }

    private void Restart()
    {
        shipFire.AddToPool(this);
        gameObject.SetActive(false);
    }
    public void Shoot(Transform shipTransform, int projectileSpeed)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        transform.position = shipTransform.position + shipTransform.up;
        transform.up = shipTransform.up;

        rb.AddForce(transform.up * projectileSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        shipFire.AddToPool(this);
    }
}
