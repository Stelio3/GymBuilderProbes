using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum Type { None, Object, Surface, Color };
public class GM_GBManager : Singleton<GM_GBManager>
{
    public GameObject getSelected { get; set; }
    public Type type { get; set; }

    GameObject lastObject;
    private void Awake()
    {
        getSelected = null;
    }
    private void Start()
    {
        Debug.Log("hay objetos: " + GM_JsonData.ReadFromJSON<GM_ObjectData>().Count);
        foreach (GM_ObjectData o in GM_JsonData.ReadFromJSON<GM_ObjectData>())
        {
            Debug.Log("Objeto: " + o.prefab.Object);
            Debug.Log("Objeto: " + o.position);
            Debug.Log("Objeto: " + o.rotation);
            GameObject savedObject;
            savedObject = Instantiate(o.prefab.Object);
            savedObject.transform.position = o.position;
            savedObject.transform.rotation = o.rotation;
        }
    }
    public void SpawnObject(GM_GBScriptableObjects go)
    {
        if (go.Object)
        {
            bool inJson = false;
            foreach(GM_ObjectData o in GM_GameDataManager.gymBuilderObjects)
            {
                if (getSelected)
                {
                    if (o.id == go.id)
                    {
                        getSelected = Instantiate(go.Object);
                        inJson = true;
                    }
                }
            }
            if (!inJson)
            {
                getSelected = Instantiate(go.Object);
                GM_ObjectData toAddJson = new GM_ObjectData();
                toAddJson.id = go.id;
                toAddJson.prefab = go;
                GM_GameDataManager.gymBuilderObjects.Add(toAddJson);
            }
        }

    }
    public void UpdateSelected(GameObject go, Type type)
    {
        if (this.type == Type.Object || this.type == Type.Surface)
        {
            lastObject = getSelected;
            getSelected = go;
            this.type = type;
            lastObject.GetComponent<GM_GBEditions>().UpdateMaterial();
        }
        getSelected = go;
        this.type = type;
        if (this.type == Type.Object || this.type == Type.Surface)
        {
            getSelected.GetComponent<GM_GBEditions>().UpdateMaterial();
        }
    }

}
