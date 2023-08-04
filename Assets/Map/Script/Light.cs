using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    private GameObject Dark;
    public void Get_Player_Shooted()
    {
        Instantiate(Dark, transform);
    }
}
