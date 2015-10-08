//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Attaching this script to a widget makes it react to key events such as tab, up, down, etc.
/// </summary>

[RequireComponent(typeof(Collider))]
[AddComponentMenu("NGUI/Interaction/Button Keys")]
public class NGUIButtonKeys : MonoBehaviour
{
	public bool startsSelected = false;
	public NGUIButtonKeys selectOnClick;
	public NGUIButtonKeys selectOnUp;
	public NGUIButtonKeys selectOnDown;
	public NGUIButtonKeys selectOnLeft;
	public NGUIButtonKeys selectOnRight;
	
	void Start ()
	{
		if (startsSelected && (NGUICamera.selectedObject == null || !NGUITools.GetActive(NGUICamera.selectedObject)))
		{
			NGUICamera.selectedObject = gameObject;
		}
	}
	 
	void OnKey (KeyCode key)
	{
		if (enabled && NGUITools.GetActive(gameObject))
		{
			switch (key)
			{
			case KeyCode.LeftArrow:
				if (selectOnLeft != null) NGUICamera.selectedObject = selectOnLeft.gameObject;
				break;
			case KeyCode.RightArrow:
				if (selectOnRight != null) NGUICamera.selectedObject = selectOnRight.gameObject;
				break;
			case KeyCode.UpArrow:
				if (selectOnUp != null) NGUICamera.selectedObject = selectOnUp.gameObject;
				break;
			case KeyCode.DownArrow:
				if (selectOnDown != null) NGUICamera.selectedObject = selectOnDown.gameObject;
				break;
			case KeyCode.Tab:
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				{
					if (selectOnLeft != null) NGUICamera.selectedObject = selectOnLeft.gameObject;
					else if (selectOnUp != null) NGUICamera.selectedObject = selectOnUp.gameObject;
					else if (selectOnDown != null) NGUICamera.selectedObject = selectOnDown.gameObject;
					else if (selectOnRight != null) NGUICamera.selectedObject = selectOnRight.gameObject;
				}
				else
				{
					if (selectOnRight != null) NGUICamera.selectedObject = selectOnRight.gameObject;
					else if (selectOnDown != null) NGUICamera.selectedObject = selectOnDown.gameObject;
					else if (selectOnUp != null) NGUICamera.selectedObject = selectOnUp.gameObject;
					else if (selectOnLeft != null) NGUICamera.selectedObject = selectOnLeft.gameObject;
				}
				break;
			}
		}
	}

	void OnClick ()
	{
		if (enabled && selectOnClick != null)
		{
			NGUICamera.selectedObject = selectOnClick.gameObject;
		}
	}
}
