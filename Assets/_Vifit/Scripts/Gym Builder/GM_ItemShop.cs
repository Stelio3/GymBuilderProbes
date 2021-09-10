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
    int inInventary;

    private void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = scriptableObject.objectImage;
        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = scriptableObject.price.ToString();
        inInventary = InInventary();
        if (inInventary > 0)
        {
            gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "(" + inInventary + ")";
        }
        else
        {
            gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "";
        }
    }
    public int InInventary()
    {
        int count = 0;
        foreach (GM_ObjectData o in GM_GameDataManager.gymBuilderObjects)
        {
            if (o.position == Vector3.zero && o.rotation == Quaternion.identity && o.objectId == scriptableObject.id)
            {
                count++;
            }
        }
        return count;
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
        if (GM_UIManager.Instance.canPointerDown)
        {
            if(inInventary > 0)
            {
                GM_GBManager.Instance.SpawnObject(scriptableObject, true);
            }
            else
            {
                GM_GBManager.Instance.SpawnObject(scriptableObject, false);
            }
            GM_UIManager.Instance.canPointerDown = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GM_UIManager.Instance.canPointerDown = true;
    }
}
