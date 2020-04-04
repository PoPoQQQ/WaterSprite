using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSave{
    public int cnt=0;
    public CollectableItem item=null;

    public void set(itemSave b)
    {
        cnt = b.cnt;
        item = b.item;
    }
}

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallBack;

	public itemSave[] itemList;

    public Plant.Type GetSeedType(int code)
    {
        if(itemList[code].cnt <=0)
            return Plant.Type.None;
        return itemList[code].item.seedType;
    }

    private void Start() {
    }

    public void initList(int num)
    {
        itemList = new itemSave[num];
        for (int i=0; i<num; i++)
            itemList[i] = new itemSave();
    }

	public void AddItem(CollectableItem newItem)
	{
        Debug.Log("add fruit "+ newItem.name + " " + newItem.itemType);
        bool exist = false;
        foreach (itemSave slot in itemList)
        {
            if(slot.item == null)
                continue;
            else if(slot.cnt>0 && slot.item.name == newItem.name)
            {
                exist = true;
                slot.cnt++;
            }
        }
        if(!exist)
        {
            bool full = true;
            foreach (itemSave slot in itemList)
            {
                if(slot.cnt == 0)
                {
                    full = false;
                    slot.cnt = 1;
                    slot.item = newItem;
                    break;
                }
            }
            if(full)
                Debug.Log("inventory full");
        }
		if(onItemChangedCallBack != null)
			onItemChangedCallBack.Invoke();
	}

	public void RemoveItem(CollectableItem newItem)
	{
        bool exist = false;
        foreach (itemSave slot in itemList)
        {
            if(slot.item == null)
                continue;
            if(slot.item.name == newItem.name)
            {
                exist = true;
                slot.cnt--;
                if(slot.cnt == 0)
                    slot.item = null;
            }
        }
		
		if(onItemChangedCallBack != null)
			onItemChangedCallBack.Invoke();	
	}

    public void swap(int a, int b)
    {
        itemSave temp = new itemSave();
        temp.set(itemList[a]);
        itemList[a].set(itemList[b]);
        itemList[b].set(temp);
        if(onItemChangedCallBack != null)
			onItemChangedCallBack.Invoke();	
    }    
}
