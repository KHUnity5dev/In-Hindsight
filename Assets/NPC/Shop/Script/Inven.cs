using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inven : MonoBehaviour
{
    #region Singleton
    public static Inven instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    void Start()
    {

    }

    public List<ItemInfo> items = new List<ItemInfo>();
    public void AddItem(ItemInfo _item)
    {
        items.Add(_item);
        onChangeItem.Invoke();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
