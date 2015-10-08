//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Inspector class used to edit NGUISprites.
/// </summary>

[CustomEditor(typeof(NGUISprite))]
public class NGUISpriteInspector : NGUIWidgetInspector
{
	protected NGUISprite mSprite;

	/// <summary>
	/// Atlas selection callback.
	/// </summary>

	void OnSelectAtlas (MonoBehaviour obj)
	{
		if (mSprite != null)
		{
			NGUIEditorTools.RegisterUndo("Atlas Selection", mSprite);
			bool resize = (mSprite.atlas == null);
			mSprite.atlas = obj as NGUIAtlas;
			if (resize) mSprite.MakePixelPerfect();
			EditorUtility.SetDirty(mSprite.gameObject);
		}
	}

	/// <summary>
	/// Sprite selection callback function.
	/// </summary>

	void SelectSprite (string spriteName)
	{
		if (mSprite != null && mSprite.spriteName != spriteName)
		{
			NGUIEditorTools.RegisterUndo("Sprite Change", mSprite);
			mSprite.spriteName = spriteName;
			mSprite.MakePixelPerfect();
			EditorUtility.SetDirty(mSprite.gameObject);
		}
	}

	/// <summary>
	/// Draw the atlas and sprite selection fields.
	/// </summary>

	override protected bool DrawProperties ()
	{
		mSprite = mWidget as NGUISprite;
		ComponentSelector.Draw<NGUIAtlas>(mSprite.atlas, OnSelectAtlas);
		if (mSprite.atlas == null) return false;
		NGUIEditorTools.AdvancedSpriteField(mSprite.atlas, mSprite.spriteName, SelectSprite, false);
		return true;
	}

	/// <summary>
	/// Sprites's custom properties based on the type.
	/// </summary>

	override protected void DrawExtraProperties ()
	{
		NGUIEditorTools.DrawSeparator();

		if (GetType() == typeof(NGUISpriteInspector))
		{
			//GUILayout.BeginHorizontal();
			NGUISprite.Type type = (NGUISprite.Type)EditorGUILayout.EnumPopup("Sprite Type", mSprite.type);
			//GUILayout.Label("sprite", GUILayout.Width(58f));
			//GUILayout.EndHorizontal();

			if (mSprite.type != type)
			{
				NGUIEditorTools.RegisterUndo("Sprite Change", mSprite);
				mSprite.type = type;
				EditorUtility.SetDirty(mSprite.gameObject);
			}
		}

        if (mSprite.type == NGUISprite.Type.Sliced || mSprite.type == NGUISprite.Type.MirrorSliced)
		{
			bool fill = EditorGUILayout.Toggle("Fill Center", mSprite.fillCenter);

			if (mSprite.fillCenter != fill)
			{
				NGUIEditorTools.RegisterUndo("Sprite Change", mSprite);
				mSprite.fillCenter = fill;
				EditorUtility.SetDirty(mSprite.gameObject);
            }

            if (mSprite.type == NGUISprite.Type.MirrorSliced)
            {
                bool reverse = EditorGUILayout.Toggle("Reverse", mSprite.fillReverse);

                if (mSprite.fillReverse != reverse)
                {
                    NGUIEditorTools.RegisterUndo("Sprite Change", mSprite);
                    mSprite.fillReverse = reverse;
                    EditorUtility.SetDirty(mSprite.gameObject);
                }
            }
		}
        else if (mSprite.type == NGUISprite.Type.MirrorSimple)
        {
            bool reverse = EditorGUILayout.Toggle("Reverse", mSprite.fillReverse);

            if (mSprite.fillReverse != reverse)
            {
                NGUIEditorTools.RegisterUndo("Sprite Change", mSprite);
                mSprite.fillReverse = reverse;
                EditorUtility.SetDirty(mSprite.gameObject);
            }
        }
		else if (mSprite.type == NGUISprite.Type.Filled)
		{
			if ((int)mSprite.fillDirection > (int)NGUISprite.FillDirection.Radial360)
			{
				mSprite.fillDirection = NGUISprite.FillDirection.Horizontal;
				EditorUtility.SetDirty(mSprite);
			}

			NGUISprite.FillDirection fillDirection = (NGUISprite.FillDirection)EditorGUILayout.EnumPopup("Fill Dir", mSprite.fillDirection);
			float fillAmount = EditorGUILayout.Slider("Fill Amount", mSprite.fillAmount, 0f, 1f);
			bool invert = EditorGUILayout.Toggle("Invert Fill", mSprite.invert);

			if (mSprite.fillDirection != fillDirection || mSprite.fillAmount != fillAmount || mSprite.invert != invert)
			{
				NGUIEditorTools.RegisterUndo("Sprite Change", mSprite);
				mSprite.fillDirection = fillDirection;
				mSprite.fillAmount = fillAmount;
				mSprite.invert = invert;
				EditorUtility.SetDirty(mSprite);
			}
		}
		GUILayout.Space(4f);
	}

	/// <summary>
	/// All widgets have a preview.
	/// </summary>

	public override bool HasPreviewGUI () { return true; }

	/// <summary>
	/// Draw the sprite preview.
	/// </summary>

	public override void OnPreviewGUI (Rect rect, GUIStyle background)
	{
		if (mSprite == null || !mSprite.isValid) return;

		Texture2D tex = mSprite.mainTexture as Texture2D;
		if (tex == null) return;

		Rect outer = new Rect(mSprite.GetAtlasSprite().outer);
		Rect inner = new Rect(mSprite.GetAtlasSprite().inner);
		Rect uv = outer;

		if (mSprite.atlas.coordinates == NGUIAtlas.Coordinates.Pixels)
		{
			uv = NGUIMath.ConvertToTexCoords(outer, tex.width, tex.height);
		}
		else
		{
			outer = NGUIMath.ConvertToPixels(outer, tex.width, tex.height, true);
			inner = NGUIMath.ConvertToPixels(inner, tex.width, tex.height, true);
		}
		NGUIEditorTools.DrawSprite(tex, rect, outer, inner, uv, mSprite.color);
	}
}
