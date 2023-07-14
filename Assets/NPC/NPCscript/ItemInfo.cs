using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType1
{
    Equipment,
    Consumables,
    Etc
}

[System.Serializable]
public class ItemInfo
{
    public ItemType1 itemtype;
    public string itemName;
    public Sprite itemImage;
    public int itemCost;

    public bool Use()
    {
        return false;
    }
}
