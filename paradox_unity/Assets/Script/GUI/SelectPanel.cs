using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectPanel : MonoBehaviour {

    public delegate void selectEndDelegate(int result);
    public event selectEndDelegate onSelectEnd;


    [SerializeField]
    private List<SelectItem> m_SelectItems;


    public void Awake()
    {
        foreach (var s in m_SelectItems)
        {
            s.GetComponent<XUIButton>().AddClickDelegate(selectDelegate);
        }
    }


    public void Set(StoryData data)
    {
        string[] split = data.content.Split('+');
        int i = 0;

        for (i = 0; i < split.Length; i++ )
        {
            m_SelectItems[i].Set(int.Parse(split[i]));
        }

        while (i < m_SelectItems.Count)
        {
            m_SelectItems[i].gameObject.SetActive(false);
            i++;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }


    private void selectDelegate(GameObject obj)
    {

        gameObject.SetActive(false);
        if (onSelectEnd != null)
        {
            onSelectEnd(obj.GetComponent<SelectItem>().id);
        }

    }
}
