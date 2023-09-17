using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopData : MonoBehaviour
{
    public List<ItemInfo> stocks = new List<ItemInfo>();
    public bool[] soldOuts;

    void Start()
    {
        //�ӽ÷� ������DB�� 1,2��° ������ �߰�
        stocks.Add(ItemDatabase.Instance.itemDB[0]);
        stocks.Add(ItemDatabase.Instance.itemDB[1]);
    
        soldOuts = new bool[stocks.Count];
        for(int i = 0; i < soldOuts.Length; i++)
        {
            soldOuts[i] = false;
        }
    }
}
