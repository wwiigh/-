using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class gold_gen : MonoBehaviour
{
    // Start is called before the first frame update
    public Text Golden_number;
    void OnEnable()
    {
        int x = UnityEngine.Random.Range(50, 150) % 150;
        Golden_number.text = "" + x;
        Global.AddMoney(x);
        // Debug.Log("you get "+x);
    }

    

}
