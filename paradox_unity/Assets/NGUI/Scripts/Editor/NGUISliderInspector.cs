//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NGUISlider))]
public class NGUISliderInspector : Editor
{
	void ValidatePivot (Transform fg, string name, NGUISlider.Direction dir)
	{
		if (fg != null)
		{
			NGUISprite sprite = fg.GetComponent<NGUISprite>();

			if (sprite != null && sprite.type != NGUISprite.Type.Filled)
			{
				if (dir == NGUISlider.Direction.Horizontal)
				{
					if (sprite.pivot != NGUIWidget.Pivot.Left &&
						sprite.pivot != NGUIWidget.Pivot.TopLeft &&
						sprite.pivot != NGUIWidget.Pivot.BottomLeft)
					{
						GUI.color = new Color(1f, 0.7f, 0f);
						GUILayout.Label(name + " should use a Left pivot");
						GUI.color = Color.white;
					}
				}
				else if (sprite.pivot != NGUIWidget.Pivot.BottomLeft &&
						 sprite.pivot != NGUIWidget.Pivot.Bottom &&
						 sprite.pivot != NGUIWidget.Pivot.BottomRight)
				{
					GUI.color = new Color(1f, 0.7f, 0f);
					GUILayout.Label(name + " should use a Bottom pivot");
					GUI.color = Color.white;
				}
			}
		}
	}

	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeControls(80f);
		NGUISlider slider = target as NGUISlider;

		NGUIEditorTools.DrawSeparator();

		float sliderValue = EditorGUILayout.Slider("Value", slider.sliderValue, 0f, 1f);

		if (slider.sliderValue != sliderValue)
		{
			NGUIEditorTools.RegisterUndo("Slider Change", slider);
			slider.sliderValue = sliderValue;
			UnityEditor.EditorUtility.SetDirty(slider);
		}

		int steps = EditorGUILayout.IntSlider("Steps", slider.numberOfSteps, 0, 11);

		if (slider.numberOfSteps != steps)
		{
			NGUIEditorTools.RegisterUndo("Slider Change", slider);
			slider.numberOfSteps = steps;
			slider.ForceUpdate();
			UnityEditor.EditorUtility.SetDirty(slider);
		}

		NGUIEditorTools.DrawSeparator();

		Transform fg = EditorGUILayout.ObjectField("Foreground", slider.foreground, typeof(Transform), true) as Transform;
		Transform tb = EditorGUILayout.ObjectField("Thumb", slider.thumb, typeof(Transform), true) as Transform;
		NGUISlider.Direction dir = (NGUISlider.Direction)EditorGUILayout.EnumPopup("Direction", slider.direction);

		// If we're using a sprite for the foreground, ensure it's using a proper pivot.
		ValidatePivot(fg, "Foreground sprite", dir);

		NGUIEditorTools.DrawSeparator();

		GameObject er = EditorGUILayout.ObjectField("Event Recv.", slider.eventReceiver, typeof(GameObject), true) as GameObject;

		GUILayout.BeginHorizontal();
		string fn = EditorGUILayout.TextField("Function", slider.functionName);
		GUILayout.Space(18f);
		GUILayout.EndHorizontal();

		if (slider.foreground != fg ||
			slider.thumb != tb ||
			slider.direction != dir ||
			slider.eventReceiver != er ||
			slider.functionName != fn)
		{
			NGUIEditorTools.RegisterUndo("Slider Change", slider);
			slider.foreground = fg;
			slider.thumb = tb;
			slider.direction = dir;
			slider.eventReceiver = er;
			slider.functionName = fn;

			if (slider.thumb != null)
			{
				slider.thumb.localPosition = Vector3.zero;
				slider.sliderValue = -1f;
				slider.sliderValue = sliderValue;
			}
			else slider.ForceUpdate();

			UnityEditor.EditorUtility.SetDirty(slider);
		}
	}
}
