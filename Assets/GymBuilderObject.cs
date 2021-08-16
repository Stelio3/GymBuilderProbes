using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BNG
{
    public class GymBuilderObject : MonoBehaviour
    {
        // Currently activating the object?
        bool active = false;

        public bool Selected { get; set; }

        // Currently hovering over the object?
        bool hovering = false;

        Rigidbody rb;
        BoxCollider bc;
        MeshRenderer mr;
        Outline outline;

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
            mr = GetComponentInChildren<MeshRenderer>();

            outline.enabled = false;
            outline.OutlineColor = Color.black;
            outline.OutlineWidth = 2f;

            rb.useGravity = false;
            rb.freezeRotation = true;
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
            if (Selected)
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

            if (GBObjectManager.Instance.getSelected == gameObject)
            {
                if (rayResult.gameObject.transform.gameObject.tag == gameObject.tag)
                {
                    gameObject.SetActive(true);
                    if (gameObject.tag == "Wall")
                    {
                        transform.rotation = Quaternion.FromToRotation(Vector3.forward, rayResult.worldNormal);
                        transform.localPosition = rayResult.worldPosition + (rayResult.worldNormal * ((GetComponent<BoxCollider>().size.z / 2) + GetComponent<BoxCollider>().center.z));
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
