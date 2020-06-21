using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class InformationBox : MonoBehaviour
{
    public Text namebox;
    public Text infobox;

    public float delayTime = 1f;

    float beginTime;

    void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        beginTime = Time.time;
    }

    void Update()
    {
        if(Time.time - beginTime > delayTime)
        {
            if(!transform.GetChild(0).gameObject.activeSelf)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }



    public void setInformation(ItemInfo info)
    {
        namebox.text = info.itemName;
        infobox.text = info.itemInfo;
    }
}
