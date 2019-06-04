using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlantOperate : MonoBehaviour
{
    public Dictionary<Plant.Type, int> seedCnt;
    public float waterCost = 30F;
    public float waterRecovery = 30F;
    public ScrollBar scrollBar;
    Player pl;
    Plant HLP() => PlantUIController.highlightedPlant;

    // Start is called before the first frame update
    void Start()
    {
        seedCnt = new Dictionary<Plant.Type, int>();
        seedCnt.Add(Plant.Type.Water, 0);
        seedCnt.Add(Plant.Type.Attack, 0);
        seedCnt.Add(Plant.Type.Consume, 0);
        AddSeed(Plant.Type.Water);
        pl = GetComponent<Player>();
    }
    public void TryPlant(Plant.Type seedType)
    {
        if (!seedCnt.ContainsKey(seedType))
            return;
        if (seedCnt[seedType] <= 0)
            return;
        seedCnt[seedType]--;
        InventoryManager.instance.useItem(Resources.Load<CollecableSeed>(CollecableSeed.seedDictionary[seedType]));
        HLP().PlantSeed(seedType);
    }
    public void TryWater()
    {
        if (HLP().watered || HLP().type == Plant.Type.None)
            return;
        if (pl.health <= waterCost)
            return;
        pl.CostHealth(waterCost);
        HLP().Water();
    }

    public void TryRemove()
    {
        if (HLP().age == 0 && HLP().type != Plant.Type.None)
            seedCnt[HLP().type]++;
        HLP().Remove();
    }
    public void TryCollect()
    {
        if (HLP().fruit <= 0)
            return;
        Debug.Log("Collect!");
        Plant.Type type = HLP().type;
        Debug.Log(type);
        HLP().Collect();
        switch(type)
        {
            case Plant.Type.Water:
                pl.AddHealth(waterRecovery);
                break;

            case Plant.Type.Attack:
                pl.atkBuffCnt++;
                scrollBar.SetATK(pl.atkBuffCnt);
                scrollBar.ShowScroll();
                break;

            case Plant.Type.Consume:
                pl.csmBuffCnt++;
                scrollBar.SetCOST(pl.csmBuffCnt);
                scrollBar.ShowScroll();
                break;
        }
    }
    public void AddSeed(Plant.Type seedType)
    {
        CollecableSeed temp = ScriptableObject.CreateInstance("CollecableSeed") as CollecableSeed;
        temp = Resources.Load<CollecableSeed>(CollecableSeed.seedDictionary[seedType]);
        InventoryManager.instance.addItem(temp);
        seedCnt[seedType]++;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
            AddSeed(Plant.Type.Water);
        if(Input.GetKeyDown(KeyCode.K))
            AddSeed(Plant.Type.Attack);
        if(Input.GetKeyDown(KeyCode.L))
            AddSeed(Plant.Type.Consume);
    }
}
