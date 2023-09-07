using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settle : MonoBehaviour
{
    public TextMeshProUGUI level;
    public TextMeshProUGUI Enemy;
    public TextMeshProUGUI Elite;
    public TextMeshProUGUI Boss;
    public TextMeshProUGUI Relic;
    public TextMeshProUGUI Victory;
    bool is_win = false;
    string end_level = Global.current_level.ToString();
    string end_enemy = Global.kill_enemys.ToString();
    string end_elite = Global.kill_elites.ToString();
    string end_boss = Global.kill_leaders.ToString();
    string end_relic = Global.kill_leaders.ToString();
    
    void Start()
    {
        change_text();
    }
    void change_text(){
        if(!is_win){
            Victory.text = "Defeat";
        }
        level.text = end_level;
        Enemy.text = end_enemy;
        Elite.text = end_elite;
        Boss.text = end_boss;
        Relic.text = end_relic;
    }
    // 傳入遊戲結束時，是贏還是輸，贏true，輸false
    public void game_is_end(bool win){
        is_win = win;
    }
    
}
