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

    void Awake()
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
    // Start is called before the first frame update
    void Start()
    {
        
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
        if(Input.GetKeyDown(KeyCode.Q))
        {
            seeds.tryPlantSeed(hilightCode);
        }

        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
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
}
