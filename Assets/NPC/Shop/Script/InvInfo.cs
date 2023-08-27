using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InvInfo : MonoBehaviour //�κ��丮 ������ �� ���� ����
{
    public GameObject inventoryPanel;
    public GameObject sellPanel;
    private SellSlot sellSlot;
    private bool isActionInv = false;
    public bool isShopMode = false;

    private InvSlot[] slots; //�κ��丮 ���Ե�
    public Transform slotHolder; //�κ��丮 ������ �θ�ü

    public List<ItemInfo> Invenitems = new List<ItemInfo>(); //�κ��丮������ ����Ʈ
    public List<int> InvenCnt = new List<int>(); //�κ��丮 ������ ���� ����Ʈ
    private List<ItemInfo> SaveItemList;
    private List<int> SaveItemCnt;
    private int SaveMoney;
    public Text moneyText;
    public int StartMoney; //�÷��̾� ���� ��

    //**�̱���
    private static InvInfo instance = null; 
    private void Awake()
    {
        if (null == instance)
        {
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            //gameObject�����ε� �� ��ũ��Ʈ�� ������Ʈ�μ� �پ��ִ� Hierarchy���� ���ӿ�����Ʈ��� ��������, 
            //���� �򰥸� ������ ���� this�� �ٿ��ֱ⵵ �Ѵ�.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� InvenCanvas�� ������ ���� �ִ�.
            //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
            //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� InvenCanvas)�� �������ش�.
            Destroy(this.gameObject);
        }
    }
    public static InvInfo Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    //**
    void Start()
    {
        inventoryPanel.SetActive(isActionInv);
        sellSlot = sellPanel.GetComponentInChildren<SellSlot>();
        slots = slotHolder.GetComponentsInChildren<InvSlot>();
        PlayerPrefs.SetInt("money", StartMoney);
        Invenitems.Add(ItemDatabase.Instance.itemDB[0]); //�Ѿ� �κ��丮 ù��° ĭ�� ����
        InvenCnt.Add(10); // �Ѿ� ����
        RedrawSlotUI();
    }
    
    public void RedrawSlotUI() //�κ��丮 UI�׸���
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i = 0; i < Invenitems.Count; i++)
        {
            slots[i].item = Invenitems[i];
            slots[i].UpdateSlotUI(InvenCnt[i]);
        }
        moneyText.text = PlayerPrefs.GetInt("money").ToString();
    }

    public void AddItemToInv(ItemInfo item, int n) //����������, ������ �޾Ƽ� �κ��丮�� �߰�.
    {
        bool isAdd = true;
        for(int i = 0; i < Invenitems.Count; i++) //�κ��丮�� ������ �������ִ��� ã��
        {
            if (Invenitems[i].itemName == item.itemName)
            {
                InvenCnt[i] += n; //������ ������ ������ ���� �ø���
                isAdd = false;
                break;
            }
        }
        if (isAdd)
        {
            InvenCnt.Add(1);
            Invenitems.Add(item); //�κ��丮 ������ ����Ʈ�� ������ �߰�
        }
        RedrawSlotUI(); //UI ������Ʈ
    }

    public void RemoveItem(int index, int n) //������ ��ġ(index), �����ϰ����ϴ� ����(n)�� �޾� �κ��丮���� ����.
    {
        if (index == 0) //�Ѿ�(index=0)�� ��� ������ 0�̾ ������� �ʰ� ����ó��.
        {
            if (InvenCnt[index] - n < 0) //�Ѿ��� ������ ���
            {
                Debug.Log("�Ѿ� ����");
                RedrawSlotUI();
                return;
            }
            InvenCnt[index] -= n;
            RedrawSlotUI();
            return;
        } 
        if(InvenCnt[index] - n < 0)
        {
            Debug.Log("�������� �����մϴ�");
            RedrawSlotUI();
            return;
        }
        InvenCnt[index] -= n;

        if (InvenCnt[index] <= 0)
        {
            Invenitems.RemoveAt(index);
            InvenCnt.RemoveAt(index);
        }

        RedrawSlotUI();
    }

    public void Save() //���� �κ��丮 ���� ����
    {
        SaveItemList = Invenitems.ToList();
        SaveItemCnt = InvenCnt.ToList();
        SaveMoney = PlayerPrefs.GetInt("money");
        Debug.Log("SaveInventory");
    } 

    public void Load() //���� �ֱ� Save() �ߴ� �κ��丮 �ҷ�����
    {
        Invenitems = SaveItemList.ToList();
        InvenCnt = SaveItemCnt.ToList();

        PlayerPrefs.SetInt("money", SaveMoney);
        Debug.Log("LoadInventory");
        RedrawSlotUI();
    } 

    public void ActiveShopModeUI() //������� �κ��丮 �ѱ�
    {
        isShopMode = true;
        isActionInv = true;
        RedrawSlotUI();
        sellSlot.RemoveSlotUI();
        inventoryPanel.SetActive(isActionInv);
        sellPanel.SetActive(isActionInv);
    } 
    public void CloseShopModeUI() //������� �κ��丮 ����
    {
        isShopMode = false;
        isActionInv = false;
        inventoryPanel.SetActive(isActionInv);
        sellPanel.SetActive(isActionInv);
    } 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isShopMode)
        {
            if (!isActionInv) //�κ��丮�� �����ִٸ� �ѱ�
            {
                isActionInv = !isActionInv;
                RedrawSlotUI();
                inventoryPanel.SetActive(isActionInv);
                InvenCnt[0] = PlayerPrefs.GetInt("Bullets");
            }
            else //�κ��丮�� �����ִٸ� ����
            {
                isActionInv = !isActionInv;
                inventoryPanel.SetActive(isActionInv);
                PlayerPrefs.SetInt("Bullets", InvenCnt[0]);
            }

        }// IŰ�� �κ��丮UI �Ѱ����
        else if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isActionInv)
            {
                isActionInv = false;
                isShopMode = false;
                PlayerPrefs.SetInt("Bullets", InvenCnt[0]);
            }
            inventoryPanel.SetActive(isActionInv);
            sellPanel.SetActive(false);
        }// ESCŰ�� �κ��丮UI ����
    }
}
