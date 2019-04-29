using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlantOperate : MonoBehaviour
{
    public Dictionary<Plant.Type, int> seedCnt;
    public float waterCost = 30F;
    public float waterRecovery = 30F;
    Player pl;
    Plant HLP() => PlantUIController.highlightedPlant;

    // Start is called before the first frame update
    void Start()
    {
        seedCnt = new Dictionary<Plant.Type, int>();
        seedCnt.Add(Plant.Type.Water, 0);
        seedCnt.Add(Plant.Type.Attack, 0);
        seedCnt.Add(Plant.Type.Consume, 0);
        seedCnt[Plant.Type.Water]++;
        pl = GetComponent<Player>();
    }
    public void TryPlant(Plant.Type seedType)
    {
        if (!seedCnt.ContainsKey(seedType))
            return;
        if (seedCnt[seedType] <= 0)
            return;
        seedCnt[seedType]--;
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
        Debug.Log(HLP().type);
        HLP().Collect();
        switch(HLP().type)
        {
            case Plant.Type.Water:
                pl.AddHealth(waterRecovery);
                break;

            case Plant.Type.Attack:
                pl.atkBuffCnt++;
                break;

            case Plant.Type.Consume:
                pl.csmBuffCnt++;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
