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
        GenerateData();// ��ü �Ƿ� ��� ����
    }

    public void UpdateList() // Ȱ��ȭ �� �Ƿ� ������Ʈ
    {
        curList = questList.FindAll(x => x.isActive == true);
        questSlots = questHolder.GetComponentsInChildren<QuestSlot>();
        for(int i=0;i<questSlots.Length; i++)
        {
            questSlots[i].questData = i<curList.Count?curList[i]:defaultQuest;
            questSlots[i].UpdateUI(); // ���� ������Ʈ
        }
    }

    void GenerateData() // ��ü �Ƿ� ��� ����
    {
        /*
        questList.Add(new QuestData("Index","Title", 
            "Description",
            "star", (bool)isActive));
        */

        questList.Add(new QuestData("1","ù ��° �Ƿ�", 
            "ù ��° �Ƿڼ����Դϴ�.",
            "2", true));
        questList.Add(new QuestData("2", "�� ��° �Ƿ�",
            "�� ��° �Ƿڼ����Դϴ�.",
            "0", true));
        questList.Add(new QuestData("3", "�� ��° �Ƿ�",
            "�� ��° �Ƿڼ����Դϴ�.",
            "0", false));

    }
}
