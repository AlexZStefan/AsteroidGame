using System;
using System.Collections;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    public static Bounds cameraBounds;

    public IEnumerator DoActionOutsideScreen(Action action)
    {
        while (gameObject.activeInHierarchy)
        {
            if (!CheckObjectOnScreen())
            {
                action();
            }
            yield return new WaitForSeconds(.2f);
        }
    }

    public bool CheckObjectOnScreen()
    {
        return cameraBounds.Contains(transform.position);
    }

    internal static void SetCameraBounds()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        cameraBounds = new Bounds(cam.transform.position, new Vector3(camWidth, camHeight, 20));
    }
}
