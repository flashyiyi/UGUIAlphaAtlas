using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

[Serializable]
public class AlphaAtlasManager : ScriptableObject, ISerializationCallbackReceiver
{
    public const string TEXTURE_ALPHA_ATLAS_PATH = "Resources/TextureAlphaAtlas/";

    [SerializeField]
    public List<string> names;

    private Dictionary<string, WeakReference> nameDict;

    static T LoadAsset<T>(string name) where T : UnityEngine.Object
    {
        string path = TEXTURE_ALPHA_ATLAS_PATH + name;
        if (path.StartsWith("Resources/"))
        {
            return Resources.Load<T>(path.Substring("Resources/".Length));
        }
        else
        {
#if UNITY_EDITOR
            if (name == "AlphaAtlasConfig")
                path = path + ".asset";
            else
                path = path + ".png";

            return UnityEditor.AssetDatabase.LoadAssetAtPath<T>("Assets/" + path);
#else
            Debug.LogError("加载不在Resources下的文件需要自行添加加载逻辑");
            return null;
#endif
        }
    }

    static AlphaAtlasManager m_Instance;
    public static AlphaAtlasManager GetInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = LoadAsset<AlphaAtlasManager>("AlphaAtlasConfig");
            if (m_Instance == null)
            {
                Debug.Log("AlphaAtlasConfig.asset No Find!");
                m_Instance = ScriptableObject.CreateInstance<AlphaAtlasManager>();
            }
            m_Instance.OnAfterDeserialize();
        }
        return m_Instance;
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        if (nameDict != null)
            return;

        nameDict = new Dictionary<string, WeakReference>();
        if (names != null)
        {
            foreach (string name in names)
            {
                nameDict.Add(name, new WeakReference(null));
            }
        }
    }

    public Texture2D GetAlphaTexture(string name)
    {
        if (!nameDict.ContainsKey(name))
            return null;
        
        WeakReference reference = nameDict[name];
        if (reference.Target == null)
            reference.Target = LoadAsset<Texture2D>(name + "_alpha");

        return reference.Target as Texture2D;
    }

    public Texture2D GetAlphaTexture(Sprite sprite)
    {
        return GetAlphaTexture(sprite.texture.name);
    }

    public void UnLoadAllTexture()
    {
        foreach (var pair in nameDict)
        {
            if (pair.Value.Target != null)
            {
                Resources.UnloadAsset(pair.Value.Target as Texture);
                pair.Value.Target = null;
            }
        }
    }
}