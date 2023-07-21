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
            case 106: // 石巨人
                if (state == -1){
                    state = 0;
                    intention = 0;
                    ShowIntention(1, 7);
                }
                else if (state == 0 ){
                    state += 1;
                    intention = 1;
                    ShowIntention(1, 5);
                }
                else if (state == 1){
                    state = 2;
                    intention = 2;
                    ShowIntention(1, 16);
                }
                else if (state == 2){
                    state = 0;
                    intention = 0;
                    ShowIntention(1, 7);
                }
                break;
            case 107: // 巨型史萊姆
                if (state == -1){
                    state = 0;
                    intention = 2;
                    ShowIntention(3, 0);
                }
                else if (state == 0 ){
                    state += 1;
                    intention = 0;
                    ShowIntention(3, 0);
                }
                else if (state == 1){
                    state = 2;
                    intention = 3;
                    ShowIntention(1, 13);
                }
                else if (state == 2){
                    state = 3;
                    intention = 2;
                    ShowIntention(3, 0);
                }
                else if (state == 3){
                    state = 4;
                    intention = 1;
                    ShowIntention(3, 0);
                }
                else if (state == 4){
                    state = 5;
                    intention = 3;
                    ShowIntention(1, 13);
                }
                else if (state == 5){
                    state = 0;
                    intention = 2;
                    ShowIntention(3, 0);
                }
                break;
            case 108: // 盜匪首領
                if (state == -1){
                    state = 0;
                    intention = 1;
                    ShowIntention(1, 12);
                }
                else if (state == 0 ){
                    state += 1;
                    intention = 0;
                    ShowIntention(2, 15);
                }
                else if (state == 1){
                    state = 2;
                    intention = 3;
                    ShowIntention(3, 0);
                }
                else if (state == 2){
                    state = 3;
                    intention = 2;
                    ShowIntention(1, 16);
                }
                else if (state == 3){
                    state = 4;
                    intention = 4;
                    ShowIntention(3, 0);
                }
                else if (state == 4){
                    state = 0;
                    intention = 1;
                    ShowIntention(1, 12);
                }
                break;
            case 201: // 羔羊
                int hp_201 = GetComponent<Character>().GetHP();
                int maxHP_201 = GetComponent<Character>().GetMaxHP();
                if (hp_201 == maxHP_201) intention = 0;
                else
                {
                    if(Random.value>0.66)intention = 1;
                    else intention = 0;
                }
                if (intention == 0) ShowIntention(1, 9);
                else if (intention == 1) ShowIntention(3, 0);
                break;
            case 202: // 烈焰蝙蝠
                if(state==-1 || state == 1 || state == 3)
                {
                    state += 1;
                    if(state == 4)state = 0;
                    intention = 0;
                    ShowIntention(1, 10);
                }
                else if(state==0 || state == 2)
                {
                    state += 1;
                    intention = 1;
                    ShowIntention(3,0);
                }
                else if(state==2)
                {
                    state = 3;
                    intention = 2;
                    ShowIntention(1, 16);
                }
                break;
            case 203: // 冷蛛
                if(state==-1 || state == 2)
                {
                    state = 0;
                    intention = 2;
                    ShowIntention(3, 0);
                }
                else if(state==0)
                {
                    state += 1;
                    intention = 0;
                    ShowIntention(3,0);
                }
                else if(state==1)
                {
                    state = 2;
                    intention = 1;
                    ShowIntention(1, 11);
                }
                break;
            case 204: // 樹妖
                if(state==-1 || state == 2)
                {
                    state = 0;
                    intention = 0;
                    ShowIntention(2, 10);
                }
                else if(state==0)
                {
                    state += 1;
                    intention = 2;
                    ShowIntention(3,0);
                    //多段攻擊
                }
                else if(state==1)
                {
                    state = 2;
                    intention = 1;
                    ShowIntention(1, 10);
                }
                break;
            case 205: // 盜匪
                intention = Random.Range(1,4);
                if(player.GetComponent<Character>().GetStatus(Status.status.bleed)==0 && intention == 1)
                {
                    intention = 0;
                }
                if (intention == 0) ShowIntention(1, 9);
                else if (intention == 1) ShowIntention(1, 13);
                else if (intention == 2) ShowIntention(1, 12);//多段攻擊
                else if (intention == 3) ShowIntention(2, 8);
                break;
            case 206: // 烈火屠夫
                if(state == -1 || state == 2)
                {
                    intention = 0;
                    state = 0;
                    ShowIntention(3,0);
                }
                else if(state == 0)
                {
                    intention = 1;
                    state = 1;
                    ShowIntention(3,0);
                }
                else if(state == 1)
                {
                    state = 2;
                    if(GetComponent<Character>().GetStatus(Status.status.power_compete)>0)
                    {
                        intention = 2;
                        ShowIntention(1,20);
                    }
                    else
                    {
                        intention = 3;
                        ShowIntention(3,0);
                    } 
                }
                break;
            case 207: // 雙生暗影(A)
                if(state == -1 || state == 1 || state == 3)
                {
                    intention = 0;
                    state += 1;
                    if(state == 4)state = 0;
                    ShowIntention(1,12);//多次攻擊
                }
                else if(state == 0)
                {
                    intention = 1;
                    state = 1;
                    ShowIntention(3,0);
                }
                else if(state == 2)
                {
                    state = 3;
                    intention = 2;
                    ShowIntention(2,10);
                }
                break;
            case 208: // 雙生暗影(B)
                if(state == -1 || state == 3)
                {
                    intention = 0;
                    state += 1;
                    if(state == 4)state = 0;
                    ShowIntention(3,0);//多次攻擊
                }
                else if(state == 0 || state == 2)
                {
                    intention = 1;
                    state += 1;
                    ShowIntention(3,0);
                }
                else if(state == 1)
                {
                    state = 2;
                    intention = 2;
                    ShowIntention(3,0);
                }
                break;
            case 209: // 莎布·尼古拉絲
                if(state == -1 || state == 4)
                {
                    state = 0;
                    intention = 0;
                    ShowIntention(3,0);
                }
                else if(state == 0)
                {
                    state = 1;
                    intention = 1;
                    ShowIntention(2,12);
                }
                else if(state == 1)
                {
                    state = 2;
                    intention = 2;
                    ShowIntention(1,23);
                }
                else if(state == 2)
                {
                    state = 3;
                    intention = 3;
                    ShowIntention(3,0);
                }
                else if(state == 3)
                {
                    state = 4;
                    intention = 4;
                    ShowIntention(3,0);
                }
                break;
            case 301: // 幽靈馬
                if(state == -1 || state == 2)
                {
                    state = 0;
                    intention = 0;
                    ShowIntention(1,9);//多次攻擊
                }
                else if(state == 0)
                {
                    state = 1;
                    intention = 1;
                    ShowIntention(2,15);
                }
                else if(state == 1)
                {
                    state = 2;
                    intention = 2;
                    ShowIntention(1,13);
                }
                break;
            case 302: // 觀察者
                //待做
                break;
            case 303: // 護衛者
                int accumulation_level = GetComponent<Character>().GetStatus(Status.status.accumulation);
                if(accumulation_level < 20)intention = 0;
                else intention = 1;
                if(intention == 0)ShowIntention(2,11);
                else ShowIntention(1,accumulation_level);
                break;
            case 304: // 追蹤者
                if(state == -1 || state == 2)
                {
                    state = 0;
                    intention = 0;
                    ShowIntention(3,0);
                }
                else if(state == 0 || state == 1)
                {
                    state += 1;
                    intention = 1;
                    //亂數傷害未實作
                    ShowIntention(1,8);
                }
                break;
            case 305: // 騎士骷髏
                //未實作
                break;
            case 306: // 屠刀骷髏
                if(state == -1 || state == 0 || state == 2)
                {
                    state += 1;
                    intention = 1;
                    ShowIntention(1,18);
                }
                else if(state == 1)
                {
                    state += 1;
                    intention = 0;
                    ShowIntention(1,11);
                }
                break;
            case 307: // 長矛骷髏
                if(state == -1 || state == 3)
                {
                    state = 0;
                    intention = 0;
                    ShowIntention(3,0);
                }
                else if(state == 0 || state == 1)
                {
                    state += 1;
                    intention = 2;
                    ShowIntention(1,12);//多次攻擊
                }
                else if(state == 2)
                {
                    state += 1;
                    intention = 1;
                    ShowIntention(1,9);
                }
                break;
            case 308: // 裂嘴
                if(state == -1 || state == 2)
                {
                    state = 0;
                    intention = 0;
                    ShowIntention(3,0);
                }
                else if(state == 0 )
                {
                    state += 1;
                    intention = 1;
                    ShowIntention(3,0);
                }
                else if(state == 1)
                {
                    state += 1;
                    intention = 2;
                    ShowIntention(1,25);
                }
                break;
            case 309: // 扭曲團塊(A)
                if(state == -1 || state == 1)
                {
                    state = 0;
                    intention = 1;
                    ShowIntention(3,0);
                }
                else if(state == 0 )
                {
                    state += 1;
                    intention = 0;
                    ShowIntention(1,8);
                    
                }
                break;
            case 310: // 扭曲團塊(B)
                if(state == -1 || state == 1)
                {
                    state = 0;
                    intention = 0;
                    ShowIntention(1,16);
                }
                else if(state == 0 )
                {
                    state += 1;
                    intention = 1;
                    ShowIntention(3,0);
                }
                break;
            case 311: // 恐懼聚合體
                if(state == -1 || state == 1|| state == 4)
                {
                    state += 1;
                    if(state == 5)state = 0;
                    intention = 0;
                    ShowIntention(3,0);
                }
                else if(state == 0 )
                {
                    state = 1;
                    intention = 1;
                    ShowIntention(3,0);
                }
                else if(state == 2 )
                {
                    state = 3;
                    intention = 2;
                    ShowIntention(1,18);
                }
                else if(state == 3)
                {
                    state = 4;
                    intention = 3;
                    //傷害顯示待做
                    ShowIntention(3,0);
                }
                break;
            case 312: // 深淵凝視者
                //待做
                break;
            case 313: // 阿薩托斯
                //待做
                break;
            case 401: // 火精靈
                if(state == -1 || state == 1)
                {
                    state = 0;
                    intention = 0;
                    ShowIntention(1,5);
                }
                else if(state == 0 )
                {
                    state += 1;
                    intention = 1;
                    ShowIntention(2,8);
                }
                break;
            case 402: // 惡意
                if(state == -1 || state == 2)
                {
                    state = 0;
                    intention = 0;
                    ShowIntention(1,10);
                }
                else if(state == 0 )
                {
                    state += 1;
                    intention = 1;
                    ShowIntention(3,0);
                }
                else if(state == 1 )
                {
                    state += 1;
                    intention = 2;
                    ShowIntention(3,0);
                }
                break;
            case 403: // 編劇家
                if(state == -1 || state == 1 || state == 4)
                {
                    state += 1;
                    if(state == 5)state = 0;
                    intention = 2;
                    ShowIntention(1,15);
                }
                else if(state == 0 )
                {
                    state += 1;
                    intention = 0;
                    ShowIntention(3,0);
                }
                else if(state == 2 )
                {
                    state += 1;
                    intention = 1;
                    ShowIntention(3,0);
                }
                else if(state == 3 )
                {
                    state += 1;
                    intention = 3;
                    ShowIntention(2,18);
                }
                else if(state == 4 )
                {
                    state += 1;
                    intention = 4;
                    ShowIntention(3,0);
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
            case 106: // 石巨人
                if(intention == 0){
                    GetComponent<Animator>().Play("106_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 7);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("106_spit");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 5);
                    player.GetComponent<Character>().AddStatus(Status.status.vulnerable, 1);
                    player.GetComponent<Character>().AddStatus(Status.status.frail, 1);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("106_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 16);
                }
                break;
            case 107: // 巨型史萊姆
                if(intention == 0){
                    GetComponent<Animator>().Play("107_attack");
                    yield return new WaitForSeconds(0.5f);
                    //卡片
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("107_attack");
                    yield return new WaitForSeconds(0.5f);
                    //卡片
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("107_attack");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 2);
                    player.GetComponent<Character>().AddStatus(Status.status.frail, 2);
                }
                else if (intention == 3){
                    GetComponent<Animator>().Play("107_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 13);
                }
                break;
            case 108: // 盜匪首領
                if(intention == 0){
                    GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    //臨時護甲
                    //偷卡片
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("108_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 12);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 2);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("108_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 16);
                    player.GetComponent<Character>().AddStatus(Status.status.vulnerable, 2);
                }
                else if (intention == 3){
                    GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    //空白
                }
                else if (intention == 4){
                    GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddStatus(Status.status.strength, 3);

                }
                break;
            case 201: // 羔羊
                if(intention == 0){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 9);
                }
                else if (intention == 1){
                    // GetComponent<Animator>().Play("108_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Heal(10);
                }
                break;
            case 202: // 烈焰蝙蝠
                if(intention == 0){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 10);
                    //卡片未做
                }
                else if (intention == 1){
                    // GetComponent<Animator>().Play("108_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddStatus(Status.status.invincible, 1);
                }
                else if(intention == 2){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 16);
                    //卡片未做
                }
                break;
            case 203: // 冷蛛
                if(intention == 0){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    //卡片未做
                }
                else if (intention == 1){
                    // GetComponent<Animator>().Play("108_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 11);
                    //給予中毒未做
                }
                else if(intention == 2){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 3);
                }
                break;
            case 204: // 樹妖
                if(intention == 0){
                    GetComponent<Animator>().Play("204_demage");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 2);
                    GetComponent<Character>().AddArmor(10);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("204_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 10);
                    GetComponent<Character>().AddStatus(Status.status.strength, 1);
                }
                else if(intention == 2){
                    GetComponent<Animator>().Play("204_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 4);
                    GetComponent<Character>().Attack(player, 4);
                    GetComponent<Character>().Attack(player, 4);
                }
                break;
            case 205: // 盜匪
                if(intention == 0){
                    GetComponent<Animator>().Play("205_attack1");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 9);
                    player.GetComponent<Character>().AddStatus(Status.status.bleed, 3);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("205_attack1");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 13);
                    GetComponent<Character>().AddStatus(Status.status.vulnerable, 1);
                }
                else if(intention == 2){
                    GetComponent<Animator>().Play("205_attack1");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 6);
                    GetComponent<Character>().Attack(player, 6);
                }
                else if(intention == 3){
                    GetComponent<Animator>().Play("205_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(8);
                }
                break;
            case 206: // 烈火屠夫
                if(intention == 0){
                    GetComponent<Animator>().Play("206_attack");
                    yield return new WaitForSeconds(0.5f);
                    //召喚
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("206_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddStatus(Status.status.strength, 5);
                    int power_level = GetComponent<Character>().GetStatus(Status.status.strength);
                    GetComponent<Character>().AddStatus(Status.status.power_compete, 2*(20+power_level));
                }
                else if(intention == 2){
                    GetComponent<Animator>().Play("206_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 20);
                }
                else if(intention == 3){
                    GetComponent<Animator>().Play("206_demage");
                    yield return new WaitForSeconds(0.5f);
                    //所有人恢復血量
                }
                break;
            case 207: // 雙生暗影(A)
                if(intention == 0){
                    GetComponent<Animator>().Play("207_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 4);
                    GetComponent<Character>().Attack(player, 4);
                    GetComponent<Character>().Attack(player, 4);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("207_skill");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.frail, 2);
                }
                else if(intention == 2){
                    GetComponent<Animator>().Play("207_summon");
                    yield return new WaitForSeconds(0.5f);
                    //所有人獲得護甲
                }
                break;
            case 208: // 雙生暗影(A)
                if(intention == 0){
                    GetComponent<Animator>().Play("208_attack");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 2);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("208_attack2");
                    yield return new WaitForSeconds(0.5f);
                    Global.AddSan(-4);
                }
                else if(intention == 2){
                    GetComponent<Animator>().Play("208_attack2");
                    yield return new WaitForSeconds(0.5f);
                    //抽卡片
                }
                break;
            case 209: // 莎布·尼古拉絲
                if(intention == 0){
                    GetComponent<Animator>().Play("209_hurt");
                    yield return new WaitForSeconds(0.5f);
                    //自己扣40
                    //召喚
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("209_walk");
                    yield return new WaitForSeconds(0.5f);
                    //所有人獲得力量3和12護甲
                }
                else if(intention == 2){
                    GetComponent<Animator>().Play("209_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 23);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 2);
                }
                else if(intention == 3){
                    GetComponent<Animator>().Play("209_walk");
                    yield return new WaitForSeconds(0.5f);
                    Global.AddSan(-5);
                }
                else if(intention == 3){
                    GetComponent<Animator>().Play("209_walk");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.mental_weak, 1);
                }
                break;
            case 301: // 幽靈馬
                if(intention == 0){
                    GetComponent<Animator>().Play("301_run");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,3);
                    GetComponent<Character>().Attack(player,3);
                    GetComponent<Character>().Attack(player,3);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("301_run");
                    yield return new WaitForSeconds(0.5f);
                    //獲得15臨時護甲
                }
                else if(intention == 2){
                    GetComponent<Animator>().Play("301_run");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 13);
                }
                break;
            case 302: // 觀察者
                if(intention == 0){
                    GetComponent<Animator>().Play("302_attack");
                    yield return new WaitForSeconds(0.5f);
                    //所有人獲得10臨時護甲
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("302_attack2");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.vulnerable, 1);

                }
                else if(intention == 2){
                    GetComponent<Animator>().Play("302_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 23);
                }
                break;
            case 303: // 護衛者
                if(intention == 0){
                    GetComponent<Animator>().Play("303_demage");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(11);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("303_attack");
                    yield return new WaitForSeconds(0.5f);
                    int accumulation_level = GetComponent<Character>().GetStatus(Status.status.accumulation);
                    GetComponent<Character>().Attack(player,accumulation_level);
                    GetComponent<Character>().AddStatus(Status.status.accumulation, -accumulation_level);
                }
                break;
            case 304: // 追蹤者
                if(intention == 0){
                    GetComponent<Animator>().Play("304_attack");
                    yield return new WaitForSeconds(0.5f);
                    //加卡片
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("304_attack");
                    yield return new WaitForSeconds(0.5f);
                    //亂數傷害未實作
                }
                break;
            case 305: // 騎士骷髏
                if(intention == 0){
                    GetComponent<Animator>().Play("305_shield");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(13);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("305_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,8);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("305_shield");
                    yield return new WaitForSeconds(0.5f);
                    int arrmor_num = GetComponent<Character>().GetArmor();
                    GetComponent<Character>().Attack(player,arrmor_num);
                }
                else if (intention == 3){
                    GetComponent<Animator>().Play("305_attack");
                    yield return new WaitForSeconds(0.5f);
                    //所有友軍獲得力量2
                }
                break;
            case 306: // 屠刀骷髏
                if(intention == 0){
                    GetComponent<Animator>().Play("306_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,18);
                    player.GetComponent<Character>().AddStatus(Status.status.vulnerable,1);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("306_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,11);
                }
                break;
            case 307: // 長矛骷髏
                if(intention == 0){
                    GetComponent<Animator>().Play("307_attack");
                    yield return new WaitForSeconds(0.5f);
                    //最左方獲得12護甲
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("307_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,4);
                    GetComponent<Character>().Attack(player,4);
                    GetComponent<Character>().Attack(player,4);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("307_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,9);
                    player.GetComponent<Character>().AddStatus(Status.status.weak,1);
                }
                break;
            case 308: // 裂嘴
                if(intention == 0){
                    GetComponent<Animator>().Play("308_attack");
                    yield return new WaitForSeconds(0.5f);
                    //待做
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("308_attack");
                    yield return new WaitForSeconds(0.5f);
                    //待做
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("308_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,28);
                    player.GetComponent<Character>().AddStatus(Status.status.weak,1);
                    player.GetComponent<Character>().AddStatus(Status.status.frail,1);
                }
                else if (intention == 3){
                    //嘲笑?
                }
                break;
            case 309: // 扭曲團塊(A)
                if(intention == 0){
                    GetComponent<Animator>().Play("309_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,8);
                    Global.AddSan(-3);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("309_attack");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.weak,1);
                }
                break;
            case 310: // 扭曲團塊(B)
                if(intention == 0){
                    GetComponent<Animator>().Play("309_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,8);
                    Global.AddSan(-3);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("309_attack");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.weak,1);
                }
                break;
            case 311: // 恐懼聚合體
                if(intention == 0){
                    GetComponent<Animator>().Play("311_demage");
                    yield return new WaitForSeconds(0.5f);
                    //召喚待做
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("311_attack");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.weak,2);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("311_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,18);
                    player.GetComponent<Character>().AddStatus(Status.status.vulnerable,2);
                }
                else if (intention == 3){
                    GetComponent<Animator>().Play("311_attack");
                    yield return new WaitForSeconds(0.5f);
                    //引爆待做
                }
                break;
            case 312: // 深淵凝視者
                if(intention == 0){
                    GetComponent<Animator>().Play("312_demage");
                    yield return new WaitForSeconds(0.5f);
                    //手牌待做
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("312_attack");
                    yield return new WaitForSeconds(0.5f);
                    //手牌待做
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("312_attack");
                    yield return new WaitForSeconds(0.5f);
                    //手牌待做
                }
                else if (intention == 3){
                    GetComponent<Animator>().Play("312_jump");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.mental_weak,1);
                    //傷害+4待做
                }
                break;
            case 313: // 阿薩托斯
                if(intention == 0){
                    GetComponent<Animator>().Play("313_fly");
                    yield return new WaitForSeconds(0.5f);
                    Global.AddSan(-30);
                    //卡片待做
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("313_fly");
                    yield return new WaitForSeconds(0.5f);
                    //卡片待做
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("313_fly");
                    yield return new WaitForSeconds(0.5f);
                    //卡片待做
                }
                else if (intention == 3){
                    GetComponent<Animator>().Play("313_fly");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,21);
                    //確認傷害待做
                }
                else if (intention == 4){
                    GetComponent<Animator>().Play("313_fly");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(18);
                    GetComponent<Character>().AddStatus(Status.status.invincible,1);
                }
                else if (intention == 5){
                    GetComponent<Animator>().Play("313_fly");
                    yield return new WaitForSeconds(0.5f);
                    if(Global.sanity > 30)Global.AddSan(-8);
                    else player.GetComponent<Character>().AddStatus(Status.status.vulnerable,2);
                }
                else if (intention == 6){
                    GetComponent<Animator>().Play("313_fly");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,14);
                    player.GetComponent<Character>().AddStatus(Status.status.compress,2);
                }
                else if (intention == 7){
                    GetComponent<Animator>().Play("313_fly");
                    yield return new WaitForSeconds(0.5f);
                    //審判待做
                }
                break;
            case 401: // 火精靈
                if(intention == 0){
                    GetComponent<Animator>().Play("401_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,5);
                    //確認傷害待做
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("401_run");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(8);
                }
                break;
            case 402: // 惡意
                if(intention == 0){
                    // GetComponent<Animator>().Play("401_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,10);
                }
                else if (intention == 1){
                    // GetComponent<Animator>().Play("401_run");
                    yield return new WaitForSeconds(0.5f);
                    Global.AddSan(-3);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("402_attack");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.burn,10);
                    //死亡待做
                }
                break;
            case 403: // 編劇家
                if(intention == 0){
                    // GetComponent<Animator>().Play("401_attack");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.weak,2);
                }
                else if (intention == 1){
                    // GetComponent<Animator>().Play("401_run");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.mental_weak,1);
                }
                else if (intention == 2){
                    // GetComponent<Animator>().Play("402_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,15);
                }
                else if (intention == 3){
                    // GetComponent<Animator>().Play("402_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(18);
                }
                else if (intention == 4){
                    // GetComponent<Animator>().Play("402_attack");
                    yield return new WaitForSeconds(0.5f);
                    //改寫待做
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

    public int GetIntentionType(){
        if (intentionObj.GetComponent<Image>().sprite == attackImg) return 1;
        if (intentionObj.GetComponent<Image>().sprite == defendImg) return 2;
        if (intentionObj.GetComponent<Image>().sprite == othersImg) return 3;
        return -1;
    }
}
