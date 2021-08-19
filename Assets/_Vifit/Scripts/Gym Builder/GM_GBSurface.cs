using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BNG
{
    public class GM_GBSurface : MonoBehaviour
    {
        MeshRenderer mr;
        private GM_GBObject builderObject;
        Outline outline;
        public GameObject canvas;
        private void Awake()
        {
            outline = GetComponent<Outline>();

            outline = outline == null ? gameObject.AddComponent<Outline>() : GetComponent<Outline>();
        }

        void Start()
        {
            mr = GetComponent<MeshRenderer>();

            outline.enabled = false;
            outline.OutlineColor = Color.black;
            outline.OutlineWidth = 6f;
        }
        public void SetSelected(PointerEventData eventData)
        {
            if (!GM_GBManager.Instance.getSelected)
            {
                if (GM_ChangeMaterial.BtnSelected)
                {
                    setColor();
                }
                else
                {
                    ResetMaterial();
                    GM_GBManager.Instance.getSurface = GM_GBManager.Instance.getSurface != gameObject ? gameObject : null;
                }
            }
            UpdateMaterial();
        }
        public void SetActive(PointerEventData eventData)
        {
            if (GM_GBManager.Instance.getSelected)
            {
                canvas.GetComponent<GraphicRaycaster>().enabled = false;
                builderObject = GM_GBManager.Instance.getSelected.GetComponent<GM_GBObject>();
                builderObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                builderObject.moveObject(eventData.pointerCurrentRaycast);
                builderObject.UpdateMaterial();
            }
            

            UpdateMaterial();
        }
        public void FirstHovering(PointerEventData eventData)
        {
            if (GM_GBManager.Instance.getSelected && GM_GBManager.Instance.getSelected.GetComponent<GM_GBObject>().firstHovering)
            {
                canvas.GetComponent<GraphicRaycaster>().enabled = false;
                builderObject = GM_GBManager.Instance.getSelected.GetComponent<GM_GBObject>();
                builderObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                builderObject.moveObject(eventData.pointerCurrentRaycast);
                builderObject.firstHovering = false;
                builderObject.UpdateMaterial();
            }


            UpdateMaterial();
        }
        public void SetInactive(PointerEventData eventData)
        {
            if (GM_GBManager.Instance.getSelected)
            {
                canvas.GetComponent<GraphicRaycaster>().enabled = true;
                builderObject = GM_GBManager.Instance.getSelected.GetComponent<GM_GBObject>();
                builderObject.gameObject.layer = LayerMask.NameToLayer("Default");
                GM_GBManager.Instance.getSelected = null;
                builderObject.UpdateMaterial();
            }
            UpdateMaterial();
        }

        public void ResetMaterial()
        {
            if (GM_GBManager.Instance.getSurface)
            {
                GM_GBManager.Instance.getSurface.GetComponent<GM_GBSurface>().outline.enabled = false;
            }
        }

        public void UpdateMaterial()
        {
            if (GM_GBManager.Instance.getSurface == gameObject)
            {
                GM_GBManager.Instance.getSurface.GetComponent<GM_GBSurface>().outline.enabled = true;
            }
        }
        public void setColor()
        {
            mr.sharedMaterial = GM_ChangeMaterial.BtnSelected.GetComponent<GM_ChangeMaterial>().material;
        }
    }
}
