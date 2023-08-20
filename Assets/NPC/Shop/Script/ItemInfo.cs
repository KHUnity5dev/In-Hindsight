using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ ������ Ŭ����

public enum ItemType1 //������ Ÿ��
{
    Equipment,
    Consumables,
    Etc
}

[System.Serializable]
public class ItemInfo //������ Ŭ����
{
    public ItemType1 itemtype;
    public int itemID;
    public string itemName;
    public Sprite itemImage;
    public int itemCost;
    public string itemDescription;
    public int itemCnt;
    

    public ItemInfo(ItemType1 _itemtype, int _itemID, string _itemName, int _itemCost, string _itemDes, int _itemCnt = 1)
    {   
        itemtype = _itemtype;
        itemID = _itemID;
        itemName = _itemName;
        itemCost = _itemCost;
        itemDescription = _itemDes;
        itemCnt = _itemCnt;
        //itemImage = Resources.Load("");// ������ �̹����� ID�� �Ȱ��� �ؼ� �ҷ����� ���ϰ� ��?
    }
    /*
    public bool Use()
    {
        return false;
    }
    */
}
