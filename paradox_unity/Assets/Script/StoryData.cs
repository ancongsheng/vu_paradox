using UnityEngine;
using System.Collections;

public class StoryData 
{

    public int id;
    public string name;
    public string content;
    public int type;
    public int next;

    public StoryData(string[] itemInfoList)
    {
        id = int.Parse(itemInfoList[0]);
        name = itemInfoList[1];
        content = itemInfoList[2].Replace("\\n", "\n");
        type = int.Parse(itemInfoList[3]);
        next = int.Parse(itemInfoList[4]);
    }
}

public class SelectionData
{
    public int id;
    public string content;
    public int needFlag;
    public int next;

    public SelectionData(string[] itemInfoList)
    {
        id = int.Parse(itemInfoList[0]);
        content = itemInfoList[1];
        next = int.Parse(itemInfoList[2]);
        needFlag = int.Parse(itemInfoList[3]);
    }
}