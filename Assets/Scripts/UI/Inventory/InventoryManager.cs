using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Inventory seeds;
    public Inventory items;
    Player pl;

    void Awake()
    {
        if(instance == null)
            instance = this;
        
        seeds = transform.Find("Seeds").gameObject.GetComponent<Inventory>();
        Transform deb1 = transform.Find("Items");
        GameObject deb2 = deb1.gameObject;
        items = transform.Find("Items").gameObject.GetComponent<Inventory>();
        pl = GetComponent<Player>();
    }

    void Start()
    {
        /*
        AddSeed(Plant.Type.Aquabud);
        AddSeed(Plant.Type.Goji);
        AddSeed(Plant.Type.Mulberry);
        */
    }

    public void addItem(CollectableItem temp)
    {
        if(temp.itemType==CollectableItem.ItemType.seed)
            seeds.AddItem(temp);
        if(temp.itemType==CollectableItem.ItemType.item)
            items.AddItem(temp);
    }

    public void useItem(CollectableItem temp)
    {
        Debug.Log(temp.seedType);
        
        if(temp.itemType==CollectableItem.ItemType.seed)
            seeds.RemoveItem(temp);
        if(temp.itemType==CollectableItem.ItemType.item)
            items.RemoveItem(temp);
    }

    public void AddSeed(Plant.Type seedType)
    {
        CollecableSeed temp = ScriptableObject.CreateInstance("CollecableSeed") as CollecableSeed;
        temp = Resources.Load<CollecableSeed>(CollecableSeed.seedDictionary[seedType]);
        addItem(temp);
    }

    public void UseSeed(Plant.Type seedType)
    {
        CollecableSeed temp = ScriptableObject.CreateInstance("CollecableSeed") as CollecableSeed;
        temp = Resources.Load<CollecableSeed>(CollecableSeed.seedDictionary[seedType]);
        useItem(temp);
    }

    public void AddFruit(Plant.Type seedType)
    {
        CollectableFruit temp = ScriptableObject.CreateInstance("CollectableFruit") as CollectableFruit;
        temp = Resources.Load<CollectableFruit>(CollectableFruit.FruitDictionary[seedType]);
        InventoryManager.instance.addItem(temp);
    }

    public void UseFruit(Plant.Type seedType)
    {
        CollecableSeed temp = ScriptableObject.CreateInstance("CollecableSeed") as CollecableSeed;
        temp = Resources.Load<CollecableSeed>(CollectableFruit.FruitDictionary[seedType]);
        useItem(temp);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            AddSeed(Plant.Type.Bubble);
            AddFruit(Plant.Type.Bubble);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            AddSeed(Plant.Type.Cloudberry);
            AddFruit(Plant.Type.Cloudberry);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            AddSeed(Plant.Type.Cyanberry);
            AddFruit(Plant.Type.Cyanberry);
        }

    }
}
