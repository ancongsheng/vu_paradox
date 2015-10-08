using UnityEngine;
using System.Collections;

public class XUIActiveBase : MonoBehaviour {

    protected bool m_isActive = false;

    protected void Awake()
    {
        //Debug.LogError("XUIActive start!");
        NGUICheckbox[] cb = gameObject.GetComponents<NGUICheckbox>();

        for (int i = 0, iMax = cb.Length; i < iMax; ++i)
        {
            cb[i].onStateChange += OnActivateDele;
        }
    }

    protected void OnActivateDele(bool isActive)
    {

        //LogManager.Log_Error( logObjName(this) + " OnActivate: " + isActive);
        //if (m_isActive != isActive)
        {
            m_isActive = isActive;
            ProcessActive();
        }
    }

    protected virtual void ProcessActive()
    {

    }

    public static string logObjName(GameObject obj)
    {
        string objName = obj.name;
        while (obj.transform && obj.transform.parent)
        {
            obj = obj.transform.parent.gameObject;
            objName = string.Format("{0}/{1} ", obj.name, objName);
        }
        return objName;
    }
    public static string logObjName(MonoBehaviour objx )
    {
        GameObject obj = objx.gameObject;
        string objName = string.Format("{0}/{1} ",obj.name, objx.GetType().Name);
        while (obj.transform && obj.transform.parent)
        {
            obj = obj.transform.parent.gameObject;
            objName = string.Format("{0}/{1}", obj.name, objName);
        }
        return objName;
    }
}
