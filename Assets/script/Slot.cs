using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerUpHandler
{
    public int Slotnum; // ½½·Ô°³¼ö
    public Item Item;
    public Image Item_Icon; 

    public void UpdateSlotUI()
    {
        Item_Icon.sprite = Item.Item_Image;
        Item_Icon.gameObject.SetActive(true);
    }
    public void RemoveSlot()
    {
        Item = null;
        Item_Icon.gameObject.SetActive(false);
    }
    public void OnPointerUp(PointerEventData eventdata)
    {
        bool isUse = Item.Use();
        if(isUse)
        {
            Inventory.Instance.RemoveItem(Slotnum);
        }
    }
}
