using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance = null;


    public const string Music_UIMain = "MainTheme-Loop";

    public float MainSoundVolume = 1.0f;
    public float MainMusicVolume = 1.0f;
    public float OldMainSoundVolume { get; private set; }
    public float OldMainMusicVolume { get; private set; }
  
    //public AudioClip MenuMusic = null;
	public string MenuMusicBundleName = "";
	public string WinMusicBundleName = "";

    public string[] LevelMusicBundleName;

    AudioSource m_MusicSource;
    AudioSource m_AmbSource;
    bool ReadyToPlayMusic = false;
    bool ReadyToPlayAmb = false;
    bool StopCurrentMusic = false;
    float fadeVolume = 0.0f;
    float fadeInTime = 1.0f;
    float fadeOutTime = 1.0f;
    Transform listener;
    Transform followThing = null;
    
    string curMusicName = "";
	string curAmbName = "";

    [HideInInspector]
    public SoundSet uiSounds;
	

    void Awake()
    {
        Instance = this;

        OldMainSoundVolume = MainSoundVolume;
        OldMainMusicVolume = MainMusicVolume;

        if (GameConfig.GetConfigInt(GameConfig.KEY_MusicOnOff) != 0)
            MainMusicVolume = 0;
        
        if (GameConfig.GetConfigInt(GameConfig.KEY_SoundOnOff) != 0)
            MainSoundVolume = 0;

        listener = new GameObject("Listener").transform;
        listener.parent = transform;
        listener.gameObject.AddComponent<AudioListener>();
        m_MusicSource = listener.gameObject.AddComponent<AudioSource>();
        m_AmbSource = listener.gameObject.AddComponent<AudioSource>();

        m_AmbSource.panLevel = 0.0f;
        m_MusicSource.panLevel = 0.0f;
        //fadeVolume = 0f;
        m_MusicSource.volume = fadeVolume * MainMusicVolume;
        m_AmbSource.volume = MainSoundVolume;
        uiSounds = GetComponent<SoundSet>();

    }

	// Use this for initialization
	void Start () 
    {
        SoundManager.Instance.PlayMusic(Music_UIMain, true, 5f);
	}

    private GameObject lastHoverObj = null;

    public void PlayButtonHover(GameObject go, bool isHover)
    {
        if (isHover)
        {
            if (go != lastHoverObj)
            {
                lastHoverObj = go;
                //NGUISounds.Play("ButtonOver", gameObject);
            }
        }
        else
        {
            lastHoverObj = null;
        }
    }
    public void PlayButtonClick(GameObject go)
    {
        lastHoverObj = go;
        uiSounds.Play("ButtonClick", gameObject);
    }

    public void PlayButtonClick(GameObject go,string pSoundName)
    {
        lastHoverObj = go;
        uiSounds.Play(pSoundName, gameObject);
    }

    public void PlayButtonPress(GameObject go, bool isOver)
    {
        lastHoverObj = go;
        if ( isOver )
            uiSounds.Play("ButtonPress", gameObject);
    }
	
	public void StopMusic()
	{
		curMusicName = "";
	    if (m_MusicSource.isPlaying)
	    {
	        StopCurrentMusic = true;
	    }	
	}

    public void PlayMusic(string MusicBundleName,bool loop, float fadeTime)
    {
        if (curMusicName != MusicBundleName)
        {
            if (m_MusicSource.isPlaying)
            {
                StopCurrentMusic = true;
            }
            else
            {
                StopCurrentMusic = false;
            }
			curMusicName = MusicBundleName;
            //StartCoroutine(StartPlayMenuMusicWhenNoMusic());
            float fTime = 1.0f / fadeTime;
//#if !UNITY_ANDROID && !UNITY_IPHONE
            StartCoroutine(LoadAndPlayMusic(curMusicName, loop, fTime));
//#endif
				
        }
    }



    private IEnumerator LoadAndPlayMusic(string _bundleName, bool _loop, float fadeTime)
    {

        while(m_MusicSource.isPlaying && curMusicName == _bundleName) yield return null;

        if (m_MusicSource.clip != null) Resources.UnloadAsset(m_MusicSource.clip);
        m_MusicSource.clip = null;


        m_MusicSource.clip = Resources.Load("Music/"+_bundleName) as AudioClip;
        Debug.Log("play " + _bundleName);
        m_MusicSource.Play();
        fadeInTime = fadeTime;
        m_MusicSource.loop = _loop;

    }


    public void PlayFadeAudio(AudioSource source, bool In, float time)
    {
        StartCoroutine(FadeAudioSource(source, In, time));
    }

    IEnumerator FadeAudioSource(AudioSource source, bool In, float time)
    {
        float origVolume = source.volume;
        float precent = 0.0f;

        if (In) source.volume = 0.0f;
        while (precent < 1.0f)
        {
            precent += Time.deltaTime * (1.0f / time);
            if (source == null) yield break;
            source.volume = origVolume * (In ? precent : (1.0f - precent));
            yield return null;
        }
    }

	// Update is called once per frame
	void Update () {
        if (followThing)
        {
            listener.position = followThing.position;
            listener.rotation = followThing.rotation;
        }
        else
        {
            //followThing = Camera.main.transform;
            //followThing = NGUICamera.mainCamera.transform;
        }

        if (m_MusicSource.isPlaying)
        {
            if (StopCurrentMusic)
            {
                if (fadeVolume > 0f)
                {
                    fadeVolume -= Time.deltaTime * fadeOutTime;
                    if (fadeVolume < 0f)
                    {
                        fadeVolume = 0f;
                        m_MusicSource.Stop();
                        StopCurrentMusic = false;
                    }
                    
                }
            }
            else
            {
                if (fadeVolume < 1f)
                {
                    fadeVolume += Time.deltaTime * fadeInTime;
                    if (fadeVolume > 1f) fadeVolume = 1f;
                }
            }
            m_MusicSource.volume = fadeVolume * MainMusicVolume;
            m_AmbSource.volume = MainSoundVolume;
        }
        if (ReadyToPlayAmb && !m_AmbSource.isPlaying && m_AmbSource.clip.isReadyToPlay)
        {
            m_AmbSource.Play();
            ReadyToPlayAmb = false;
        }

        if (ReadyToPlayMusic && !m_MusicSource.isPlaying && m_MusicSource.clip.isReadyToPlay)
        {
            m_MusicSource.Play();
            ReadyToPlayMusic = false;
        }

	}
}
