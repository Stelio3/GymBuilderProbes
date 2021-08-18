using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMaterial : MonoBehaviour
{
    public Material material;

    public void changeMaterial() {
        GBObjectManager.Instance.changeMaterial(material);
    }
}
