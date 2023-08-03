using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    // public int money; -> µ·Àº PlayerPrefs·Î..
    private void Awake()
    {
        instance = this;
    }
    public List<ItemInfo> itemDB = new List<ItemInfo>();
}
