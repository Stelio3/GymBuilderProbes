using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    public GameObject go;
    public void ShowObject() {
        Instantiate(go);
    }
}
