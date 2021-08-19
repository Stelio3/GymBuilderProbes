using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GM_GBManager : Singleton<GM_GBManager>
{
    public GameObject getSelected { get; set; }
    public GameObject getSurface { get; set; }
    

    private void Awake()
    {
        getSelected = null;
        getSurface = null;
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
        if (getSelected)
        {
            getSelected.GetComponent<GM_GBObject>().locked = !getSelected.GetComponent<GM_GBObject>().locked;
        }
    }

}
