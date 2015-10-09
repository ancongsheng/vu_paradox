using UnityEngine;
using System.Collections;

public class SelectItem : MonoBehaviour {

    public int id = 0;

    [SerializeField]
    private NGUILabel m_ShowText;

    public void Set(int _id)
    {
        SelectionData data = MainGame.instance.selection[_id];
        m_ShowText.text = data.content;
        id = _id;

        if (data.needFlag > 0 && !MainGame.instance.currentFlag.Get(data.needFlag))
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

}
