using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] GameObject intentionTemplate;
    [SerializeField] Sprite attackImg;
    [SerializeField] Sprite defendImg;
    [SerializeField] Sprite othersImg;
    GameObject intentionObj = null;
    GameObject player = null;
    int intention = -1;
    int state = -1;
    GameObject GetPlayer(){
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        return player;
    }
    public void SetIntention(){
        int id = GetComponent<Character>().GetEnemyID();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        switch(id){
            case 101: // 史萊姆
                intention = Random.Range(0, 3);
                if (intention == 0) ShowIntention(1, 6);
                else if (intention == 1) ShowIntention(1, 10);
                else if (intention == 2) ShowIntention(3, 0);
                break;
            case 102: // 哥布林
                intention = Random.Range(0, 3);
                if (intention == 0) ShowIntention(1, 10);
                else if (intention == 1) ShowIntention(1, 6);
                else if (intention == 2) ShowIntention(2, 8);
                break;
            case 103: // 幻影
                if (state == -1){
                    state = 0;
                    intention = 0;
                    ShowIntention(3, 0);
                }
                else if (state == 0){
                    state = 1;
                    intention = 1;
                    ShowIntention(1, 8);
                }
                else if (state == 1){
                    state = 0;
                    intention = 2;
                    ShowIntention(3, 0);
                }
                break;
            case 104: //岩蟹
                int hp = GetComponent<Character>().GetHP();
                int maxHP = GetComponent<Character>().GetMaxHP();
                if (hp == maxHP) intention = Random.Range(0, 2);
                else intention = 2;
                if (intention == 0) ShowIntention(1, 6);
                else if (intention == 1) ShowIntention(1, 3);
                else if (intention == 2) ShowIntention(2, 5);
                break;
            case 105: // 石像
                if (state == -1){
                    state = 0;
                    intention = 0;
                    ShowIntention(3, 0);
                }
                else if (state == 0 || state == 1){
                    state += 1;
                    intention = 1;
                    ShowIntention(2, 5);
                }
                else if (state == 2){
                    state = 0;
                    intention = 2;
                    ShowIntention(3, 0);
                }
                break;
            default:
                Debug.Log("SetIntention: Unknown enemy id " + id.ToString());
                break;
        }
    }
    public void Move(){
        StartCoroutine(_Move());
    }
    IEnumerator _Move(){
        int id = GetComponent<Character>().GetEnemyID();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        switch(id){
            case 101: // 史萊姆
                if (intention == 0){
                    GetComponent<Animator>().Play("101_strike");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 6);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 1);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("101_strike");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 10);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("101_spit");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 1);
                    player.GetComponent<Character>().AddStatus(Status.status.frail, 1);
                }
                break;
            case 102: // 哥布林
                if (intention == 0){
                    GetComponent<Animator>().Play("102_strike");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 10);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("102_strike");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 6);
                    GetComponent<Character>().AddStatus(Status.status.strength, 2);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("102_evade");
                    GetComponent<Character>().AddArmor(8);
                }
                break;
            case 103: // 幻影
                if (intention == 0){
                    GetComponent<Character>().AddStatus(Status.status.invincible, 1);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("103_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 8);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("103_attack");
                    yield return new WaitForSeconds(0.5f);
                    Global.AddSan(-2);
                }
                break;
            case 104: //岩蟹
                if (intention == 0){
                    GetComponent<Animator>().Play("104_attack1");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 6);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("104_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 3);
                    player.GetComponent<Character>().AddStatus(Status.status.bleed, 3);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("104_skill");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Heal(2);
                    GetComponent<Character>().AddArmor(5);
                }
                break;
            case 105: // 石像
                if (intention == 0){
                    GetComponent<Animator>().Play("105_reinforce");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddStatus(Status.status.taunt, 99);
                    GetComponent<Character>().AddStatus(Status.status.spike, 3);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("105_repair");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(5);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("105_reinforce");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddStatus(Status.status.spike, 3);
                }
                break;
            default:
                Debug.Log("Enemy move: Unknown enemy id " + id.ToString());
                break;
        }
    }

    void ShowIntention(int type, int value){
        if (intentionObj == null){
            intentionObj = Instantiate(intentionTemplate, transform);
            intentionObj.transform.localPosition = new Vector3(0, 200, 0);
        }
        switch(type){
            case 1:
                intentionObj.GetComponent<Image>().sprite = attackImg;
                if (value > 0)
                    intentionObj.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = (BattleController.ComputeDamage(gameObject, GetPlayer(), value)).ToString();
                break;
            case 2:
                intentionObj.GetComponent<Image>().sprite = defendImg;
                if (value > 0)
                    intentionObj.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = value.ToString();
                break;
            case 3:
                intentionObj.GetComponent<Image>().sprite = othersImg;
                break;
            default:
                Debug.Log("ShowIntention: Unknown type " + type.ToString());
                break;
        }
        if (value == 0) intentionObj.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "";
    }
}
