using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BNG
{
    public class GymBuilderObject : MonoBehaviour
    {
        Rigidbody rb;
        BoxCollider bc;
        MeshFilter mf;

        RaycastGymBuilder ray;

        private void Awake()
        {
            ray = new RaycastGymBuilder();
        }
        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            bc = gameObject.GetComponent<BoxCollider>();
            mf = gameObject.GetComponent<MeshFilter>();

            rb = rb == null ? gameObject.AddComponent<Rigidbody>() : gameObject.GetComponent<Rigidbody>();
            bc = bc == null ? gameObject.AddComponent<BoxCollider>() : gameObject.GetComponent<BoxCollider>();
            mf = mf == null ? gameObject.AddComponent<MeshFilter>() : gameObject.GetComponent<MeshFilter>();

            MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            int i = 0;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;

                i++;
            }
            mf.mesh = new Mesh();
            mf.mesh.CombineMeshes(combine);

            rb.useGravity = false;
            rb.freezeRotation = true;

            bc.size = mf.mesh.bounds.size;
            bc.center = mf.mesh.bounds.center;
        }

        /*public void show(PointerEventData eventData)
        {
            gameObject.SetActive(true);
        }

        public void notshow(PointerEventData eventData)
        {
            gameObject.SetActive(false);
        }

        public void SetActive(PointerEventData eventData)
        {
            ray.canMove(true);
            ray.setGameObject(gameObject);
        }

        // No longer ohlding down activate
        public void SetInactive(PointerEventData eventData)
        {
            ray.canMove(false);
            ray.setGameObject(null);
        }*/
    }
}
