using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class for_test : MonoBehaviour
{
    // Start is called before the first frame update
    public void save()
    {
        Global.SaveData();
        Global.AddHp(-10);
    }
    public void load()
    {
        Global.ReadData();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
