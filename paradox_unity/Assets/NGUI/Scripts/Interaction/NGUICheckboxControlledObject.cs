//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Example script showing how to activate or deactivate a game object when OnActivate event is received.
/// OnActivate event is sent out by the NGUICheckbox script.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Checkbox Controlled Object")]
public class NGUICheckboxControlledObject : MonoBehaviour
{
	public GameObject target;
	public bool inverse = false;

	void OnEnable ()
	{
		NGUICheckbox chk = GetComponent<NGUICheckbox>();
		if (chk != null) OnActivate(chk.isChecked);
	}

	void OnActivate (bool isActive)
	{
		if (target != null)
		{
			NGUITools.SetActive(target, inverse ? !isActive : isActive);
			NGUIPanel panel = NGUITools.FindInParents<NGUIPanel>(target);
			if (panel != null) panel.Refresh();
		}
	}
}