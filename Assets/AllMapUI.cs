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
        Debug.Log(PlayerInventory.Bullets);
        PlayerPrefs.SetInt("Bullets", PlayerInventory.Bullets); 
        Debug.Log(PlayerPrefs.GetInt("Bullets"));
        Debug.Log(PlayerInventory.Bullets);
        //모든 아이템 Save end
        ClearMaps.Add(SceneManager.GetActiveScene().name);
        QuestManager.questList[1].star = "2";
        QuestManager.questList[0].isActive = false;
        SceneManager.LoadScene("LobbyMap");
    }
}
