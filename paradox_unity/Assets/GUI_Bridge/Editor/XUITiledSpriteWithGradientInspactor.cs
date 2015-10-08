using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(XUITiledSpriteWithGradient))]
public class XUITiledSpriteWithGradientInspactor : NGUITiledSpriteInspector
{
    protected XUITiledSpriteWithGradient mSpriteGradient;

    /// <summary>
    /// Draw the atlas and sprite selection fields.
    /// </summary>
    /// 

    override protected bool DrawProperties()
    {
        mSpriteGradient = mWidget as XUITiledSpriteWithGradient;

        if (base.DrawProperties())
        {
            Color color = EditorGUILayout.ColorField("Color Gradient", mSpriteGradient.gradientColor);

            if (mSpriteGradient.gradientColor != color)
            {
                NGUIEditorTools.RegisterUndo("Color Change", mSpriteGradient);
                mSpriteGradient.gradientColor = color;
            }

            XUITiledSpriteWithGradient.GradientDirection dir = (XUITiledSpriteWithGradient.GradientDirection)EditorGUILayout.EnumPopup("Direction", mSpriteGradient.gradientDirection);

            if ( mSpriteGradient.gradientDirection != dir)
            {
                NGUIEditorTools.RegisterUndo("Direction Change", mSpriteGradient);
                mSpriteGradient.gradientDirection = dir;
            }

            AnimationCurve curve = mSpriteGradient.gradientCurve;
            curve = EditorGUILayout.CurveField("Gradient Curve", curve, Color.green, new Rect(0f, 0f, 1f, 1f));

            if (isCurveEqual( curve,  mSpriteGradient.gradientCurve))
            {
                NGUIEditorTools.RegisterUndo("Curve Change", mSpriteGradient);
                mSpriteGradient.gradientCurve = curve;
            }

            return true;
        }
        return false;
    }

    private bool isCurveEqual( AnimationCurve cA, AnimationCurve cB)
    {
        bool ret = false;

        if (cA.length == cB.length && cA.postWrapMode == cB.postWrapMode && cA.preWrapMode == cB.preWrapMode )
        {
            int len = cA.length;
            for (int i = 0; i < len; ++i)
            {
                if ( !cA[i].Equals(cB[i]) )
                    return false;
            }
            ret = true;
        }

        return ret;
    }

}
