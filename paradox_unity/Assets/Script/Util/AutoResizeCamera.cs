using UnityEngine;
using System.Collections;

public class AutoResizeCamera : MonoBehaviour {


	void Awake ()
    {
        Camera camMain = this.transform.GetComponent<Camera>();

        camMain.AutoResize();
    }
}


public static class CameraAutoResize
{
    public static void AutoResize(this Camera target)
    {
        if (target == null)
            return;
        target.AutoResize(Screen.width, Screen.height);
    }

    public static void AutoResize(this Camera target, float width, float height)
    {
        if (target == null)
            return;

        if (width / height != 9 / 16)
        {
            float orthSize = 13.5f * (height / width);
            orthSize = Mathf.Clamp(orthSize, 18f, orthSize);
            target.orthographicSize = orthSize;
        }
    }
}