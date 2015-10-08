using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private List<Transform> panels;


    private static UIManager instance = null;

    private GameObject curPanel = null;

    public static void ShowPanel(string name)
    {
        instance.showPanel(name);
    }


    void Awake()
    {
        instance = this;
    }


    private void showPanel(string name)
    {
        if (curPanel != null) Destroy(curPanel);
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
    }
}
