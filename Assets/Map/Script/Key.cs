using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public void Get_Player_Interacted(GameObject Players)
    {
        Player.HasKey = true;
        Destroy(gameObject);
    }
}
