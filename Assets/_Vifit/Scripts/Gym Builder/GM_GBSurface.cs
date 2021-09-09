using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GM_GBSurface : GM_GBEditions
{
    MeshRenderer mr;
    public Material[] materials;
    protected override void Awake()
    {
        base.Awake();
        if (id <= 0)
        {
            id = TemplateId.id;
            TemplateId.id--;
        }
    }
    protected override void Start()
    {
        base.Start();
        mr = GetComponent<MeshRenderer>();
        outline.OutlineWidth = 6f;
        mr.material = materials[PlayerPrefs.GetInt(id.ToString())];
    }
    public override void SetColor()
    {
        GM_ChangeMaterial materialData = GM_UIManager.Instance.ButtonSelected.GetComponent<GM_ChangeMaterial>();
        mr.material = materialData.material;
        PlayerPrefs.SetInt(id.ToString(), materialData.id);

    }
}
public static class TemplateId
{
    public static int id;
}
