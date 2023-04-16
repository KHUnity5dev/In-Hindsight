using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Button : MonoBehaviour
{
    [SerializeField]
    private GameObject Stage_Manager;
    public void Get_Player_Interacted()
    {
        Stage_Manager.SendMessage("Next_Level");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
