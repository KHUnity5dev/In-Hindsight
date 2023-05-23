using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Itemeft/Consumable/Health")]
public class Itemhealingeffect : Itemeffect
{
    public int Healingpoint = 0;
    public override bool ExecuteRole()
    {
        Debug.Log("PlayerHp Add:" + Healingpoint);
        return true;
    }
}
