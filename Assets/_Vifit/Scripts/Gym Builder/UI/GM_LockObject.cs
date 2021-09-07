using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_LockObject : GM_Options
{
    protected Color normalColor;
    private void Start()
    {
        normalColor = GetComponent<Button>().colors.normalColor;
    }
    public override void Selected()
    {
        SetSelected(OptionType.Lock);
    }
    public void ObjectLocked(bool status)
    {
        ColorBlock cb = GetComponent<Button>().colors;
        cb.selectedColor = status ? GetComponent<Button>().colors.pressedColor : normalColor;
        GetComponent<Button>().colors = cb;
    }
}
