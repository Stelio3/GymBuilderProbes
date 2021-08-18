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

        [HideInInspector]
        public bool locked = false;

        // Currently hovering over the object?
        bool hovering = false;

        Rigidbody rb;
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

            GBObjectManager.Instance.getSelected = gameObject;

            if (GBObjectManager.Instance.getSurface)
            {
                GBObjectManager.Instance.getSurface.GetComponent<GymBuilderSurface>().ResetMaterial();
                GBObjectManager.Instance.getSurface = null;
                
            }


            rb.useGravity = false;
            rb.freezeRotation = true;
        }
        public void SetSelected(PointerEventData eventData)
        {
            GBObjectManager.Instance.getSelected = GBObjectManager.Instance.getSelected != gameObject ? gameObject : null;
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
            if (GBObjectManager.Instance.getSelected == gameObject)
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
            Debug.Log(locked);
            if (GBObjectManager.Instance.getSelected == gameObject && !locked)
            {
                if (rayResult.gameObject.transform.gameObject.tag == gameObject.tag)
                {
                    gameObject.SetActive(true);
                    if (gameObject.tag == "Wall")
                    {
                        transform.localPosition = rayResult.worldPosition + (rayResult.worldNormal * ((GetComponent<BoxCollider>().size.z / 2) + GetComponent<BoxCollider>().center.z + 0.01f));
                        transform.rotation = Quaternion.FromToRotation(Vector3.forward, rayResult.worldNormal);
                        if(rayResult.worldNormal == -Vector3.forward)
                        {
                            transform.up = Vector3.up;
                            transform.forward = rayResult.worldNormal;
                        }
                    }
                    else if (gameObject.tag == "Floor")
                    {
                        transform.localPosition = rayResult.worldPosition + (rayResult.worldNormal * ((GetComponent<BoxCollider>().size.y / 2) - GetComponent<BoxCollider>().center.y -0.0001f));
                    }else if(gameObject.tag == "Roof")
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
                Debug.Log(hit.normal);
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
