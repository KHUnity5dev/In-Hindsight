using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : MonoBehaviour
{
    private Collider2D[] ObjectsInRange;
    public bool ISGrenade;
    bool isActivated = false;

    [SerializeField]
    private float Range;
    void Start(){
        Invoke("Get_Player_Shooted",2f);
        Destroy(gameObject,2f);
    }
    public void Get_Player_Shooted()
    {
        if(isActivated) return;
        isActivated = true;
        ObjectsInRange = Physics2D.OverlapCircleAll(gameObject.transform.position, Range, LayerMask.GetMask("Enemy") );
        for (int i = 0; i < ObjectsInRange.Length; i++)
        {
            if(ObjectsInRange[i].gameObject.name.Split("ss")?[1] == "_Enemy")
                ObjectsInRange[i].gameObject.SendMessage("OnDamaged", 100);
        }

        ObjectsInRange = Physics2D.OverlapCircleAll(gameObject.transform.position, Range, LayerMask.GetMask("Player"));
        for (int i = 0; i < ObjectsInRange.Length; i++)
        {
            Player.Dead();
        }
        ObjectsInRange = Physics2D.OverlapCircleAll(gameObject.transform.position, Range, LayerMask.GetMask("Object") | LayerMask.GetMask("Enemy"));
        for (int i = 0; i < ObjectsInRange.Length; i++)
        {
            if(ObjectsInRange[i].gameObject.name.Split('s')[0] == "Bo")
                ObjectsInRange[i].gameObject.GetComponent<BossBomb>()?.Get_Player_Shooted();
            if(ObjectsInRange[i].gameObject.tag == "Shootable")
                Destroy(ObjectsInRange[i].gameObject);
        }
        Destroy(gameObject);
    }
}
