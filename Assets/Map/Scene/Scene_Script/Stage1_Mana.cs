using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Stage1_Mana : MonoBehaviour
{
    public void Restart_Level()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /*public void Next_Level()
    {
        PlayerPrefs.SetString("Stage", "Stage2");
    }*/
    public void Next_Level(int level)
    {
        Debug.Log("Level is ");
        Debug.Log(level);
        PlayerPrefs.SetString("Stage", "Stage2");
        SceneManager.LoadScene("Stage" + level.ToString());
    }

    // Update is called once per frame
    // ���ؼ�, ��ǲ �޴��� ������ �ű��.
    void Update()
    {
        /*if (Input.GetMouseButtonDown(2))
        {
            Next_Level();
        }*/
    }
}
