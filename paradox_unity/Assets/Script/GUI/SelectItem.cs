using UnityEngine;
using System.Collections;

public class SelectItem : MonoBehaviour {

    public int id = 0;

    [SerializeField]
    private NGUILabel m_ShowText;

    public void Set(string _cont, int _id)
    {
        m_ShowText.text = _cont;
        id = _id;

        gameObject.SetActive(true);
    }

}
