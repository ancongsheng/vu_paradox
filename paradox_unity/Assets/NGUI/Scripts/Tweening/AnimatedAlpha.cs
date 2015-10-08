//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Makes it possible to animate alpha of the widget or a panel.
/// </summary>

public class AnimatedAlpha : MonoBehaviour
{
	public float alpha = 1f;

	NGUIWidget mWidget;
	NGUIPanel mPanel;
	
	void Awake ()
	{
		mWidget = GetComponent<NGUIWidget>();
		mPanel = GetComponent<NGUIPanel>();
		Update();
	}

	void Update ()
	{
		if (mWidget != null) mWidget.alpha = alpha;
		if (mPanel != null) mPanel.alpha = alpha;
	}
}
