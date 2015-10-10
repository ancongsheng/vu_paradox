using UnityEngine;
using System.Collections;

public class CameraTool : MonoBehaviour {

    public static NGUICamera uiCamera;

    [SerializeField]
    private Camera m_Camera;


    Rect origRect;
	// Use this for initialization

    static LayerMask cachedLm;
    static LayerMask currentLm;

    public static int lockCount = 0;

    void Awake()
    {
        uiCamera = m_Camera.GetComponent<NGUICamera>();
        cachedLm = uiCamera.eventReceiverMask;
        currentLm = cachedLm;
    }

    public static void SetTutorialLock(bool _val)
    {
        Debug.Log("Tutorial Lock camera = " + _val + "lockCount=" + lockCount);
        if (_val)
        {
            currentLm = 1 << LayerMask.NameToLayer("Tutorial");
            if (lockCount == 0)
            {
                uiCamera.eventReceiverMask = currentLm;
            }
        }
        else
        {
            currentLm = cachedLm;
            if (lockCount == 0)
            {
                uiCamera.eventReceiverMask = currentLm;
            }
        }
        //LogManager.Log_Error("Tutorial End Lock camera = " + currentLm.value);
    }

    public static void Lock(bool _lock)
    {
        if (_lock)
        {
            lockCount++;
            Debug.Log("Lock camera : count=" + lockCount);
            uiCamera.eventReceiverMask = 0;
        }
        else
        {
            lockCount--;
            Debug.Log("unlock camera : count=" + lockCount);
            if (lockCount == 0)
            {
                uiCamera.eventReceiverMask = currentLm;
            }
        }
    }

    public static void SetEnable(bool _enable)
    {
        uiCamera.cachedCamera.enabled = _enable;
    }
	
}
