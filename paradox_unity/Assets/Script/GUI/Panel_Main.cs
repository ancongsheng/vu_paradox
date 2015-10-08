using UnityEngine;
using System.Collections;

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void next()
    {
        data = MainGame.instance.text[MainGame.instance.currentID];


    }
}
