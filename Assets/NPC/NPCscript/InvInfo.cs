using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvInfo : MonoBehaviour
{
    Inven inven;

    public GameObject inventoryPanel;

    public InvSlot[] slots;
    public Transform slotHolder;
    

    void Start()
    {
        inven = Inven.instance;
        slots = slotHolder.GetComponentsInChildren<InvSlot>();
        inven.onChangeItem += RedrawSlotUI;
    }
    
    void RedrawSlotUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
