using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Health : MonoBehaviour
{
    public Image health_bar;
    public TMP_Text health_num;
    int health_max;
    int health_now;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health_max = Global.player_max_hp;
        health_now = Global.player_hp;
        health_num.text = health_now + "/" + health_max ;
        health_bar.fillAmount = (float)health_now / (float)health_max;
        GetComponent<UI_Control>().Change_text("生命值 (" + health_now + "/" + health_max + ")");
    }
}
