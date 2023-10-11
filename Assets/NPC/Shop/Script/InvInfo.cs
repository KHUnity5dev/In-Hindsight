using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InvInfo : MonoBehaviour //인벤토리 데이터 및 슬롯 관리
{
    public GameObject inventoryPanel;
    public GameObject sellPanel;
    private SellSlot sellSlot;
    private bool isActionInv = false;
    public bool isShopMode = false;

    private InvSlot[] slots; //인벤토리 슬롯들
    public Transform slotHolder; //인벤토리 슬롯의 부모객체

    public List<ItemInfo> Invenitems = new List<ItemInfo>(); //인벤토리아이템 리스트
    public List<int> InvenCnt = new List<int>(); //인벤토리 아이템 수량 리스트
    private List<ItemInfo> SaveItemList;
    private List<int> SaveItemCnt;
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
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 InvenCanvas가 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 InvenCanvas)을 삭제해준다.
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
        if(AllMapUI.ClearMaps.Count < 1){
            Debug.Log(AllMapUI.ClearMaps);
            PlayerPrefs.SetInt("money", StartMoney);
            PlayerPrefs.SetInt("Bullets", 10);
            PlayerPrefs.SetInt("Grenade", 0);
            InvenCnt.Add(10); // 총알 개수
            InvenCnt.Add(0); // 수류탄 개수
        }
        Invenitems.Add(ItemDatabase.Instance.itemDB[0]); //총알 인벤토리 첫번째 칸에 고정
        Invenitems.Add(ItemDatabase.Instance.itemDB[1]); //수류탄 인벤토리 두번째 칸에 고정
        // InvenCnt[0] = PlayerPrefs.GetInt("Bullets");
        // InvenCnt[1] = PlayerPrefs.GetInt("Grenade");
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
            // slots[i].UpdateSlotUI(InvenCnt[i]);
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
                // InvenCnt[i] += n; //동일한 아이템 있으면 개수 늘리기
                if (true){
                    PlayerPrefs.SetInt(item.itemName, PlayerPrefs.GetInt(item.itemName)+n);
                    PlayerInventory.Bullets = PlayerPrefs.GetInt("Bullets");
                    PlayerInventory.Grenade = PlayerPrefs.GetInt("Grenade");
                    InvenCnt[i] = PlayerPrefs.GetInt(item.itemName);
                }
                isAdd = false;
                break;
            }
            else {
                Debug.Log("두번째아이템");
            }
        }
        if (isAdd)
        {
            InvenCnt.Add(1);
            Invenitems.Add(item); //인벤토리 아이템 리스트에 아이템 추가
            Debug.Log("두번째아이템 리스트추가");
        }
        RedrawSlotUI(); //UI 업데이트
    }

    public void RemoveItem(int index, int n) //아이템 위치(index), 제거하고자하는 개수(n)을 받아 인벤토리에서 제거.
    {
        if (index > -1) //총알(index=0)의 경우 개수가 0이어도 사라지지 않게 예외처리.-> 그냥 모든애들 안사라지게
        {
            if (InvenCnt[index] - n < 0) //총알이 부족한 경우
            {
                Debug.Log("아이템 부족");
                RedrawSlotUI();
                return;
            }
            if(index == 0){
                PlayerPrefs.SetInt("Bullets", PlayerPrefs.GetInt("Bullets")-n);
                InvenCnt[index] = PlayerPrefs.GetInt("Bullets");
                PlayerInventory.Bullets = PlayerPrefs.GetInt("Bullets");
            } else if (index == 1){
                PlayerPrefs.SetInt("Grenade", PlayerPrefs.GetInt("Grenade")-n);
                InvenCnt[index] = PlayerPrefs.GetInt("Grenade");
                PlayerInventory.Grenade = PlayerPrefs.GetInt("Grenade");
            }
            // InvenCnt[index] -= n;
            RedrawSlotUI();
            if (InvenCnt[index] <= 0)
                RedrawSlotUI();
                return;
        } 
        if(InvenCnt[index] - n < 0)
        {
            Debug.Log("아이템이 부족합니다");
            RedrawSlotUI();
            return;
        }
        // PlayerPrefs.SetInt("Bullets", PlayerPrefs.GetInt("Bullets")-n);
        // InvenCnt[index] -= n;
        // InvenCnt[index] = PlayerPrefs.GetInt("Bullets");
        if (InvenCnt[index] <= 0)
        {
            Invenitems.RemoveAt(index);
            InvenCnt.RemoveAt(index);
        }

        RedrawSlotUI();
    }

    public void Save() //현재 인벤토리 정보 저장
    {
        // SaveItemList = Invenitems.ToList();
        // SaveItemCnt = InvenCnt.ToList();
        SaveMoney = PlayerPrefs.GetInt("money");
        // PlayerPrefs.SetInt("Bullets",InvenCnt[0]);
        Debug.Log("SaveInventory");
    }

    public void Load() //가장 최근 Save() 했던 인벤토리 불러오기
    {
        // Invenitems = SaveItemList.ToList();
        // InvenCnt = SaveItemCnt.ToList();

        // PlayerPrefs.GetInt("Bullets",InvenCnt[0]);
        // PlayerPrefs.SetInt("money", SaveMoney);
        Debug.Log("LoadInventory");
        RedrawSlotUI();
    }

    public void ActiveShopModeUI() //상점모드 인벤토리 켜기
    {
        isShopMode = true;
        isActionInv = true;
        RedrawSlotUI();
        sellSlot.RemoveSlotUI();
        inventoryPanel.SetActive(isActionInv);
        sellPanel.SetActive(isActionInv);
    } 
    public void CloseShopModeUI() //상점모드 인벤토리 끄기
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
            if (!isActionInv) //인벤토리가 꺼져있다면 켜기
            {
                isActionInv = !isActionInv;
                // InvenCnt[0] = PlayerInventory.Bullets + PlayerInventory.Magazine;
                RedrawSlotUI();
                inventoryPanel.SetActive(isActionInv);
            }
            else //인벤토리가 켜져있다면 끄기
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
                // PlayerPrefs.SetInt("Bullets", InvenCnt[0]);
            }
            inventoryPanel.SetActive(isActionInv);
            sellPanel.SetActive(false);
        }// ESC키로 인벤토리UI 끄기
    }
}
