using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour //ItemDB, ���ӻ��� �����۵����� ����
{
    public static ItemDatabase instance;

    private void Awake()
    {
        instance = this;
        //itemDB.Add(new ItemInfo(ItemType1.Equipment, 100, "�Ѿ�", 50, "���� ����ϱ� ���� �ʿ��� �Ѿ��̴�."))
    }
    public List<ItemInfo> itemDB = new List<ItemInfo>();
}
