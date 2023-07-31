using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadnBody : MonoBehaviour
{
    public bool IsHead;
    GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        Enemy = transform.parent.gameObject;
    }
    public void OnDamaged(float dmg)
    {
        Debug.Log("ThisisHeadorBody");
        if(IsHead){
            Enemy.SendMessage("OnDamaged", 10*dmg);
        }
        else{
            Enemy.SendMessage("OnDamaged", dmg);
        }
    }
}
