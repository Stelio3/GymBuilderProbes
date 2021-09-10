using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRUiKits.Utils;

public class GM_DisplayContent : MonoBehaviour
{
    public GameObject displayGo;
    public GameObject infoPanel;
    const int rowCount = 3;
    public GameObject empty;
    GameObject row;
    // Start is called before the first frame update

    private void Awake()
    {
        GM_ItemShop.infoPanel = infoPanel;
    }
    void Start()
    {
        DisplayAll();
    }
    public void DisplayAll()
    {
        ResetDisplay();
        int listLength = SerializableObjects.gb_objectList.Count;
        for (int i = 0; i < listLength; i++)
        {
            if (i % rowCount == 0)
            {
                CreateRow();
            }
            Instantiate(displayGo, row.transform).GetComponent<GM_ItemShop>().scriptableObject = SerializableObjects.Get(i + 1);
        }
    }
    public void DisplayObjects()
    {
        ResetDisplay();
        int listLength = SerializableObjects.gb_objectList.Count;
        for (int i = 0; i < listLength; i++)
        {
            if (i % rowCount == 0)
            {
                CreateRow();
            }
            if (SerializableObjects.Get(i+1).Object.GetComponent<GM_GBObject>() != null)
            { 
                Instantiate(displayGo, row.transform).GetComponent<GM_ItemShop>().scriptableObject = SerializableObjects.Get(i + 1);
            }
        }
    }
    public void DisplaySurfaces()
    {
        ResetDisplay();
        int listLength = SerializableObjects.gb_objectList.Count;
        for (int i = 0; i < listLength; i++)
        {
            if (i % rowCount == 0)
            {
                CreateRow();
            }
            if (SerializableObjects.Get(i+1).Object.GetComponent<GM_GBSurface>() != null)
            {
                Instantiate(displayGo, row.transform).GetComponent<GM_ItemShop>().scriptableObject = SerializableObjects.Get(i + 1);
            }
        }
    }
    public void DisplayInventary()
    {
        ResetDisplay();
        List<int> created = new List<int>();
        int i = 0;
        foreach (GM_ObjectData o in GM_GameDataManager.gymBuilderObjects)
        {
            if(o.position == Vector3.zero && o.rotation == Quaternion.identity && !created.Contains(o.objectId))
            {
                if (i % rowCount == 0)
                {
                    CreateRow();
                }
                i++;
                Instantiate(displayGo, row.transform).GetComponent<GM_ItemShop>().scriptableObject = SerializableObjects.Get(o.objectId);
                created.Add(o.objectId);
            }
        }
    }

    private void CreateRow()
    {
        row = Instantiate(empty, gameObject.transform);
        row.AddComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y / 2);
        row.AddComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
    }
    private void ResetDisplay()
    {
        if(transform.childCount > 0)
        {
            RectTransform[] allChildren = GetComponentsInChildren<RectTransform>();
            foreach (RectTransform child in allChildren)
            {
                if(child.gameObject != gameObject)
                    Destroy(child.gameObject);
            }
        }
        
    }
}
