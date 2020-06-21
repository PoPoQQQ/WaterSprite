using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemInfo{
    public int id;
    public string itemName;
    public string itemInfo;
}

[System.Serializable]
public class InfoRoot{
    public ItemInfo[] seedInformation;
    public ItemInfo[] itemInformation; 
}

public class InformationReader : MonoBehaviour
{
    public static InformationReader instance;

    InformationBox box;

    public TextAsset infoJsonFile;

    InfoRoot infoList;

    ItemInfo defaultItem = new ItemInfo();

    void Awake()
    {
        if(instance != null)
            Debug.Log("UI update error! multiple UI");
        instance = this;

        readJson();
    }

    void readJson()
    {
        infoList = JsonUtility.FromJson<InfoRoot>(infoJsonFile.text);
        defaultItem.id = -1;
        defaultItem.itemName = "未找到该物品！";
        defaultItem.itemInfo = "ERROR";
        //debugInfo();
    }

    void debugInfo()
    {
        //Debug.Log(infoJsonFile.text);
        Debug.Log("seed information:" + " " + infoList.seedInformation.Length);
        foreach(ItemInfo info in infoList.seedInformation)
            Debug.Log(info.id + " " + info.itemName + " " + info.itemInfo);
        Debug.Log("item information:" + " " + infoList.itemInformation.Length);
        foreach(ItemInfo info in infoList.itemInformation)
            Debug.Log(info.id + " " + info.itemName + " " + info.itemInfo);
    }

    public ItemInfo GetInfo(bool seed, Plant.Type seedType)
    {
        if(seed)
        {
            foreach(var tmp in  infoList.seedInformation)
            {
                if(tmp.id == (int)seedType)
                {
                    return tmp;
                }
            }
        }
        else
        {
            foreach(var tmp in  infoList.itemInformation)
            {
                if(tmp.id == (int)seedType)
                {
                    return tmp;
                }
            }
        }
        return defaultItem;
    }

    public void GetItemInformation(bool seed, Plant.Type seedType, Vector3 pos)
    {
        box = Instantiate(Resources.Load("Prefabs/UI/ItemInformation") as GameObject, pos, Quaternion.identity).GetComponent<InformationBox>();
        box.transform.SetParent(transform);
        box.transform.SetAsLastSibling();
        box.setInformation(GetInfo(seed, seedType));
    }

    public void destoryInfo()
    {
        Destroy(box.gameObject);
    }
}
