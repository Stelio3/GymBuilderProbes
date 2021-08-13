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
            getSelected = Instantiate(go);

        }
        
    }
    public void changeMaterial(Material material)
    {
        if (getSurface.GetComponent<GymBuilderSurface>())
        {
            getSurface.GetComponent<GymBuilderSurface>().setColor(material);
        }
    }

}
