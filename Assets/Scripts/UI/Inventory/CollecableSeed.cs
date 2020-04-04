using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Seed Item", menuName = "Item/Seed Item")]
public class CollecableSeed : CollectableItem
{
    public static Dictionary<Plant.Type, string> seedDictionary = new Dictionary<Plant.Type, string>(){
        {Plant.Type.Aquabud, "ItemAsset/Seeds/Aquabud"},
        {Plant.Type.Cyanberry, "ItemAsset/Seeds/Cyanberry"},
        {Plant.Type.Goji, "ItemAsset/Seeds/Goji"},
        {Plant.Type.Mulberry, "ItemAsset/Seeds/Mulberry"},
        {Plant.Type.Wisplum, "ItemAsset/Seeds/Wisplum"},
        {Plant.Type.Lime, "ItemAsset/Seeds/Lime"},
        {Plant.Type.Cloudberry, "ItemAsset/Seeds/Cloudberry"},
        {Plant.Type.Dragonfruit, "ItemAsset/Seeds/Dragonfruit"},
        {Plant.Type.Jujube, "ItemAsset/Seeds/Jujube"},
        {Plant.Type.Persimmon, "ItemAsset/Seeds/Persimmon"},
        {Plant.Type.Miracle, "ItemAsset/Seeds/Miracle"},
        {Plant.Type.Bubble, "ItemAsset/Seeds/Bubble"},
        {Plant.Type.Turret, "ItemAsset/Seeds/Turret"},
        {Plant.Type.Mango, "ItemAsset/Seeds/Mango"},
        {Plant.Type.Lychee, "ItemAsset/Seeds/Lychee"}
    };
}
