using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GM_GBEditions : MonoBehaviour
{
    public int id { get; set; }
    protected Outline outline;
    [HideInInspector]
    public  bool locked = false;
    bool active = false;
    bool hovering = false;

    BNG.PointerEvents pointerEvents;

    void OnEnable()
    {
        MenuBuilder.OnMenuBuilder += ChangePointerEvents;
    }

    void OnDisable()
    {
        MenuBuilder.OnMenuBuilder -= ChangePointerEvents;
    }
    protected virtual void Awake()
    {
        pointerEvents = GetComponent<PointerEvents>();
        outline = GetComponent<Outline>() == null ? gameObject.AddComponent<Outline>() : GetComponent<Outline>();
    }

    protected virtual void Start()
    {
        outline.enabled = false;
        outline.OutlineColor = Color.black;
    }
    // Hovering over our object
    public virtual void SetHovering(PointerEventData eventData)
    {
        hovering = true;
        UpdateMaterial();
    }

    // No longer hovering over our object
    public virtual void ResetHovering(PointerEventData eventData)
    {
        hovering = false;
        active = false;

        UpdateMaterial();
    }
    // Holding down activate
    public virtual void SetActive(PointerEventData eventData)
    {
        active = true;

        UpdateMaterial();
    }

    // No longer holding down activate
    public virtual void SetInactive(PointerEventData eventData)
    {
        active = false;

        UpdateMaterial();
    }

    public abstract void moveObject(RaycastResult rayResult);
    public void UpdateMaterial()
    {
        if (GM_GBManager.Instance.GetSelected == gameObject)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }

    void ChangePointerEvents(bool status)
    {
        pointerEvents.enabled = status;
        GM_GBManager.Instance.UpdateSelected(null, Type.None);
    }
}
