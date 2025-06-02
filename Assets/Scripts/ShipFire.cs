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
    private GameObject projectileContainer;

    private void Start()
    {
        if (!projectilePrefab) projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
        if (!projectileContainer) projectileContainer = new GameObject("ProjectileContainer");
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
        foreach(Transform t in projectileContainer.transform)
        {
            var projectile = t.GetComponentInChildren<Projectile>(true);
            projectile.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack && !Player.invulnerable)
        {
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
            projectile = Instantiate(projectilePrefab, projectileContainer.transform).GetComponentInChildren<Projectile>();
        else
        {
            projectile = ammo[0];
            ammo.RemoveAt(0);
        }
        return projectile;
    }

    private IEnumerator Attack()
    {
        SoundManager.Instance.PlaySFX("SFX_fire");
        canAttack = false;
        Projectile projectile = GetProjectile();
        projectile.Shoot(transform, projectileSpeed);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
