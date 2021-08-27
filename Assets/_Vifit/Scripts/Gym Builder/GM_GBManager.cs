using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum Type { None, Object, Surface, Color };
public class GM_GBManager : Singleton<GM_GBManager>
{
    public GameObject GetSelected { get; set; }
    public Type TypeSelected { get; set; }
    int lastId;
    GameObject lastObject;
    private void Awake()
    {
        GetSelected = null;
        lastId = GM_JsonData.ReadFromJSON<GM_ObjectData>().Count;
    }
    private void Start()
    {
        foreach (GM_ObjectData o in GM_JsonData.ReadFromJSON<GM_ObjectData>())
        {
            GameObject savedObject = Instantiate(SerializableObjects.Get(o.id).Object);
            savedObject.transform.position = o.position;
            savedObject.transform.rotation = o.rotation;
            GM_GameDataManager.gymBuilderObjects.Add(o);
        }
    }
    public void SpawnObject(GM_GBScriptableObjects go)
    {
        if (go.Object)
        {
            bool inJson = false;
            foreach(GM_ObjectData o in GM_GameDataManager.gymBuilderObjects)
            {
                if (GetSelected)
                {
                    if (o.id == go.id)
                    {
                        GetSelected = Instantiate(go.Object);
                        inJson = true;
                    }
                }
            }
            if (!inJson)
            {
                GetSelected = Instantiate(go.Object);
                GM_ObjectData toAddJson = new GM_ObjectData();
                toAddJson.objectId = go.id;
                lastId++;
                toAddJson.id = lastId;
                GM_GameDataManager.gymBuilderObjects.Add(toAddJson);
            }
        }

    }
    public void UpdateSelected(GameObject go, Type type)
    {
        if (TypeSelected == Type.Object || TypeSelected == Type.Surface)
        {
            lastObject = GetSelected;
            GetSelected = go;
            TypeSelected = type;
            lastObject.GetComponent<GM_GBEditions>().UpdateMaterial();
        }
        GetSelected = go;
        TypeSelected = type;
        if (TypeSelected == Type.Object || TypeSelected == Type.Surface)
        {
            GetSelected.GetComponent<GM_GBEditions>().UpdateMaterial();
        }
    }

}
