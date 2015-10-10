using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private List<Transform> panels;

    [SerializeField]
    private NGUIPanel m_FadeBlack;


    private static UIManager instance = null;

    private GameObject curPanel = null;

    public static void ShowPanel(string name)
    {
        CameraTool.Lock(true);
        instance.StartCoroutine(instance.showPanelCoroutine(name));
    }


    void Awake()
    {
        instance = this;
    }


    private IEnumerator showPanelCoroutine(string name)
    {
        m_FadeBlack.enabled = true;

        if (curPanel != null)
        {
            m_FadeBlack.alpha = 0;
            TweenAlpha.Begin(m_FadeBlack.gameObject, 1, 1);
            yield return new WaitForSeconds(1f);
            Destroy(curPanel);
        }

        
        yield return null;
        m_FadeBlack.alpha = 1;

        foreach (var p in instance.panels)
        {
            if (p.name == name)
            {
                Transform t = Instantiate(p, Vector3.zero, Quaternion.identity) as Transform;
                t.parent = transform;
                t.localScale = Vector3.one;
                t.name = name;
                curPanel = t.gameObject;
                break;
            }
        }

        TweenAlpha.Begin(m_FadeBlack.gameObject, 1, 0);
        yield return new WaitForSeconds(1f);
        m_FadeBlack.enabled = false;
        CameraTool.Lock(false);
    }
}
