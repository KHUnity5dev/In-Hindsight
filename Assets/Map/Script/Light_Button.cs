using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Button : MonoBehaviour, Object_Interacted_Interface
{
    public GameObject Light;

    public void Get_Player_Interacted()
    {
        Light.GetComponent<Light>().On_Off();
    }


    public void Get_Enemy_Interacted()
    {
        Light.GetComponent<Light>().On();
    }

}
