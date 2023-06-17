using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public void Move(){
        int id = GetComponent<Character>().GetEnemyID();
        GameObject player = transform.parent.GetComponent<BattleController>().GetPlayer();
        int result, state;

        switch(id){
            case 101: // 史萊姆
                result = Random.Range(1, 3);
                if (result == 1){
                    GetComponent<Character>().Attack(player, 6);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 1);
                }
                else if (result == 2){
                    GetComponent<Character>().Attack(player, 10);
                }
                else{
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 1);
                    player.GetComponent<Character>().AddStatus(Status.status.frail, 1);
                }
                break;
            case 102: // 哥布林
                result = Random.Range(1, 3);
                if (result == 1){
                    GetComponent<Character>().Attack(player, 10);
                }
                else if (result == 2){
                    GetComponent<Character>().Attack(player, 6);
                    GetComponent<Character>().AddStatus(Status.status.strength, 2);
                }
                else{
                    GetComponent<Character>().AddArmor(8);
                }
                break;
            case 103: // 幻影
                state = GetComponent<Character>().GetState();
                if (state == 0){
                    GetComponent<Character>().AddStatus(Status.status.invincible, 1);
                    GetComponent<Character>().SetState(1);
                }
                else if (state == 1){
                    GetComponent<Character>().Attack(player, 8);
                    GetComponent<Character>().SetState(2);
                }
                else{
                    Global.AddSan(-2);
                    GetComponent<Character>().SetState(1);
                }
                break;
            case 104: //岩蟹
                int hp = GetComponent<Character>().GetHP();
                int maxHP = GetComponent<Character>().GetMaxHP();
                if (hp == maxHP){
                    if (Random.Range(0, 2) == 0){
                        GetComponent<Character>().Attack(player, 6);
                    }
                    else{
                        GetComponent<Character>().Attack(player, 3);
                        player.GetComponent<Character>().AddStatus(Status.status.bleed, 3);
                    }
                }
                else{
                    GetComponent<Character>().Heal(2);
                    GetComponent<Character>().AddArmor(5);
                }
                break;
            case 105: // 石像
                state = GetComponent<Character>().GetState();
                if (state == 0){
                    GetComponent<Character>().AddStatus(Status.status.taunt, 99);
                    GetComponent<Character>().AddStatus(Status.status.spike, 3);
                    GetComponent<Character>().SetState(1);
                }
                else if (state == 1 || state == 2){
                    GetComponent<Character>().AddArmor(5);
                    GetComponent<Character>().SetState(state + 1);
                }
                else{
                    GetComponent<Character>().AddStatus(Status.status.spike, 3);
                    GetComponent<Character>().SetState(1);
                }
                break;
            default:
                Debug.Log("Enemy move: Unknown enemy id " + id.ToString());
                break;
        }
    }
}
