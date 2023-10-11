using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int Enemynum; //Enemy 종류번호
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    public GameObject Bullet;
    public bool OnTarget;
    public float AttackRange = 5;
    private void Start()
    {
        Enemynum = gameObject.GetComponent<Enemy>().Enemynum;
        OnTarget = false;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public void Attack(GameObject scan){
        // GameObject temp =  Instantiate(Bullet, transform.position, Quaternion.identity);
        // Destroy(temp , 10);
        // scan을 죽이는 코드
        if(Enemynum == 1){
            transform.Find("explod").gameObject.SendMessage("Get_Player_Shooted");
            return;
        }
        Player.Dead();
    }
    public GameObject Check()
    {
        LayerMask mask = LayerMask.GetMask("Player") | LayerMask.GetMask("Object") | LayerMask.GetMask("Smoke"); //Player와 Object와 Smoke만 검출
        RaycastHit2D[] rayHit;
        Vector3[] Dir;
        if(Enemynum == 3){
            Dir = new Vector3[1];
            rayHit = new RaycastHit2D[1];
            Vector3 tempVector = new Vector3(Mathf.Cos(transform.rotation.eulerAngles.z),
                                         Mathf.Sin(transform.rotation.eulerAngles.z) , 0);
            Dir[0] = AttackRange * tempVector * (sprite.flipX == false ? 1 : -1);
        } else{
            Dir = new Vector3[5];
            rayHit= new RaycastHit2D[5];
            Dir[0] = AttackRange * Vector3.right * (sprite.flipX == false ? 1 : -1);
            Dir[1] = AttackRange * Vector3.right * (sprite.flipX == false ? 1 : -1) + Vector3.up * 2;
            Dir[2] = AttackRange * Vector3.right * (sprite.flipX == false ? 1 : -1) + Vector3.down * 2;
            Dir[3] = AttackRange * Vector3.right * (sprite.flipX == false ? 1 : -1) + Vector3.up;
            Dir[4] = AttackRange * Vector3.right * (sprite.flipX == false ? 1 : -1) + Vector3.down;
        }

        GameObject Return = null;
        for (int i = 0; i < Dir.Length; i++){
            Debug.DrawRay(rigid.position, Dir[i], new Color(0, 1, 0));
            rayHit[i] = Physics2D.Raycast
                (rigid.position, Dir[i], AttackRange, mask);
            if (rayHit[i].collider != null)
            {
                Return = rayHit[i].transform.gameObject;
                if (Mathf.Pow(2,rayHit[i].transform.gameObject.layer) == LayerMask.GetMask("Player")){
                    OnTarget = true;
                    return rayHit[i].transform.gameObject;
                }
            }
        }
        OnTarget = false;
        return Return;
    }
}
