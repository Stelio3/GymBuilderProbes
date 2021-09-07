using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BNG;

public class GM_ChangeMaterial : GM_Options
{
    public Material material;
    public override void Selected()
    {
        SetSelected(OptionType.Color);
    }
}
