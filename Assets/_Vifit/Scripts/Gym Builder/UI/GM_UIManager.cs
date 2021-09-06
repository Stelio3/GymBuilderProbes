using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_UIManager : Singleton<GM_UIManager>
{
    public GameObject ButtonSelected { get; set; }

    private void Awake()
    {
        ButtonSelected = null;
    }
}
