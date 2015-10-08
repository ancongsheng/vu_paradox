//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright ?2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Sample script showing how easy it is to implement a standard button that swaps sprites.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Image Button")]
public class NGUIImageButton : XUIActiveBase
{
	public NGUISprite target;
	public string normalSprite;
	public string hoverSprite;
	public string pressedSprite;
    public string disabledSprite;

	void OnEnable ()
	{
		if (target != null)
		{
            if (isEnabled)
            {
                target.spriteName = NGUICamera.IsHighlighted(gameObject) ? hoverSprite : normalSprite;
                ProcessActive();
            }
            else
                UpdateImageState(false);
		}
	}


    [SerializeField]
    public bool isEnabled
    {
        get
        {
            Collider col = collider;
            return col && col.enabled;
        }
        set
        {
            Collider col = collider;
            if (!col) return;

            if (col.enabled != value)
            {
                col.enabled = value;
                UpdateImageState(value);
            }
        }
    }

	void Start ()
	{
		if (target == null) target = GetComponentInChildren<NGUISprite>();
	}

	void OnHover (bool isOver)
	{
        if (isEnabled && target != null && !m_isActive)
		{
			target.spriteName = isOver ? hoverSprite : normalSprite;
			target.MakePixelPerfect();
		}
	}

	void OnPress (bool pressed)
	{
        if (isEnabled && target != null && !m_isActive)
		{
			target.spriteName = pressed ? pressedSprite : normalSprite;
			target.MakePixelPerfect();
		}
	}

    public void UpdateImageState(bool shouldBeEnabled)
    {
        if ( target != null )
        {
            target.spriteName = shouldBeEnabled ? normalSprite : disabledSprite;
            //target.MakePixelPerfect();
        }
    }

    protected override void ProcessActive()
    {
        if (target != null)
        {
            target.spriteName = m_isActive ? pressedSprite : normalSprite;
            target.MakePixelPerfect();
        }
    }
}