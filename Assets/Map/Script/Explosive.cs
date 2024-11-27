using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour, Object__Shooted_Interface, Object__Explodable_Interface
{
    public void Explod(float Range)
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
        ObjectsInRange = Physics2D.OverlapCircleAll(gameObject.transform.position, Range, LayerMask.GetMask("Object"));
        for (int i = 0; i < ObjectsInRange.Length; i++)
        {
            Object__Shooted_Interface Shootable = ObjectsInRange[i].GetComponent<Object__Shooted_Interface>();
            if (Shootable != null)
            {
                Destroy(ObjectsInRange[i].gameObject);
            }
            Object__Explodable_Interface explodable = ObjectsInRange[i].GetComponent<Object__Explodable_Interface>();
            if (explodable != null)
            {
                explodable.Get_Exploded();
            }
        }
        Destroy(gameObject);
        
    }

    private Collider2D[] ObjectsInRange;

    [SerializeField]
    private float Range;

    public void Get_Player_Shooted()
    {
        Explod(Range);
    }
    public void Get_Exploded()
    {
        Explod(Range);
    }
    public void Get_Enemy_Shooted()
    {
        Explod(Range);
    }

}
