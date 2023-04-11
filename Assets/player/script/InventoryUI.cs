using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory Inven; // �κ��丮�� ������
    public GameObject Inventory_Panel;
    bool activeInventory = false; // �κ��丮�� Ȱ��ȭ ����

    public Slot[] Slots;
    public Transform Slotholder; // slot���� �������ִ� �ν��Ͻ�

    private void Start()
    {
        Inven = Inventory.Instance;
        Slots =Slotholder.GetComponentsInChildren<Slot>(); // �ڽ� ������Ʈ�� ��� ������
        Inven.onSlotCountChange += SlotChange;
        Inven.onChangeItem += RedrawSlotUI;
        Inventory_Panel.SetActive(activeInventory);
    }

    private void SlotChange(int val)
    {
        for (int i = 0; i < Slots.Length; i++) // SlotCnt ��ŭ�� ���� Ȱ��ȭ
        {
            Slots[i].Slotnum = i; //�� ������ �������� ��ȣ
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
