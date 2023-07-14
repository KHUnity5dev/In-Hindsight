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

    public ShopData shopData;
    public ShopSlot[] shopSlots;
    public Transform shopHolder;


    public void Active(GameObject scanObj) // UI ���� �ݴ� �Լ�
    // scanObj�� �޾Ƽ� �װ��� tag�� ���� ����� ������
    {
        if (scanObj.tag == "Shop") //����UI
        {
            if (isActionShop)
            {
                isActionInv = false;
                isActionShop = false;
            }
            else
            {
                isActionInv = true;
                isActionShop = true;

                for(int i = 0; i < shopData.stocks.Count; i++)
                {
                    shopSlots[i].item = shopData.stocks[i];
                    shopSlots[i].UpdateSlotUI();
                }
            }
            ShopUI.SetActive(isActionShop);
            InvenUI.SetActive(isActionInv);
        }
        if (scanObj.tag == "Quest") //�Ƿ�UI
        {
            if (isActionQuest)
            {
                isActionQuest = false;
            }
            else
            {
                isActionQuest = true;
            }
            QuestUI.SetActive(isActionQuest);
        }
    }
    private void Start() //UI�ʱ�ȭ
    {
        ShopUI.SetActive(isActionShop);
        InvenUI.SetActive(isActionInv);
        QuestUI.SetActive(isActionQuest);

        shopSlots = shopHolder.GetComponentsInChildren<ShopSlot>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            isActionInv = !isActionInv;
            InvenUI.SetActive(isActionInv);
        }
    }
}