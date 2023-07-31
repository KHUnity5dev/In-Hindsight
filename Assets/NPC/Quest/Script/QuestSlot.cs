using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    //Slot
    public GameObject Slot;
    public QuestData questData;
    public Text questTitle;
    public Image star;
    public Sprite star0, star1, star2, star3;
    //Description Panel
    public GameObject DesPanel;
    public QuestDes questDes;


    public void UpdateUI()
    {
        switch (questData.star)
        {
            case "0":
                star.sprite = star0;
                break;
            case "1":
                star.sprite = star1;
                break;
            case "2":
                star.sprite = star2;
                break;
            case "3":
                star.sprite = star3;
                break;
        }
        questTitle.text = questData.Title;
        Slot.gameObject.SetActive(questData.isActive);
    }

    public void SlotClick()
    {
        questDes.UpdatePanel(questData);
    }
}