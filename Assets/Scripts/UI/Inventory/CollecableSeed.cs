using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Seed Item", menuName = "Item/Seed Item")]
public class CollecableSeed : CollectableItem
{
    public static Dictionary<Plant.Type, string> seedDictionary = new Dictionary<Plant.Type, string>(){
        {Plant.Type.Aquabud, "ItemAsset/Seeds/Aquabud"},
        {Plant.Type.Mulberry, "ItemAsset/Seeds/Mulberry"},
        {Plant.Type.Cyanberry, "ItemAsset/Seeds/Cyanberry"},
        {Plant.Type.Goji, "ItemAsset/Seeds/Goji"},
        {Plant.Type.Lime, "ItemAsset/Seeds/Lime"},
        {Plant.Type.Cloudberry, "ItemAsset/Seeds/Cloudberry"}
    };

    public Plant.Type seedType;
}
