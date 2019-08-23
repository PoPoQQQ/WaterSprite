using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Inventory seeds;

    void Awake()
    {
        if(instance == null)
            instance = this;
        
        seeds = transform.Find("Seeds").gameObject.GetComponent<Inventory>();
    }

    public void addItem(CollectableItem temp)
    {
        if(temp.itemType==CollectableItem.ItemType.seed)
        {
            seeds.AddItem(temp);
        }
    }

    public void useItem(CollectableItem temp)
    {
        
        if(temp.itemType==CollectableItem.ItemType.seed)
        {
            seeds.RemoveItem(temp);
        }
    }
}
