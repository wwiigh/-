using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Implement : MonoBehaviour
{
    // public delegate void ReturnEnemy(GameObject enemy);
    // ReturnEnemy selectEnemy_callback = null;
    public int use_count = 0;
    GameObject player;
    GameObject[] enemys;
    void Start()
    {
        use_count = 0;

    }
    void Update()
    {
       
        if(Map_System.now_state != Map_System.map_state.fight)
        {
            use_count = 0;
        }
    }
    public bool Use_Item(int id)
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        switch (id)
        {
            case 101:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                foreach (var enemy in enemys)
                {
                    enemy.GetComponent<Character>().AddStatus(Status.status.weak,2);
                }
                break;
            case 102:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 103:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                player.GetComponent<Character>().AddStatus(Status.status.invincible,1);
                break;
            case 104:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                foreach (var enemy in enemys)
                {
                    int hp = enemy.GetComponent<Character>().GetHP();
                    int attack = (int)(hp * 0.1f);
                    enemy.GetComponent<Character>().LoseHP(attack);
                    player.GetComponent<Character>().Heal(attack);
                }
                break;
            case 105:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                player.GetComponent<Character>().AddArmor(10);
                break;
            case 106:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                foreach (var enemy in enemys)
                {
                    enemy.GetComponent<Character>().LoseHP(10);
                }
                break;
            case 107:
                Global.AddHp(15);
                break;
            case 108:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                foreach (var enemy in enemys)
                {
                    enemy.GetComponent<Character>().AddStatus(Status.status.weak,5);
                }
                break;
            case 109:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                FindObjectOfType<BattleController>().SelectEnemy(KillEnemy);
                break;
            case 110:
                Global.AddHp(10);
                Global.AddSan(-1);
                break;
            case 111:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 112:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                foreach (var enemy in enemys)
                {
                    int hp = enemy.GetComponent<Character>().GetHP();
                    int attack = (int)(hp * 0.1f);
                    enemy.GetComponent<Character>().LoseHP(attack);
                }
                break;
            case 113:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                foreach (var enemy in enemys)
                {
                    List<(Status.status _status, int level)> status = enemy.GetComponent<Character>().GetAllStatus();
                    List<(Status.status _status, int level)> status_copy = new List<(Status.status _status, int level)>();
                    foreach (var item in status)
                    {
                        status_copy.Add(item);
                        // enemy.GetComponent<Character>().AddStatus(item._status,-item.level);
                    }
                    foreach (var item in status_copy)
                    {
                        enemy.GetComponent<Character>().AddStatus(item._status,-item.level);
                    }
                }
                break;
            case 114:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                foreach (var enemy in enemys)
                {
                    enemy.GetComponent<Character>().AddStatus(Status.status.burn,10);
                }
                break;
            case 115:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                foreach (var enemy in enemys)
                {
                    int rv_115 = -35;
                    enemy.GetComponent<Character>().AddStatus(Status.status.damage_adjust,rv_115);
                }
                break;
            case 116:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 117:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 118:
                Global.AddHp(15);
                break;
            case 119:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 120:
                Global.AddHp(15);
                Global.AddSan(-15);
                break;
            case 201:
                Global.AddHp(15);
                break;
            case 202:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                player.GetComponent<Character>().AddStatus(Status.status.strength,3);
                break;
            case 203:
                Global.AddSan(15);
                break;
            case 204:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                foreach (var enemy in enemys)
                {
                    int hp = enemy.GetComponent<Character>().GetHP();
                    enemy.GetComponent<Character>().GetHit((int)(hp * 0.15f));
                }
                break;
            case 205:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 206:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 207:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                Global.AddMaxHp(-10);
                //手牌
                break;
            case 208:
                Global.AddHp((int)(Global.player_hp*Random.Range(0.0f,0.4f)));
                Global.AddSan(-10);
                break;
            case 209:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                int rv_209 = Random.Range(0,41);
                player.GetComponent<Character>().AddStatus(Status.status.damage_adjust,rv_209);
                Global.AddSan(-10);
                break;
            case 210:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                foreach (var enemy in enemys)
                {
                    int rv_210 = Random.Range(0,41);
                    enemy.GetComponent<Character>().AddStatus(Status.status.damage_adjust,-rv_210);
                }
                Global.AddSan(-10);
                break;
            case 211:
                Global.AddSan((int)(Global.sanity*Random.Range(0.0f,0.4f)));
                Global.AddHp(-10);
                break;
            default:
                break;
        }
        use_count+=1;
        return true;
    }

   
    public void KillEnemy(GameObject enemy)
    {
        if(enemy.GetComponent<Character>().GetMaxHP()*0.15f > enemy.GetComponent<Character>().GetHP())
        {
            enemy.GetComponent<Character>().LoseHP( enemy.GetComponent<Character>().GetHP());
        }
    }

}
