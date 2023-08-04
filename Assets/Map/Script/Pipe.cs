using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    private GameObject Smoke;
    public void Get_Player_Shooted()
    {
        Instantiate(Smoke, transform);
    }
}
