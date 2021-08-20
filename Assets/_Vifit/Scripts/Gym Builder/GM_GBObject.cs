using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace BNG
{
    public class GM_GBObject : GM_GBEditions
    {
        public GM_GBScriptableObjects scriptableObject;

        Rigidbody rb;
        [HideInInspector]
        public BoxCollider bc;

        public bool firstHovering = true;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody>();
            bc = GetComponent<BoxCollider>();

            rb = rb == null ? gameObject.AddComponent<Rigidbody>() : GetComponent<Rigidbody>();
            bc = bc == null ? gameObject.AddComponent<BoxCollider>() : GetComponent<BoxCollider>();
        }
        protected override void Start()
        {
            base.Start();
            rb.useGravity = false;
            rb.freezeRotation = true;

            outline.OutlineWidth = 2f;

            GM_GBManager.Instance.UpdateSelected(gameObject, Type.Object);
        }
        public void SetSelected(PointerEventData eventData)
        {
            if (GM_GBManager.Instance.type == Type.Color)
            {
                GM_GBManager.Instance.getSelected.GetComponent<Image>().color = GM_GBManager.Instance.getSelected.GetComponent<GM_ChangeMaterial>().material.color;
            }
            if (GM_GBManager.Instance.getSelected)
            {
                if (GM_GBManager.Instance.getSelected != gameObject)
                {
                    GM_GBManager.Instance.UpdateSelected(gameObject, Type.Object);
                }
                else
                {
                    GM_GBManager.Instance.UpdateSelected(null, Type.None);
                }
            }
            else
            {
                GM_GBManager.Instance.UpdateSelected(gameObject, Type.Object);
            }
        }

        public override void moveObject(RaycastResult rayResult)
        {
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
        /*private void OnTriggerStay(Collider other)
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
        }*/
    }
}
