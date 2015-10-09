using UnityEngine;
using System.Collections;
using System.Text;

public class Panel_Main : MonoBehaviour {

    [SerializeField]
    private NGUILabel m_ShowText;
    [SerializeField]
    private NGUILabel m_ShowName;
    [SerializeField]
    private GameObject m_NameGroup;

    [SerializeField]
    private NGUITexture m_CharaTex;

    [SerializeField]
    private XUIButton m_NextBtn;
    [SerializeField]
    private XUIButton m_MenuBtn;

    [SerializeField]
    private SelectPanel m_SelectPanel;



    private StoryData data = null;
    private int idx = 0;
    private StringBuilder showText;
    private float waitTime = 0;

    private bool inPause = false;
    private bool skipRead = false;

	// Use this for initialization
	void Start () 
    {
        m_NextBtn.AddClickDelegate(nextDelegate);
        m_SelectPanel.onSelectEnd += onSelectEnd;


        m_SelectPanel.gameObject.SetActive(false);

        next();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (data == null) return;


        if (Time.time < waitTime || inPause) return;

        if (idx < data.content.Length)
        {
            char c = data.content[idx];
            if (c == '#')
            {
                int idx2 = data.content.IndexOf('#', idx + 1);
                string cmd = data.content.Substring(idx + 1, idx2 - idx - 1);
                processCmd(cmd);
                idx = idx2 + 1;
            }
            else if (c == '[')//for ngui color label
            {
                while (c != ']')
                {
                    showText.Append(c);
                    idx++;
                    c = data.content[idx];
                }
                showText.Append(c);
                m_ShowText.text = showText.ToString();
            }
            else
            {
                showText.Append(c);
                m_ShowText.text = showText.ToString();
                waitTime = Time.time + 0.1f;
                idx++;
            }
        }
        else
        {
            waitNext();
        }
	}

    private void processCmd(string cmd)
    {
        Debug.Log(cmd);
        waitTime = Time.time + 1f;

        if (cmd == "pause")
        {
            waitNext();
        }
    }

    private void next()
    {
        data = MainGame.instance.text[MainGame.instance.currentID];

        if (skipRead && PlayerData.GetTextFlag(MainGame.instance.currentID))
        {
            Time.timeScale = 10f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        idx = 0;
        showText = new StringBuilder();
        m_NextBtn.gameObject.SetActive(false);

        switch (data.type)
        {
            case 1:
                m_SelectPanel.Set(data);
                inPause = true;
                m_SelectPanel.Show();
        	    break;
            default:
                m_ShowText.text = string.Empty;
                if (data.name != string.Empty)
                {
                    m_NameGroup.SetActive(true);
                    m_ShowName.text = data.name;
                }
                else
                {
                    m_NameGroup.SetActive(false);
                }
                inPause = false;
                break;
        }

    }




    private void nextDelegate(GameObject obj)
    {
        inPause = false;

        if (idx >= data.content.Length)
        {
            PlayerData.SetTextFlag(MainGame.instance.currentID);
            MainGame.instance.currentID = data.next;
            next();
        }

        m_NextBtn.gameObject.SetActive(false);
    }


    private void onSelectEnd(int result)
    {
        SelectionData data = MainGame.instance.selection[result];
        MainGame.instance.currentFlag[result] = true;
        MainGame.instance.currentID = data.next;
        next();
    }

    private void waitNext()
    {
        if (skipRead && PlayerData.GetTextFlag(MainGame.instance.currentID))
        {
            nextDelegate(null);
        }
        else
        {

            inPause = true;
            m_NextBtn.gameObject.SetActive(true);
        }
    }
}
