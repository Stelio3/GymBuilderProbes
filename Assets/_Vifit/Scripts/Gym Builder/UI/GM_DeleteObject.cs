using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_DeleteObject : MonoBehaviour
{
    public void DeleteObject()
    {
        if(GM_GBManager.Instance.type == Type.Object || GM_GBManager.Instance.type == Type.Surface)
        {
            Destroy(GM_GBManager.Instance.getSelected);
        }
        GM_GBManager.Instance.UpdateSelected(null, Type.None);
    }
}
