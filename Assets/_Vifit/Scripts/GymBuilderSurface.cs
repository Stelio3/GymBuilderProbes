using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BNG
{
    public class GymBuilderSurface : MonoBehaviour
    {
        MeshRenderer mr;
        private GymBuilderObject builderObject;
        Outline outline;

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
            if (!GBObjectManager.Instance.getSelected)
            {
                if (ChangeMaterial.BtnSelected)
                {
                    setColor();
                }
                else
                {
                    ResetMaterial();
                    GBObjectManager.Instance.getSurface = GBObjectManager.Instance.getSurface != gameObject ? gameObject : null;
                }
            }
            UpdateMaterial();
        }
        public void SetActive(PointerEventData eventData)
        {
            if (GBObjectManager.Instance.getSelected)
            {
                builderObject = GBObjectManager.Instance.getSelected.GetComponent<GymBuilderObject>();
                builderObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                builderObject.moveObject(eventData);
                builderObject.UpdateMaterial();
            }
            

            UpdateMaterial();
        }
        public void SetInactive(PointerEventData eventData)
        {
            if (GBObjectManager.Instance.getSelected)
            {
                builderObject = GBObjectManager.Instance.getSelected.GetComponent<GymBuilderObject>();
                builderObject.gameObject.layer = LayerMask.NameToLayer("Default");
                GBObjectManager.Instance.getSelected = null;
                builderObject.UpdateMaterial();
            }
            UpdateMaterial();
        }

        public void ResetMaterial()
        {
            if (GBObjectManager.Instance.getSurface)
            {
                GBObjectManager.Instance.getSurface.GetComponent<GymBuilderSurface>().outline.enabled = false;
            }
        }

        public void UpdateMaterial()
        {
            if (GBObjectManager.Instance.getSurface == gameObject)
            {
                GBObjectManager.Instance.getSurface.GetComponent<GymBuilderSurface>().outline.enabled = true;
            }
        }
        public void setColor()
        {
            mr.sharedMaterial = ChangeMaterial.BtnSelected.GetComponent<ChangeMaterial>().material;
        }
    }
}
