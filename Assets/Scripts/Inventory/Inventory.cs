using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged on_item_changed_call_back;

    public List<Item> items = new List<Item>();

    public int space = 10;
    private int key_count = 0;
    private bool key_found = false;
    public Item full_key;

    public bool Add(Item i)
    {
        if (i.name == "Half_Key")
        {
            Debug.Log("Found half a key");
            key_count++;
        }

        if (items.Count >= space)
        {
            Debug.Log("Not enough space inside Inventory");
            return false;
        }

        if (key_count == 2)
        {
            items.Add(full_key);
            Item temp = items.Find(key => key.name == "Half_Key");
            items.Remove(temp);
            key_count = 0;
        }
        else
            items.Add(i);

        if(on_item_changed_call_back != null) //call back when things change locally
            on_item_changed_call_back.Invoke();

        return true;
    }

    public void Remove(Item i)
    {
        items.Remove(i);

        if (on_item_changed_call_back != null) //call back when things change locally
            on_item_changed_call_back.Invoke();
    }
}
