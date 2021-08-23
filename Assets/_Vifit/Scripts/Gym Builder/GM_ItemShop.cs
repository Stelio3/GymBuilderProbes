using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_ItemShop : MonoBehaviour
{
    public GM_GBScriptableObjects scriptableObject;
    public GameObject canvas;

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = scriptableObject.objectImage;
    }
    public void ShowObject() {
        canvas.GetComponent<GraphicRaycaster>().enabled = false;
        GM_GBManager.Instance.SpawnObject(scriptableObject);
    }
}
