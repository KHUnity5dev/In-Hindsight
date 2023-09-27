using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explod : MonoBehaviour
{
    private Collider2D[] ObjectsInRange;

    [SerializeField]
    private float Range;

    public void Get_Player_Shooted()
    {

        ObjectsInRange = Physics2D.OverlapCircleAll(gameObject.transform.position, Range, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < ObjectsInRange.Length; i++)
        {
            ObjectsInRange[i].gameObject.SendMessage("OnDamaged", 100);
        }

        ObjectsInRange = Physics2D.OverlapCircleAll(gameObject.transform.position, Range, LayerMask.GetMask("Player"));
        for (int i = 0; i < ObjectsInRange.Length; i++)
        {
            Player.Dead();
        }
        Destroy(gameObject);
    }

}
