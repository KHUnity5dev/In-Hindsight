using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Button : riciever
{
    private void Get_Player_Interacted()
    {
        Debug.Log("pressed by player _ light");
    }
    private void Get_Enemy_Interacted()
    {
        Debug.Log("pressed by enemy_light");
    }

}

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
