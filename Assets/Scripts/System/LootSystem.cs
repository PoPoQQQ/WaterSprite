using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour
{
    public Dictionary<Plant.Type, float> lootChanceMultiplier;
    public Dictionary<Plant.Type, int> lootCnt;
    void Start()
    {

        lootChanceMultiplier = new Dictionary<Plant.Type, float>
        {
            { Plant.Type.Aquabud, 1F },
            { Plant.Type.Cyanberry, 1F },
            { Plant.Type.Goji, 1F },
            { Plant.Type.Mulberry, 1F },
            { Plant.Type.Wisplum, 1F },
            { Plant.Type.Lime, 1F },
            { Plant.Type.Cloudberry, 1F },
            { Plant.Type.Dragonfruit, 1F },
            { Plant.Type.Jujube, 1F },
            { Plant.Type.Persimmon, 1F },
        };

        lootCnt = new Dictionary<Plant.Type, int>
        {
            { Plant.Type.Aquabud, 0 },
            { Plant.Type.Cyanberry, 0 },
            { Plant.Type.Goji, 0 },
            { Plant.Type.Mulberry, 0 },
            { Plant.Type.Wisplum, 0 },
            { Plant.Type.Lime, 0 },
            { Plant.Type.Cloudberry, 0 },
            { Plant.Type.Dragonfruit, 0 },
            { Plant.Type.Jujube, 0 },
            { Plant.Type.Persimmon, 0 },
        };

    }

    public void LootSeed(Vector3 pos, Plant.Type type, float chance)
    {
        if (type == Plant.Type.Goji || type == Plant.Type.Mulberry || type == Plant.Type.Wisplum)
            chance *= 1.0F - 0.15F * lootCnt[type];

        lootChanceMultiplier[type] += chance;

        float r = Random.Range(0F, 1F);
        if(r <= lootChanceMultiplier[type] * chance)
        {
            SeedItem.Generate(pos, type);
            lootChanceMultiplier[type] = 0.5F;
            lootCnt[type]++;
        }
    }

}
