using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool IsMove;
    public float speed; //이동 속도
    protected Vector2 direction; // Enemy가 이동하는 방향
    private Rigidbody2D rigid; // Rigidbody2D 컴포넌트
    public int Enemynum; //Enemy 종류번호, 순찰패턴
    public int EnemyAI; // AI 고지능 정도를 판단함.
    public float[,] PatrolArray; //순찰 패턴 Array
    public bool IsChase; //Chase해야될때 Enemy.cs에서 true로만들고 Chase들어갈때 false 만든다.
    private Coroutine patrolCoroutine; // Coroutine 담는 변수
    Vector2 Target;
    void Start()
    {
        Target = Vector2.zero;
        IsMove = false;
        IsChase = false;
        PatrolArray = new float[,] {{speed,speed,speed,0,-speed,-speed,speed,0,-speed,-speed}};
        //PatrolArray 2차원배열 동적할당으로 패턴 넣어놓기
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(speed, 0);
    }

    public void Move() //순찰
    {
        IsMove = true;
        patrolCoroutine = StartCoroutine(Patrol());//0번 종류의 Enemy순찰
    }
    //Noise 방향으로 이동하는 Chase
    public void Chase(GameObject Noise)
    {
        IsChase = true;
        if (patrolCoroutine != null)
        {
            StopCoroutine(patrolCoroutine);
        }
        Vector2 noiseDirection = Noise.transform.position;
        Vector2 positionDiff = new Vector2(noiseDirection.x - transform.position.x 
                                        , noiseDirection.y - transform.position.y);
        direction = positionDiff.normalized.x > 0 ? Vector2.right : Vector2.left;
        Debug.Log(positionDiff);
        Debug.Log(direction);
        rigid.velocity = direction*speed;
        Target = noiseDirection;
    }
    private void FixedUpdate() {
        //Chase상태가 됐을 때 끝까지 Chase 한 후 다시 Patrol
        if(IsChase){
            Debug.Log(Target);
            if(Target.x - transform.position.x < 0.5f && Target.x - transform.position.x > -0.5f){
                Debug.Log("골인");
                if (patrolCoroutine != null)
                    patrolCoroutine = StartCoroutine(Patrol());
                Target = Vector2.zero;
                IsChase = false;
            }
        }
    }
    IEnumerator Patrol()
    {
        //종류에 맞는 순찰패턴
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(PatrolArray[Enemynum,i]);
            Debug.Log(rigid.velocity);
            rigid.velocity = new Vector2(PatrolArray[Enemynum,i], rigid.velocity.y); //velocity는 원래 speed가 있고, 방향만 패턴Array에서 가져와서 곱함
            IsMove = (i == 9) ? false : true;
            yield return new WaitForSeconds(2f);
        }

    }
}