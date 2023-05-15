using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playercontrol : MonoBehaviour
{
    Vector2 Inputvec;
    
    Rigidbody2D Rigid;
    SpriteRenderer Spriterenderer;
    Animator Anim;
    Vector3 Raydir;
    Vector3 Playerdir; // 1이면 오른쪽 -1이면 왼쪽

    public GameObject ActiveUI;
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

        if (Input.GetKeyDown(KeyCode.P)) // 달리기 시작
        {
            Player.Dead();
        }
        if (Playerdir==Vector3.left) // 플레이어의 방향 확인
        {
            Raydir = Vector3.left;

        }
        else //플레이어의 방향 확인
        {
            Raydir = Vector3.right;
        }

        Gunshot();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Invoke("Reload",1); // 1초 뒤에 리로드
        }

        Debug.DrawRay(Rigid.position + 0.2f * Vector2.up, Raydir, new Color(1, 0, 0)); // 상호 작용 레이
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 rayPosition = Rigid.position + 0.2f * Vector2.up;
            RaycastHit2D rayHitobject = Physics2D.Raycast(rayPosition, Raydir, 1f, LayerMask.GetMask("Object"));
            RaycastHit2D rayHitenemy = Physics2D.Raycast(rayPosition, Raydir, 1f, LayerMask.GetMask("Enemy"));
            RaycastHit2D rayHitNPC = Physics2D.Raycast(rayPosition, Raydir, 1f, LayerMask.GetMask("NPC"));

            if (rayHitenemy)
            {
                if (rayHitenemy.distance < 0.5f)
                {
                    Debug.Log(rayHitenemy.collider.name);
                    GameObject obj = rayHitenemy.collider.gameObject;
                    if (obj.tag == "Enemy") // 암살
                    {
                        Debug.Log("Enemy detected!!!!");
                        Anim.SetTrigger("Amsal!");
                        obj.GetComponent<EnemyStat>().OnDamaged(100);
                    }
                    else if( obj.tag == "Dead_Enemy")
                    {
                        obj.SendMessage("Get_Enemy_Item");
                    }
                }
            }
            else if (rayHitobject)
            {
                if (rayHitobject.distance < 0.5f)
                {
                    Debug.Log(rayHitobject.collider.name);
                    GameObject obj = rayHitobject.collider.gameObject;
                    if(obj.tag == "Dore"){//문열기
                        // Invoke("Get_Player_Interacted",0.1f);
                        obj.SendMessage("Get_Player_Interacted");
                    }
                    else if(obj.tag == "Stair")
                    {
                        obj.SendMessage("Get_Player_Interacted");
                    }
                    else if (obj.tag == "Box")
                    {
                        obj.SendMessage("Get_Player_Interacted");
                    }
                }
            }
            else if (rayHitNPC.collider != null)
            {
                if (rayHitNPC.distance < 0.5f)
                {
                    Debug.Log(rayHitNPC.collider.name);
                    GameObject obj = rayHitNPC.collider.gameObject;
                    ActiveUI.GetComponent<ActiveUI>().Active(obj);
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


        Rigid.MovePosition(Rigid.position + nextVec + new Vector2(0,Physics.gravity.y * Time.deltaTime));
        
    }
    void FlipControl()
    {
        if (Playerdir.x < 0)
        {
            Spriterenderer.flipX = true;
        }
        else
            Spriterenderer.flipX = false;
    }
    void Reload()
    {
        if (Inventory.Bullets > 0 && Inventory.Magazine < Inventory.Maxmagazine)
        {
            Inventory.Bullets -= (Inventory.Maxmagazine - Inventory.Magazine);
            Inventory.Magazine = Inventory.Maxmagazine;
            Debug.Log(Inventory.Bullets);
        }
    }
    bool Isgrounded()
    {
        return Physics2D.Raycast(Rigid.position + Vector2.up, Vector3.down, 1.08f, LayerMask.GetMask("Ground")); //추후 캐릭터가 바뀔시 길이 변경 필요
    }
    void OnMove(InputValue value) // 이동 함수
    {
        Inputvec = value.Get<Vector2>();
        Inputvec.y = 0f;
    }
    void OnFlipA()
    {
        Playerdir = Vector3.left;
        FlipControl();
    }
    void OnFlipD()
    {
        Playerdir = Vector3.right;
        FlipControl();
    }
    void GunNoiseCreater()
    {
        Player.Noise_Timer = -1;
        Player.NoiseCreater(Player.Gun_Noise);
    }
    void Gunshot()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)) - (Vector3)(Rigid.position + Vector2.up);
        if (Input.GetMouseButtonDown(0) && Inventory.Magazine > 0) // 총쏘기
        {
            Inventory.Magazine--;
            Debug.Log(Inventory.Magazine);
            Anim.SetTrigger("Attack");
            Playerdir = point.normalized;
            GunNoiseCreater();
            FlipControl();
            RaycastHit2D rayHit = Physics2D.Raycast(Rigid.position + Vector2.up, point, 100f, LayerMask.GetMask("Enemy"));
            RaycastHit2D rayHitobject = Physics2D.Raycast(Rigid.position + Vector2.up, point, 100f, LayerMask.GetMask("Enemy"));
            if (rayHit)
            {
                GameObject obj = rayHit.collider.gameObject;
                rayHit.transform.gameObject.GetComponent<EnemyStat>().OnDamaged(10); //Enemy가 피가 닳는 코드 한줄
            }
            if  (rayHitobject)
            {
                GameObject obj = rayHitobject.collider.gameObject;
                if (obj.tag == "Light")
                {
                    obj.SendMessage("Get_Player_Interacted");
                }
            }
        }
        Debug.DrawRay(Rigid.position + Vector2.up, point, new Color(1, 0, 1)); // 마우스 포인터 레이
    }
}
