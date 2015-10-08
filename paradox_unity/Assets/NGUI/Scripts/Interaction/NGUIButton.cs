//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Similar to NGUIButtonColor, but adds a 'disabled' state based on whether the collider is enabled or not.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Button")]
public class NGUIButton : NGUIButtonColor
{
	/// <summary>
	/// Color that will be applied when the button is disabled.
	/// </summary>

	public Color disabledColor = Color.grey;

	/// <summary>
	/// If the collider is disabled, assume the disabled color.
	/// </summary>

	protected override void OnEnable ()
	{
        if (isEnabled)
            base.OnEnable();
        else
            //UpdateColor(false, true);
            OnActivateDele(m_isActive);
	}

	public override void OnHover (bool isOver) { if (isEnabled) base.OnHover(isOver); }
	public override void OnPress (bool isPressed) { if (isEnabled) base.OnPress(isPressed); }

	/// <summary>
	/// Whether the button should be enabled.
	/// </summary>
    private int m_isEnabled = -1;
	public bool isEnabled
	{
		get
		{
            if (m_isEnabled == -1)
            {
                Collider col = collider;
                m_isEnabled =  col && col.enabled ? 1 : 0;
                OnActivateDele(m_isActive);
            }
            
            return m_isEnabled == 0 ? false : true;
		}
		set
		{
			Collider col = collider;
			if (!col) return;

			//if (col.enabled != value)
            int v = value ? 1 : 0;
            if ( m_isEnabled != v )
			{
                m_isEnabled = v;
				//col.enabled = value;
				//UpdateColor(value, true);
			}

            //if (value == false) m_isActive = false;

            OnActivateDele(m_isActive);
		}
	}

	/// <summary>
	/// Update the button's color to either enabled or disabled state.
	/// </summary>

	public void UpdateColor (bool shouldBeEnabled, bool immediate)
	{
		if (tweenTarget)
		{
			if (!mStarted)
			{
				mStarted = true;
				Init();
			}

            defaultColor = m_isActive ? pressed : isEnabled ? mDefaultColor : disabledColor;
			Color c = shouldBeEnabled ? defaultColor : disabledColor;
            TweenColor tc = TweenColor.Begin(tweenTarget, duration, c);

			if (immediate)
			{
				tc.color = c;
				tc.enabled = false;
			}
		}
	}

    protected override void ProcessActive()
    {
        //LogManager.Log_Error(logObjName(this) + "ProccACT: " + m_isActive + " isEnable: " + isEnabled + " colors: " + pressed + defaultColor + mDefaultColor + disabledColor);
        if (tweenTarget)
        {
            if (!mStarted)
			{
				mStarted = true;
				Init();
			}

            defaultColor = m_isActive ? pressed : (isEnabled ? mDefaultColor : disabledColor);
            //Color c = isEnabled ? (NGUICamera.IsHighlighted(gameObject) ? hover : defaultColor) : disabledColor;
            Color c = isEnabled ? (NGUICamera.IsHighlighted(gameObject) ? hover : defaultColor) : (m_isActive? defaultColor : disabledColor);
            TweenColor tc = TweenColor.Begin(tweenTarget, duration, c);

            //LogManager.Log_Error(logObjName(this) + "ProccACT: " + m_isActive + " isEnable: " + isEnabled + " colors: "+ c + pressed + defaultColor + mDefaultColor + disabledColor);

            tc.color = c;
            tc.enabled = false;
        }

    }
}
