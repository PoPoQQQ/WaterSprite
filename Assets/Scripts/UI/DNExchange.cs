﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DNExchange : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dayIcon;
    public GameObject nightIcon;

    public Text dayCount;

    public void setDayIcon()
    {
        dayIcon.SetActive(true);
        nightIcon.SetActive(false);
    }

    public void setNightIcon()
    {
        dayIcon.SetActive(false);
        nightIcon.SetActive(true);
    }

    public void setDayCnt(int num)
    {
        dayCount.text = "" + num;
    } 

    void start()
    {
        setDayIcon();
    }
}