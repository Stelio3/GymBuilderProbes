using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public GameObject go;
    public void ShowObject() {
        GBObjectManager.Instance.CreateNew(go);
    }
}
