using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XUIPanelManager : MonoBehaviour
{
    public static XUIPanelManager instance = null;


    private XUIPanel m_currentPanel = null;
    private Stack<XUIPanel> m_panelStack = null;
    private Dictionary<string,XUIPanel> m_panelDict = new Dictionary<string,XUIPanel>();

    // ----

    void Awake()
    {
        instance = this;

        m_currentPanel = null;
        m_panelStack = new Stack<XUIPanel>();

        foreach ( XUIPanel p in this.gameObject.GetComponentsInChildren<XUIPanel>())
        {
            m_panelDict.Add(p.name, p);
        }
    }

    // ----

    public XUIPanel CurrentPanel
    {
        get
        {
            return m_currentPanel;
        }
    }

    public void Push(string panelName)
    {
        XUIPanel pNew = null;

        if (m_panelDict.TryGetValue(panelName, out pNew))
        {
            XUIPanel pOld = m_panelStack.Peek();
            if (pOld != null)
            {
                pOld.Dismiss();
            }

            pNew.BringIn();

            m_panelStack.Push(pNew);
        }
    }

    public void Replace(string panelName)
    {
        XUIPanel pNew = null;

        if (m_panelDict.TryGetValue(panelName, out pNew))
        {
            XUIPanel pOld = m_panelStack.Pop();
            if (pOld != null)
            {
                pOld.Dismiss();
            }

            pNew.BringIn();

            m_panelStack.Push(pNew);
        }
    }

    public void Pop()
    {
        if (m_panelStack.Count > 0)
        {
            XUIPanel pOld = m_panelStack.Pop();
            XUIPanel pNew = m_panelStack.Peek();

            pOld.Dismiss();
            pNew.BringIn();
            
        }
    }

    public void PopTo(string panelName)
    {
        while (m_panelStack.Count > 0 && m_panelStack.Peek().name != panelName)
        {
            Pop();
        }
    }

    public void ClearAll()
    {

    }

    // ----


}
