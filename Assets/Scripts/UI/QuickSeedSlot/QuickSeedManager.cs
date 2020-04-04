using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSeedManager : MonoBehaviour
{

    float slotDelTime = 0.1f;
    float currentTime = .0f;

    int num;
    QuickSeedSlot[] quickSlots;

    Inventory seeds;

    int hilightCode=0;

    int HILIGHT{get{return hilightCode;}}
    public float waterCost = 10F;

    void Start()
    {
        
        quickSlots = GetComponentsInChildren<QuickSeedSlot>();
        Inventory[] inventories = FindObjectsOfType<Inventory>();
        for(int i=0;i<inventories.Length;i++)
        {
            if(inventories[i].gameObject.name=="Seeds"){
                seeds = inventories[i];
                break;
            }
        }
        seeds.onItemChangedCallBack += setSlot;
        updateHilight();
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
            quickSlots[i].setSlot(seeds.itemList[i]);
        }
    }

    public void plantSeed(Plant currentPlant)
    {
        Plant.Type hilightType = seeds.GetSeedType(hilightCode);
        if(currentPlant.type != Plant.Type.None)
        {
            if(currentPlant.mature)
            {
                if(PlayerFruitEater.Usable(currentPlant.type))
                {
                    PlayerFruitEater.Eat(currentPlant.type);
                    currentPlant.Collect();
                }
                else
                {
                    InventoryManager.instance.AddFruit(currentPlant.type);
                    currentPlant.Collect();
                }
                
            }
            else if(!currentPlant.watered)
            {
                currentPlant.Water();
                FindObjectOfType<Player>().CostHealth(waterCost);
            }
        }
        else if(currentPlant.type == Plant.Type.None && hilightType != Plant.Type.None)
        {
            //Debug.Log(hilightType);
            InventoryManager.instance.UseSeed(hilightType);
            currentPlant.PlantSeed(hilightType);
        }

    }
}
