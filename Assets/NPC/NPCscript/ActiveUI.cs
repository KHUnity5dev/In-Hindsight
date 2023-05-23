using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUI : MonoBehaviour
{
    public GameObject ShopUI;
    public GameObject InvenUI;
    public GameObject QuestUI;
    bool isActionShop;
    bool isActionQuest;

    public void Active(GameObject scanObj) // UI ���� �ݴ� �Լ�
    // scanObj�� �޾Ƽ� �װ��� tag�� ���� ����� ������
    {
        if (scanObj.tag == "Shop") //����UI
        {
            if (isActionShop)
            {
                isActionShop = false;
            }
            else
            {
                isActionShop = true;
            }
            ShopUI.SetActive(isActionShop);
            InvenUI.SetActive(isActionShop);
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
        isActionQuest = false;
        isActionShop = false;
        ShopUI.SetActive(isActionShop);
        InvenUI.SetActive(isActionShop);
        QuestUI.SetActive(isActionQuest); 
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isActionQuest) isActionQuest = false;
            if (isActionShop) isActionShop = false;
            ShopUI.SetActive(isActionShop);
            InvenUI.SetActive(isActionShop);
            QuestUI.SetActive(isActionQuest);
        }
    }
}