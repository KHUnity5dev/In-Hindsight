using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDes : MonoBehaviour
{
    public GameObject DesPanel;
    private QuestData questData;
    private GameObject Scene_Manager;
    bool isActive;

    private void Start()
    {
        isActive = false;
        Scene_Manager = GameObject.Find("SceneMana");
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

    public void StartButtonClick() //SceneManager에게 스테이지 정보 넘겨줌
    {
        Debug.Log("StartButtonClick, Index: " + questData.Index);
        Scene_Manager.SendMessage("Load_Level", questData.Index);
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
