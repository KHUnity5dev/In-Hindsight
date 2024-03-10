using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Enemynum; //Enemy 종류번호
    public bool IsBoss;
    public EnemyAttack enemyAttack;
    public EnemyMove enemyMove;
    public EnemyStat enemyStat;
    public float AttackCoolTime;
    float CurAttackCoolTime;
    Rigidbody2D rigid;
    GameObject Scan;
    SpriteRenderer sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        CurAttackCoolTime = AttackCoolTime; //기본 1초
        enemyMove.IsBoss = IsBoss;
        enemyStat.IsBoss = IsBoss;
    }
    
    // Update is called once per frame
    void Update()
    {   
        Scan = enemyAttack.Check();
        
    }
    void FixedUpdate() {
        
        if (enemyAttack.OnTarget){
            transform.GetChild(0).gameObject.SetActive(true);// 대상 발견시 느낌표 뜸
            enemyMove.Chase(Scan);
            // if (IsBoss)
            // {

            // }
            CurAttackCoolTime -= 0.02f;
            if(CurAttackCoolTime <= 0.01f){
                CurAttackCoolTime = AttackCoolTime;
                enemyAttack.Attack(Scan);
            }
        } else{
            // if(IsBoss){
            //     enemyMove.Chase(Scan);
            // }
            // else
            transform.GetChild(0).gameObject.SetActive(false); // 대상 미 발견시 느낌표 없앰
            CurAttackCoolTime = AttackCoolTime;
        }
        if (!enemyMove.IsMove){
            enemyMove.Move();
        }
        if(rigid.velocity.x !=0)
            sprite.flipX = (rigid.velocity.x > 0)? false : true;

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Noise") {
            if(Enemynum != 3)
                enemyMove.Chase(other.gameObject);//소음 감지됐을때의 행동 함수 호출
        }
        // if ( other.gameObject.name == "Door"){
        //     other.gameObject.SendMessage("Get_Enemy_Interacted");
        //     Debug.Log("SendMessage");
        // }
    }
}
