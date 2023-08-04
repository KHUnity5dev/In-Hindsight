using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InitScene_Mana : MonoBehaviour
{
    //씬을 옮기는건 initscene manager에서.
    [SerializeField]
    public Image itemslider;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Stage"))
        {
            Debug.Log(PlayerPrefs.GetString("Stage"));
            StartCoroutine(LoadSceneAsync(PlayerPrefs.GetString("Stage")));
        }
        else{
            StartCoroutine(LoadSceneAsync("Stage1"));
        }

    }

    IEnumerator LoadSceneAsync(string scenename)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenename);

        while (!operation.isDone)
        {
            itemslider.fillAmount = operation.progress;
            yield return null;
        }
    }
}
