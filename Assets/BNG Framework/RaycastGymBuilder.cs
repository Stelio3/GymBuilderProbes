using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGymBuilder : MonoBehaviour
{
    
    private LineRenderer lineRenderer;

    public LayerMask layerMask;
    public float maxlength = 10f;
    public GameObject go;

    void Start(){
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxlength, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider != null)
            {
                go.SetActive(true);
                go.gameObject.transform.position = hit.point;

                lineRenderer.SetPosition(0, Vector3.zero);
                lineRenderer.SetPosition(1, new Vector3(0, 0, Vector3.Distance(transform.position, hit.point)));
            }
            else
            {
                go.SetActive(false);
            }
        }
        
    }
}
