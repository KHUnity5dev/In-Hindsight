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

        }
        else //�� �� ������ Ŭ�� -> ������ Ŭ���ϸ� ������ -1�ǵ��� �߽��ϴ�.
        {
            int index = InvInfo.Instance.Invenitems.IndexOf(item);
            InvInfo.Instance.InvenCnt[index] -= 1;
            if (InvInfo.Instance.InvenCnt[index] == 0)
            {
                InvInfo.Instance.Invenitems.RemoveAt(index);
                InvInfo.Instance.InvenCnt.RemoveAt(index);
            }
        }
        InvInfo.Instance.RedrawSlotUI();
    }
}
