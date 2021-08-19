using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GymBuilder Object", menuName = "GymBuilder Object")]
public class GM_GBScriptableObjects : ScriptableObject
{
    public int id;
    public new string name;
    public string description;
    public int price;

    public Sprite objectImage;
    public GameObject Object;

    public GBScriptableObjectsType.Type type;
}

public class GBScriptableObjectsType : ScriptableObject
{
    public enum Type { Floor, Wall, Roof };
}
