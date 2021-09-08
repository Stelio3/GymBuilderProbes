using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GM_GBSurface : GM_GBEditions
{
    MeshRenderer mr;
    public Material[] materials;
    protected override void Start()
    {
        base.Start();
        mr = GetComponent<MeshRenderer>();
        outline.OutlineWidth = 6f;
    }
    public override void SetColor()
    {
        GM_ChangeMaterial materialData = GM_UIManager.Instance.ButtonSelected.GetComponent<GM_ChangeMaterial>();
        mr.material = materialData.material;
        GM_GameDataManager.UpdateData(gameObject).materialId = materialData.id;
    }
}
