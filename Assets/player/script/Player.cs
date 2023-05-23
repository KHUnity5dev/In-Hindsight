using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Animator Anim;
    SpriteRenderer Renderer;

    private static Player Inst;  // ����ƽ �÷��̾� ��ü �ϳ� �ۿ� �������� �ʴ´�.
    public enum State
    {
        Idle,
        Walk,
        Run,
        Dead
    };
    public enum SightState
    {
        Hide,
        Normal,
        Light
    }
    private int Hp;
    private int Maxhp = 100;
    public int Player_Hp
    {
        get
        {
            return Hp;
        }
    }
    [SerializeField]
    private State playerstate; // �÷��̾��� ���� 
    public static State Player_State
    {
        get
        {
            return Inst.playerstate;
        }
        set
        {
            Inst.playerstate = value;
        }
    }
    [SerializeField]
    private SightState playersight; // �÷��̾��� �þ� ����
    public static SightState Player_Sight
    {
        get { return Inst.playersight; }
        set { Inst.playersight = value; }
    } 
    [SerializeField]
    private float speed = 3f; //�Ϲ� �ӵ�
    public static float Player_Speed
    {
        get { return Inst.speed; }
        set { Inst.speed = value; }
    }
    [SerializeField]
    private float runspeed = 2f; //�޸��� �ӵ� ���?
    public static float Run_Speed
    {
        get { return Inst.runspeed; }
    }

    [SerializeField]
    private GameObject m_noiseprefab;
    public static GameObject Noise_Prefab
    {
        get { return Inst.m_noiseprefab; }
    }

    public float Noise_Timer = 1f; // ���� �ֱ�

    void Awake()
    {
        if(Inst != null)
        {
            Destroy(gameObject);
            return;
        }
        Inst = this; // �÷��̾ �����Ϸ��� �� �ڵ尡 �������? ����Ǿ���� 
        Anim = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    //public void Init()
    //{
    //    HP = MaxHP;
    //    playerSight = SightState.Normal;
    //    playerState = State.Idle;
    //}
    public void RunNoiseCreater() // �޸��� ���� �ݶ��̴��� �����? �Լ�
    {
        Noise_Timer -= Time.deltaTime;
        if (Noise_Timer < 0f)
        {
            GameObject Noise = Instantiate(Noise_Prefab);
            Noise_Timer = 1f;
            Noise.transform.position = gameObject.transform.position;
        }
    }
    void Update()
    {
        if (Player_State == State.Run)
        {
            RunNoiseCreater();
        }
    }
    public static void Dead()
    {
        Player_State = State.Dead;
        Inst.Anim.SetTrigger("isDead");
        Inst.GetComponent<Playercontrol>().enabled = false;
        Inst.GetComponent<PlayerInput>().enabled = false;
    }
}
