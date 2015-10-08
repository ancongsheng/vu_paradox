//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Simple example script of how a button can be offset visibly when the mouse hovers over it or it gets pressed.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class NGUIButtonOffset : XUIActiveBase
{
	public Transform tweenTarget;
	public Vector3 hover = Vector3.zero;
	public Vector3 pressed = new Vector3(2f, -2f);
	public float duration = 0.2f;

	Vector3 mPos;
    Vector3 mDefaultPos;
    Vector3 mDefaultPressed;
	bool mStarted = false;
	bool mHighlighted = false;

	void Start ()
	{
		if (!mStarted)
		{
			mStarted = true;
			if (tweenTarget == null) tweenTarget = transform;
			mPos = tweenTarget.localPosition;

            mDefaultPos = mPos;
            mDefaultPressed = pressed;
		}
	}

	void OnEnable () { if (mStarted && mHighlighted) OnHover(NGUICamera.IsHighlighted(gameObject)); }

	void OnDisable ()
	{
		if (mStarted && tweenTarget != null)
		{
			TweenPosition tc = tweenTarget.GetComponent<TweenPosition>();

			if (tc != null)
			{
				tc.position = mPos;
				tc.enabled = false;
			}
		}
	}

	void OnPress (bool isPressed)
	{
		if (enabled)
		{
			if (!mStarted) Start();
			TweenPosition.Begin(tweenTarget.gameObject, duration, isPressed ? mPos + pressed :
				(NGUICamera.IsHighlighted(gameObject) ? mPos + hover : mPos)).method = NGUITweener.Method.EaseInOut;
		}
	}

	void OnHover (bool isOver)
	{
		if (enabled)
		{
			if (!mStarted) Start();
			TweenPosition.Begin(tweenTarget.gameObject, duration, isOver ? mPos + hover : mPos).method = NGUITweener.Method.EaseInOut;
			mHighlighted = isOver;
		}
    }
    protected override void ProcessActive()
    {
        if (!mStarted) Start();
        mPos = m_isActive ? mDefaultPos + pressed : mDefaultPos;
        pressed = m_isActive ? Vector3.zero : mDefaultPressed;

        TweenPosition.Begin(tweenTarget.gameObject, duration,
                (NGUICamera.IsHighlighted(gameObject) ? mPos + hover : mPos)).method = NGUITweener.Method.EaseInOut;
    }
}
