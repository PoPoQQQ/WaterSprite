using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour
{
    public Dictionary<Plant.Type, float> lootChance;
    public Dictionary<Plant.Type, int> lootCnt;
    void Start()
    {

        lootChance = new Dictionary<Plant.Type, float>();
        lootChance.Add(Plant.Type.Water, 1F);
        lootChance.Add(Plant.Type.Attack, 1F);
        lootChance.Add(Plant.Type.Consume, 1F);

        lootCnt = new Dictionary<Plant.Type, int>();
        lootCnt.Add(Plant.Type.Water, 0);
        lootCnt.Add(Plant.Type.Attack, 0);
        lootCnt.Add(Plant.Type.Consume, 0);

    }

    public void LootSeed(Vector3 pos, Plant.Type type, float chanceIncrement)
    {
        if (type == Plant.Type.Attack || type == Plant.Type.Consume)
            chanceIncrement *= 1.0F - 0.15F * lootCnt[type];

        lootChance[type] += chanceIncrement;
        float r = Random.Range(0F, 1F);
        if(r <= lootChance[type])
        {
            SeedItem.Generate(pos, type);
            lootChance[type] = 0;
            lootCnt[type]++;
        }
    }

}
