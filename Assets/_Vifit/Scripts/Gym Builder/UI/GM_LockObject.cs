using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_LockObject : MonoBehaviour
{
    private void Update()
    {
        if (GM_GBManager.Instance.getSelected)
        {
            gameObject.GetComponent<Image>().color = GM_GBManager.Instance.getSelected.GetComponent<GM_GBObject>().locked ? Color.grey : Color.white;
        }
    }
    public void lockObject()
    {
        GM_GBManager.Instance.lockObject();
    }
}
