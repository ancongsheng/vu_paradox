using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MainGame : MonoBehaviour {

    public static MainGame instance = null;


    public Dictionary<int, StoryData> text = new Dictionary<int, StoryData>();


    public BitArray currentFlag;
    public int currentID = 0;


    void Awake()
    {
        instance = this;
    }

    void OnDestroy()
    {
        instance = null;

        PlayerData.Save();
    }

	// Use this for initialization
	void Start () 
    {
        init();

        //show startup
	}
	
	
    public void loadSaveData(int id)
    {
        currentFlag.SetAll(false);

        //load file
        string filepath = string.Format("{0}/sav{1}", Application.persistentDataPath, id);
        Debug.Log(filepath);

        if (!File.Exists(filepath))
        {
            //show error
            return;
        }

        FileStream fs = File.Open(filepath, FileMode.Open);

        BinaryReader reader = new BinaryReader(fs);
        currentID = reader.ReadInt32();
        byte[] buf = reader.ReadBytes(10);
        currentFlag = new BitArray(buf);

        fs.Flush();
        fs.Close();

        startGame();
    }

    public void createNewGame()
    {
        currentFlag.SetAll(false);
        if (PlayerData.GetEndingFlag(1)) currentFlag[1] = true;
        if (PlayerData.GetEndingFlag(2)) currentFlag[2] = true;
        if (PlayerData.GetEndingFlag(3)) currentFlag[3] = true;

        currentID = 1;
        startGame();
    }



    private void init()
    {
        //parseData();

        PlayerData.Load();
    }


    private void startGame()
    {

    }


    private void parseData()
    {
        string[] itemRowsList = ResourceManager.LoadText("main");
        text.Clear();

        //Skip first three lines.
        for (int i = 2; i < itemRowsList.Length; ++i)
        {
            string[] itemColumnsList = itemRowsList[i].Split('\t');
            if (itemColumnsList.Length < 3)
            {
                Debug.LogWarning("The source data seems to have an inconsistent number of columns: " + itemColumnsList.Length);
                continue;
            }

            StoryData data = new StoryData(itemColumnsList);
            text.Add(data.id, data);

        }
    }

}
