using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonData : MonoBehaviour
{
    string filename = "data.json";
    string path;

    GameDataManager gameData = new GameDataManager();
    void Start()
    {
        path = Application.dataPath + "/" + filename;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameData.date = System.DateTime.Now.ToShortDateString();
            gameData.time = System.DateTime.Now.ToShortTimeString();

            GBObjectData q1 = new GBObjectData();
            q1.name = "q1";
            q1.position = gameObject.transform.position;

            GBObjectData q2 = new GBObjectData();
            q2.name = "q2";
            q2.position = gameObject.transform.position;

            GBObjectData q3 = new GBObjectData();
            q3.name = "q3";
            q3.position = gameObject.transform.position;

            gameData.gymBuilderObjects.Add(q1);
            gameData.gymBuilderObjects.Add(q2);
            gameData.gymBuilderObjects.Add(q3);

            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReadData();
        }
    }
    void SaveData()
    {
        JsonWrapper wrapper = new JsonWrapper();
        wrapper.GymBuilder = gameData;
        wrapper.gameData = gameData;

        string contents = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(path, contents);
    }
    void ReadData()
    {
        try
        {
            if (File.Exists(path))
            {
                string contents = File.ReadAllText(path);
                JsonWrapper wrapper = JsonUtility.FromJson<JsonWrapper>(contents);
                gameData = wrapper.GymBuilder;

                foreach (GBObjectData q in gameData.gymBuilderObjects)
                {
                    Debug.Log(q.name + q.position);
                }
            }
            else
            {
                Debug.Log("File does not exist");
                gameData = new GameDataManager();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }


    }
}
