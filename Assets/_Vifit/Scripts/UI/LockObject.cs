using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockObject : MonoBehaviour
{
    private void Update()
    {
        gameObject.GetComponent<Image>().color = GBObjectManager.Instance.getSelected.GetComponent<GymBuilderObject>().locked ? Color.grey : Color.white;
    }
    public void lockObject()
    {
        GBObjectManager.Instance.lockObject();
    }
}
