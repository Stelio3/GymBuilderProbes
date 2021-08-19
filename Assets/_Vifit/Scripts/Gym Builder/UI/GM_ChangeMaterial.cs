using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BNG;

public class GM_ChangeMaterial : MonoBehaviour
{
    public Material material;
    public static GameObject BtnSelected { get; set; }

    GM_GBObject builderObject;

    public void changeMaterial() {
        if (GM_GBManager.Instance.getSelected)
        {
            builderObject = GM_GBManager.Instance.getSelected.GetComponent<GM_GBObject>();
            GM_GBManager.Instance.getSelected = null;
            builderObject.UpdateMaterial();
        }
        if (BtnSelected)
        {
            if (BtnSelected != gameObject)
            {
                BtnSelected.GetComponent<Image>().color = BtnSelected.GetComponent<GM_ChangeMaterial>().material.color;
                BtnSelected = gameObject;
                BtnSelected.GetComponent<Image>().color = new Color(material.color.r, material.color.g, material.color.b, 0.5f);
            }
            else
            {
                BtnSelected.GetComponent<Image>().color = material.color;
                BtnSelected = null;
            }
        }
        else
        {
            BtnSelected = gameObject;
            BtnSelected.GetComponent<Image>().color = new Color(material.color.r, material.color.g, material.color.b, 0.5f);
        }
    }
    private void Reset()
    {
        
    }


}
