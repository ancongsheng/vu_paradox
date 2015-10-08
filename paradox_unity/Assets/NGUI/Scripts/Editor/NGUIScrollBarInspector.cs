//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NGUIScrollBar))]
public class NGUIScrollBarInspector : Editor
{
	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeControls(80f);
		NGUIScrollBar sb = target as NGUIScrollBar;

		NGUIEditorTools.DrawSeparator();

		float val = EditorGUILayout.Slider("Value", sb.scrollValue, 0f, 1f);
		float size = EditorGUILayout.Slider("Size", sb.barSize, 0f, 1f);
		float alpha = EditorGUILayout.Slider("Alpha", sb.alpha, 0f, 1f);

		NGUIEditorTools.DrawSeparator();

		NGUISprite bg = (NGUISprite)EditorGUILayout.ObjectField("Background", sb.background, typeof(NGUISprite), true);
		NGUISprite fg = (NGUISprite)EditorGUILayout.ObjectField("Foreground", sb.foreground, typeof(NGUISprite), true);
		NGUIScrollBar.Direction dir = (NGUIScrollBar.Direction)EditorGUILayout.EnumPopup("Direction", sb.direction);
		bool inv = EditorGUILayout.Toggle("Inverted", sb.inverted);

		if (sb.scrollValue != val ||
			sb.barSize != size ||
			sb.background != bg ||
			sb.foreground != fg ||
			sb.direction != dir ||
			sb.inverted != inv ||
			sb.alpha != alpha)
		{
			NGUIEditorTools.RegisterUndo("Scroll Bar Change", sb);
			sb.scrollValue = val;
			sb.barSize = size;
			sb.inverted = inv;
			sb.background = bg;
			sb.foreground = fg;
			sb.direction = dir;
			sb.alpha = alpha;
			UnityEditor.EditorUtility.SetDirty(sb);
		}
	}
}