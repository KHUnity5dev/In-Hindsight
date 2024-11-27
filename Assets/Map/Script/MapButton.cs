using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapButton : MonoBehaviour, Object_Interacted_Interface
{
    public void Get_Player_Interacted()
    {
        Debug.Log("pressed by player_button");
    }
    public void Get_Enemy_Interacted()
    {
        Debug.Log("pressed by enemy_button");
    }
}
