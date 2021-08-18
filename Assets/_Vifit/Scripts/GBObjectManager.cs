using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GBObjectManager : Singleton<GBObjectManager>
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
        if (go.GetComponent<GymBuilderObject>())
        {
            Vector3 newObjectLocation = (PlayerStats.Instance.player.transform.forward * 2f) + PlayerStats.Instance.player.transform.position;
            getSelected = Instantiate(go, newObjectLocation, go.transform.rotation);
            getSelected.transform.LookAt(PlayerStats.Instance.player.transform);
        }
        
    }
    public void changeMaterial(Material material)
    {
        if (getSurface)
        {
            getSurface.GetComponent<GymBuilderSurface>().setColor(material);
        }
    }
    public void lockObject()
    {
        if (getSelected)
        {
            getSelected.GetComponent<GymBuilderObject>().locked = !getSelected.GetComponent<GymBuilderObject>().locked;
        }
    }

}
