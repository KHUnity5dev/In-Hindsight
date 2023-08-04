using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Mana : MonoBehaviour
{

    [SerializeField]
    private GameObject Stage_Manager;

    public void Restart_Level()
    {
        Stage_Manager.SendMessage("Restart_Level");
    }
    public void Load_Level(int level)
    {
        Stage_Manager.SendMessage("Next_Level", level);
    }
}
