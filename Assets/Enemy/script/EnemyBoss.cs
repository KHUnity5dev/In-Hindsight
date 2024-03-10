using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBoss : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    public bool OnTarget;
    public float AttackRange = 2;
    [SerializeField] GameObject BossBomb;
    [SerializeField] GameObject BossGrenade;
    [SerializeField] float time = 0f;
    [SerializeField] float Hp = 50f;
    private void Start()
    {
        OnTarget = false;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    void FixedUpdate(){
        if(Check()){
            transform.GetChild(0).gameObject.SetActive(true);
            Player.Dead();
        }
        time += 0.02f;
        if(time >= 3f){
            time = 0f;
            int rand_num = Random.Range(0,3);
            Debug.Log("randnum : " + rand_num.ToString());
            switch(rand_num){
                case 0:
                    for( int i = 1; i <= 10; i++) {
                        Instantiate(BossBomb, new Vector3(transform.position.x - 2 - (2*i), transform.position.y+5) 
                        , Quaternion.identity);
                    }
                    break;
                case 1:
                    for( int i = 1; i <= 10; i++) {
                        Instantiate(BossBomb, new Vector3(transform.position.x - 2 - (2*i+1), transform.position.y+5) 
                        , Quaternion.identity);
                    }
                    break;
                case 2:
                    StartCoroutine(ThrowGrenade());
                    break;
                default:
                    break;
            }
        }
    }
    void OnDamaged(float Damage)
    {
        if(Damage == 100) // 폭탄만
            Hp -= 1;
        if(Hp<0){
            Destroy(gameObject);
            GameObject.Find("SceneMana").SendMessage("GameClear");
            SceneManager.LoadScene("Ending_Scene");
        }
    }
    IEnumerator ThrowGrenade(){
        for(int i = 1; i <= 10; i++){
            GameObject grenade = Instantiate(BossGrenade, transform.position, Quaternion.identity);
            int rand_num = Random.Range(0,50) * 10;
            Vector3 point = new Vector2(-1, 1);
            grenade.GetComponent<Rigidbody2D>().AddForce
            (point.normalized * (300 + rand_num)+ Vector3.up*100, ForceMode2D.Force);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
    public void Attack(GameObject scan){
        Player.Dead();
    }
    public GameObject Check()
    {
        LayerMask mask = LayerMask.GetMask("Player"); //Player와 Object와 Smoke만 검출
        RaycastHit2D[] rayHit;
        Vector3[] Dir;
        Dir = new Vector3[5];
        rayHit= new RaycastHit2D[5];
        Dir[0] = AttackRange * Vector3.right * (sprite.flipX == false ? 1 : -1);
        Dir[1] = AttackRange * Vector3.right * (sprite.flipX == false ? 1 : -1) + Vector3.up * 2;
        Dir[2] = AttackRange * Vector3.right * (sprite.flipX == false ? 1 : -1) + Vector3.down * 2;
        Dir[3] = AttackRange * Vector3.right * (sprite.flipX == false ? 1 : -1) + Vector3.up;
        Dir[4] = AttackRange * Vector3.right * (sprite.flipX == false ? 1 : -1) + Vector3.down;
        

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
