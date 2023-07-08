using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Money : MonoBehaviour
{

    public TMP_Text money_num;
    int money_now;    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        money_now = Global.money;
        money_num.text = money_now.ToString();
    }
}
