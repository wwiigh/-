using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    [SerializeField] GameObject map;
    [SerializeField] GameObject bag;
    // Start is called before the first frame update
    public void save()
    {
        Global.SaveData();
        map.GetComponent<Map_Generate>().save();
        bag.GetComponent<Bag_System>().save();
        Event_Select.Save_Data();
    }
}
