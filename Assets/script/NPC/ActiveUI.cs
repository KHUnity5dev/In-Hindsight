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

    public void Active(GameObject scanObj) // UI ¿­°í ´Ý´Â ÇÔ¼ö
    {
        if (scanObj.tag == "Shop") //»óÁ¡UI
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
        if (scanObj.tag == "Quest") //ÀÇ·ÚUI
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
}