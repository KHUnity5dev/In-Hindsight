using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour //상점 구매기능
{
    //구매수량 정하는거 만들기
    public ItemInfo item;
    public Image itemIcon;
    public Text itemName;
    public Text itemCost;

    private int buyNum = 1;

    public void Start()
    {

    }

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemName.text = item.itemName;
        itemCost.text = item.itemCost.ToString();
        itemIcon.gameObject.SetActive(true);
    }
    
    public void RemoveSlotUI()
    {
        itemIcon.gameObject.SetActive(false);
        itemName.text = "";
        itemCost.text = "";
    }

    public void ClickBuyButton()
    {
        if (itemIcon.gameObject.activeSelf) 
        {
            int money = PlayerPrefs.GetInt("money");
            if(money - item.itemCost*buyNum < 0)
            {
                Debug.Log("구매가능수량을 초과했습니다.");
                return;
            }
            PlayerPrefs.SetInt("money", money - item.itemCost * buyNum);
            InvInfo.Instance.AddItemToInv(item, buyNum);
            InvInfo.Instance.Save();
        }
        
    }
}