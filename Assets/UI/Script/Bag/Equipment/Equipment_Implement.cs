using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Implement : MonoBehaviour
{
    [SerializeField] List<int> normal_equipment_list = new List<int>();
    GameObject player;

    public void Use_Equipment(int id)
    {
        switch (id)
        {
            case 1:
                //待做
                break;
            case 2:
                //待做
                break;
            case 3:
                player.GetComponent<Character>().AddArmor(5);
                break;
            case 4:
                //待做
                break;
            case 5:
                player.GetComponent<Character>().AddStatus(Status.status.strength,1);
                //待做
                break;
            case 6:
                //待做
                break;
            case 7:
                player.GetComponent<Character>().AddStatus(Status.status.strength,5);
                break;
            case 8:
                player.GetComponent<Character>().AddStatus(Status.status.rampart,5);
                break;
            case 9:
                Global.AddHp(-(int)(Global.player_max_hp*0.2f));
                player.GetComponent<Character>().AddStatus(Status.status.strength,5);
                break;
            case 10:
                player.GetComponent<Character>().AddStatus(Status.status.rampart,3);
                break;
            case 11:
                //待做
                break;
            case 12:
                //待做
                break;
            case 13:
                //待做
                break;
            case 14:
                player.GetComponent<Character>().AddStatus(Status.status.temporary_strength,1);
                break;
            case 15:
                //待做
                break;
            case 16:
                player.GetComponent<Character>().AddArmor(4);
                break;
            case 17:
                //待做
                break;
            case 18:
                player.GetComponent<Character>().AddStatus(Status.status.dexterity,1);
                break;
            case 19:
                //待做
                break;
            case 20:
                //待做
                break;
            case 21:
                List<int> equipment_list = GetComponent<Bag_System>().Return_All_Equipment();
                int normal = 0;
                foreach (var equipment in equipment_list)
                {
                    if(normal_equipment_list.Contains(equipment))normal+=1;
                }
                player.GetComponent<Character>().AddArmor(normal*4);
                break;
            case 22:
                int s = player.GetComponent<Character>().GetStatus(Status.status.strength);
                //移除力量
                player.GetComponent<Character>().AddStatus(Status.status.temporary_strength,3*s);
                break;
            case 23:
                //待做
                break;
            case 24:
                //待做
                break;
            case 25:
                Global.AddSan(-10);
                //敵人扣血
                break;
            case 26:
                player.GetComponent<Character>().AddStatus(Status.status.strength,1);
                break;
            case 27:
                player.GetComponent<Character>().AddStatus(Status.status.dexterity,1);
                break;
            case 28:
                //給敵人狀態
                break;
            case 29:
                //待做
                break;
            case 30:
                //抽牌
                break;
            default:
                break;
        }
    }
}
