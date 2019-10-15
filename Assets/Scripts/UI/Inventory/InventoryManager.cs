using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Inventory seeds;
    public Inventory items;

    void Awake()
    {
        if(instance == null)
            instance = this;
        
        seeds = transform.Find("Seeds").gameObject.GetComponent<Inventory>();
        Transform deb1 = transform.Find("Items");
        GameObject deb2 = deb1.gameObject;
        items = transform.Find("Items").gameObject.GetComponent<Inventory>();
    }

    public void addItem(CollectableItem temp)
    {
        if(temp.itemType==CollectableItem.ItemType.seed)
            seeds.AddItem(temp);
        if(temp.itemType==CollectableItem.ItemType.item)
            items.AddItem(temp);
    }

    public void useItem(CollectableItem temp)
    {
        
        if(temp.itemType==CollectableItem.ItemType.seed)
            seeds.RemoveItem(temp);
        if(temp.itemType==CollectableItem.ItemType.item)
            items.RemoveItem(temp);
    }
}
