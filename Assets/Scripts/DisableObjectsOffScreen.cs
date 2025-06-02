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
        Player.instance.GetComponent<ShipFire>().AddToPool(GetComponent<Projectile>());
        gameObject.SetActive(false);
    }
}
