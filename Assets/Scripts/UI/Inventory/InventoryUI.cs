using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public InventorySlot[] slotList;
    int SlotNum;

    public int pointerOnSlot;


    void Awake()
    {
        inventory = GetComponent<Inventory>();
        slotList = GetComponentsInChildren<InventorySlot>();
        for(int i=0; i<slotList.Length; i++)
            slotList[i].code = i;
        SlotNum = slotList.Length;
        inventory.initList(SlotNum);
    }
    void Start () {

		inventory.onItemChangedCallBack += UpdateUI;
		UpdateUI();
	}

    public void setPointer(int num)
    {
        pointerOnSlot = num;
    }

	void UpdateUI()
	{
        for(int i=0; i<SlotNum; i++)
        {
            itemSave temp = inventory.itemList[i];
            slotList[i].setItem(inventory.itemList[i]);
        }
	}
}
