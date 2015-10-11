using UnityEngine;
using System.Collections;

public class Panel_Start : MonoBehaviour {

    [SerializeField]
    private XUIButton m_StartUpBtn;

    [SerializeField]
    private GameObject m_MenuGroup;

    [SerializeField]
    private XUIButton[] m_MenuBtn;

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


        m_StartUpBtn.AddClickDelegate(startUpDelegate);
        m_MenuGroup.SetActive(false);

        m_MenuBtn[0].AddClickDelegate(startDelegate);

        SoundManager.Instance.PlayMusic("title", true, 1f);
	}

    void OnDestroy()
    {
        m_Bg.mainTexture = null;
        SoundManager.Instance.StopMusic();
    }


    private void startUpDelegate(GameObject obj)
    {
        StartCoroutine(showMenuCoroutine());
        
    }

    private void startDelegate(GameObject obj)
    {
        MainGame.instance.createNewGame();
    }

    private void loadDelegate(GameObject obj)
    {
        //show load
    }

    private IEnumerator showMenuCoroutine()
    {
        CameraTool.Lock(true);
        TweenAlpha.Begin(m_StartUpBtn.gameObject, 0.5f, 0);
        yield return new WaitForSeconds(0.5f);
        m_StartUpBtn.gameObject.SetActive(false);
        m_MenuGroup.SetActive(true);

        foreach (var btn in m_MenuBtn)
        {
            Vector3 position = btn.transform.localPosition - Vector3.right * 1100;
            TweenPosition.Begin(btn.gameObject, 0.5f, position);
            btn.GetComponent<TweenPosition>().method = NGUITweener.Method.CubicOut;
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(0.5f);
        CameraTool.Lock(false);
    }
	
	
}
