using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlantOperate : MonoBehaviour
{
    public Dictionary<Plant.Type, int> seedCnt;
    public float waterCost = 30F;
    Player pl;
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
        PlantUIController.highlightedPlant.PlantSeed(seedType);
    }
    public void TryWater()
    {
        if (PlantUIController.highlightedPlant.watered || PlantUIController.highlightedPlant.type == Plant.Type.None)
            return;
        if (pl.health <= waterCost)
            return;
        pl.CostHealth(waterCost);
        PlantUIController.highlightedPlant.Water();
    }

    public void TryRemove()
    {
        if (PlantUIController.highlightedPlant.age == 0 && PlantUIController.highlightedPlant.type != Plant.Type.None)
            seedCnt[PlantUIController.highlightedPlant.type]++;
        PlantUIController.highlightedPlant.Remove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
