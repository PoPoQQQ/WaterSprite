using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSeedSlot : MonoBehaviour
{
    public Image icon;
    public Text num;
    public Image Rect;

    // Start is called before the first frame update
    void Awake()
    {
        icon = transform.Find("Icon").gameObject.GetComponent<Image>();
        num = transform.Find("Text").gameObject.GetComponent<Text>();
        Rect = transform.Find("rect").gameObject.GetComponent<Image>();
    }

    public void setSlot(itemSave item)
    {
        if(item.cnt == 0)
        {
            icon.enabled = false;
            num.enabled = false;
        }
        else
        {
            icon.enabled = true;
            num.enabled = true;
            icon.sprite = item.item.icon;
            icon.SetNativeSize();
            num.text = item.cnt.ToString();
        }
        
    }

    public void setHilight(bool t)
    {
        if(Rect == null){
            Debug.Log(transform.parent.name + " " + gameObject.name);
            Debug.Log(Rect);
            Debug.Log(transform.Find("rect").name);
        }
        Rect.enabled = t;
    }

    public void setAlpha(float a)
    {
        Color c = icon.color;
        c.a = a;
        icon.color = c;
    }
}
