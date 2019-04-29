using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlantOperate : MonoBehaviour
{
    public Dictionary<Plant.Type, int> seedCnt;
    float plantCost = 30F;
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
    public void TryPlant(Plant plant, Plant.Type seedType)
    {
        if (!seedCnt.ContainsKey(seedType))
            return;
        if (seedCnt[seedType] <= 0)
            return;
        if (pl.health <= plantCost)
            return;
        pl.CostHealth(plantCost);
        plant.PlantSeed(seedType);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
