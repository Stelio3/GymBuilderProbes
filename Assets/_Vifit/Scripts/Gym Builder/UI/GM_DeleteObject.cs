using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_DeleteObject : GM_Options
{
    public override void Selected()
    {
        SetSelected(OptionType.Delete);
    }
}
