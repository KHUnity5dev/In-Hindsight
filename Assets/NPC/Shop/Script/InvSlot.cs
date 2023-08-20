using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour //인벤토리 개별 슬롯 관리
{
    public ItemInfo item;
    public Image itemIcon;
    public Text itemName;
    public Text itemNum;

    public bool isShopMode;

    public void UpdateSlotUI(int Cnt)
    {
        itemIcon.sprite = item.itemImage;
        itemName.text = item.itemName;
        itemNum.text = Cnt.ToString();
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item = null;
        itemName.text = "";
        itemNum.text = "";
        itemIcon.gameObject.SetActive(false);
    }

    public void OnClick() // Inventory Slot Click
    {
        if (isShopMode) { return; }
        if (item != null)
        {
            Debug.Log(item.itemName + ", Cost: " + item.itemCost.ToString());
        }
    }
}
