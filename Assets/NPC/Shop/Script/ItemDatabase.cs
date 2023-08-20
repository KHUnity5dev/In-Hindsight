using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour //ItemDB, 게임상의 아이템데이터 저장
{
    public static ItemDatabase instance;

    private void Awake()
    {
        instance = this;
        //itemDB.Add(new ItemInfo(ItemType1.Equipment, 100, "총알", 50, "총을 사용하기 위해 필요한 총알이다."))
    }
    public List<ItemInfo> itemDB = new List<ItemInfo>();
}
