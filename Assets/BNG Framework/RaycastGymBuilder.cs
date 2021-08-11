using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace BNG
{
    public class RaycastGymBuilder : MonoBehaviour
    {
        [HideInInspector]
        public bool move = false;
        [Tooltip("The controller side this pointer is on")]
        public ControllerHand ControllerSide = ControllerHand.Right;

        [Tooltip("If true this object will update the VRUISystem's Left or Right Transform property")]
        public bool AutoUpdateUITransforms = true;

        private LineRenderer lineRenderer;

        public LayerMask layerMask;
        public float maxlength = 10f;

        public GameObject go = null;

        public Material redM, greenM;

        VRUISystem uiSystem;
        PointerEventData data;
        void Awake()
        {
            if (go)
            {
                go = Instantiate(go);
            }
            lineRenderer = GetComponent<LineRenderer>();
            uiSystem = VRUISystem.Instance;
        }
        void OnEnable()
        {
            // Automatically update VR System with out transforms
            if (AutoUpdateUITransforms && ControllerSide == ControllerHand.Left)
            {
                uiSystem.LeftPointerTransform = transform;
            }
            else if (AutoUpdateUITransforms && ControllerSide == ControllerHand.Right)
            {
                uiSystem.RightPointerTransform = transform;
            }

            uiSystem.UpdateControllerHand(ControllerSide);
        }

        void Update()
        {
            data = uiSystem.EventData;
            if (data == null || data.pointerCurrentRaycast.gameObject == null)
            {
                hideObject();
                return;
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxlength, layerMask, QueryTriggerInteraction.Ignore))
            {
                float distance = Vector3.Distance(transform.position, hit.point);
                lineRenderer.SetPosition(0, Vector3.zero);
                lineRenderer.SetPosition(1, new Vector3(0, 0, distance));
                lineRenderer.material = hit.transform.gameObject.tag == go.tag ? greenM : redM;
                if (hit.transform.gameObject.tag == "Wall")
                {
                    go.transform.rotation = hit.transform.rotation;
                    go.transform.position = hit.point - (hit.transform.right * (go.GetComponent<MeshFilter>().mesh.bounds.extents.x + 0.001f));
                }
                else if (hit.transform.gameObject.tag == "Roof")
                {
                    go.transform.position = hit.point - (Vector3.up * (go.GetComponent<MeshFilter>().mesh.bounds.extents.y + 0.001f));
                }
                else
                {
                    go.transform.position = hit.point + (Vector3.up * (go.GetComponent<MeshFilter>().mesh.bounds.extents.y + 0.001f));
                }
            }
            else
            {
                hideObject();
            }
        }
        public void hideObject()
        {
            lineRenderer.material = redM;
        }
    }
}