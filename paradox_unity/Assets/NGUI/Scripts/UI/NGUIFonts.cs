using UnityEngine;
using System.Collections;

public class NGUIFonts : MonoBehaviour {
	
	private NGUILabel m_StatusText;
	private bool tshScrKbStt;
	
	// Use this for initialization
	void Awake () {
		tshScrKbStt = true;
		m_StatusText = this.gameObject.GetComponent<NGUILabel>();
	}
	
	// Update is called once per frame
	void Update () {
		
#if UNITY_ANDROID || UNITY_IPHONE

		if (TouchScreenKeyboard.visible == true && tshScrKbStt == true){
			updateFont();
		}
		if (TouchScreenKeyboard.visible == false){
			tshScrKbStt = true;
		}
		
#endif
	}
	
	void updateFont(){
		tshScrKbStt = false;
		m_StatusText.font.MarkAsDirty();
	}
}
