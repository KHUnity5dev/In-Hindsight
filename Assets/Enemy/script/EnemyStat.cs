using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float HP = 100;
    public GameObject[] DropItem;
    void Start() {
        
    }
    void FixedUpdate() {
        // HP -= 0.2f;
        // if(HP <= 0)
        //     Die();//죽음
    }
    public void OnDamaged(float dmg){
        HP -= dmg;
        if(HP <= 0) {
            Die();//죽음
            //죽고나서 layer 죽은 enemy로 바꿔야함.
        }
    }
    void Die(){
        int i = 0;
        foreach(GameObject item in DropItem){
            Instantiate(item, transform.position + Vector3.right*i, Quaternion.identity);
            i++;
        }//아이템 드랍
        //죽는 소리 추가요망
        Destroy(gameObject,1.0f);//1초뒤 사망
    }
}
