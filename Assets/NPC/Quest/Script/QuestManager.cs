using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestData> questList, curList;
    public QuestSlot[] questSlots;
    public Transform questHolder;

    QuestData defaultQuest;

    private void Awake()
    {
        defaultQuest = new QuestData("00", "",
            "",
            "0", false);
        questList = new List<QuestData> (); 
        GenerateData();// 전체 의뢰 목록 생성
    }

    public void UpdateList() // 활성화 된 의뢰 업데이트
    {
        curList = questList.FindAll(x => x.isActive == true);
        questSlots = questHolder.GetComponentsInChildren<QuestSlot>();
        for(int i=0;i<questSlots.Length; i++)
        {
            questSlots[i].questData = i<curList.Count?curList[i]:defaultQuest;
            questSlots[i].UpdateUI(); // 슬롯 업데이트
        }
    }

    void GenerateData() // 전체 의뢰 목록 생성
    {
        /*
        questList.Add(new QuestData("Index","Title", 
            "Description",
            "star", (bool)isActive));
        */

        questList.Add(new QuestData("1","첫 번째 의뢰", 
            "첫 번째 의뢰설명입니다.",
            "2", true));
        questList.Add(new QuestData("2", "두 번째 의뢰",
            "두 번째 의뢰설명입니다.",
            "0", true));
        questList.Add(new QuestData("3", "세 번째 의뢰",
            "세 번째 의뢰설명입니다.",
            "0", false));

    }
}
