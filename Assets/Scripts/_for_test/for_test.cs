using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class for_test : MonoBehaviour
{
    public int[] items;
    public int[] equipments;
    public int[] relics;
    // Start is called before the first frame update
    // public GameObject equipment;

    public void Add_TO_Bag()
    {
        FindObjectOfType<Bag_System>().clear();
        foreach (var item in items)
        {
            Global.AddItemToBag(item,"item");
        }
        foreach (var item in equipments)
        {
            Global.AddItemToBag(item,"equipment");
        }
        foreach (var item in relics)
        {
            Global.AddItemToBag(item,"relic");
        }
    }

    public void save()
    {
        Global.SaveData();
        Global.AddHp(-10);
    }
    public void Set_false(GameObject ob)
    {
        Global.LeaveBattle();
        // equipment.SetActive(false);
        // this.gameObject.SetActive(false);
    }
    public void load()
    {
        Global.ReadData();
    }
    public void leave()
    {
        this.gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
