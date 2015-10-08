//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using UnityEditor;

/// <summary>
/// Inspector class used to view NGUIDrawCalls.
/// </summary>

[CustomEditor(typeof(NGUIDrawCall))]
public class NGUIDrawCallInspector : Editor
{
	/// <summary>
	/// Draw the inspector widget.
	/// </summary>

	public override void OnInspectorGUI ()
	{
		if (Event.current.type == EventType.Repaint || Event.current.type == EventType.Layout)
		{
			NGUIDrawCall dc = target as NGUIDrawCall;

			NGUIPanel[] panels = (NGUIPanel[])Component.FindObjectsOfType(typeof(NGUIPanel));

			foreach (NGUIPanel p in panels)
			{
				if (p.drawCalls.Contains(dc))
				{
					EditorGUILayout.LabelField("Owner Panel", NGUITools.GetHierarchy(p.gameObject));
					EditorGUILayout.LabelField("Triangles", dc.triangles.ToString());
					return;
				}
			}
			if (Event.current.type == EventType.Repaint) Debug.LogWarning("Orphaned NGUIDrawCall detected!\nUse [Selection -> Force Delete] to get rid of it.");
		}
	}
}