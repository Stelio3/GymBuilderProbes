using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class GM_Options : MonoBehaviour
{
    protected Color selectedColor;

    private void Awake()
    {
        selectedColor = GetComponent<Button>().colors.selectedColor;
    }
    public void SetSelected(OptionType type)
    {
        if (GM_UIManager.Instance.ButtonSelected == gameObject)
        {
            GM_UIManager.Instance.UpdateSelected(null, OptionType.None);
        }
        else
        {
            GM_UIManager.Instance.UpdateSelected(gameObject, type);
        }
    }
    public abstract void Selected();
    public void UpdateMaterial()
    {
        ColorBlock cb = GetComponent<Button>().colors;
        if (GM_UIManager.Instance.ButtonSelected == gameObject)
        {
            cb.selectedColor = selectedColor;
        }
        else
        {
            cb.selectedColor = GetComponent<Button>().colors.normalColor;
        }
        GetComponent<Button>().colors = cb;
    }
}
