using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GM_ItemShop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public GM_GBScriptableObjects scriptableObject;
    public static GameObject infoPanel;
    bool canPointerDown;

    private void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = scriptableObject.objectImage;
        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = scriptableObject.price.ToString();
        canPointerDown = true;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canPointerDown)
        {
            GM_GBManager.Instance.SpawnObject(scriptableObject);
            canPointerDown = !canPointerDown;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canPointerDown = !canPointerDown;
    }
}
