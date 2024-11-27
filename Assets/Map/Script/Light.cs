using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour, Object__Shooted_Interface
{
    [SerializeField]
    private bool IsOn;
    
    public GameObject Dark;
    private GameObject Spawned_Dark;

    private void Spawn_Dark()
    {
        Spawned_Dark = Instantiate(Dark, transform);
    }

    private void Start()
    {
        if(IsOn == false)
        {
            Spawn_Dark();
        }
    }
    public void Get_Player_Shooted()
    {
        if(IsOn == false)
             Spawn_Dark();
        gameObject.layer = 8;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Light);
    }
    public void Get_Enemy_Shooted()
    {
        if (IsOn == false)
            Spawn_Dark();
        gameObject.layer = 8;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Light);
    }

    public void Off()
    {
        Debug.Log("aefnwlkklnkelnfsjsesnfewknj.jbjdskzbjzkdbjdsvbvds");
            Spawn_Dark();
        IsOn = false;
    }
    public void On()
    {
        Destroy(Spawned_Dark);
        IsOn = true;
    }

    public void On_Off()
    {
        if(IsOn == true)
        {
            Off();
        }
        else
        {
            On();
        }
    }
}

