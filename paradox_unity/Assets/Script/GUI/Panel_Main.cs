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



    private StoryData data = null;
    private int idx = 0;
    private StringBuilder showText;
    private float waitTime = 0;

    private bool inPause = false;

	// Use this for initialization
	void Start () 
    {
        m_NextBtn.AddClickDelegate(nextDelegate);

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
                string cmd = data.content.Substring(idx, idx2 - idx);
                processCmd(cmd);
                idx = idx2;
            }
            else
            {
                showText.Append(c);
                m_ShowText.text = showText.ToString();
                waitTime = Time.time + 0.1f;
                idx++;
            }
        }
	}

    private void processCmd(string cmd)
    {
        Debug.Log(cmd);
        waitTime = Time.time + 1f;

        if (cmd == "#pause#") inPause = true;
    }

    private void next()
    {
        data = MainGame.instance.text[MainGame.instance.currentID];

        idx = 0;
        showText = new StringBuilder();
        print(data.content);
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
    }
}
