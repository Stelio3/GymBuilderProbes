using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum OptionType { None, Delete, Lock, Color };
public class GM_UIManager : Singleton<GM_UIManager>
{
    public GameObject ButtonSelected { get; set; }
    public OptionType OptionSelected { get; set; }
    GameObject lastObject;

    private void Awake()
    {
        ButtonSelected = null;
    }

    public void UpdateSelected(GameObject go, OptionType type)
    {
        if (OptionSelected != OptionType.None)
        {
            lastObject = ButtonSelected;
            ButtonSelected = go;
            OptionSelected = type;
            lastObject.GetComponent<GM_Options>().UpdateMaterial();
        }
        ButtonSelected = go;
        OptionSelected = type;
        if (OptionSelected != OptionType.None)
        {
            ButtonSelected.GetComponent<GM_Options>().UpdateMaterial();
        }
    }
}
