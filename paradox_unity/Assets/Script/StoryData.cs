using UnityEngine;
using System.Collections;

public class StoryData 
{

    public int id;
    public string name;
    public string content;
    public int type;
    public int next;
    public string param;

    public StoryData(string[] itemInfoList)
    {
        id = int.Parse(itemInfoList[0]);
        name = itemInfoList[1];
        content = itemInfoList[2];
        type = int.Parse(itemInfoList[3]);
        next = int.Parse(itemInfoList[4]);
        param = itemInfoList[5];
    }
}
