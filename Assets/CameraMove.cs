using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float LeftMax =-10;
    float RightMax =10;
    public float width= 5;
    void Awake()
    {
        transform.position = GameObject.Find("Player").GetComponent<Transform>().position - new Vector3(0, 0, 10);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerPos = GameObject.Find("Player").GetComponent<Transform>().position;
        if (PlayerPos.x - width > LeftMax && PlayerPos.x + width < RightMax)
            transform.position = GameObject.Find("Player").GetComponent<Transform>().position - new Vector3(0,0,10);
    }
}
