using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BNG
{
    public class GymBuilderObject : MonoBehaviour
    {
        public Material HighlightMaterial;
        Material initialMaterial;

        // Currently activating the object?
        bool active = false;

        public bool Selected { get; set; }

        // Currently hovering over the object?
        bool hovering = false;

        Rigidbody rb;
        BoxCollider bc;
        MeshRenderer mr;
        public Material matSelected;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            bc = GetComponent<BoxCollider>();

            rb = rb == null ? gameObject.AddComponent<Rigidbody>() : GetComponent<Rigidbody>();
            bc = bc == null ? gameObject.AddComponent<BoxCollider>() : GetComponent<BoxCollider>();

            mr = GetComponentInChildren<MeshRenderer>();

            rb.useGravity = false;
            rb.freezeRotation = true;

            initialMaterial = mr.sharedMaterial;
        }

        public void SetSelected(PointerEventData eventData)
        {
            if (GBObjectManager.Instance.getSelected == gameObject)
            {
                GBObjectManager.Instance.getSelected = null;
                Selected = false;
            }
            else
            {
                GBObjectManager.Instance.getSelected = gameObject;
                Selected = true;
            }
            UpdateMaterial();
        }

        // Holding down activate
        public void SetActive(PointerEventData eventData)
        {
            active = true;

            UpdateMaterial();
        }

        // No longer ohlding down activate
        public void SetInactive(PointerEventData eventData)
        {
            active = false;

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

        public void UpdateMaterial()
        {
            if (active || Selected)
            {
                mr.sharedMaterial = matSelected;
            }
            else if (hovering)
            {
                mr.sharedMaterial = HighlightMaterial;
            }
            else
            {
                mr.sharedMaterial = initialMaterial;
            }
        }

        public void moveObject(PointerEventData eventData)
        {
            RaycastResult rayResult = eventData.pointerCurrentRaycast;

            if (Selected)
            {
                if (rayResult.gameObject.transform.gameObject.tag == gameObject.tag)
                {
                    gameObject.SetActive(true);
                    if (gameObject.tag == "Wall")
                    {
                        transform.rotation = Quaternion.FromToRotation(-Vector3.right, rayResult.worldNormal);
                        transform.localPosition = rayResult.worldPosition + (rayResult.worldNormal * ((GetComponent<BoxCollider>().size.x / 2) + GetComponent<BoxCollider>().center.x));
                    }else if (gameObject.tag == "Floor")
                    {
                        transform.localPosition = rayResult.worldPosition + (rayResult.worldNormal * ((GetComponent<BoxCollider>().size.y / 2) - GetComponent<BoxCollider>().center.y));
                    }else if(gameObject.tag == "Roof")
                    {
                        transform.localPosition = rayResult.worldPosition + (rayResult.worldNormal * ((GetComponent<BoxCollider>().size.y / 2) + GetComponent<BoxCollider>().center.y));
                    }
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
