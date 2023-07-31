using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDes : MonoBehaviour
{
    public GameObject DesPanel;
    public QuestData questData;
    public GameObject Scene_Manager;
    bool isActive;

    private void Start()
    {
        isActive = false;
    }

    public void UpdatePanel(QuestData _questData)
    {
        questData = _questData;
        DesPanel.GetComponentsInChildren<Text>()[0].text = questData.Title; //제목
        DesPanel.GetComponentsInChildren<Text>()[2].text = questData.Description; //설명
        isActive = true;
        DesPanel.gameObject.SetActive(isActive);

}

    public void CloseButtonClick()
    {
        isActive = false;
        DesPanel.gameObject.SetActive(isActive);
    }

    public void StartButtonClick()
    {
        Debug.Log("StartButtonClick, Index: " + questData.Index);
        Scene_Manager.SendMessage("Load_Level", int.Parse(questData.Index));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isActive)
            {
                isActive = false;
                DesPanel.gameObject.SetActive(isActive);
            }
        }
    }
}
