using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public enum Type { None, Object, Surface };
public class GM_GBManager : Singleton<GM_GBManager>
{
    public GameObject GetSelected { get; set; }
    public CurrencyManager currencyManager;
    public Type TypeSelected { get; set; }
    int lastId;
    GameObject lastObject;
    private void Awake()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        GetSelected = null;
        lastId = GM_JsonData.ReadFromJSON<GM_ObjectData>().Count;
    }
    private void Start()
    {
        foreach (GM_ObjectData o in GM_JsonData.ReadFromJSON<GM_ObjectData>())
        {
            if (o.position != Vector3.zero)
            {
                GameObject savedObject = Instantiate(SerializableObjects.Get(o.objectId).Object);
                savedObject.GetComponent<GM_GBEditions>().id = o.id;
                savedObject.GetComponent<GM_GBEditions>().locked = o.locked;
                savedObject.transform.position = o.position;
                savedObject.transform.rotation = o.rotation;
            }
            GM_GameDataManager.gymBuilderObjects.Add(o);
        }
    }
    public void SpawnObject(GM_GBScriptableObjects go, bool inInventary)
    {
        if (go.Object)
        {
            if (inInventary)
            {
                bool droped = false;
                foreach (GM_ObjectData o in GM_GameDataManager.gymBuilderObjects)
                {
                    if (o.objectId == go.id && !droped)
                    {
                        GetSelected = Instantiate(go.Object);
                        GetSelected.GetComponent<GM_GBEditions>().id = o.id;
                        droped = true;
                        GM_UIManager.Instance.canvas.GetComponent<GraphicRaycaster>().enabled = false;
                    }
                }
            }
            else if(go.price < currencyManager.GetCoins())
            {
                GM_ObjectData toAddJson = new GM_ObjectData();
                toAddJson.objectId = go.id;
                lastId++;
                toAddJson.id = lastId;
                GetSelected = Instantiate(go.Object);
                GetSelected.GetComponent<GM_GBEditions>().id = lastId;
                currencyManager.RemoveCoins(go.price);
                GM_GameDataManager.gymBuilderObjects.Add(toAddJson);
                GM_UIManager.Instance.canvas.GetComponent<GraphicRaycaster>().enabled = false;
            }
            else
            {
                InputBridge.Instance.VibrateController(0.1f, 0.3f, 0.1f, ControllerHand.Left);
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
