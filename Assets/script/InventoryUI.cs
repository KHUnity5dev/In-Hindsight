using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory Inven; // 인벤토리를 가져옴
    public GameObject Inventory_Panel;
    bool activeInventory = false; // 인벤토리의 활성화 여부

    public Slot[] Slots;
    public Transform Slotholder; // slot들을 가지고있는 인스턴스

    private void Start()
    {
        Inven = Inventory.Instance;
        Slots =Slotholder.GetComponentsInChildren<Slot>(); // 자식 오브젝트를 모두 가져옴
        Inven.onSlotCountChange += SlotChange;
        Inven.onChangeItem += RedrawSlotUI;
        Inventory_Panel.SetActive(activeInventory);
    }

    private void SlotChange(int val)
    {
        for (int i = 0; i < Slots.Length; i++) // SlotCnt 만큼만 슬롯 활성화
        {
            Slots[i].Slotnum = i; //각 슬롯의 아이템의 번호
            if (i < Inven.SlotCnt)
                Slots[i].GetComponent<Button>().interactable = true;
            else
                Slots[i].GetComponent<Button>().interactable = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            Inventory_Panel.SetActive(activeInventory);
        }
    }
    public void AddSlot()
    {
        Inven.SlotCnt++;
    }
    void RedrawSlotUI()
    {
        for(int i = 0; i<Slots.Length;i++)
        {
            Slots[i].RemoveSlot();
        }
        for (int i = 0; i < Inven.Items.Count; i++)
        {
            Slots[i].Item = Inven.Items[i];
            Slots[i].UpdateSlotUI();
        }
    }
}
