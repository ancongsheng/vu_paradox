using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public int requireId;
    public int next;
    public int addValueName;
    public int addValueVal;
    public int setFlag;

    public SelectionData(string[] itemInfoList)
    {
        id = int.Parse(itemInfoList[0]);
        content = itemInfoList[1];
        next = int.Parse(itemInfoList[2]);
        requireId = int.Parse(itemInfoList[3]);
        addValueName = int.Parse(itemInfoList[4]);
        addValueVal = int.Parse(itemInfoList[5]);
        setFlag = int.Parse(itemInfoList[6]);
    }
}

public class ConditionData
{
    public int id;
    public List<int> needFlag;
    public int needFlagVal;
    public int needValueName;
    public int needValueMinVal;

    public ConditionData(string[] itemInfoList)
    {
        id = int.Parse(itemInfoList[0]);

        needFlag = new List<int>();
        if (itemInfoList[1] != string.Empty)
        {
            string[] split = itemInfoList[1].Split('+');
            int i = 0;
            for (i = 0; i < split.Length; i++)
            {
                needFlag.Add(int.Parse(split[i]));
            }
        }


        needFlagVal = int.Parse(itemInfoList[2]);
        needValueName = int.Parse(itemInfoList[3]);
        needValueMinVal = int.Parse(itemInfoList[4]);
    }
}


public enum ContentType
{
    Default = 0,
    Select,
    PassiveSelect,
    End = 10,
}


public enum GameValueType
{
    None = 0,
    Naru0,
    Naru1,
    Naru2,
    Naru3,
    Max,
}

public enum FlagType
{
    None = 0,
    ED_Naru1,
    ED_Naru2,
    ED_Naru3,
    TE_Naru1,
    TE_Naru2,
    TE_Naru3,
    HE_Naru1,
    HE_Naru2,
    HE_Naru3,

}