using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
 private BoxCollider2D Door_Physical_Collider;
    private Animator animator;
    public void Get_Player_Interacted(GameObject Player)
    {

        Door_Physical_Collider = GetComponent<BoxCollider2D>();
        Door_Physical_Collider.isTrigger = !Door_Physical_Collider.isTrigger;
        if(Door_Physical_Collider.isTrigger == true)
        {
            this.gameObject.layer = 8;
        }
        else
        {
            this.gameObject.layer = 6;
        }

        AudioManager.instance.PlaySfx(AudioManager.Sfx.door);
        animator = GetComponent<Animator>();
        animator.SetBool("Is_Open", !animator.GetBool("Is_Open"));
    }
    public void Get_Enemy_Interacted(GameObject enemy)
    {
        Door_Physical_Collider = GetComponent<BoxCollider2D>();
        Door_Physical_Collider.isTrigger = !Door_Physical_Collider.isTrigger;

        if (Door_Physical_Collider.isTrigger == true)
        {
            this.gameObject.layer = 8;
        }
        else
        {
            this.gameObject.layer = 6;
        }


        animator = GetComponent<Animator>();
        animator.SetBool("Is_Open", !animator.GetBool("Is_Open"));
    }

    public void IsOpen_Enemy_Interacted(GameObject enemy)
    {
        Door_Physical_Collider = GetComponent<BoxCollider2D>();
        Door_Physical_Collider.isTrigger = !Door_Physical_Collider.isTrigger;
        enemy.SendMessage("Door_Is_Open", Door_Physical_Collider.isTrigger);
    }

    public void Open_Interacted()
    {
        Door_Physical_Collider = GetComponent<BoxCollider2D>();
        Door_Physical_Collider.isTrigger = true;

        animator = GetComponent<Animator>();
        animator.SetBool("Is_Open", true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.door);
        this.gameObject.layer = 8;
    }
    public void Close_Interacted()
    {
        Door_Physical_Collider = GetComponent<BoxCollider2D>();
        Door_Physical_Collider.isTrigger = false;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.door);
        animator = GetComponent<Animator>();
        animator.SetBool("Is_Open", false);
        this.gameObject.layer = 6;
    }
}