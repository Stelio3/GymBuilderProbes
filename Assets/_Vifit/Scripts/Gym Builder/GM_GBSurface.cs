using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GM_GBSurface : GM_GBEditions
{
    MeshRenderer mr;
    protected override void Start()
    {
        base.Start();
        mr = GetComponent<MeshRenderer>();
        outline.OutlineWidth = 6f;
    }
    public override void SetColor()
    {
        mr.sharedMaterial = GM_UIManager.Instance.ButtonSelected.GetComponent<GM_ChangeMaterial>().material;
        GM_GameDataManager.UpdateData(gameObject).material = mr.sharedMaterial;
    }
}
