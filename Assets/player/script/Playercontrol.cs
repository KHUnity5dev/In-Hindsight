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
    Vector3 Playerdir; // 1�̸� ������ -1�̸� ����

    public GameObject ActiveUI;
    void Awake() // �����Ҷ� �ѹ��� ����Ǵ� �Լ�
    {
        Spriterenderer = GetComponent<SpriteRenderer>();
        Rigid = GetComponent<Rigidbody2D>(); // getcomponent ������Ʈ���� ������Ʈ�� �������� �Լ�
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() // �ϳ��� �����Ӹ��� ȣ��Ǵ� ���� �ֱ� �Լ�
    {
        if (Inputvec == Vector2.zero)
            Anim.SetBool("isWalking", false);
        else
            Anim.SetBool("isWalking", true);

        if (Input.GetKeyDown(KeyCode.LeftShift)) // �޸��� ����
        {
            Player.Player_Speed = Player.Player_Speed + 2f;
            Player.Player_State = Player.State.Run;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)) // �޸��� ��
        {
            Player.Player_Speed = Player.Player_Speed - 2f;
            Player.Player_State = Player.State.Walk;
        }

        if (Input.GetKeyDown(KeyCode.P)) // �޸��� ����
        {
            Player.Dead();
        }
        if (Playerdir==Vector3.left) // �÷��̾��� ���� Ȯ��
        {
            Raydir = Vector3.left;

        }
        else //�÷��̾��� ���� Ȯ��
        {
            Raydir = Vector3.right;
        }

        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)) - (Vector3)(Rigid.position + Vector2.up);
        if (Input.GetMouseButtonDown(0) && Inventory.Magazine > 0) // �ѽ��
        {
            Inventory.Magazine--;
            Debug.Log(Inventory.Magazine);
            Anim.SetTrigger("Attack");
            Playerdir = point.normalized;
            FlipControl();
            RaycastHit2D rayHit = Physics2D.Raycast(Rigid.position + Vector2.up, point, 100f, LayerMask.GetMask("Enemy"));
            if (rayHit)
            {
                GameObject obj = rayHit.collider.gameObject;
                rayHit.transform.gameObject.GetComponent<EnemyStat>().OnDamaged(10); //Enemy�� �ǰ� ��� �ڵ� ����
            }    
        }
        Debug.DrawRay(Rigid.position + Vector2.up, point, new Color(1, 0, 1)); // ���콺 ������ ����

        if (Input.GetKeyDown(KeyCode.R))
        {
            Invoke("Reload",1);
        }

        Debug.DrawRay(Rigid.position + Vector2.up, Raydir, new Color(1, 0, 0)); // ��ȣ �ۿ� ����
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D rayHitobject = Physics2D.Raycast(Rigid.position + Vector2.up, Raydir, 1f, LayerMask.GetMask("Object"));
            RaycastHit2D rayHitenemy = Physics2D.Raycast(Rigid.position + Vector2.up, Raydir, 1f, LayerMask.GetMask("Enemy"));
            RaycastHit2D rayHitNPC = Physics2D.Raycast(Rigid.position + Vector2.up, Raydir, 1f, LayerMask.GetMask("NPC"));

            if (rayHitenemy)
            {
                if (rayHitenemy.distance < 0.5f)
                {
                    Debug.Log(rayHitenemy.collider.name);
                    GameObject obj = rayHitenemy.collider.gameObject;
                    if (obj.tag == "Enemy") // �ϻ�
                    {
                        Debug.Log("Enemy detected!!!!");
                        Anim.SetTrigger("Amsal!");
                        obj.SetActive(false);
                    }
                }
            }
            else if (rayHitobject)
            {
                if (rayHitobject.distance < 0.5f)
                {
                    Debug.Log(rayHitobject.collider.name);
                    GameObject obj = rayHitobject.collider.gameObject;
                    if(obj.tag == "Dore"){//������
                        // Invoke("Get_Player_Interacted",0.1f);
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
    void FixedUpdate() //�������� �����Ӹ��� ȣ��Ǵ� �����ֱ� �Լ�
    {
        //1. ���� �ش�
        //rigid.AddForce(inputVec);
        //2. �ӵ�����
        //rigid.velocity = inputVec;
        //3. ��ġ �̵�
        Vector2 nextVec = Inputvec * Player.Player_Speed * Time.fixedDeltaTime;
        Rigid.MovePosition(Rigid.position + nextVec);
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
    void OnMove(InputValue value) // �̵� �Լ�
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
        Playerdir = Vector3.left;
        FlipControl();
    }
    void OnFlipD()
    {
        Playerdir = Vector3.right;
        FlipControl();
    }
}
