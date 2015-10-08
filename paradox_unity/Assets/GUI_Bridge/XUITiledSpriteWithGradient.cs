using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Sprite (Tiled With Gradient)")]
public class XUITiledSpriteWithGradient : NGUITiledSprite
{

    public enum GradientDirection
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop,
    }

    public delegate float LerpDelegate(Vector3 pos);

    LerpDelegate mLerpFunction;

    [HideInInspector][SerializeField] Color mGradientColor = Color.white;
    [HideInInspector][SerializeField] GradientDirection mGradientDirection = GradientDirection.LeftToRight;
    [HideInInspector][SerializeField] AnimationCurve mCurve = AnimationCurve.Linear(0f,0f,1f,1f);

    /// <summary>
    /// Color used by the widget.
    /// </summary>

    public Color gradientColor { get { return mGradientColor; } set { if (mGradientColor != value) { mGradientColor = value; mChanged = true; } } }
    public AnimationCurve gradientCurve { get { return mCurve; } set { mCurve = value; mChanged = true;  } }
    public GradientDirection gradientDirection { get { return mGradientDirection; } set { mGradientDirection = value; mChanged = true; } }

	/// <summary>
	/// Fill the draw buffers.
	/// </summary>

#if UNITY_3_5_4 || UNITY_3_5_2 || UNITY_3_5_3
    public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color> cols)
#else
    public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
#endif
    {
        int VertStart = verts.size;
        int UVStart = uvs.size;
        int ColStart = cols.size;

        base.OnFill(verts, uvs, cols);
        
        switch( mGradientDirection )
        {
            case GradientDirection.LeftToRight:
                mLerpFunction = fLeftToRight;
                break;
            case GradientDirection.RightToLeft:
                mLerpFunction = fRightToLeft;
                break;
            case GradientDirection.TopToBottom:
                mLerpFunction = fTopToBottom;
                break;
            case GradientDirection.BottomToTop:
                mLerpFunction = fBottomToTop;
                break;

            default:
                mLerpFunction = fLeftToRight;
                break;
        }

        for (int i = VertStart, j = ColStart ; i < verts.size; ++i,++j)
        {
            float f = mLerpFunction(verts[i]);
            cols[j] = Color.Lerp(color, mGradientColor, mCurve.Evaluate(f));
        }

    }

    float fLeftToRight(Vector3 pos)
    {
        return pos.x;
    }
    float fRightToLeft(Vector3 pos)
    {
        return 1f - pos.x;
    }
    float fTopToBottom(Vector3 pos)
    {
        return -pos.y;
    }
    float fBottomToTop(Vector3 pos)
    {
        return 1f + pos.y;
    }
}
