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
            Vector3 newObjectLocation = (PlayerStats.Instance.player.transform.forward * 2f) + PlayerStats.Instance.player.transform.position;
            getSelected = Instantiate(go, newObjectLocation, go.transform.rotation);
            getSelected.transform.LookAt(PlayerStats.Instance.player.transform);
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
