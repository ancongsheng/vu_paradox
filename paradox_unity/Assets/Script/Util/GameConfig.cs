using UnityEngine;
using System.Collections;

public class GameConfig {

    public static string KEY_SoundOnOff = "KEY_SoundOnOff";
    public static string KEY_MusicOnOff = "KEY_MusicOnOff";

    public static string KEY_AutoOnOff = "KEY_AutoOnOff";

	public static void Init()
    {

    }

    public static int GetConfigInt(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
            return 0;
        }
    }

    public static string GetConfigString(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        else
        {
            PlayerPrefs.SetString(key, "");
            return "";
        }
    }

}
