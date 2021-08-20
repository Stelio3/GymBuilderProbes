﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BNG;

public class GM_ChangeMaterial : MonoBehaviour
{
    public Material material;
    public void changeMaterial() {
        if (GM_GBManager.Instance.type == Type.Color)
        {
            if (GM_GBManager.Instance.getSelected != gameObject || !GM_GBManager.Instance.getSelected)
            {
                GM_GBManager.Instance.getSelected.GetComponent<Image>().color = GM_GBManager.Instance.getSelected.GetComponent<GM_ChangeMaterial>().material.color;
                GM_GBManager.Instance.UpdateSelected(gameObject, Type.Color);
                GM_GBManager.Instance.getSelected.GetComponent<Image>().color = new Color(material.color.r, material.color.g, material.color.b, 0.5f);
            }
            else
            {
                GM_GBManager.Instance.getSelected.GetComponent<Image>().color = material.color;
                GM_GBManager.Instance.UpdateSelected(null, Type.None);
            }
        }
        else
        {
            GM_GBManager.Instance.UpdateSelected(gameObject, Type.Color);
            GM_GBManager.Instance.getSelected.GetComponent<Image>().color = new Color(material.color.r, material.color.g, material.color.b, 0.5f);
        }
    }
}
