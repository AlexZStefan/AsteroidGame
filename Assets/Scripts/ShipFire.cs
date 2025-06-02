using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFire : MonoBehaviour
{
    public static bool canAttack = true;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private float attackCooldown = .5f;
    [SerializeField] private int projectileSpeed = 1500;
    [SerializeField] List<Projectile> ammo = new List<Projectile>();

    private int attackIndex = 0;

    private void Start()
    {
        if (!projectilePrefab) projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack && !Player.invulnerable)
        {
            StopAllCoroutines();
            StartCoroutine(Attack());
        }
    }

    public void AddToPool(Projectile projectile)
    {
        ammo.Add(projectile);
    }

    public Projectile GetProjectile()
    {
        Projectile projectile;
        if (ammo.Count == 0)
            projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
        else
        {
            projectile = ammo[0];
            ammo.Remove(projectile);
        }

        return projectile;
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        Projectile projectile = GetProjectile();
        projectile.gameObject.SetActive(true);
        projectile.Shoot(transform, projectileSpeed);
        if (attackIndex > ammo.Count - 1) attackIndex = 0;
        else attackIndex++;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
