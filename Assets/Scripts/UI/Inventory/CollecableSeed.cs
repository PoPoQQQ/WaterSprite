using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Seed Item", menuName = "Item/Seed Item")]
public class CollecableSeed : CollectableItem
{
    public static Dictionary<Plant.Type, string> seedDictionary = new Dictionary<Plant.Type, string>(){
        {Plant.Type.Water, "ItemAsset/Seeds/WaterSeed"},
        {Plant.Type.Attack, "ItemAsset/Seeds/AtkSeed"}, 
        {Plant.Type.Consume, "ItemAsset/Seeds/CsmSeed"}
    };




    public Plant.Type seedType;
}
