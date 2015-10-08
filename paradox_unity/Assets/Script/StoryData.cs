using UnityEngine;
using System.Collections;

public class StoryData 
{

    public int id;
    public string content;
    public int type;
    public int next;

    public StoryData(string[] itemInfoList)
    {
        id = int.Parse(itemInfoList[0]);
        content = itemInfoList[1];
        type = int.Parse(itemInfoList[2]);
        next = int.Parse(itemInfoList[3]);
    }
}
