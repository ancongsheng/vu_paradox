using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class XUIButtonKey : MonoBehaviour
{

    public bool startsSelected = false;
    public KeyCode onKey = KeyCode.Tab;
    public XUIButtonKey onSelectObj = null;

    void Start()
    {
        if (startsSelected && (NGUICamera.selectedObject == null || !NGUICamera.selectedObject.active))
        {
            NGUICamera.selectedObject = gameObject;
        }
    }

    void OnKey(KeyCode key)
    {
        if (enabled && gameObject.active)
        {

            if (key == onKey)
            {
                if (onSelectObj != null) NGUICamera.selectedObject = onSelectObj.gameObject;
            }
        }
    }
}
