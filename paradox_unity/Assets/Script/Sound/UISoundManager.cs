using UnityEngine;
using System.Collections;

public class UISoundManager : MonoBehaviour {

    public static UISoundManager instance = null;

    private SoundSet m_SoundSet;

	void Awake()
    {
        instance = this;
        m_SoundSet = gameObject.GetComponent<SoundSet>();
    }

    void OnDestroy()
    {
        instance = null;
    }

    public AudioSource Play(string groupName)
    {
        return m_SoundSet.Play(groupName, gameObject);
    }

    public AudioSource Play(string groupName, bool loop)
    {
        return m_SoundSet.Play(groupName, gameObject, SoundManager.Instance.MainSoundVolume, loop, true);
    }
}
