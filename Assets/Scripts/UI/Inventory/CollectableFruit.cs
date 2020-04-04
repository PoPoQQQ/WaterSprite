using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fruit Item", menuName = "Item/Fruit Item")]
public class CollectableFruit : CollectableItem
{
    public static Dictionary<Plant.Type, string> FruitDictionary = new Dictionary<Plant.Type, string>(){
        {Plant.Type.Aquabud, "ItemAsset/Fruits/Aquabud"},
        {Plant.Type.Cyanberry, "ItemAsset/Fruits/Cyanberry"},
        {Plant.Type.Goji, "ItemAsset/Fruits/Goji"},
        {Plant.Type.Mulberry, "ItemAsset/Fruits/Mulberry"},
        {Plant.Type.Wisplum, "ItemAsset/Fruits/Wisplum"},
        {Plant.Type.Lime, "ItemAsset/Fruits/Lime"},
        {Plant.Type.Cloudberry, "ItemAsset/Fruits/Cloudberry"},
        {Plant.Type.Dragonfruit, "ItemAsset/Fruits/Dragonfruit"},
        {Plant.Type.Jujube, "ItemAsset/Fruits/Jujube"},
        {Plant.Type.Persimmon, "ItemAsset/Fruits/Persimmon"},
        {Plant.Type.Miracle, "ItemAsset/Fruits/Miracle"},
        {Plant.Type.Bubble, "ItemAsset/Fruits/Bubble"},
        {Plant.Type.Turret, "ItemAsset/Fruits/Turret"},
        {Plant.Type.Mango, "ItemAsset/Fruits/Mango"},
        {Plant.Type.Lychee, "ItemAsset/Fruits/Lychee"}
    };
}
