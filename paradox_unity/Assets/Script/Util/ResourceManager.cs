using UnityEngine;
using System.Collections;

public class ResourceManager {





    public static string[] LoadText(string fileName)
    {
        string[] itemRowsList = null;

        TextAsset item = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
        if (item != null)
        {
            itemRowsList = item.text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

            Resources.UnloadAsset(item);
        }
        else
        {
            Debug.LogError("Failed to load file: " + fileName);
        }

        return itemRowsList;
    }

    public static void LoadIcon(string bundleName, NGUITexture _texture)
    {
        if (_texture != null)
            _texture.mainTexture = Resources.Load(bundleName) as Texture;
    }

}
