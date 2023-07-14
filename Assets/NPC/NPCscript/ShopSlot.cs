using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.UIElements;

public class ShopSlot : MonoBehaviour
{
    public int slotnum;
    public ItemInfo item;
    public Image itemIcon;
    public bool soldOut = false;
    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }
    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

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
}
