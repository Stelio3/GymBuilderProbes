using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_DeleteObject : MonoBehaviour
{
    public void DeleteObject()
    {
        if(GM_GBManager.Instance.TypeSelected == Type.Object || GM_GBManager.Instance.TypeSelected == Type.Surface)
        {
            BNG.InputBridge.Instance.VibrateController(0.1f, 0.3f, 0.1f, BNG.ControllerHand.Left);
            Destroy(GM_GBManager.Instance.GetSelected);
        }
        GM_GBManager.Instance.UpdateSelected(null, Type.None);
    }
}
