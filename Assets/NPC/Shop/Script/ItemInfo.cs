using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템 데이터 클래스

public enum ItemType1 //아이템 타입
{
    Equipment,
    Consumables,
    Etc
}

[System.Serializable]
public class ItemInfo //아이템 클래스
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
        //itemImage = Resources.Load("");// 아이템 이미지는 ID와 똑같이 해서 불러오기 편하게 함?
    }
    /*
    public bool Use()
    {
        return false;
    }
    */
}
