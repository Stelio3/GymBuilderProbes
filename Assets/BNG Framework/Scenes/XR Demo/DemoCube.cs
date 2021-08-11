using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BNG
{

    /// <summary>
    /// This is an example of how to highlight an object on hover / activate. This is used in the Demo scene in conjunction with the "PointerEvents" component.
    /// </summary>
    public class DemoCube : MonoBehaviour
    {


        public Material HighlightMaterial;
        public Material ActiveMaterial;
        // Currently hovering over the object?
        bool hovering = false;

        Material initialMaterial;
        MeshRenderer render;

        void Start()
        {
            render = GetComponentInChildren<MeshRenderer>();
            initialMaterial = render.sharedMaterial;
        }

        // Holding down activate
        public void SetActive(PointerEventData eventData)
        {
            PlaceObjet(true);
        }

        // No longer ohlding down activate
        public void SetInactive(PointerEventData eventData)
        {
            PlaceObjet(false);
        }

        public void PlaceObjet(bool active)
        {
            if (active)
            {
                transform.position += new Vector3(0, 0, 3);
            }
            else
            {
                transform.position += new Vector3(0, 0, -3);
            }
        }
    }
}

