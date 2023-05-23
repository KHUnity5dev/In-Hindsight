using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestButton : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("Quest Button Clicked");
        //Unity BuildSetting에 Scene추가해야함
        //SendMessage로 Quest정보 전달?
        //SceneManager.LoadScene("Scene이름");
    }
}
