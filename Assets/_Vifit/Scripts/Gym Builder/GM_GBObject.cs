using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace BNG
{
    public class GM_GBObject : MonoBehaviour
    {
        public GM_GBScriptableObjects scriptableObject;
        // Currently activating the object?
        bool active = false;
        GM_ChangeMaterial changeMaterial;

        [HideInInspector]
        public bool locked = false;

        // Currently hovering over the object?
        bool hovering = false;

        Rigidbody rb;
        [HideInInspector]
        public BoxCollider bc;
        Outline outline;

        bool colisioned = false;
        Vector3 colisionNormal;
        Vector3 colisionDistance;

        private void Awake()
        {
            outline = GetComponent<Outline>();
            rb = GetComponent<Rigidbody>();
            bc = GetComponent<BoxCollider>();

            outline = outline == null ? gameObject.AddComponent<Outline>() : GetComponent<Outline>();
            rb = rb == null ? gameObject.AddComponent<Rigidbody>() : GetComponent<Rigidbody>();
            bc = bc == null ? gameObject.AddComponent<BoxCollider>() : GetComponent<BoxCollider>();
        }
        void Start()
        {
            outline.enabled = true;
            outline.OutlineColor = Color.black;
            outline.OutlineWidth = 2f;

            GM_GBManager.Instance.getSelected = gameObject;

            if (GM_GBManager.Instance.getSurface)
            {
                GM_GBManager.Instance.getSurface.GetComponent<GM_GBSurface>().ResetMaterial();
                GM_GBManager.Instance.getSurface = null;
                
            }


            rb.useGravity = false;
            rb.freezeRotation = true;
        }
        public void SetSelected(PointerEventData eventData)
        {
            if (GM_ChangeMaterial.BtnSelected)
            {
                changeMaterial = GM_ChangeMaterial.BtnSelected.GetComponent<GM_ChangeMaterial>();
                GM_ChangeMaterial.BtnSelected.GetComponent<Image>().color = changeMaterial.material.color;
                GM_ChangeMaterial.BtnSelected = null;
            }
            GM_GBManager.Instance.getSelected = GM_GBManager.Instance.getSelected != gameObject ? gameObject : null;
            UpdateMaterial();
        }
        // Hovering over our object
        public void SetHovering(PointerEventData eventData)
        {
            hovering = true;
            UpdateMaterial();
        }

        // No longer hovering over our object
        public void ResetHovering(PointerEventData eventData)
        {
            hovering = false;
            active = false;

            UpdateMaterial();
        }

        // Holding down activate
        public void SetActive(PointerEventData eventData)
        {
            active = true;

            UpdateMaterial();
        }

        // No longer holding down activate
        public void SetInactive(PointerEventData eventData)
        {
            active = false;

            UpdateMaterial();
        }

        public void UpdateMaterial()
        {
            if (GM_GBManager.Instance.getSelected == gameObject)
            {
                outline.enabled = true;
                outline.OutlineColor = Color.black;
            }
            else if (hovering || active)
            {
                outline.OutlineColor = Color.red;
            }
            else
            {
                outline.enabled = false;
            }
        }

        public void moveObject(PointerEventData eventData)
        {
            RaycastResult rayResult = eventData.pointerCurrentRaycast;
            if (GM_GBManager.Instance.getSelected == gameObject && !locked)
            {
                if (rayResult.gameObject.transform.gameObject.tag == scriptableObject.type.ToString())
                {
                    gameObject.SetActive(true);
                    if (rayResult.gameObject.transform.gameObject.tag == "Wall")
                    {
                        transform.localPosition = rayResult.worldPosition + (rayResult.worldNormal * ((GetComponent<BoxCollider>().size.z / 2) + GetComponent<BoxCollider>().center.z + 0.01f));
                        transform.rotation = Quaternion.FromToRotation(Vector3.forward, rayResult.worldNormal);
                        if(rayResult.worldNormal == -Vector3.forward)
                        {
                            transform.up = Vector3.up;
                            transform.forward = rayResult.worldNormal;
                        }
                    }
                    else if (rayResult.gameObject.transform.gameObject.tag == "Floor")
                    {
                        transform.localPosition = rayResult.worldPosition + (rayResult.worldNormal * ((GetComponent<BoxCollider>().size.y / 2) - GetComponent<BoxCollider>().center.y -0.0001f));
                    }else if(rayResult.gameObject.transform.gameObject.tag == "Roof")
                    {
                        transform.localPosition = rayResult.worldPosition + (rayResult.worldNormal * ((GetComponent<BoxCollider>().size.y / 2) + GetComponent<BoxCollider>().center.y + 0.0001f));
                    }
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
        private void OnTriggerStay(Collider other)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.right, out hit))
            {
                colisionNormal = hit.normal;
                colisionDistance = hit.point;
            }
            colisioned = true;
        }
        private void OnTriggerExit(Collider other)
        {
            colisioned = false;
        }
    }
}
