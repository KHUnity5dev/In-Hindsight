using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Playercontrol : MonoBehaviour
{
    Vector2 Inputvec;
    
    Rigidbody2D Rigid;
    SpriteRenderer Spriterenderer;
    Animator Anim;
    Vector2 Playerdir = Vector2.right;
    bool QuestUIOn = false;
    bool InvenUIOn = false;
    bool PauseUIOn = false;
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
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Footstep);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)) // �޸��� ��
        {
            Player.Player_Speed = Player.Player_Speed - 2f;
            Player.Player_State = Player.State.Walk;
        }
        Pause();
        ReloadCaller();
        Gunshot();
        QuestCaller();
        InvenCaller();

        Debug.DrawRay(Rigid.position + 0.2f * Vector2.up, 5*Playerdir, new Color(1, 0, 0));


        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("#####"+Playerdir);
            Vector2 rayPosition = Rigid.position + 0.2f * Vector2.up;
            LayerMask mask = LayerMask.GetMask("OpenDoor") | LayerMask.GetMask("Object");
            RaycastHit2D rayHitobj;
            RaycastHit2D rayHitenemy;
            RaycastHit2D rayHitNPC;
            rayHitobj = Physics2D.Raycast(rayPosition, Playerdir, 1f, mask);
            rayHitenemy = Physics2D.Raycast(rayPosition, Playerdir, 1f, LayerMask.GetMask("Enemy"));
            rayHitNPC = Physics2D.Raycast(rayPosition, Playerdir, 1f, LayerMask.GetMask("NPC"));


            if (rayHitenemy)
            {
                if (rayHitenemy.distance < 0.5f || rayHitobj.distance > -0.5f)
                {
                    Debug.Log(rayHitenemy.collider.name);
                    GameObject obj = rayHitenemy.collider.gameObject;
                    if (obj.tag == "Enemy") // �ϻ�
                    {
                        Debug.Log("Enemy detected!!!!");
                        Anim.SetTrigger("Amsal!");
                        obj.GetComponent<EnemyStat>().OnDamaged(100);
                    }
                    else if (obj.tag == "Dead_Enemy")
                    {
                        obj.SendMessage("Get_Enemy_Item");
                    }
                }
            }
            if (rayHitobj)
            {
                if (rayHitobj.distance < 0.5f || rayHitobj.distance > -0.5f)
                {
                    Debug.Log(rayHitobj.collider.name);
                    GameObject obj = rayHitobj.collider.gameObject;
                    if(obj.tag == "Door"){//������
                        // Invoke("Get_Player_Interacted",0.1f);
                        obj.SendMessage("Get_Player_Interacted", gameObject);
                        Debug.Log("dddddddddd");
                    }
                    else if(obj.tag == "Stair")
                    {
                        obj.SendMessage("Get_Player_Interacted", gameObject);
                    }
                    else if (obj.tag == "Box")
                    {
                        obj.SendMessage("Get_Player_Interacted", gameObject);
                    }
                }
            }
            else if (rayHitNPC.collider != null)
            {
                if (rayHitNPC.distance < 0.5f || rayHitobj.distance > -0.5f)
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
        if (PlayerInventory.Bullets > 0 && PlayerInventory.Magazine < PlayerInventory.Maxmagazine)
        {
            PlayerInventory.Magazine = PlayerInventory.Maxmagazine;
            Debug.Log(PlayerInventory.Bullets);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Reload);
        }
    }
    bool Isgrounded()
    {
        return Physics2D.Raycast(Rigid.position + Vector2.up, Vector3.down, 1.08f, LayerMask.GetMask("Ground")); //���� ĳ���Ͱ� �ٲ�� ���� ���� �ʿ�
    }
    void OnMove(InputValue value) // �̵� �Լ�
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Footstep);
        Inputvec = value.Get<Vector2>();
        Inputvec.y = 0f;
    }
    void OnFlipA()
    {
        Playerdir = Vector2.left;
        FlipControl();
    }
    void OnFlipD()
    {
        Playerdir = Vector2.right;
        FlipControl();
    }
    void GunNoiseCreater()
    {
        Player.Noise_Timer = -1;
        Player.NoiseCreater(PlayerInventory.MyGun.Gun_Noise());
    }
    void Gunshot()
    {
        if(SceneManager.GetActiveScene().name == "LobbyMap"){
            return;
        }
        if (InvenUIOn || QuestUIOn || PauseUIOn)
            return;
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)) - (Vector3)(Rigid.position + Vector2.up);
        if (Input.GetMouseButtonDown(0) && PlayerInventory.Magazine > 0) // �ѽ��
        {
            PlayerInventory.Magazine--;
            PlayerInventory.Bullets--;
            Debug.Log(PlayerInventory.Magazine);
            Anim.SetTrigger("Attack");
            Playerdir = point.normalized;
            GunNoiseCreater();
            FlipControl();
            RaycastHit2D rayHit = Physics2D.Raycast(Rigid.position, point, 10f, LayerMask.GetMask("Enemy") | LayerMask.GetMask("Object"));

            if (rayHit)
            {
                GameObject obj = rayHit.collider.gameObject;
                if (obj.tag == "Enemy")
                {
                    obj.SendMessage("OnDamaged", PlayerInventory.MyGun.Damage());
                }
                else if (obj.tag == "Shootable")
                {
                    obj.SendMessage("Get_Player_Shooted");
                }
                Debug.Log(obj.name);
            }

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Gunshot);
        }
        Debug.DrawRay(Rigid.position, point, new Color(1, 0, 1)); // ���콺 ������ ����
    }
    void ReloadCaller()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Invoke("Reload", 1);
        }
    }
    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // �޸��� ����
        {
            Player.Dead();
        }
    }
    void QuestCaller()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!QuestUIOn)
            {
                //Quest UI Active
                QuestUIOn = true;
                Debug.Log("ON!!!1");
            }
            else
            {
                //Quest Ui unActive
                QuestUIOn = false;
            }    
        }
    }
    void InvenCaller()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!InvenUIOn)
            {
                //Quest UI Active
                InvenUIOn = true;
            }
            else
            {
                //Quest Ui unActive
                InvenUIOn = false;
            }
        }
    }
}