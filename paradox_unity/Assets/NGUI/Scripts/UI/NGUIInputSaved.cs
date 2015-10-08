//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Editable text input field that automatically saves its data to PlayerPrefs.
/// </summary>

[AddComponentMenu("NGUI/UI/Input (Saved)")]
public class NGUIInputSaved : NGUIInput
{
	public string playerPrefsField;

	public override string text
	{
		get
		{
			return base.text;
		}
		set
		{
			base.text = value;
			SaveToPlayerPrefs(value);
		}
	}

	void Awake ()
	{
		onSubmit = SaveToPlayerPrefs;

		if (!string.IsNullOrEmpty(playerPrefsField) && PlayerPrefs.HasKey(playerPrefsField))
		{
			text = PlayerPrefs.GetString(playerPrefsField);
		}
	}

    public void Save()
    {
        if (!string.IsNullOrEmpty(playerPrefsField))
        {
            PlayerPrefs.SetString(playerPrefsField, text);
        }
    }

	private void SaveToPlayerPrefs (string val)
	{
		if (!string.IsNullOrEmpty(playerPrefsField))
		{
			PlayerPrefs.SetString(playerPrefsField, val);
		}
	}

	void OnApplicationQuit ()
	{
		SaveToPlayerPrefs(text);
	}
}
