﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GM_GBObject : GM_GBEditions
{
    protected override void Start()
    {
        base.Start();
        outline.OutlineWidth = 2f;
    }
}
