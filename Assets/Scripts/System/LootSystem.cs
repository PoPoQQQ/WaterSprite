using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour
{
    public Dictionary<Plant.Type, float> lootChanceMultiplier;
    public Dictionary<Plant.Type, float> basicLootChance;
    public Dictionary<Plant.Type, int> lootCnt;
    void Start()
    {

        lootChanceMultiplier = new Dictionary<Plant.Type, float>
        {
            { Plant.Type.Aquabud, 2F },
            { Plant.Type.Goji, 2F },
            { Plant.Type.Mulberry, 2F },
            { Plant.Type.Wisplum, 1F },
            { Plant.Type.Cyanberry, 1F },
            { Plant.Type.Lychee, 1F },
            { Plant.Type.Mango, 1F },
            { Plant.Type.Lime, 1F },
            { Plant.Type.Cloudberry, 1F },
            { Plant.Type.Dragonfruit, 1F },
            { Plant.Type.Jujube, 1F },
            { Plant.Type.Persimmon, 1F },
            { Plant.Type.Turret, 1F },
            { Plant.Type.Bubble, 1F },
        };

        basicLootChance = new Dictionary<Plant.Type, float>
        {
            { Plant.Type.Aquabud, 1F },
            { Plant.Type.Goji, 1F },
            { Plant.Type.Mulberry, 1F },
            { Plant.Type.Wisplum, 1F },
            { Plant.Type.Cyanberry, Random.Range(0.5F,1.5F) },
            { Plant.Type.Lychee, Random.Range(0.5F,1.5F) },
            { Plant.Type.Mango, Random.Range(0.5F,1.5F) },
            { Plant.Type.Lime, Random.Range(0.5F,1.5F) },
            { Plant.Type.Cloudberry, Random.Range(0.5F,1.5F) },
            { Plant.Type.Dragonfruit, Random.Range(0.5F,1.5F) },
            { Plant.Type.Jujube, Random.Range(0.5F,1.5F) },
            { Plant.Type.Persimmon, Random.Range(0.5F,1.5F) },
            { Plant.Type.Turret, Random.Range(0.6F,1.4F) },
            { Plant.Type.Bubble, Random.Range(0.6F,1.4F) },
        };

        float t = basicLootChance[Plant.Type.Cyanberry] + basicLootChance[Plant.Type.Mango] + basicLootChance[Plant.Type.Lychee];
        basicLootChance[Plant.Type.Cyanberry] *= 3 / t;
        basicLootChance[Plant.Type.Mango] *= 3 / t;
        basicLootChance[Plant.Type.Lychee] *= 3 / t;

        t = basicLootChance[Plant.Type.Lime] + basicLootChance[Plant.Type.Cloudberry] + basicLootChance[Plant.Type.Dragonfruit]
            + basicLootChance[Plant.Type.Jujube]+ basicLootChance[Plant.Type.Persimmon];
        basicLootChance[Plant.Type.Cloudberry] *= 5 / t;
        basicLootChance[Plant.Type.Lime] *= 5 / t;
        basicLootChance[Plant.Type.Dragonfruit ] *= 5 / t;
        basicLootChance[Plant.Type.Jujube] *= 5 / t;
        basicLootChance[Plant.Type.Persimmon] *= 5 / t;

        t = basicLootChance[Plant.Type.Bubble] + basicLootChance[Plant.Type.Turret];
        basicLootChance[Plant.Type.Turret] *= 2 / t;
        basicLootChance[Plant.Type.Bubble] *= 2 / t;


        lootCnt = new Dictionary<Plant.Type, int>
        {
            { Plant.Type.Aquabud, 0 },
            { Plant.Type.Goji, 0 },
            { Plant.Type.Mulberry, 0 },
            { Plant.Type.Wisplum, 0 },
            { Plant.Type.Cyanberry, 0 },
            { Plant.Type.Lychee, 0 },
            { Plant.Type.Mango, 0 },
            { Plant.Type.Lime, 0 },
            { Plant.Type.Cloudberry, 0 },
            { Plant.Type.Dragonfruit, 0 },
            { Plant.Type.Jujube, 0 },
            { Plant.Type.Persimmon, 0 },
            { Plant.Type.Turret, 0 },
            { Plant.Type.Bubble, 0 },
        };

    }

    public void LootSeed(Vector3 pos, Plant.Type type, float chance)
    {
        if (type == Plant.Type.None || type == Plant.Type.Withered || chance <= 0F)
            return;

        float p = 0F, r = 0F;
        if (type == Plant.Type.Aquabud)
        {
            r = 0.015F * (lootCnt[Plant.Type.Aquabud] - 15);
            r = Mathf.Clamp(r, 0F, 0.6F);
            if(Random.Range(0F,1F)<=r)
            {
                type = Random.Range(0F, 2F) < 1F ? Plant.Type.Turret : Plant.Type.Bubble;
            }
        }
        if (type == Plant.Type.Goji)
        {
            r = 0.2F * lootCnt[Plant.Type.Goji];
            if (Random.Range(0F, 1F) <= r)
            {
                p = Random.Range(0F, 3F);
                if (p < 1F)
                    type = Plant.Type.Dragonfruit;
                else if (p < 2F)
                    type = Plant.Type.Jujube;
                else
                    type = Plant.Type.Persimmon;
            }
        }
        if(type == Plant.Type.Mulberry)
        {
            r = 0.2F * lootCnt[Plant.Type.Mulberry];
            if (Random.Range(0F, 1F) <= r)
            {
                p = Random.Range(0F, 3F);
                if (p < 1F)
                    type = Plant.Type.Lychee;
                else if (p < 2F)
                    type = Plant.Type.Cyanberry;
                else
                    type = Plant.Type.Mango;
            }
        }
        if (type == Plant.Type.Wisplum)
        {
            r = 0.33334F * lootCnt[Plant.Type.Wisplum];
            if (Random.Range(0F, 1F) <= r)
            {
                type = Random.Range(0F, 2F) < 1F ? Plant.Type.Lime : Plant.Type.Cloudberry;
            }
        }

        lootChanceMultiplier[type] += chance * basicLootChance[type];

        float rand = Random.Range(0F, 1F);
        if(rand <= lootChanceMultiplier[type] * chance)
        {
            SeedItem.Generate(pos, type);
            lootChanceMultiplier[type] = 0.8F;
            lootCnt[type]++;
        }
    }
    
}
