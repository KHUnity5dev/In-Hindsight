using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    public GameObject Bullet;
    public bool OnTarget;
    private void Start()
    {
        OnTarget = false;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public void Attack(){
        GameObject temp =  Instantiate(Bullet, transform.position, Quaternion.identity);
        Destroy(temp , 10);
    }
    public GameObject Check()
    {
        LayerMask mask = LayerMask.GetMask("Player") | LayerMask.GetMask("Object"); //Player와 Object만 검출
        RaycastHit2D[] rayHit = new RaycastHit2D[5];
        Vector3[] Dir = new Vector3[5];
        Dir[0] = 5 * Vector3.right * (sprite.flipX == false ? 1 : -1);
        Dir[1] = 5 * Vector3.right * (sprite.flipX == false ? 1 : -1) + Vector3.up * 2;
        Dir[2] = 5 * Vector3.right * (sprite.flipX == false ? 1 : -1) + Vector3.down * 2;
        Dir[3] = 5 * Vector3.right * (sprite.flipX == false ? 1 : -1) + Vector3.up;
        Dir[4] = 5 * Vector3.right * (sprite.flipX == false ? 1 : -1) + Vector3.down;

        GameObject Return = null;
        for (int i = 0; i < 5; i++){

            Debug.DrawRay(rigid.position, Dir[i], new Color(0, 1, 0));
            rayHit[i] = Physics2D.Raycast
                (rigid.position, Dir[i], 5, mask);
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
