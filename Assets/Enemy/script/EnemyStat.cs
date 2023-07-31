using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float bodyHP = 100; // 몸 피격용 HP
    public float headHP = 50; // 머리 피격용 HP
    public GameObject[] DropItem;
    public AudioClip deathSound; // 죽는 소리
    void Start() {
        
    }
    void FixedUpdate() {
    
    }
    public void OnDamaged(float dmg)
    {
        bodyHP -= dmg;
        if (bodyHP <= 0)
        {
            Die(); // 몸 피격으로 죽음
        }
    }
    // public void OnBodyDamaged(float dmg)
    // {
    //     bodyHP -= dmg;
    //     if (bodyHP <= 0)
    //     {
    //         Die(); // 몸 피격으로 죽음
    //     }
    // }
    // public void OnHeadDamaged(float dmg)
    // {
    //     headHP -= dmg;
    //     if (headHP <= 0)
    //     {
    //         Die(); // 머리 피격으로 죽음
    //     }
    // }
    void Die(){
        int i = 0;
        foreach(GameObject item in DropItem){
            Instantiate(item, transform.position + Vector3.right*i, Quaternion.identity);
            i++;
        }//아이템 드랍
        AudioSource.PlayClipAtPoint(deathSound, transform.position); // 죽는 소리 재생
        gameObject.layer = LayerMask.NameToLayer("DeadEnemy"); // 레이어 변경
        Destroy(gameObject, 0.1f);//0.1초뒤 사망
    }
}