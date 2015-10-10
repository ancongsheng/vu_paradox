using UnityEngine;
using System.Collections;

public class GameTexture : MonoBehaviour {


    [SerializeField]
    private NGUITexture m_Tex;



    private static Color defaultOverlay = new Color(128, 128, 128);

	public void show(string name)
    {
        show(name, 1f);
    }

    public void show(string name, float fadeTime)
    {
        show(name, fadeTime, defaultOverlay);
    }

    public void show(string name, float fadeTime, Color overlayColor)
    {
        StartCoroutine(crossfadeCoroutine(name, fadeTime, overlayColor));
    }

    private IEnumerator crossfadeCoroutine(string name, float fadeTime, Color overlayColor)
    {
        if (m_Tex.mainTexture != null)
        {
            TweenAlpha.Begin(m_Tex.gameObject, fadeTime, 0);
            yield return new WaitForSeconds(fadeTime);
        }

        m_Tex.alpha = 0;
        ResourceManager.LoadIcon(name, m_Tex);
        TweenAlpha.Begin(m_Tex.gameObject, fadeTime, 1);
        m_Tex.color = overlayColor;
        

    }

    public void hide(float fadeTime)
    {
        m_Tex.mainTexture = null;
    }
}
