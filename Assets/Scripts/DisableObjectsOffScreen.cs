using UnityEngine;

public class DisableObjectsOffScreen : ScreenWrap
{
    private void OnEnable()
    {
        StartCoroutine(DoActionOutsideScreen(() => DisableObject()));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
        Player.instance.GetComponent<ShipFire>().AddToPool(GetComponent<Projectile>());
    }
}
