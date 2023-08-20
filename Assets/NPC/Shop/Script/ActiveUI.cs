using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUI : MonoBehaviour //Player의 호출을 받아 UI활성화
{
    public GameObject ShopUI;
    public GameObject QuestUI;

    bool isActionShop = false;
    bool isActionQuest = false;

    //shop
    public ShopData shopData;
    public ShopSlot[] shopSlots;
    public Transform shopHolder;
    public BuySlot buyslot;

    //quest
    public QuestManager questManager;
    public GameObject QuestDesPanel;



    public void Active(GameObject scanObj) // UI 켜고끄기
    {
        if (scanObj.tag == "Shop") //상점UI
        {
            ActiveShopUI();
        }
        if (scanObj.tag == "Quest")  //의뢰UI
        {
            ActiveQuestUI();
        }
    }
    private void Start() //UI초기화
    {
        ShopUI.SetActive(isActionShop);
        QuestUI.SetActive(isActionQuest);

        //상점slot 불러오기
        shopSlots = shopHolder.GetComponentsInChildren<ShopSlot>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // esc키 누를 시 모든 UI 끄기
        {
            if (isActionQuest)
            {
                QuestDesPanel.SetActive(false);
                isActionQuest = false;
            } 
            if (isActionShop)
            {
                isActionShop = false;
            }
            ShopUI.SetActive(isActionShop);
            QuestUI.SetActive(isActionQuest);
        }
    }

    public void ActiveQuestUI()
    {
        if (isActionQuest&&QuestDesPanel.activeSelf==false) //끄기
        {
            isActionQuest = false;
            QuestDesPanel.SetActive(false);
        }
        else //켜기
        {
            isActionQuest = true;
            QuestUI.SetActive(isActionQuest);
            questManager.UpdateList();
        }
        QuestUI.SetActive(isActionQuest);
    }

     void ActiveShopUI()
    {
        if (isActionShop) //끄기
        {
            InvInfo.Instance.CloseShopModeUI();
            isActionShop = false;
        }
        else //켜기
        {
            InvInfo.Instance.ActiveShopModeUI();
            isActionShop = true;
            buyslot.RemoveSlotUI();
            // 슬롯마다 상점데이터에 따라 업데이트
            for (int i = 0; i < shopSlots.Length ; i++)
            {
                if (i < shopData.stocks.Count) {
                    shopSlots[i].item = shopData.stocks[i];
                }
                else
                {
                    shopSlots[i].item = null;
                }
                shopSlots[i].UpdateSlotUI();
            }
            InvInfo.Instance.ActiveShopModeUI();
        }
        ShopUI.SetActive(isActionShop);
    }

    public void CloseShopUI()
    {
        isActionShop = false;
        ShopUI.SetActive(isActionShop);
        InvInfo.Instance.CloseShopModeUI();
    }
}