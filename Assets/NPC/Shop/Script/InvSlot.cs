using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour //�κ��丮 ���� ���� ����
{
    public ItemInfo item;
    public Image itemIcon;
    public Text itemName;
    public Text itemNum;
    public SellSlot sellSlot;

    public void UpdateSlotUI(int Cnt) //���� UI ������Ʈ, Cnt�� ������ ����
    {
        itemIcon.sprite = item.itemImage;
        itemName.text = item.itemName;
        itemNum.text = Cnt.ToString();
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot() //���� �ʱ�ȭ
    {
        item = null;
        itemName.text = "";
        itemNum.text = "";
        itemIcon.gameObject.SetActive(false);
    }

    public void OnClick() // Inventory Slot Click
    {
        if (item == null) //�� ���� Ŭ��
        {
            Debug.Log("�������� �����ϴ�.");
        }

        else if (InvInfo.Instance.isShopMode) //�������� ������ Ŭ��
        {
            if(sellSlot.item == null || sellSlot.item != item) //�ǸŽ����� �������� Ŭ���� �����۰� �ٸ����
            {
                sellSlot.item = item;
                sellSlot.UpdateSlotUI();
            }
            else
            {
                sellSlot.UpdateSlotUI();
            }
        }
        else //�� �� ������ Ŭ�� -> ������ Ŭ���ϸ� ������ -1�ǵ��� �߽��ϴ�.
        {
            int index = InvInfo.Instance.Invenitems.IndexOf(item);
            InvInfo.Instance.RemoveItem(index, 1);
        }
        InvInfo.Instance.RedrawSlotUI();
    }
}
