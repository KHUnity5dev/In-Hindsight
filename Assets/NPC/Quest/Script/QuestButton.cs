using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestButton : MonoBehaviour
{

    public GameObject Scene_Manager;
    private int stagenum = 1;
    public void OnClick()
    {
        Debug.Log("Quest Button Clicked");
        Scene_Manager.SendMessage("Load_Level", stagenum);

    }
}
