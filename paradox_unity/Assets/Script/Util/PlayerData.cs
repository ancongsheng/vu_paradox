using UnityEngine;
using System.Collections;
using System.IO;

public class PlayerData  {

    BitArray textflag;
    BitArray endingflag;

    private static PlayerData instance = null;

    private static string path_text = Application.persistentDataPath + "/text";
    private static string path_end = Application.persistentDataPath + "/ending";


    public static void Load()
    {
        if (instance == null) instance = new PlayerData();

        FileStream fs = null;
        Debug.Log(path_text);
        fs = File.Open(path_text, FileMode.OpenOrCreate);

        byte[] buf = new byte[1024];
        fs.Read(buf, 0, 1024);
        instance.textflag = new BitArray(buf);

        fs.Flush();
        fs.Close();

        fs = File.Open(path_end, FileMode.OpenOrCreate);

        byte[] buf2 = new byte[10];
        fs.Read(buf2, 0, 10);
        instance.endingflag = new BitArray(buf2);

        fs.Flush();
        fs.Close();
    }


    public static void Save()
    {
        FileStream fs = null;
        Debug.Log(path_text);
        fs = File.Open(path_text, FileMode.OpenOrCreate);

        byte[] buf = new byte[1024];
        instance.textflag.CopyTo(buf, 0);
        fs.Write(buf, 0, 1024);

        fs.Flush();
        fs.Close();

        fs = File.Open(path_end, FileMode.OpenOrCreate);

        byte[] buf2 = new byte[10];
        instance.endingflag.CopyTo(buf2, 0);
        fs.Write(buf2, 0, 10);

        fs.Flush();
        fs.Close();
    }

    public static bool GetTextFlag(int id)
    {
        return instance.textflag.Get(id);
    }

    public static bool GetEndingFlag(int id)
    {
        return instance.endingflag.Get(id);
    }

    public static void SetTextFlag(int id)
    {
        instance.textflag[id] = true;
    }


    private PlayerData()
    {
    }
}
