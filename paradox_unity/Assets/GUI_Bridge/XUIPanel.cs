using UnityEngine;
using System.Collections;

public class XUIPanel : MonoBehaviour
{
    public NGUITweener bringInTween;
    public NGUITweener dismissTween;


    public Vector3 m_PositionOn = Vector3.zero;
    public Vector3 m_PositionOff = Vector3.zero;

    private Transform cachedTransform = null;

    public string panelName
    {
        get
        {
            return cachedTransform.gameObject.name;
        }
    }

    void Awake()
    {
        cachedTransform = this.transform;
        m_PositionOn = cachedTransform.localPosition;
    }

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    public void BringIn()
    {

    }

    public void Active()
    {
        cachedTransform.localPosition = m_PositionOn;
    }

    public void Dismiss()
    {

    }

    public void Inactive()
    {
        cachedTransform.localPosition = m_PositionOff;
    }


    // ----

    public virtual void AddInitDelegate(NGUIEventListener.BoolDelegate del)
    {
        //changeDelegate += del;
    }

}
