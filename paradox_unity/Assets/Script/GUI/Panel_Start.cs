using UnityEngine;
using System.Collections;

public class Panel_Start : MonoBehaviour {

    [SerializeField]
    private XUIButton m_StartUpBtn;


    [SerializeField]
    private XUIButton m_NewGameBtn;
    [SerializeField]
    private XUIButton m_LoadGameBtn;
    [SerializeField]
    private XUIButton m_ConfigBtn;
    [SerializeField]
    private XUIButton m_ExtraBtn;
    [SerializeField]
    private XUIButton m_AboutBtn;

    [SerializeField]
    private NGUITexture m_Bg;


	// Use this for initialization
	void Start () 
    {
        int idx = 0;
        if (PlayerData.GetEndingFlag(1)) idx += 1;
        if (PlayerData.GetEndingFlag(2)) idx += 2;
        if (PlayerData.GetEndingFlag(3)) idx += 4;

        if (PlayerData.GetEndingFlag(0)) idx = 8;

        ResourceManager.LoadIcon("start" + idx, m_Bg);

	}


    private void startUpDelegate(GameObject obj)
    {

    }

    private void startDelegate(GameObject obj)
    {
        MainGame.instance.createNewGame();
    }

    private void loadDelegate(GameObject obj)
    {
        //show load
    }


	
	
}
