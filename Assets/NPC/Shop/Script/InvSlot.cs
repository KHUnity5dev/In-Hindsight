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
    public SellSlot sellSlot;

    public void UpdateSlotUI(int Cnt) //슬롯 UI 업데이트, Cnt는 아이템 수량
    {
        itemIcon.sprite = item.itemImage;
        itemName.text = item.itemName;
        itemNum.text = Cnt.ToString();
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot() //슬롯 초기화
    {
        item = null;
        itemName.text = "";
        itemNum.text = "";
        itemIcon.gameObject.SetActive(false);
    }

    public void OnClick() // Inventory Slot Click
    {
        if (item == null) //빈 슬롯 클릭
        {
            Debug.Log("아이템이 없습니다.");
        }

        else if (InvInfo.Instance.isShopMode) //상점에서 아이템 클릭
        {
            if(sellSlot.item == null || sellSlot.item != item) //판매슬롯의 아이템이 클릭한 아이템과 다른경우
            {
                sellSlot.item = item;
                sellSlot.UpdateSlotUI();
            }
            else
            {
                sellSlot.UpdateSlotUI();
            }
        }
        else //그 외 아이템 클릭 -> 지금은 클릭하면 수량이 -1되도록 했습니다.
        {
            int index = InvInfo.Instance.Invenitems.IndexOf(item);
            InvInfo.Instance.RemoveItem(index, 1);
        }
        InvInfo.Instance.RedrawSlotUI();
    }
}
