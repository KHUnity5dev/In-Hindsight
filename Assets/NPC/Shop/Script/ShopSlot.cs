using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.UIElements;

public class ShopSlot : MonoBehaviour //���� ���� ���� ����
{
    public int slotnum;
    public ItemInfo item;
    public Image itemIcon;
    public Text itemText;
    public Text itemCost;
    public bool soldOut = false;

    public BuySlot buySlot;

    public void UpdateSlotUI()
    {
        if(item == null) { 
            itemIcon.gameObject.SetActive(false);
            itemText.text = "";
            itemCost.text = "";
            GetComponent<Button>().interactable = false;
        }
        else {
            itemIcon.sprite = item.itemImage;
            itemText.text = item.itemName;
            itemCost.text = item.itemCost.ToString();
            itemIcon.gameObject.SetActive(true);
        }
        
    }
    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        buySlot.item = item;
        buySlot.UpdateSlotUI();
    }

    /* ���콺 ������ ���� �ø��� ������ ���� ������.. �Ҽ�������
    public void OnPointerUp(PointerEventData eventData)
    {
        if (item != null)
        {
            if (ItemDatabase.instance.money >= item.itemCost && !soldOut)
            {
                ItemDatabase.instance.money -= item.itemCost;
                Inven.instance.AddItem(item);
            }
        }
    }
    */
}
