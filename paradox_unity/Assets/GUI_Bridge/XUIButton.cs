using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(NGUIImageButton))]
public class XUIButton : XUIClickResponder
{
    private NGUIImageButton[] m_ButtonImage = null;
    private NGUIButton[] m_Button = null;
    private XUIActiveBase[] m_ButtonColor = null;
    private NGUICheckbox[] m_ButtonCheck = null;

    private NGUIButtonColor m_InterColor = null;
    private NGUIButtonOffset m_InterOffset = null;
    private NGUIButtonScale m_InterScale = null;

    [SerializeField]private bool m_HoverSound = false;
    [SerializeField]private bool m_ClickSound = true;

    [SerializeField]
    private bool m_SpecialClickSound = false;
    [SerializeField]
    private string m_ClickSoundName = "";

    [SerializeField]
    private float m_ClickInteval = 0.5f;

    private static float m_ClickTime = 0;


    public bool clickSound
    {
        get
        {
            return m_ClickSound;
        }
        set
        {
            SetClickSound(value);
        }
    }
    public bool hoverSound
    {
        get
        {
            return m_HoverSound;
        }
        set
        {
            SetHoverSound(value);
        }
    }

	// Use this for initialization
    void Awake()
    {
        m_ButtonImage = this.GetComponents<NGUIImageButton>();
        m_Button = this.GetComponents<NGUIButton>();
        m_ButtonColor = this.GetComponents<XUIActiveBase>();
        m_ButtonCheck = this.GetComponents<NGUICheckbox>();
	}

    void Start()
    {
#if UNITY_ANDROID || UNITY_IPHONE
        if (m_ClickSound)
            //AddPressDelegate(SoundManager.Instance.PlayButtonPress);
            //AddClickDelegate(SoundManager.Instance.PlayButtonClick);
            AddClickDelegate(PlaySoundDelegate);
#else
        if (m_ClickSound)
        {
            AddClickDelegate(PlaySoundDelegate);
        }
        if (m_HoverSound)
            AddHoverDelegate(SoundManager.Instance.PlayButtonHover);
#endif
    }

    protected void SetClickSound(bool isEnable)
    {
        if (isEnable != m_ClickSound)
        {
            m_ClickSound = isEnable;
            if (isEnable)
                AddClickDelegate(PlaySoundDelegate);
            else
                RemoveClickDelegate(PlaySoundDelegate);
        }
    }
    protected void SetHoverSound(bool isEnable)
    {
        m_HoverSound = isEnable;
        if (isEnable != m_HoverSound)
        {
            if (isEnable)
                AddHoverDelegate(SoundManager.Instance.PlayButtonHover);
            else
                RemoveHoverDelegate(SoundManager.Instance.PlayButtonHover);
        }
    }

    protected void PlaySoundDelegate(GameObject obj)
    {
        if (m_SpecialClickSound)
        {
            UISoundManager.instance.Play(m_ClickSoundName);
        }
        else
        {
            //SoundManager.Instance.uiSounds.Play("GameEnter", SoundManager.Instance.gameObject);
            if (m_ClickSoundName != null && m_ClickSoundName.Trim().Length>1)
            {
                SoundManager.Instance.PlayButtonClick(obj, m_ClickSoundName);
            }else
            {
                SoundManager.Instance.PlayButtonClick(obj);
            }
            
            //UISoundManager.instance.Play(Default);
        }
    }

    // ----
    /// <summary>
    /// Indicates the state of the button
    /// </summary>
    public enum CONTROL_STATE
    {
        /// <summary>
        /// The button is "normal", awaiting input
        /// </summary>
        NORMAL,

        /// <summary>
        /// The button has an input device hovering over it.
        /// </summary>
        OVER,

        /// <summary>
        /// The button is being pressed
        /// </summary>
        ACTIVE,

        /// <summary>
        /// The button is disabled
        /// </summary>
        DISABLED
    };


    public override void OnHover(bool isOver)
    {
        if (onHover != null && isEnabled)
        {
            onHover(gameObject, isOver);
        }
    }

