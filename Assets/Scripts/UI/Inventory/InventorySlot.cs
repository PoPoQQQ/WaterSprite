using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Image icon;
    Text count;
    CollectableItem item;
    GameObject parentUI;
    public int code;

    void Awake()
    {
        parentUI = transform.parent.gameObject;
        icon = transform.Find("Icon").gameObject.GetComponent<Image>();
        count = transform.Find("Text").gameObject.GetComponent<Text>();
        icon.enabled = false;
        count.enabled = false;
    }

    public void setItem(itemSave temp)
    {
        if(temp.cnt == 0)
        {
            icon.enabled = false;
            count.enabled = false;
        }
        else
        {
            icon.enabled = true;
            count.enabled = true;
            item = temp.item;
            count.text = temp.cnt.ToString();
            icon.sprite = temp.item.icon;
        }
    }

    GameObject temp;

    public void BeginDragUI()
    {
        if(icon.enabled)
        {
            
            temp = Instantiate(icon.gameObject, Input.mousePosition, Quaternion.identity);
            temp.transform.SetParent(transform.parent.parent);
            temp.transform.SetAsLastSibling();
            temp.GetComponent<Image>().raycastTarget = false;
            icon.enabled = false;
            count.enabled = false;
        }
    }

    public void DragUI()
    {
        
        if(temp != null)
        {
            temp.transform.position = Input.mousePosition;
        }
    }

    public void EndDragUI()
    {
        if(temp == null)
            return;
         if(parentUI.GetComponent<InventoryUI>().pointerOnSlot == -1)
        {
            icon.enabled = true;
            count.enabled = true;
        }
        else
        {
            parentUI.GetComponent<Inventory>().swap(code, parentUI.GetComponent<InventoryUI>().pointerOnSlot);
        }
        Destroy(temp);
    }
}
