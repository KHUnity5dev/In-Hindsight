using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    private int sight;
    private int head;
    private int magazine;
    private float damage = 10;
    private float gunnoise = 20;
    public int Sight
    {
        get { return sight; }
        set { sight = value; }
    }
    public int Head { 
        get { return head;}
        set { head = value; }
    }
    public int Magazine
    {
        get { return magazine; }
        set { magazine = value; }
    }
    public float Damage()
    {
        float AllDamage = damage;
        if(head == 1)
        {
            AllDamage += 10;
        }
        return AllDamage;
    }
    public float Gun_Noise()
    {
        float AllNoise = gunnoise;
        if (head == 2)
            AllNoise -= 10;
        return AllNoise;
    }
}
public class PlayerInventory : MonoBehaviour
{
    #region Singleton
    public static PlayerInventory Instance;
    public static Gun MyGun = new Gun();
    public static int Bullets = 100;
    public static int Maxmagazine = 10;
    public static int Magazine = 10;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        //Bullets = PlayerPrefs.GetInt("Bullets");
        if (Bullets < Maxmagazine)
        {
            Magazine = Bullets;
        }
        else
            Magazine = Maxmagazine;
    }
    #endregion




    void Update()
    {
        
    }
}
