using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : riciever
{
    private BoxCollider2D Door_Physical_Collider;
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Get_Player_Interacted()
    {
        Door_Physical_Collider = GetComponent<BoxCollider2D>();
        Door_Physical_Collider.isTrigger = !Door_Physical_Collider.isTrigger;

        animator = GetComponent<Animator>();
        animator.SetBool("Is_Open", !animator.GetBool("Is_Open"));
    }

    public void Get_Enemy_Interacted()
    {
        Door_Physical_Collider = GetComponent<BoxCollider2D>();
        Door_Physical_Collider.isTrigger = !Door_Physical_Collider.isTrigger;

        animator = GetComponent<Animator>();
        animator.SetBool("Is_Open", !animator.GetBool("Is_Open"));
    }
}