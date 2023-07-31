using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sprite.flipX = rigid.velocity.x > 0 ? false : true;
        if(rigid.velocity.x > 0.1f || rigid.velocity.x < -0.1f){
            anim.SetBool("IsWalk",true);
        } else{
            anim.SetBool("IsWalk",false);
        }
    }
}
