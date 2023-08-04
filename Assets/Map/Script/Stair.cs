using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField]
    private int Height;

    public void Get_Player_Interacted(GameObject Player)
    {
        Player.transform.position = Player.transform.position + new Vector3(0, Height, 0);
    }
    public void Get_Enemy_Interacted(GameObject enemy)
    {
        enemy.transform.position = enemy.transform.position + new Vector3(0, Height, 0);
    }
}
