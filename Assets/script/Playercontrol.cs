using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playercontrol : MonoBehaviour
{
    Vector2 Inputvec;
    
    Rigidbody2D Rigid;
    SpriteRenderer Spriterenderer;
    Animator Anim;
    Vector3 Raydir;
    int Playerdir; // 1이면 오른쪽 -1이면 왼쪽
    void Awake() // 시작할때 한번만 실행되는 함수
    {
        Spriterenderer = GetComponent<SpriteRenderer>();
        Rigid = GetComponent<Rigidbody2D>(); // getcomponent 오브젝트에서 컴포넌트를 가져오는 함수
        Anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update() // 하나의 프레임마다 호출되는 생명 주기 함수
    {
        if (Inputvec == Vector2.zero)
            Anim.SetBool("isWalking", false);
        else
            Anim.SetBool("isWalking", true);

        if (Input.GetKeyDown(KeyCode.LeftShift)) // 달리기 시작
        {
            Player.Player_Speed = Player.Player_Speed + 2f;
            Player.Player_State = Player.State.Run;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)) // 달리기 끝
        {
            Player.Player_Speed = Player.Player_Speed - 2f;
            Player.Player_State = Player.State.Walk;
        }
        if(Input.GetMouseButtonDown(0))
            Anim.SetTrigger("Attack");

        if (Playerdir==-1) // 플레이어의 방향 확인
        {
            Raydir = Vector3.left;
            Debug.DrawRay(Rigid.position + Vector2.up, Raydir, new Color(1, 0, 0));

        }
        else //플레이어의 방향 확인
        {
            Raydir = Vector3.right;
            Debug.DrawRay(Rigid.position + Vector2.up, Raydir, new Color(1, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(Rigid.position + Vector2.up, Raydir, 1f, LayerMask.GetMask("Object"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    Debug.Log(rayHit.collider.name);
                    GameObject obj = rayHit.collider.gameObject;
                    if(obj.tag  == "Enemy")
                    {
                        Debug.Log("Enemy detected!!!!");
                        Anim.SetTrigger("Attack");
                        obj.SetActive(false);
                    }
                    else if(obj.tag == "Dore")
                        obj.SetActive(false);
                }
            }
        }
    }
    void FixedUpdate() //물리연산 프레임마다 호출되는 생명주기 함수
    {
        //1. 힘을 준다
        //rigid.AddForce(inputVec);
        //2. 속도제어
        //rigid.velocity = inputVec;
        //3. 위치 이동
        Vector2 nextVec = Inputvec * Player.Player_Speed * Time.fixedDeltaTime;
        Rigid.MovePosition(Rigid.position + nextVec);

    }
    void OnMove(InputValue value) // 이동 함수
    {
        Inputvec = value.Get<Vector2>();
        Inputvec.y = 0f;
    }
    //void OnRun()
    //{
    //    inputVec.x = inputVec.x * Player.RunSpeed;
    //}
    void OnFlipA()
    {
        Spriterenderer.flipX = true;
        Playerdir = -1;
    }
    void OnFlipD()
    {
        Spriterenderer.flipX = false;
        Playerdir = 1;
    }
}
