using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUI : MonoBehaviour
{
    public GameObject ShopUI;
    public GameObject InvenUI;
    public GameObject QuestUI;

    bool isActionShop = false;
    bool isActionQuest = false;
    bool isActionInv = false;

    //shop
    public ShopData shopData;
    public ShopSlot[] shopSlots;
    public Transform shopHolder;

    //quest
    public QuestManager questManager;



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
        InvenUI.SetActive(isActionInv);
        QuestUI.SetActive(isActionQuest);

        //상점slot 불러오기
        shopSlots = shopHolder.GetComponentsInChildren<ShopSlot>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // esc키 누를 시 모든 UI 끄기
        {
            if (isActionQuest) isActionQuest = false;
            if (isActionShop)
            {
                isActionShop = false;
                isActionInv = false;
            }
            ShopUI.SetActive(isActionShop);
            InvenUI.SetActive(isActionShop);
            QuestUI.SetActive(isActionQuest);
        }
        if (Input.GetKeyDown(KeyCode.I)&&!isActionShop) // I키로 인벤토리UI 켜고끄기
        {
            isActionInv = !isActionInv;
            InvenUI.SetActive(isActionInv);
        }
    }

    void ActiveQuestUI()
    {
        if (isActionQuest) //끄기
        {
            isActionQuest = false;
        }
        else //켜기
        {
            isActionQuest = true;
            questManager.UpdateList();
        }
        QuestUI.SetActive(isActionQuest);
    }

    void ActiveShopUI()
    {
        if (isActionShop) //끄기
        {
            isActionInv = false;
            isActionShop = false;
        }
        else //켜기
        {
            isActionInv = true;
            isActionShop = true;

            // 슬롯마다 상점데이터에 따라 업데이트
            for (int i = 0; i < shopData.stocks.Count; i++)
            {
                shopSlots[i].item = shopData.stocks[i];
                shopSlots[i].UpdateSlotUI();
            }
        }
        ShopUI.SetActive(isActionShop);
        InvenUI.SetActive(isActionInv);
    }
}