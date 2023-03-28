using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumables,
    Etc
}

[System.Serializable]
public class Item
{
    public ItemType Item_Type;
    public string Item_Name;
    public Sprite Item_Image;
    public List<Itemeffect> effects; //������ ȿ������ ���� ����Ʈ
    public bool Use()
    {
        bool isUsed = false;
        foreach (Itemeffect eft in effects)
        {
            isUsed = eft.ExecuteRole();
        }
        return isUsed;
    }
}
