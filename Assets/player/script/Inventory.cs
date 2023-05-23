using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    private int sight;
    private int head;
    private int magazine;
    private float damage = 10;
    private float gunnoise = 20;
    public int Sight
    {
        get { return sight; }
        set { sight = value; }
    }
    public int Head { 
        get { return head;}
        set { head = value; }
    }
    public int Magazine
    {
        get { return magazine; }
        set { magazine = value; }
    }
    public float Damage()
    {
        float AllDamage = damage;
        if(head == 1)
        {
            AllDamage += 10;
        }
        return AllDamage;
    }
    public float Gun_Noise()
    {
        float AllNoise = gunnoise;
        if (head == 2)
            AllNoise -= 10;
        return AllNoise;
    }
}
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

    public static Gun MyGun = new Gun();
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
