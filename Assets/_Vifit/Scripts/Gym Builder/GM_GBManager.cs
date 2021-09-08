﻿using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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
            if(o.position != Vector3.zero)
            {
                GameObject savedObject = Instantiate(SerializableObjects.Get(o.objectId).Object);
                savedObject.GetComponent<GM_GBEditions>().id = o.id;
                savedObject.transform.position = o.position;
                savedObject.transform.rotation = o.rotation;
            }
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
                    if (o.objectId == go.id)
                    {
                        GetSelected = Instantiate(go.Object);
                        GetSelected.GetComponent<GM_GBEditions>().id = o.id;
                        inJson = true;
                    }
                }
            }
            if (!inJson && go.price < currencyManager.GetCoins())
            {
                GetSelected = Instantiate(go.Object);
                GM_ObjectData toAddJson = new GM_ObjectData();
                toAddJson.objectId = go.id;
                lastId++;
                toAddJson.id = lastId;
                GetSelected.GetComponent<GM_GBEditions>().id = lastId;
                currencyManager.RemoveCoins(go.price);
                GM_GameDataManager.gymBuilderObjects.Add(toAddJson);
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
