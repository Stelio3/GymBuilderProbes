using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BNG
{
    public class GM_GBSurface : GM_GBEditions
    {
        MeshRenderer mr;
        private GM_GBObject builderObject;
        public GameObject canvas;

        protected override void Start()
        {
            base.Start();
            mr = GetComponent<MeshRenderer>();
            outline.OutlineWidth = 6f;
        }
        public void SetSelected(PointerEventData eventData)
        {
            if (GM_GBManager.Instance.type != Type.Object)
            {
                if (GM_GBManager.Instance.type == Type.Color)
                {
                    setColor();
                }
                else
                {
                    if (GM_GBManager.Instance.getSelected)
                    {
                        if (GM_GBManager.Instance.getSelected != gameObject)
                        {
                            GM_GBManager.Instance.UpdateSelected(gameObject, Type.Surface);
                        }
                        else
                        {
                            GM_GBManager.Instance.UpdateSelected(null, Type.None);
                        }
                    }
                    else
                    {
                        GM_GBManager.Instance.UpdateSelected(gameObject, Type.Surface);
                    }
                    
                }
            }
        }
        public override void SetActive(PointerEventData eventData)
        {
            if (GM_GBManager.Instance.type == Type.Object)
            {
                canvas.GetComponent<GraphicRaycaster>().enabled = false;
                builderObject = GM_GBManager.Instance.getSelected.GetComponent<GM_GBObject>();
                builderObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                builderObject.moveObject(eventData.pointerCurrentRaycast);
            }
        }
        public void FirstHovering(PointerEventData eventData)
        {
            if (GM_GBManager.Instance.type == Type.Object && GM_GBManager.Instance.getSelected.GetComponent<GM_GBObject>().firstHovering)
            {
                canvas.GetComponent<GraphicRaycaster>().enabled = false;
                builderObject = GM_GBManager.Instance.getSelected.GetComponent<GM_GBObject>();
                builderObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                builderObject.moveObject(eventData.pointerCurrentRaycast);
                builderObject.firstHovering = false;
            }
        }
        public override void SetInactive(PointerEventData eventData)
        {
            if (GM_GBManager.Instance.type == Type.Object)
            {
                canvas.GetComponent<GraphicRaycaster>().enabled = true;
                builderObject = GM_GBManager.Instance.getSelected.GetComponent<GM_GBObject>();
                builderObject.gameObject.layer = LayerMask.NameToLayer("Default");
                GM_GBManager.Instance.UpdateSelected(null, Type.None);
            }
        }
        public override void moveObject(RaycastResult rayResult)
        {

        }
        public void setColor()
        {
            mr.sharedMaterial = GM_GBManager.Instance.getSelected.GetComponent<GM_ChangeMaterial>().material;
            GM_GameDataManager.UpdateData().material = mr.sharedMaterial;
        }
    }
}
