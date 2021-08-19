using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    public GM_GBScriptableObjects scriptableObject;

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = scriptableObject.objectImage;
    }
    public void ShowObject() {
        GM_GBManager.Instance.CreateNew(scriptableObject.Object);
    }
}
