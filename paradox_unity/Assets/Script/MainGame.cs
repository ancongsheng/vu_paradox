using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MainGame : MonoBehaviour {

    public static MainGame instance = null;


    public Dictionary<int, StoryData> text = new Dictionary<int, StoryData>();
    public Dictionary<int, SelectionData> selection = new Dictionary<int, SelectionData>();

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

        UIManager.ShowPanel("Start_Panel");
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
        currentFlag = new BitArray(80, false);
        if (PlayerData.GetEndingFlag(1)) currentFlag[1] = true;
        if (PlayerData.GetEndingFlag(2)) currentFlag[2] = true;
        if (PlayerData.GetEndingFlag(3)) currentFlag[3] = true;

        currentID = 1;
        startGame();
    }



    private void init()
    {
        parseData();

        PlayerData.Load();
    }


    private void startGame()
    {
        UIManager.ShowPanel("Main_Panel");
    }


    private void parseData()
    {
        string[] itemRowsList = ResourceManager.LoadText("main.text");
        text.Clear();

        //Skip first three lines.
        for (int i = 3; i < itemRowsList.Length; ++i)
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

        itemRowsList = ResourceManager.LoadText("main.selection");
        selection.Clear();

        //Skip first three lines.
        for (int i = 3; i < itemRowsList.Length; ++i)
        {
            string[] itemColumnsList = itemRowsList[i].Split('\t');
            if (itemColumnsList.Length < 3)
            {
                Debug.LogWarning("The source data seems to have an inconsistent number of columns: " + itemColumnsList.Length);
                continue;
            }

            SelectionData data = new SelectionData(itemColumnsList);
            selection.Add(data.id, data);

        }
    }

}
