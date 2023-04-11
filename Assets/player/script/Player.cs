using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Animator Anim;
    SpriteRenderer Renderer;

    private static Player Inst;  // 스태틱 플레이어 객체 하나 밖에 생성되지 않는다.
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
    private State playerstate; // 플레이어의 상태 
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
    private SightState playersight; // 플레이어의 시야 상태
    public static SightState Player_Sight
    {
        get { return Inst.playersight; }
        set { Inst.playersight = value; }
    } 
    [SerializeField]
    private float speed = 3f; //일반 속도
    public static float Player_Speed
    {
        get { return Inst.speed; }
        set { Inst.speed = value; }
    }
    [SerializeField]
    private float runspeed = 2f; //달릴때 속도 배속
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

    public float Noise_Timer = 1f; // 소음 주기

    void Awake()
    {
        if(Inst != null)
        {
            Destroy(gameObject);
            return;
        }
        Inst = this; // 플레이어를 참조하려면 이 코드가 가장먼저 실행되어야함 
        Anim = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    //public void Init()
    //{
    //    HP = MaxHP;
    //    playerSight = SightState.Normal;
    //    playerState = State.Idle;
    //}
    public void RunNoiseCreater() // 달릴때 소음 콜라이더를 만드는 함수
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
