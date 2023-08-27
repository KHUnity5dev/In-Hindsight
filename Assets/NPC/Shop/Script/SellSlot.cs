using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellSlot : MonoBehaviour //�κ��丮 ������� �Ǹ� ����
{
    public ItemInfo item;
    public Image itemIcon;
    public Text itemName;
    public Text itemCost;

    private int sellNum = 1;

    public void Start()
    {
        item = null;
        RemoveSlotUI();
    }

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemName.text = item.itemName;
        itemCost.text = item.itemCost.ToString();
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlotUI()
    {
        sellNum = 1;
        itemIcon.gameObject.SetActive(false);
        itemName.text = "";
        itemCost.text = "";
        item = null;
    }

    public void ClickSellButton()
    {
        if (itemIcon.gameObject.activeSelf)
        {
            int index = InvInfo.Instance.Invenitems.IndexOf(item);
            int money = PlayerPrefs.GetInt("money");
            if (InvInfo.Instance.InvenCnt[index] - sellNum < 0)
            {
                Debug.Log("�ǸŰ��ɼ����� �ʰ��߽��ϴ�.");
                return;
            } 
            PlayerPrefs.SetInt("money", money + item.itemCost * sellNum);
            InvInfo.Instance.RemoveItem(index, sellNum);
            RemoveSlotUI();
        }

    }
}
