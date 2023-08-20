using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InvInfo : MonoBehaviour //인벤토리 데이터 및 슬롯 관리
{
    public GameObject inventoryPanel;
    private bool isActionInv = false;
    public bool isShopMode = false;

    private InvSlot[] slots; //인벤토리 슬롯들
    public Transform slotHolder; //인벤토리 슬롯의 부모객체

    public List<ItemInfo> Invenitems = new List<ItemInfo>(); //인벤토리아이템 리스트
    public List<int> InvenCnt = new List<int>(); //인벤토리 아이템 수량 리스트
    private List<ItemInfo> SaveItemList = new List<ItemInfo>();
    private List<int> SaveItemCnt = new List<int>();
    private int SaveMoney;
    public Text moneyText;
    public int StartMoney; //플레이어 시작 돈

    //**싱글톤
    private static InvInfo instance = null; 
    private void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
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
        slots = slotHolder.GetComponentsInChildren<InvSlot>();
        PlayerPrefs.SetInt("money", StartMoney);
        RedrawSlotUI();
    }
    
    public void RedrawSlotUI() //인벤토리 UI그리기
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

    public void AddItemToInv(ItemInfo item, int n) //아이템정보, 개수를 받아서 인벤토리에 추가.
    {
        bool isAdd = true;
        for(int i = 0; i < Invenitems.Count; i++) //인벤토리에 동일한 아이템있는지 찾기
        {
            if (Invenitems[i].itemName == item.itemName)
            {
                InvenCnt[i] += n; //동일한 아이템 있으면 개수 늘리기
                isAdd = false;
                break;
            }
        }
        if (isAdd)
        {
            InvenCnt.Add(1);
            Invenitems.Add(item); //인벤토리 아이템 리스트에 아이템 추가
        }
        RedrawSlotUI(); //UI 업데이트
    }

    public void Save() //현재 인벤토리 정보 저장
    {
        SaveItemList = Invenitems.ToList();
        SaveItemCnt = InvenCnt.ToList();
        SaveMoney = PlayerPrefs.GetInt("money");
        Debug.Log("SaveInventory");
    } 

    public void Load() //저장했던 인벤토리 불러오기
    {
        Invenitems = SaveItemList.ToList();
        InvenCnt = SaveItemCnt.ToList();

        PlayerPrefs.SetInt("money", SaveMoney);
        Debug.Log("LoadInventory");
        RedrawSlotUI();
    } 

    public void ActiveShopModeUI() //상점모드 인벤토리 켜기
    {
        isShopMode = true;
        isActionInv = true;
        RedrawSlotUI();
        inventoryPanel.SetActive(isActionInv);
    } 
    public void CloseShopModeUI() //상점모드 인벤토리 끄기
    {
        isShopMode = false;
        isActionInv = false;
        inventoryPanel.SetActive(isActionInv);
    } 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isShopMode)
        {
            if (!isActionInv)
            {
                isActionInv = !isActionInv;
                RedrawSlotUI();
                inventoryPanel.SetActive(isActionInv);
            }
            else
            {
                isActionInv = !isActionInv;
                inventoryPanel.SetActive(isActionInv);
            }

        }// I키로 인벤토리UI 켜고끄기
        else if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isActionInv)
            {
                isActionInv = false;
                isShopMode = false;
            }
            inventoryPanel.SetActive(isActionInv);
        }// ESC키로 인벤토리UI 끄기
    }
}
