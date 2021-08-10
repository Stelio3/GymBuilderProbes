using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGymBuilder : MonoBehaviour
{
    
    private LineRenderer lineRenderer;

    public LayerMask layerMask;
    public float maxlength = 10f;
    public GameObject go;

    public Material redM, greenM;

    void Start(){
        lineRenderer = GetComponent<LineRenderer>();

        if (go.GetComponentInChildren<Rigidbody>() == null)
        {
            Rigidbody rbChild = go.transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
            rbChild.useGravity = false;
        }
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxlength, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.gameObject.tag == "Wall" && go.tag == "Wall" || hit.transform.gameObject.tag == "Roof" && go.tag == "Roof")
            {
                lineRenderer.material = greenM;
            }
            else
            {
                lineRenderer.material = redM;
            }
            go.GetComponentInChildren<MeshRenderer>().enabled = true;
            go.gameObject.transform.position = hit.point;
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, new Vector3(0, 0, Vector3.Distance(transform.position, go.gameObject.transform.GetChild(0).position)));
        }
        else
        {
            go.gameObject.transform.GetChild(0).localPosition = Vector3.zero;
            go.gameObject.transform.GetChild(0).localRotation = Quaternion.identity;
            go.GetComponentInChildren<MeshRenderer>().enabled = false;
            lineRenderer.material = redM;
        }
        
    }
}
