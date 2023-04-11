using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    private float Noise_Timer  = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Noise_Timer -= Time.deltaTime;
        if (Noise_Timer < 0f)
            Destroy(gameObject);
    }
}
