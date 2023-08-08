using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class for_test : MonoBehaviour
{
    // Start is called before the first frame update
    // public GameObject equipment;
    public void save()
    {
        Global.SaveData();
        Global.AddHp(-10);
    }
    public void Set_false(GameObject ob)
    {
        ob.SetActive(false);
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
