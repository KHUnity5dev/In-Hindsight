using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemdatabase : MonoBehaviour
{
    public static Itemdatabase Databaseinstance;
    // Start is called before the first frame update
    void Awake()
    {
        Databaseinstance = this;
    }
    public List<Item> Item_DB =new List<Item> ();
    // Update is called once per frame

    public GameObject Fielditem_Prefab;
    public Vector3[] Pos;
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(Fielditem_Prefab, Pos[i],Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(Item_DB[Random.Range(0,3)]);
        }
    }
}