    public override void OnClick()
    {

        if (m_ClickTime != 0)
        {
            if (Time.realtimeSinceStartup - m_ClickTime < m_ClickInteval)
            {
                return;
            }
        }

        m_ClickTime = Time.realtimeSinceStartup;




        PreClickValid = true;
        if (onPreClick != null)
        {
            onPreClick(gameObject);
        }
        if (!PreClickValid)
        {
            return;
        }

        if (isEnabled)
        {
            if (onClick != null)
                onClick(gameObject);

            if (m_ButtonCheck != null && m_ButtonCheck.Length > 0)
            {
                for (int i = 0, iMax = m_ButtonCheck.Length; i < iMax; ++i)
                    m_ButtonCheck[i].SendMessage("OnClickX");
            }
        }
    }

    public void ForceClick()
    {
        if (isEnabled)
        {
            if (onClick != null)
                onClick(gameObject);

            if (m_ButtonCheck != null && m_ButtonCheck.Length > 0)
            {
                for (int i = 0, iMax = m_ButtonCheck.Length; i < iMax; ++i)
                    m_ButtonCheck[i].SendMessage("OnClickX");
            }
        }
    }

    public bool isEnabled
    {
        get
        {
            bool ret = false;
            if (m_ButtonImage != null && m_ButtonImage.Length > 0)
            {
                for (int i = 0, iMax = m_ButtonImage.Length; i < iMax; ++i)
                    ret = ret || m_ButtonImage[i].isEnabled;
            }
            else if (m_Button != null && m_Button.Length > 0)
            {
                for (int i = 0, iMax = m_Button.Length; i < iMax; ++i)
                    ret = ret || m_Button[i].isEnabled;
            }
            else
                ret = true;

            return ret;
        }
        set
        {
            bool useCol = true;
            if (m_ButtonImage != null && m_ButtonImage.Length > 0)
            {
                for (int i = 0, iMax = m_ButtonImage.Length; i < iMax; ++i)
                    m_ButtonImage[i].isEnabled = value;
                useCol = false;
            }
            if (m_Button != null && m_Button.Length > 0)
            {
                //LogManager.Log_Error("Button count: " + m_Button.Length);
                for (int i = 0, iMax = m_Button.Length; i < iMax; ++i)
                {
                    //LogManager.Log_Error("curr: " + i);
                    if ( m_Button[i] != null)
                        m_Button[i].isEnabled = value;
                }
                useCol = false;
            }

            if (m_ButtonCheck != null && m_ButtonCheck.Length > 0)
            {
                for (int i = 0, iMax = m_ButtonCheck.Length; i < iMax; ++i)
                    m_ButtonCheck[i].enabled = value;
                useCol = false;
            }

            if (m_ButtonColor != null && m_ButtonColor.Length > 0)
            {
                for (int i = 0, iMax = m_ButtonColor.Length; i < iMax; ++i)
                    m_ButtonColor[i].enabled = value;
                useCol = false;
            }

            if (useCol)
            {
                collider.enabled = value;
            }
        }
    }

    public void SetState(CONTROL_STATE s)
    {
        switch (s)
        {
            case CONTROL_STATE.NORMAL:
                if (m_ButtonImage != null && m_ButtonImage.Length > 0)
                {
                    for (int i = 0, iMax = m_ButtonImage.Length; i < iMax; ++i)
                        m_ButtonImage[i].target.spriteName = m_ButtonImage[i].normalSprite;
                }
                break;
            case CONTROL_STATE.OVER:
                if (m_ButtonImage != null && m_ButtonImage.Length > 0)
                {
                    for (int i = 0, iMax = m_ButtonImage.Length; i < iMax; ++i)
                        m_ButtonImage[i].target.spriteName = m_ButtonImage[i].hoverSprite;
                }
                break;
            case CONTROL_STATE.ACTIVE:
                if (m_ButtonImage != null && m_ButtonImage.Length > 0)
                {
                    for (int i = 0, iMax = m_ButtonImage.Length; i < iMax; ++i)
                        m_ButtonImage[i].target.spriteName = m_ButtonImage[i].pressedSprite;
                }
                break;
            case CONTROL_STATE.DISABLED:
                if (m_ButtonImage != null && m_ButtonImage.Length > 0)
                {
                    for (int i = 0, iMax = m_ButtonImage.Length; i < iMax; ++i)
                        m_ButtonImage[i].target.spriteName = m_ButtonImage[i].disabledSprite;
                }
                break;
            default:
                break;

        }
    }

}
