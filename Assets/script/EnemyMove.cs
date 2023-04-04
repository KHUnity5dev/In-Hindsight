using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Queue<int> Movequeue;
    public float MoveSpeed = 2;
    // Start is called before the first frame update
    public bool IsMove;
    void Start()
    {
        Movequeue = new Queue<int>();
        IsMove = false;
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RightMove(){
        rigid.velocity = new Vector2(MoveSpeed,0);
    }
    void LeftMove(){
        rigid.velocity = new Vector2(-MoveSpeed,0);
    }
    public void Move(){
        IsMove = true;
        //move큐에 어떻게 이동할 지 저장
        for (int i = 0; i < 10 ; i ++){
            Movequeue.Enqueue(Random.Range(0,2));
        }
        int[] nextMove = new int[10];
        //nextmove에 다시 저장, 큐에서 빼냄
        for (int i = 0; i< 10; i++){
            nextMove[i] = Movequeue.Dequeue();
        }
        RightMove(); // 임시 코드
        //nextmove에 따라 좌,우이동
        // ---------------------
        // for(int i = 0; i<10; i++){
        //     if(nextMove[i] == 0){
        //         RightMove();
        //     }else if (nextMove[i] == 1){
        //         LeftMove();
        //     }
        // }
        //-------------------------
        // 위 for문 부분은 Coroutine 을 사용해서 for문 하나 돌때마다 시간지연을 시켜야합니다.
        // 좌,우 이동간에 만약 0.5초~ 1.5초 사이의 텀이 있다면 왼쪽으로 이동하다가 오른쪽으로 이동하다가
        // 왼쪽,왼쪽,오른쪽,왼쪽,오른쪽,오른쪽,오른쪽 (...) 이런식으로 패턴형으로 움직일 수 있습니다.
        // 랜덤으로 할지 패턴을 미리 여럿 정해놔서 리스트에 담아두고 사용할지 Enemy 종류마다 서로다른 정해진 패턴을 사용할지 생각해야합니다
        // 시간 지연 후
        Invoke("temp",2);
    }
    void temp() {
        IsMove = false;
        MoveSpeed *= -1;
    }
}
