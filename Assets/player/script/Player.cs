using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Animator Anim;
    SpriteRenderer Renderer;
    BoxCollider2D BoxCollider;
    
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
    private float runspeed = 2f; //�޸��� �ӵ� ���
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

    [SerializeField]
    private float m_noisetimer = 1f; // ���� �ֱ�
    public static float Noise_Timer
    {
        get { return Inst.m_noisetimer; }
        set { Inst.m_noisetimer = value; }
    }
    [SerializeField]
    private float m_runnoise = 10f;
    public static float Run_Noise
    {
        get { return Inst.m_runnoise; }
    }
    [SerializeField]
    private Scene_Mana m_scenemanager;
    public static Scene_Mana Scene_Mana
    {
        get { return Inst.m_scenemanager; }
    }
    void Awake()
    {
        if(Inst != null)
        {
            Destroy(gameObject);
            return;
        }
        Inst = this; // �÷��̾ �����Ϸ��� �� �ڵ尡 ������� ����Ǿ���� 
        Anim = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
        BoxCollider = GetComponent<BoxCollider2D>();

    }

    //public void Init()
    //{
    //    HP = MaxHP;
    //    playerSight = SightState.Normal;
    //    playerState = State.Idle;
    //}
    static public void NoiseCreater(float size) // �޸��� ���� �ݶ��̴��� ����� �Լ�
    {
        Noise_Timer -= Time.deltaTime;
        if (Noise_Timer < 0f)
        {
            GameObject Noise = Instantiate(Noise_Prefab);
            Noise_Timer = 1f;
            Noise.transform.position = Inst.gameObject.transform.position;
            Noise.transform.localScale = new Vector3(size, size, 1);
        
        }

    }
    void Update()
    {
        if (Player_State == State.Run)
        {
            NoiseCreater(10f);
        }
    }
    public static void Dead()
    {
        Player_State = State.Dead;
        Inst.Anim.SetTrigger("isDead");
        Inst.m_scenemanager.SendMessage("GameOver");
        //Inst.BoxCollider.size = new Vector2(1.245603f, 1.234156f);
        //Inst.BoxCollider.offset = new Vector2(-0.3350806f, 0.293649f);
        Inst.GetComponent<Playercontrol>().enabled = false;
        Inst.GetComponent<PlayerInput>().enabled = false;
        AudioManager.instance.PlayBgm(false);
    }
}
