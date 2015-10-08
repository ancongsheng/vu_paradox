using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Range
{
	// Fields
	public float m_max;
	public float m_min;

	// Methods
	public Range(float min, float max)
	{
		this.m_min = min;
		this.m_max = max;
	}

	public float GetRandom()
	{
		return Random.Range(this.m_min, this.m_max);
	}

	// Properties
	public float max
	{
		get
		{
			return this.m_max;
		}
	}

	public float min
	{
		get
		{
			return this.m_min;
		}
	}
}

[System.Serializable]
public class SoundGroup
{
	public string groupName = "";
	public List<AudioClip> m_audioClips = new List<AudioClip>();
    public float Volume = 1f;
}

public class SoundSet : MonoBehaviour
{
    // Fields
	public List<SoundGroup> m_audioGroups = new List<SoundGroup>();
    protected AudioClip m_currentClip;
	protected SoundGroup m_currentGroup;
    //public Range m_lowPassCutoff = new Range(22000f, 1000f);
    //public Range m_lowPassDistance = new Range(1f, 20f);
    //public bool m_lowPassEnabled;
    public Range m_pitchRange = new Range(1f, 1f);
    public Range m_rolloffDistance = new Range(1f, 100f);
    public AudioRolloffMode m_rolloffMode = AudioRolloffMode.Linear;
    //public SoundGroup m_soundGroup = SoundGroup.Generic;
    //public Range m_volumeRange = new Range(1f, 1f);

    // Methods
	public void Awake()
	{
		if(m_audioGroups != null && m_audioGroups.Count>0)
			m_currentGroup = m_audioGroups[0];
	}

	public bool SetCurrentGroup(string groupName)
	{
		foreach (SoundGroup group in m_audioGroups)
		{
			if (group.groupName == groupName)
			{
				m_currentGroup = group;
                return true;
			}
		}
        return false;
	}
    protected AudioClip GetRandomClip()
    {
        List<AudioClip> list = new List<AudioClip>();
		foreach (AudioClip clip in m_currentGroup.m_audioClips)
        {
			if ((clip != null) && ((clip != this.m_currentClip) || (m_currentGroup.m_audioClips.Count < 2)))
            {
                list.Add(clip);
            }
        }
        if (list.Count > 0)
        {
            this.m_currentClip = list[Random.Range(0, list.Count)];
        }
        else
        {
            this.m_currentClip = null;
        }
        return this.m_currentClip;
    }

    public bool HasSounds()
    {
		return (m_audioGroups.Count > 0);
    }

    public AudioSource Play(string GroupName, GameObject sourceObject)
    {
        return this.Play(GroupName, sourceObject, SoundManager.Instance.MainSoundVolume, false, true);
    }

    public AudioSource Play(string GroupName, Vector3 position)
    {
        return this.Play(GroupName, position, SoundManager.Instance.MainSoundVolume, false);
    }

    void Play(AudioSource source, float volume)
    {
        //SoundVolumeControl controller = null;
        if (this.SetupAudioSource(source, volume/*, out controller*/))
        {
            source.Play();
        }
    }

    public AudioSource Play(string GroupName, GameObject sourceObject, bool attachToObject)
    {
        return this.Play(GroupName, sourceObject, SoundManager.Instance.MainSoundVolume, false, attachToObject);
    }

    public AudioSource Play(string GroupName, GameObject sourceObject, float volume)
    {
        return this.Play(GroupName,sourceObject, volume, false, true);
    }

    public AudioSource Play(string GroupName, Vector3 position, float volume)
    {
        return this.Play(GroupName,position, volume, false);
    }

    public AudioSource Play(string GroupName, Vector3 position, float volume, bool loop)
    {
        if (!this.HasSounds())
        {
            return null;
        }
        if (!SetCurrentGroup(GroupName)) return null;
        GameObject obj2 = new GameObject("AudioSource") {
            transform = { position = position }
        };
        AudioSource source = obj2.AddComponent<AudioSource>();
		/*
        if (this.m_lowPassEnabled)
        {
            obj2.AddComponent<AudioLowPassFilter>();
            LowPassController controller = obj2.AddComponent<LowPassController>();
            controller.distanceRange = this.m_lowPassDistance;
            controller.cutoffRange = this.m_lowPassCutoff;
        }*/
        this.Play(source, volume);
        obj2.name = obj2.name + ": " + (!loop ? "Clip[" : ", Looping clip[") + ((this.m_currentClip == null) ? "null]" : (this.m_currentClip.name + "]"));
        if (this.m_currentClip == null)
        {
            Object.Destroy(obj2);
            return null;
        }
        if (loop)
        {
            source.loop = true;
            return source;
        }
        Object.Destroy(obj2, this.m_currentClip.length);
        return source;
    }

    public AudioSource Play(string GroupName, GameObject sourceObject, float volume, bool loop, bool attachToObject)
    {
        AudioSource source = this.Play(GroupName, sourceObject.transform.position, volume, loop);
        if (source != null)
        {
            if (attachToObject)
            {
                source.transform.parent = sourceObject.transform;
            }
            source.name = source.name + " Spawned from " + ((sourceObject == null) ? "null" : sourceObject.name);
            return source;
        }
		if (m_audioGroups.Count > 0)
        {
            string name;
            if (sourceObject == null)
            {
                name = "[Null source object specified]!";
                return source;
            }
            name = sourceObject.name;
            for (Transform transform = sourceObject.transform; transform.parent != null; transform = transform.parent)
            {
                name = transform.parent.name + "/" + name;
            }
            name = name + ".";
        }
        return source;
    }

    public AudioSource PlayLoop(string GroupName, GameObject sourceObject)
    {
        return this.Play(GroupName, sourceObject, SoundManager.Instance.MainSoundVolume, true, true);
    }

    public void PlayOneShot(AudioSource source, float volume)
    {
        //SoundVolumeControl controller = null;
        if (this.SetupAudioSource(source, volume/*, out controller*/))
        {
            source.PlayOneShot(source.clip);
        }
    }

    public bool SetupAudioSource(AudioSource source, float volume/*, out SoundVolumeControl controller*/)
    {
        if ((source == null) || !this.HasSounds())
        {
            //controller = null;
            return false;
        }
        source.clip = this.GetRandomClip();
        if (source.clip == null)
        {
            string name = source.name;
            for (Transform transform = source.transform; transform.parent != null; transform = transform.parent)
            {
                name = transform.parent.name + "/" + name;
            }
            return false;
        }
		/*
        SoundMixer component = null;
        //if (GameManager.GetInstance() != null)
        {
			component = SoundMixer.Instance;
        }
        if (component != null)
        {
            float volumeSetting = this.m_volumeRange.GetRandom() * volume;
            source.bypassEffects = component.GetGroupBypassEffects(this.m_soundGroup);
            source.volume = component.GetGroupVolume(this.m_soundGroup) * volumeSetting;
            controller = component.RegisterAudioSource(source, this.m_soundGroup, volumeSetting);
        }
        else
        {
            source.volume = this.m_volumeRange.GetRandom() * volume;
            controller = null;
        }*/
        source.volume = m_currentGroup.Volume * volume;
        source.pitch = this.m_pitchRange.GetRandom();
        source.dopplerLevel = 0f;
        source.minDistance = this.m_rolloffDistance.min;
        source.maxDistance = this.m_rolloffDistance.max;
        source.rolloffMode = this.m_rolloffMode;
        source.playOnAwake = false;
        return true;
    }
}

 
