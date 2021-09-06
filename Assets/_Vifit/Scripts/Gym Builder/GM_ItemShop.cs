using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GM_ItemShop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GM_GBScriptableObjects scriptableObject;
    public GameObject canvas, infoPanel;

    private void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = scriptableObject.objectImage;
        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = scriptableObject.price.ToString();
    }
    public void ShowObject() {
        canvas.GetComponent<GraphicRaycaster>().enabled = false;
        GM_GBManager.Instance.SpawnObject(scriptableObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoPanel.SetActive(true);
        infoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = scriptableObject.name;
        infoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = scriptableObject.description;
        infoPanel.transform.GetChild(2).GetComponent<Image>().sprite = scriptableObject.objectImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel.SetActive(false);
        infoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        infoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        infoPanel.transform.GetChild(2).GetComponent<Image>().sprite = null;
    }
}
