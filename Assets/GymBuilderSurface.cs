using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BNG
{
    public class GymBuilderSurface : MonoBehaviour
    {
        public Material HighlightMaterial;
        Material initialMaterial;

        public bool Selected { get; set; }
        MeshRenderer mr;
        public Material matSelected;
        private GymBuilderObject builderObject;
        void Start()
        {
            mr = GetComponent<MeshRenderer>();
            initialMaterial = mr.sharedMaterial;
        }
        public void SetSelected(PointerEventData eventData)
        {
            if (GBObjectManager.Instance.getSelected.GetComponent<GymBuilderObject>())
            {
                builderObject = GBObjectManager.Instance.getSelected.GetComponent<GymBuilderObject>();
            }
               
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
            UpdateMaterial();
        }
        public void SetActive(PointerEventData eventData)
        {
            if (GBObjectManager.Instance.getSelected.GetComponent<GymBuilderObject>())
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

        public void UpdateMaterial()
        {
            if (Selected && GBObjectManager.Instance.getSurface == gameObject)
            {
                mr.sharedMaterial = matSelected;
            }
            else
            {
                mr.sharedMaterial = initialMaterial;
            }
        }
        public void setColor(Material material)
        {
            if (Selected)
            {
                Selected = false;
                GBObjectManager.Instance.getSurface = null;
                mr.sharedMaterial = material;
                initialMaterial = material;
            }
        }
    }
}
