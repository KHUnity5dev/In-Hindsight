using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Mana : MonoBehaviour
{

    [SerializeField]
    private GameObject Stage_Manager;

    public void Restart_Level()
    {
        Stage_Manager.SendMessage("Next_Level");
    }
    public void Load_Level()
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
