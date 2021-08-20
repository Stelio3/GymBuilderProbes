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
    GameObject lastObject;
    public Type type;

    public delegate void OnMenuBuilderAction(bool status);
    public static event OnMenuBuilderAction OnMenuBuilder;

    private void Awake()
    {
        getSelected = null;
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
        else
        {
            getSelected = go;
            this.type = type;
            if (this.type == Type.Object || this.type == Type.Surface)
            {
                getSelected.GetComponent<GM_GBEditions>().UpdateMaterial();
            }
        }
    }
    public void CreateNew(GameObject go)
    {
        if (go.GetComponent<GM_GBObject>())
        {
            getSelected = Instantiate(go);
        }
        
    }
    public void lockObject()
    {
        GM_GBEditions.LockObject();
    }

}
