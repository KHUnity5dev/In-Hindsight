using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField]
    private int Up;
    [SerializeField]
    private int Down;
    [SerializeField]
    private int Height;
    public void Get_Player_Interacted_Up(GameObject Player)
    {
        Player.transform.position = Player.transform.position + new Vector3(0, Up, 0);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Stair);
    }
    public void Get_Player_Interacted_Down(GameObject Player)
    {
        Player.transform.position = Player.transform.position - new Vector3(0, Down, 0);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Stair);
    }
    public void Get_Enemy_Interacted(GameObject enemy)
    {
        enemy.transform.position = enemy.transform.position + new Vector3(0, Height, 0);
    }
}
