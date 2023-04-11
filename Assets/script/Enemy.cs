using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyAttack enemyAttack;
    public EnemyMove enemyMove;
    public EnemyStat enemyStat;
    public float AttackCoolTime;
    float CurAttackCoolTime;
    GameObject Scan;
    
    // Start is called before the first frame update
    void Start()
    {
        CurAttackCoolTime = AttackCoolTime;
    }
    
    // Update is called once per frame
    void Update()
    {   
        Scan = enemyAttack.Check();
    }
    void FixedUpdate() {
        CurAttackCoolTime -= 0.02f;
        if (enemyAttack.OnTarget){
            if(CurAttackCoolTime <= 0.01f){
                enemyAttack.Attack();
                CurAttackCoolTime = AttackCoolTime;
            }
        }
        if (!enemyMove.IsMove){
            enemyMove.Move();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Noise") {
            enemyMove.Chase(other.gameObject);//소음 감지됐을때의 행동 함수 호출
        }
    }
}
