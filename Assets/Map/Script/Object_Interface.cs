using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Object_Interacted_Interface
{
    void Get_Player_Interacted();
    void Get_Enemy_Interacted();
}
public interface Object__Shooted_Interface
{
    void Get_Player_Shooted();
    void Get_Enemy_Shooted();
}
public interface Object__Explodable_Interface
{
    void Get_Exploded();
}


public interface Object__OnCollid_Interface
{
    void Get_Player_Collid();
    void Get_Enemy_Collid();
}
