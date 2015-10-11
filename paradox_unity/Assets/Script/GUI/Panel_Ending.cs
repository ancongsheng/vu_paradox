using UnityEngine;
using System.Collections;

public class Panel_Ending : MonoBehaviour {

    [SerializeField]
    private NGUILabel m_EndText;

	// Use this for initialization
	void Start () 
    {
        m_EndText.text = MainGame.instance.text[MainGame.instance.currentID].content;
	}

    void OnClick()
    {
        UIManager.ShowPanel("Start_Panel");
    }
}
