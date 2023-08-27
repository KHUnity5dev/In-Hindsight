using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopData : MonoBehaviour
{
    public List<ItemInfo> stocks = new List<ItemInfo>();
    public bool[] soldOuts;

    void Start()
    {
        //임시로 아이템DB의 1,2번째 아이템 추가
        stocks.Add(ItemDatabase.Instance.itemDB[0]);
        stocks.Add(ItemDatabase.Instance.itemDB[1]);

        soldOuts = new bool[stocks.Count];
        for(int i = 0; i < soldOuts.Length; i++)
        {
            soldOuts[i] = false;
        }
    }
}
