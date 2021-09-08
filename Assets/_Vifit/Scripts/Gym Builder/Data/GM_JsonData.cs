using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Linq;

public static class GM_JsonData 
{
    public static void SaveToJSON<T>(List<T> toSave)
    {
        string content = JsonHelper.ToJson(toSave.ToArray(), true);
        WriteFile(GetPath(), content);
    }

    public static void SaveToJSON<T>(T toSave)
    {
        string content = JsonUtility.ToJson(toSave, true);
        WriteFile(GetPath(), content);
    }

    public static List<GM_ObjectData> ReadFromJSON<T>()
    {
        string content = ReadFile(GetPath());

        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return new List<GM_ObjectData>();
        }

        List<GM_ObjectData> res = JsonHelper.FromJson<GM_ObjectData>(content).ToList();

        return res;
    }
    private static string GetPath()
    {
        string filename = "data.json";
        return Application.dataPath + "/_Vifit/Scripts/Gym Builder/Data/" + filename;
    }

    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        GM_Wrapper<T> wrapper = JsonUtility.FromJson<GM_Wrapper<T>>(json);
        return wrapper.GymBuilder;
    }

    public static string ToJson<T>(T[] array)
    {
        GM_Wrapper<T> wrapper = new GM_Wrapper<T>();
        wrapper.GymBuilder = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        GM_Wrapper<T> wrapper = new GM_Wrapper<T>();
        wrapper.GymBuilder = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
}
[Serializable]
public class GM_Wrapper<T>
{
    public T[] GymBuilder;
}
[Serializable]
public class GM_GameDataManager
{
    public static List<GM_ObjectData> gymBuilderObjects = new List<GM_ObjectData>();
    public static GM_ObjectData UpdateData(GameObject go)
    {
        return gymBuilderObjects.Find(o => o.id == go.GetComponent<GM_GBEditions>().id);
    }
}
[Serializable]
public class GM_ObjectData
{
    public int id;
    public int objectId;
    public Material material;
    public bool locked;
    public Vector3 position;
    public Quaternion rotation;
}
