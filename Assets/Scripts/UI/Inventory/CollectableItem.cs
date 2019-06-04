using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : ScriptableObject
{
    //public delegate void useItem();
    //public useItem useCallBack;
    public enum ItemType {seed, item, collection};

    [Header("item information")]
    public string name;
    public Sprite icon;
    public ItemType itemType;
}
