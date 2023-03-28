using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item Item;
    public SpriteRenderer Image;

    public void SetItem(Item _item)
    {
        Item.Item_Name = _item.Item_Name;
        Item.Item_Image = _item.Item_Image;
        Item.Item_Type = _item.Item_Type;
        Item.effects = _item.effects;
        Image.sprite = _item.Item_Image;
    }
    public Item GetItem()
    {
        return Item;
    }
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
