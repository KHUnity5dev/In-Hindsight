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
    int Playerdir; // 1�̸� ������ -1�̸� ����
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
        if(Input.GetMouseButtonDown(0))
            Anim.SetTrigger("Attack");

        if (Playerdir==-1) // �÷��̾��� ���� Ȯ��
        {
            Raydir = Vector3.left;
            Debug.DrawRay(Rigid.position + Vector2.up, Raydir, new Color(1, 0, 0));

        }
        else //�÷��̾��� ���� Ȯ��
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
        Spriterenderer.flipX = true;
        Playerdir = -1;
    }
    void OnFlipD()
    {
        Spriterenderer.flipX = false;
        Playerdir = 1;
    }
}
