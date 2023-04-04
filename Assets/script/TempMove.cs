using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMove : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y);
    }
}
