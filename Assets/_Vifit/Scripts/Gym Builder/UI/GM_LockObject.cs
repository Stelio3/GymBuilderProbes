using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_LockObject : MonoBehaviour
{
    private void Update()
    {
        if (GM_GBManager.Instance.TypeSelected == Type.Object || GM_GBManager.Instance.TypeSelected == Type.Surface)
        {
            gameObject.GetComponent<Image>().color = GM_GBManager.Instance.GetSelected.GetComponent<GM_GBEditions>().locked ? Color.grey : Color.white;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
    public void lockObject()
    {
        if (GM_GBManager.Instance.TypeSelected == Type.Object || GM_GBManager.Instance.TypeSelected == Type.Surface)
        {
            GM_GBManager.Instance.GetSelected.GetComponent<GM_GBEditions>().locked = !GM_GBManager.Instance.GetSelected.GetComponent<GM_GBEditions>().locked;
            GM_GameDataManager.UpdateData().locked = GM_GBManager.Instance.GetSelected.GetComponent<GM_GBEditions>().locked;
        }
    }
}
