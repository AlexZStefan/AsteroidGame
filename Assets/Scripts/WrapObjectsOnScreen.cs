using UnityEngine;

public class WrapObjectsOnScreen : ScreenWrap
{
    private void OnEnable()
    {
        StartCoroutine(DoActionOutsideScreen(() => WrapPosition()));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void WrapPosition()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.x > 1)
        {
            viewportPosition.x = 0;
        }
        else if (viewportPosition.x < 0)
        {
            viewportPosition.x = 1;
        }
        if (viewportPosition.y > 1)
        {
            viewportPosition.y = 0;
        }
        else if (viewportPosition.y < 0)
        {
            viewportPosition.y = 1;
        }
        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);
    }
}
