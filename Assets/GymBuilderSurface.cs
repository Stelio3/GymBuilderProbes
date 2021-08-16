using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BNG
{
    public class GymBuilderSurface : MonoBehaviour
    {
        Material initialMaterial;

        public bool Selected { get; set; }
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
            initialMaterial = mr.sharedMaterial;

            outline.enabled = false;
            outline.OutlineColor = Color.black;
            outline.OutlineWidth = 6f;
        }
        public void SetSelected(PointerEventData eventData)
        {
            if (GBObjectManager.Instance.getSelected)
            {
                builderObject = GBObjectManager.Instance.getSelected.GetComponent<GymBuilderObject>();

                if (!builderObject.Selected)
                {
                    if (GBObjectManager.Instance.getSurface == gameObject)
                    {
                        GBObjectManager.Instance.getSurface = null;
                        Selected = false;
                    }
                    else
                    {
                        GBObjectManager.Instance.getSurface = gameObject;
                        Selected = true;
                    }
                }
                builderObject.Selected = false;
                builderObject.gameObject.layer = LayerMask.NameToLayer("Default");
                builderObject.UpdateMaterial();
            }
            else
            {
                GBObjectManager.Instance.getSurface = gameObject;
                Selected = true;
            }

            UpdateMaterial();
        }
        public void SetActive(PointerEventData eventData)
        {
            if (GBObjectManager.Instance.getSelected)
            {
                builderObject = GBObjectManager.Instance.getSelected.GetComponent<GymBuilderObject>();
                if (builderObject.Selected)
                {
                    builderObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    builderObject.moveObject(eventData);
                }
            }
            

            UpdateMaterial();
        }
        public void SetInactive(PointerEventData eventData)
        {
            if (GBObjectManager.Instance.getSelected.GetComponent<GymBuilderObject>())
            {
                builderObject = GBObjectManager.Instance.getSelected.GetComponent<GymBuilderObject>();
                builderObject.Selected = false;
             }


            UpdateMaterial();
        }

        public void UpdateMaterial()
        {
            if (Selected && GBObjectManager.Instance.getSurface == gameObject)
            {
                outline.enabled = true;
            }
            else
            {
                outline.enabled = false;
            }
        }
        public void setColor(Material material)
        {
            if (Selected)
            {
                Selected = false;
                outline.enabled = false;
                GBObjectManager.Instance.getSurface = null;
                mr.sharedMaterial = material;
                initialMaterial = material;
            }
        }
    }
}
