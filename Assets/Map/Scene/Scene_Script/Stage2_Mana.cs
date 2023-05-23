using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Stage2_Mana : MonoBehaviour
{
    public void Restart_Level()
    {
        PlayerPrefs.SetInt("Stage", 1);
        SceneManager.LoadScene("Stage1");
    }
    public void Next_Level()
    {
        PlayerPrefs.SetInt("Stage", 2);
        SceneManager.LoadScene("Stage2");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}