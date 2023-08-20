using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject Smoke;
    public void Get_Player_Shooted()
    {
        Debug.Log("666");
        Instantiate(Smoke, transform);

        gameObject.layer = 8;

    }
}
