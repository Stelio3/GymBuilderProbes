using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SerializableObjects : MonoBehaviour
{
    public static Dictionary<int, GM_GBScriptableObjects> gb_objectList = new Dictionary<int, GM_GBScriptableObjects>();
    public List<GM_GBScriptableObjects> Gb_items;

    private void Awake()
    {
        foreach (var item in Gb_items)
        {
            gb_objectList.Add(item.id, item);
        }
    }

    public static GM_GBScriptableObjects Get(int id)
    {
        return gb_objectList[id];
    }
}
