using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GM_GBEditions : MonoBehaviour
{
    protected Rigidbody rb;
    protected BoxCollider bc;
    public GM_GBScriptableObjects scriptableObject;
    public int id { get; set; }
    protected Outline outline;
    [HideInInspector]
    public  bool locked = false;
    bool active = false;
    bool hovering = false;

    PointerEvents pointerEvents;

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
        rb = GetComponent<Rigidbody>() == null ? gameObject.AddComponent<Rigidbody>() : GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>() == null ? gameObject.AddComponent<BoxCollider>() : GetComponent<BoxCollider>();

        outline = GetComponent<Outline>() == null ? gameObject.AddComponent<Outline>() : GetComponent<Outline>();
        pointerEvents = GetComponent<PointerEvents>() == null ? gameObject.AddComponent<PointerEvents>() : GetComponent<PointerEvents>();

        UpdatePointerEvents();
    }
    private void UpdatePointerEvents()
    {
        pointerEvents.OnPointerClickEvent = new PointerEventDataEvent();
        pointerEvents.OnPointerEnterEvent = new PointerEventDataEvent();
        pointerEvents.OnPointerExitEvent = new PointerEventDataEvent();
        pointerEvents.OnPointerDownEvent = new PointerEventDataEvent();
        pointerEvents.OnPointerUpEvent = new PointerEventDataEvent();

        pointerEvents.OnPointerClickEvent.AddListener(delegate { SetSelected(VRUISystem.Instance.EventData); });
        pointerEvents.OnPointerEnterEvent.AddListener(delegate { SetHovering(VRUISystem.Instance.EventData); });
        pointerEvents.OnPointerExitEvent.AddListener(delegate { ResetHovering(VRUISystem.Instance.EventData); });
        pointerEvents.OnPointerDownEvent.AddListener(delegate { SetActive(VRUISystem.Instance.EventData); });
        pointerEvents.OnPointerUpEvent.AddListener(delegate { SetInactive(VRUISystem.Instance.EventData); });
    }

    protected virtual void Start()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.freezeRotation = true;

        outline.enabled = false;
        outline.OutlineColor = Color.black;
    }

    public virtual void SetSelected(PointerEventData eventData)
    {
        switch (GM_UIManager.Instance.OptionSelected)
        {
            case OptionType.Delete:
                InputBridge.Instance.VibrateController(0.1f, 0.3f, 0.1f, ControllerHand.Left);
                GM_GameDataManager.UpdateData(gameObject).position = Vector3.zero;
                GM_GameDataManager.UpdateData(gameObject).rotation = Quaternion.identity;

                Destroy(gameObject);
                GM_GBManager.Instance.UpdateSelected(null, Type.None);
                break;

            case OptionType.Lock:
                InputBridge.Instance.VibrateController(0.1f, 0.3f, 0.1f, ControllerHand.Left);
                locked = !locked;
                GM_GameDataManager.UpdateData(gameObject).locked = locked;
                break;

            case OptionType.Color:
                SetColor();
                break;

            default:

                break;
        }
    }
    // Hovering over our object
    public virtual void SetHovering(PointerEventData eventData)
    {
        if (GM_UIManager.Instance.OptionSelected == OptionType.None)
        {
            FindObjectOfType<GM_LockObject>().ObjectLocked(locked);
        }
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
        if (GM_UIManager.Instance.OptionSelected == OptionType.None)
        {
            if(GM_GBManager.Instance.GetSelected == null)
            {
                GM_GBManager.Instance.GetSelected = gameObject;
            }
            else if(GM_GBManager.Instance.GetSelected != gameObject && GM_GBManager.Instance.GetSelected.GetComponent<GM_SurfaceTemplate>() == null)
            {
                GM_GBManager.Instance.GetSelected.layer = LayerMask.NameToLayer("Ignore Raycast");
                moveObject(eventData.pointerCurrentRaycast);
                GM_GameDataManager.UpdateData(GM_GBManager.Instance.GetSelected).position = GM_GBManager.Instance.GetSelected.transform.localPosition;
                GM_GameDataManager.UpdateData(GM_GBManager.Instance.GetSelected).rotation = GM_GBManager.Instance.GetSelected.transform.rotation;
            }
        }
        UpdateMaterial();
    }

    // No longer holding down activate
    public virtual void SetInactive(PointerEventData eventData)
    {
        active = false;
        if (GM_GBManager.Instance.GetSelected)
        {
            GM_GBManager.Instance.GetSelected.layer = LayerMask.NameToLayer("Default");
            GM_GBManager.Instance.GetSelected = null;
        }
        UpdateMaterial();
    }

    public virtual void SetColor() { }
    public virtual void moveObject(RaycastResult rayResult)
    {
        if (rayResult.gameObject.transform.gameObject.CompareTag("Wall"))
        {
            GM_GBManager.Instance.GetSelected.transform.localPosition = rayResult.worldPosition + (rayResult.worldNormal * (((GM_GBManager.Instance.GetSelected.GetComponent<BoxCollider>().size.z / 2) + GM_GBManager.Instance.GetSelected.GetComponent<BoxCollider>().center.z + 0.01f) * GM_GBManager.Instance.GetSelected.transform.localScale.z));
            GM_GBManager.Instance.GetSelected.transform.rotation = Quaternion.FromToRotation(Vector3.forward, rayResult.worldNormal);
            if (rayResult.worldNormal == -Vector3.forward)
            {
                GM_GBManager.Instance.GetSelected.transform.up = Vector3.up;
                GM_GBManager.Instance.GetSelected.transform.forward = rayResult.worldNormal;
            }
        }
        else if (rayResult.gameObject.transform.gameObject.CompareTag("Floor"))
        {
            GM_GBManager.Instance.GetSelected.transform.localPosition = rayResult.worldPosition + (rayResult.worldNormal * (((GM_GBManager.Instance.GetSelected.GetComponent<BoxCollider>().size.y / 2) - GM_GBManager.Instance.GetSelected.GetComponent<BoxCollider>().center.y - 0.0001f) * GM_GBManager.Instance.GetSelected.transform.localScale.y));
        }
        else if (rayResult.gameObject.transform.gameObject.CompareTag("Roof"))
        {
            GM_GBManager.Instance.GetSelected.transform.localPosition = rayResult.worldPosition + (rayResult.worldNormal * (((GM_GBManager.Instance.GetSelected.GetComponent<BoxCollider>().size.y / 2) + GM_GBManager.Instance.GetSelected.GetComponent<BoxCollider>().center.y + 0.0001f) * GM_GBManager.Instance.GetSelected.transform.localScale.y));
        }
    }
    public void UpdateMaterial()
    {
        if ((hovering || active) && GM_GBManager.Instance.GetSelected == null)
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
