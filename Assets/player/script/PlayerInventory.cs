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
    public int Head
    {
        get { return head; }
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
        if (head == 1)
        {
            AllDamage += 10;
        }
        return AllDamage;
    }
    public float Accuracy()
    {
        return Random.Range(0.0f, 2.0f);
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
    public static int Bullets = 0;
    public static int Grenade = 0;
    public static int Maxmagazine = 10;
    public static int Magazine = 0;
    [SerializeField]
    private GameObject m_grenade;
    public static GameObject Grenade_pref
    {
        get { return Instance.m_grenade; }
    }
    void OnEnable()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Debug.Log("Player : StageStart");
        Bullets = PlayerPrefs.GetInt("Bullets");
        Grenade = PlayerPrefs.GetInt("Grenade");

        if (Bullets < Maxmagazine)
        {
            Magazine = Bullets;
        }
        else
            Magazine = Maxmagazine;
    }
    void FixedUpdate()
    {
    }

    #endregion

}