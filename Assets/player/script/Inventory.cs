using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    public delegate void OnSlotCountChange(int val); // 대리자 정의 슬롯 카운트가 바뀌면 알려줌
    public OnSlotCountChange onSlotCountChange; // 대리자 인스턴스

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> Items =new List<Item>();
    
    private int slotCnt;

    
    public static int Bullets = 100;
    public static int Maxmagazine = 10;
    public static int Magazine = 10;
    public int SlotCnt
    {
        get { return slotCnt; }
        set 
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }
    private void Start()
    {
        SlotCnt = 4;
    }
    public void Get_Item(int bullets)
    {
        Bullets += bullets;
    }
    public bool AddItem(Item _item)
    {
        if(Items.Count < slotCnt)
        {
            Items.Add(_item);
            if (onChangeItem != null)
            {
                onChangeItem.Invoke();
            }
            return true;
        }
        return false;
    }
    public void RemoveItem(int _index)
    {
        Items.RemoveAt(_index);
        onChangeItem.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("FieldItem"))
        {
            FieldItems fielditems =collision.GetComponent<FieldItems>();
            if (AddItem(fielditems.GetItem()))
                fielditems.DestroyItem();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
