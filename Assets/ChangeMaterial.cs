using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material material;
    public void change() {
        GBObjectManager.Instance.changeMaterial(material);
    }
}
