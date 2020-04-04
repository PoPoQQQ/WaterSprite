using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickFruitManager : MonoBehaviour
{
    float slotDelTime = 0.1f;
    float currentTime = .0f;

    int num;
    QuickSeedSlot[] quickSlots;

    Inventory fruits;

    PlayerFruitEater fruitEater;

    int hilightCode=0;

    Plant.Type hilightType{get{return fruits.itemList[hilightCode].item.seedType;}}

    void Start()
    {
        fruitEater = FindObjectOfType<PlayerFruitEater>();
        quickSlots = GetComponentsInChildren<QuickSeedSlot>();
        Inventory[] inventories = FindObjectsOfType<Inventory>();
        for(int i=0;i<inventories.Length;i++)
        {
            if(inventories[i].gameObject.name=="Items"){
                fruits = inventories[i];
                break;
            }
        }
        fruits.onItemChangedCallBack += setSlot;
        //Debug.Log(fruits.name);
        updateHilight();
        setSlot();
        gameObject.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        
        if(currentTime > 0)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= slotDelTime)
                currentTime = 0;
        }
        if(scrollWheel != 0 && currentTime == 0)
        {
            currentTime += Time.deltaTime;
            hilightCode += (-1)*(int)Mathf.Sign(scrollWheel);
            if(hilightCode == quickSlots.Length)
                hilightCode = 0;
            if(hilightCode<0)
                hilightCode = quickSlots.Length -1;
            updateHilight();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            UseFruit();
        }

        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
    }

    void UseFruit()
    {
        if(fruits.itemList[hilightCode].cnt<=0)
            return;
        if(PlayerFruitEater.Usable(hilightType))
        {
            PlayerFruitEater.Eat(hilightType);
            fruits.RemoveItem(fruits.itemList[hilightCode].item);
        }
        else{
            Debug.Log("Unusable!");
        }
    }

    void updateAlpha()
    {
        for(int i=0;i<quickSlots.Length;i++)
        {
            if(fruits.itemList[i].cnt<=0)
                return;
            if(PlayerFruitEater.Usable(fruits.itemList[i].item.seedType))
                quickSlots[i].setAlpha(1f);
            else
                quickSlots[i].setAlpha(.5f);
        }
    }

    void updateHilight()
    {
        for(int i=0; i<quickSlots.Length; i++)
        {
            quickSlots[i].setHilight(i==hilightCode);
        }
    }

    void setSlot()
    {
        for(int i=0;i<quickSlots.Length;i++)
        {
            quickSlots[i].setSlot(fruits.itemList[i]);
        }
    }

}
