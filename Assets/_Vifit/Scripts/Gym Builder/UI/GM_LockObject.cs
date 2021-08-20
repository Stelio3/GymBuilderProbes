using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_LockObject : MonoBehaviour
{
    private void Update()
    {
        if (GM_GBManager.Instance.type == Type.Object || GM_GBManager.Instance.type == Type.Surface)
        {
            gameObject.GetComponent<Image>().color = GM_GBManager.Instance.getSelected.GetComponent<GM_GBEditions>().locked ? Color.grey : Color.white;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
    public void lockObject()
    {
        GM_GBManager.Instance.lockObject();
    }
}
