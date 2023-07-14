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
    public void Next_Level()
    {
        PlayerPrefs.SetString("Stage", "Stage2");
        SceneManager.LoadScene("Init_Scene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // 말해서, 인풋 메니저 쪽으로 옮기기.
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Next_Level();
        }
    }
}
