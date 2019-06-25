﻿using System.Collections;
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
            num.text = item.cnt.ToString();
        }
        
    }

    public void setHilight(bool t)
    {
        Rect.enabled = t;
    }
}