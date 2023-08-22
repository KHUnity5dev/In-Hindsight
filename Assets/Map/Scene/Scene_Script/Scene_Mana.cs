using UnityEngine;

public class Scene_Mana : MonoBehaviour
{

    [SerializeField]
    private GameObject Stage_Manager;
    [SerializeField]
    private GameObject MenuUI;
    [SerializeField]
    private GameObject OverUI;
    [SerializeField]
    private GameObject ClearUI;


    private void Start()
    {
        MenuUI.SetActive(false);
        OverUI.SetActive(false);
        ClearUI.SetActive(false);
    }

    public void GameOver()
    {
        OverUI.SetActive(true);
    }
    public void GameExit()
    {

        if (MenuUI.activeSelf == false)
        {
            MenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            resume();
        }
    }

    public void Load_Level(string level)
    {
        //level�� �������� �̸����� �����ϱ�. ������Ʈ ���� ���ߴ°� ������ ���̶�.
        Stage_Manager.SendMessage("Next_Level", level);
    }

    public void GameClear()
    {

        //Save Prefebs, Inventory
        ClearUI.SetActive(true);
    }

    public void resume() {
        Time.timeScale = 1f;
        MenuUI.SetActive(false);
    }
    public void Restart_Level()
    {
        Stage_Manager.SendMessage("Restart_Level");
    }
    public void Load_Lobby()
    {
        Stage_Manager.SendMessage("Next_Level", "LobbyMap");
    }

    public void Option()
    {
        //not working yet
    }
    public void Quit()
    {
        Application.Quit();
    }



}
