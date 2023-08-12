using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settle : MonoBehaviour
{
    public TextMeshProUGUI layer;
    public TextMeshProUGUI Enemy;
    public TextMeshProUGUI Elite;
    public TextMeshProUGUI Boss;
    public TextMeshProUGUI Relic;
    public TextMeshProUGUI Victory;
    bool is_win = false;
    string end_layer = "87", end_enemy = "87", end_elite = "87", end_boss = "87", end_relic = "87";
    
    void Start()
    {
        change_text();
    }
    void change_text(){
        if(!is_win){
            Victory.text = "Defeat";
        }
        layer.text = end_layer;
        Enemy.text = end_enemy;
        Elite.text = end_elite;
        Boss.text = end_boss;
        Relic.text = end_relic;
    }
    
}
