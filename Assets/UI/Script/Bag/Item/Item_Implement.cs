using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Implement : MonoBehaviour
{
    public int use_count = 0;
    GameObject player;
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
       
        switch (id)
        {
            case 101:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 102:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 103:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 104:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 105:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                player.GetComponent<Character>().AddArmor(10);
                break;
            case 106:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 107:
                Global.AddHp(15);
                break;
            case 108:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 109:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
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
                //待做
                break;
            case 113:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 114:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
                break;
            case 115:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //待做
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
                //待做
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
                //傷害
                Global.AddSan(-10);
                break;
            case 210:
                if(Map_System.now_state != Map_System.map_state.fight)return false;
                //傷害
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
}
