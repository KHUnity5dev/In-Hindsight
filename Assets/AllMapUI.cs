using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllMapUI : MonoBehaviour
{
    public static List<string> ClearMaps = new List<string>();
    public void Restart()
    {
        Debug.Log("RestartButton");
        //모든 아이템 다시 Load
            //플레이어 cs에서 자체적으로 Load.
        //Load end
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene("LobbyMap");
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }
    public void Clear()
    {
        Debug.Log("ClearButton");

        //모든 아이템 Save

        PlayerPrefs.SetInt("Bullets", PlayerInventory.Bullets); 
        PlayerPrefs.SetInt("Grenade", PlayerInventory.Grenade); 

        //모든 아이템 Save end


        ClearMaps.Add(SceneManager.GetActiveScene().name);


        int stagenum = int.Parse(SceneManager.GetActiveScene().name.Substring(5,1));
        Debug.Log(stagenum);

        QuestManager.questList[stagenum-1].star = "2"; //스타를 bronze로 바굼
        // QuestManager.questList[stagenum-1].isActive = false; // 의뢰 걍 없애버림
        QuestManager.questList[stagenum].isActive = true; // 의뢰오픈
        SceneManager.LoadScene("LobbyMap");
        
    }
}
