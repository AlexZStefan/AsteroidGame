using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public static bool invulnerable = false;
    GameObject shield;
    Collider2D collider;

    private void Start()
    {
        if (instance == null) instance = this;
        shield = transform.GetChild(0).gameObject;
        collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Menu.onRestart += Restart;
    }

    private void Restart()
    {
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        GetComponent<ShipMovement>().StopAllMovement();
        StopAllCoroutines();
        DisableInvulerability();
    }

    private void OnDisable()
    {
        Menu.onRestart -= Restart;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (invulnerable) return;
        if (collision.gameObject.tag == "Enemy")
        {
            PlayerLifeSystem.DeductLife();

            StartCoroutine(Invulnerable());
        }
    }

    private void EnableInvulerability()
    {
        ShipFire.canAttack = false;
        invulnerable = true;
        shield.SetActive(true);
        collider.enabled = false;
    }

    private void DisableInvulerability() {
        invulnerable = false;
        ShipFire.canAttack = true;
        shield.SetActive(false);
        collider.enabled = true;
    }

    private IEnumerator Invulnerable()
    {
        EnableInvulerability();
        yield return new WaitForSeconds(2f);
        DisableInvulerability();
    }
}
