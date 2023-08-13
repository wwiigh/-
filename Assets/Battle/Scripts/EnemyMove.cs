using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        Character player_character = player.GetComponent<Character>();

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
                if (hp == maxHP) intention = Random.Range(1, 3);
                else intention = 0;
                if (intention == 0) ShowIntention(3, 0);
                else if (intention == 1) ShowIntention(1, 6);
                else if (intention == 2) ShowIntention(1, 3);
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
                if (GetComponent<Character>().GetHP() <= GetComponent<Character>().GetMaxHP() / 2 &&
                    GetComponent<Character>().GetStatus(Status.status.rock_solid) < 2){
                    intention = 3;
                    ShowIntention(3, 0);
                }
                else if (state == -1){
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
                else if (state == 0){
                    state = 1;
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
                    ShowIntention(1, 9);
                }
                else if (state == 0 ){
                    state += 1;
                    intention = 0;
                    ShowIntention(3, 0);
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
                    ShowIntention(1, 9);
                }
                break;
            case 201: // 羔羊
                int hp_201 = GetComponent<Character>().GetHP();
                int maxHP_201 = GetComponent<Character>().GetMaxHP();
                if (hp_201 == maxHP_201) intention = 0;
                else
                {
                    if(Random.value < 0.34) intention = 1;
                    else intention = 0;
                }
                if (intention == 0) ShowIntention(1, 9);
                else if (intention == 1) ShowIntention(3, 0);
                break;
            case 202: // 烈焰蝙蝠
                if(state == -1 || state == 1 || state == 4)
                {
                    if (state == 4) state = 0;
                    else state += 1;
                    intention = 0;
                    ShowIntention(1, 10);
                }
                else if(state == 0 || state == 2)
                {
                    state += 1;
                    intention = 1;
                    ShowIntention(3,0);
                }
                else if(state == 3)
                {
                    state = 4;
                    intention = 2;
                    ShowIntention(1, 16);
                }
                break;
            case 203: // 冷蛛
                if(state == -1 || state == 2)
                {
                    state = 0;
                    intention = 2;
                    ShowIntention(3, 0);
                }
                else if(state == 0)
                {
                    state = 1;
                    intention = 0;
                    ShowIntention(3,0);
                }
                else if(state == 1)
                {
                    state = 2;
                    intention = 1;
                    ShowIntention(1, 11);
                }
                break;
            case 204: // 樹妖
                if(state == -1 || state == 2)
                {
                    state = 0;
                    intention = 0;
                    ShowIntention(2, 10);
                }
                else if(state == 0)
                {
                    state = 1;
                    intention = 2;
                    ShowIntention(1, 4, 3);
                }
                else if(state == 1)
                {
                    state = 2;
                    intention = 1;
                    ShowIntention(1, 10);
                }
                break;
            case 205: // 盜匪
                intention = Random.Range(1,4);
                if(player_character.GetStatus(Status.status.bleed) == 0 && intention == 1)
                {
                    intention = 0;
                }
                if (intention == 0) ShowIntention(1, 9);
                else if (intention == 1) ShowIntention(1, 13);
                else if (intention == 2) ShowIntention(3, 0);
                else if (intention == 3) ShowIntention(2, 8);
                break;
            case 206: // 烈火屠夫
                if(state == -1 || state == 3)
                {
                    intention = 0;
                    state = 0;
                    ShowIntention(3,0);
                }
                else if(state == 0)
                {
                    intention = 1;
                    state = 1;
                    ShowIntention(2,20);
                }
                else if(state == 1)
                {
                    intention = 2;
                    state = 2;
                    ShowIntention(3,0);
                }
                else if(state == 2)
                {
                    state = 3;
                    if(GetComponent<Character>().GetStatus(Status.status.power_compete) > 0)
                    {
                        intention = 3;
                        ShowIntention(1,20);
                    }
                    else
                    {
                        intention = 4;
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
                    ShowIntention(1, 4, 3);//多次攻擊
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
                    ShowIntention(3,0);
                }
                else if(state == 0 || state == 2)
                {
                    intention = 1;
                    state += 1;
                    ShowIntention(1,5);
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
                    state = 1;
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
    int GetDamage(int value){
        return BattleController.ComputeDamage(gameObject, GetPlayer(), value);
    }
    public (string name, string description) GetIntention(){
        int id = GetComponent<Character>().GetEnemyID();
        switch(id){
            case 101:
                if (intention == 0) return ("黏液撞擊", "造成<color=red>" + GetDamage(6).ToString() + "</color>點傷害，給予<color=red>1</color>層虛弱");
                if (intention == 1) return ("撞擊", "造成<color=red>" + GetDamage(10).ToString() + "</color>點傷害");
                if (intention == 2) return ("腐蝕", "給予<color=red>1</color>層虛弱、<color=red>1</color>層脆弱");
                break;
            case 102:
                if (intention == 0) return ("戳刺", "造成<color=red>" + GetDamage(10).ToString() + "</color>點傷害");
                if (intention == 1) return ("發怒", "造成<color=red>" + GetDamage(6).ToString() + "</color>點傷害，獲得<color=green>2</color>點力量");
                if (intention == 2) return ("躲閃", "獲得<color=green>8</color>點護甲");
                break;
            case 103:
                if (intention == 0) return ("模糊輪廓", "免疫下次受到的傷害");
                if (intention == 1) return ("攻擊", "造成<color=red>" + GetDamage(8).ToString() + "</color>點傷害");
                if (intention == 2) return ("驚嚇", "理智<color=red>-2</color>");
                break;
            case 104:
                if (intention == 0) return ("躲藏", "恢復<color=green>2</color>點生命");
                if (intention == 1) return ("揮擊", "造成<color=red>" + GetDamage(6).ToString() + "</color>點傷害");
                if (intention == 2) return ("利螯", "造成<color=red>" + GetDamage(3).ToString() + "</color>點傷害，給予<color=red>3</color>層流血");
                break;
            case 105:
                if (intention == 0) return ("路障", "獲得嘲諷與<color=green>3</color>點荊棘");
                if (intention == 1) return ("修補", "獲得<color=green>5</color>點護甲");
                if (intention == 2) return ("強化", "獲得<color=green>3</color>點荊棘");
                break;
            case 106:
                if (intention == 0) return ("揮拳", "造成<color=red>" + GetDamage(7).ToString() + "</color>點傷害");
                if (intention == 1) return ("戰吼", "造成<color=red>" + GetDamage(5).ToString() + "</color>點傷害，給予<color=red>1</color>層易傷與<color=red>1</color>層脆弱");
                if (intention == 2) return ("巨臂橫掃", "造成<color=red>" + GetDamage(16).ToString() + "</color>點傷害");
                if (intention == 3) return ("超硬化", "獲得<color=green>1</color>層堅硬");
                break;
            case 107:
                if (intention == 0) return ("黏液噴射", "將1張「腐蝕黏液」加入抽牌堆");
                if (intention == 1) return ("黏液噴射", "將2張「虛弱黏液」加入抽牌堆");
                if (intention == 2) return ("腐蝕", "給予2層虛弱與2層脆弱");
                if (intention == 3) return ("飛拳", "造成<color=red>" + GetDamage(13).ToString() + "</color>點傷害");
                break;
            case 108:
                if (intention == 0) return ("偷竊", "使玩家下回合少抽1張牌。\n若行動時仍有護甲，改為2張");
                if (intention == 1) return ("掩護劈砍", "造成<color=red>" + GetDamage(9).ToString() + "</color>點傷害，給予<color=red>1</color>層虛弱，獲得<color=green>15</color>點護甲");
                if (intention == 2) return ("強力劈砍", "造成<color=red>" + GetDamage(16).ToString() + "</color>點傷害，給予<color=red>2</color>層易傷");
                if (intention == 3) return ("情報抹消", "使玩家下回合結束時，手中一張隨機牌變為「空白」");
                if (intention == 4) return ("興奮", "獲得<color=green>4</color>點力量");
                break;
                
            case 201:
                if (intention == 0) return ("撞擊", "造成<color=red>" + GetDamage(9).ToString() + "</color>點傷害");
                if (intention == 1) return ("舔舐", "恢復<color=green>10</color>點生命");
                break;
            case 202:
                if (intention == 0) return ("火球", "造成<color=red>" + GetDamage(10).ToString() + "</color>點傷害，將1張「火苗」加入玩家手中");
                if (intention == 1) return ("飛翔", "免疫下次受到的傷害");
                if (intention == 2) return ("噴火", "造成<color=red>" + GetDamage(16).ToString() + "</color>點傷害，將玩家手中所有「火苗」變為「烈焰」");
                break;
            case 203:
                if (intention == 0) return ("結網", "將1張「蜘蛛網」加入玩家手中");
                if (intention == 1) return ("毒液注入", "造成<color=red>" + GetDamage(11).ToString() + "</color>點傷害，隨後，若玩家沒有護甲，給予<color=red>1</color>層易傷");
                if (intention == 2) return ("神經毒素", "給予<color=red>3</color>層虛弱");
                break;
            case 204:
                if (intention == 0) return ("樹根纏繞", "獲得<color=green>10</color>點護甲，給予<color=red>2</color>層虛弱");
                if (intention == 1) return ("爪擊", "造成<color=red>" + GetDamage(10).ToString() + "</color>點傷害，獲得<color=green>1</color>點力量");
                if (intention == 2) return ("藤蔓長鞭", "造成<color=red>" + GetDamage(4).ToString() + "</color>點傷害<color=red>3</color>次");
                break;
            case 205:
                if (intention == 0) return ("刺擊", "造成<color=red>" + GetDamage(9).ToString() + "</color>點傷害，給予<color=red>3</color>層流血");
                if (intention == 1) return ("攻擊弱點", "造成<color=red>" + GetDamage(13).ToString() + "</color>點傷害，給予<color=green>1</color>層易傷");
                if (intention == 2) return ("更換箭矢", "獲得<color=green>2</color>點力量，下次造成的傷害<color=green>+5</color>");
                if (intention == 3) return ("躲閃", "獲得<color=green>8</color>點護甲");
                break;
            case 206:
                if (intention == 0) return ("召喚隨從", "給予所有已在場的「火精靈」<color=green>10</color>點護甲與<color=green>5</color>點力量\n召喚<color=green>2</color>個「火精靈」");
                if (intention == 1) return ("烈焰焚身", "獲得<color=green>20</color>點護甲、<color=green>" + (20+2*GetComponent<Character>().GetStatus(Status.status.strength)).ToString() + "</color>層較勁");
                if (intention == 2) return ("伸展肌肉", "獲得<color=green>5</color>點力量");
                if (intention == 3) return ("碎顱擊", "造成<color=red>" + GetDamage(20).ToString() + "</color>點傷害\n若擁有較勁狀態，清空層數並獲得<color=green>10</color>點力量");
                if (intention == 4) return ("重整旗鼓", "所有友軍恢復<color=green>12</color>點生命");
                break;
            case 207:
                if (intention == 0) return ("切割", "造成<color=red>" + GetDamage(4).ToString() + "</color>點傷害<color=red>3</color>次");
                if (intention == 1) return ("詭譎氣息", "給予<color=red>2</color>層脆弱");
                if (intention == 2) return ("暗影庇護", "所有友軍獲得<color=green>10</color>點護甲");
                break;
            case 208:
                if (intention == 0) return ("力竭詛咒", "給予<color=red>2</color>層虛弱");
                if (intention == 1) return ("噩夢斬擊", "造成<color=red>" + GetDamage(5).ToString() + "</color>點傷害，理智<color=red>-5</color>");
                if (intention == 2) return ("精神入侵", "將<color=red>2</color>張「失憶」加入抽牌堆");
                break;
            case 209:
                if (intention == 0) return ("孕育", "增加所有「羔羊」13點生命值上限\n召喚兩隻「羔羊」，每召喚一隻就失去<color=red>20</color>點生命\n若生命不足則不會召喚");
                if (intention == 1) return ("祝福", "所有友軍獲得<color=green>12</color>點護甲與<color=green>3</color>點力量");
                if (intention == 2) return ("觸手鞭笞", "造成<color=red>" + GetDamage(23).ToString() + "</color>點傷害，給予<color=red>2</color>層虛弱");
                if (intention == 3) return ("褻語", "理智<color=red>-6</color>");
                if (intention == 4) return ("次聲波", "給予「每回合理智<color=red>-1</color>」效果");
                break;
            case 401:
                if (intention == 0) return ("火焰彈", "造成<color=red>" + GetDamage(5).ToString() + "</color>點傷害，隨後，若玩家沒有護甲，給予<color=red>3</color>層燃燒");
                if (intention == 1) return ("防護", "獲得<color=green>8</color>點護甲");
                break;
            default:
                Debug.Log("GetIntention: Unknown enemy id " + id.ToString());
                break;
        }
        return (null, null);
    }
    public void Move(){
        StartCoroutine(_Move());
    }
    IEnumerator _Move(){
        int id = GetComponent<Character>().GetEnemyID();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Character player_character = player.GetComponent<Character>();
        Deck deck = Object.FindAnyObjectByType<Deck>();
        BattleEffects effects = GameObject.FindGameObjectWithTag("BattleEffects").GetComponent<BattleEffects>();

        switch(id){
            case 101: // 史萊姆
                if (intention == 0){
                    GetComponent<Animator>().Play("101_strike");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 6);
                    player_character.AddStatus(Status.status.weak, 1);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("101_strike");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 10);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("101_spit");
                    yield return new WaitForSeconds(0.5f);
                    player_character.AddStatus(Status.status.weak, 1);
                    player_character.AddStatus(Status.status.frail, 1);
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
                    GetComponent<Animator>().Play("104_skill");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Heal(2);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("104_attack1");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 6);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("104_attack2");
                    yield return new WaitForSeconds(0.71f);
                    GetComponent<Character>().Attack(player, 3);
                    player_character.AddStatus(Status.status.bleed, 3);
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
                    yield return new WaitForSeconds(0.625f);
                    GetComponent<Character>().Attack(player, 7);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("106_spit");
                    yield return new WaitForSeconds(0.666f);
                    GetComponent<Character>().Attack(player, 5);
                    player_character.AddStatus(Status.status.vulnerable, 1);
                    player_character.AddStatus(Status.status.frail, 1);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("106_swing");
                    yield return new WaitForSeconds(0.625f);
                    GetComponent<Character>().Attack(player, 16);
                }
                else if (intention == 3){
                    GetComponent<Animator>().Play("106_spit");
                    yield return new WaitForSeconds(0.625f);
                    GetComponent<Character>().AddStatus(Status.status.rock_solid, 1);
                }
                break;
            case 107: // 巨型史萊姆
                if(intention == 0){
                    GetComponent<Animator>().Play("107_attack");
                    yield return new WaitForSeconds(0.5f);
                    deck.AddCardToDrawPile(deck.GetCard(202));
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("107_attack");
                    yield return new WaitForSeconds(0.5f);
                    deck.AddCardToDrawPile(deck.GetCard(203));
                    deck.AddCardToDrawPile(deck.GetCard(203));
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("107_attack");
                    yield return new WaitForSeconds(0.5f);
                    player_character.AddStatus(Status.status.weak, 2);
                    player_character.AddStatus(Status.status.frail, 2);
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
                    yield return new WaitForSeconds(0.555f);
                    effects.Play(player, "thief hand");
                    yield return new WaitForSeconds(0.375f);
                    if (GetComponent<Character>().GetArmor() > 0 || GetComponent<Character>().GetBlock() > 0)
                        player_character.AddStatus(Status.status.draw_less, 2);
                    else player_character.AddStatus(Status.status.draw_less, 1);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("108_attack");
                    yield return new WaitForSeconds(0.4f);
                    GetComponent<Character>().Attack(player, 9);
                    player_character.AddStatus(Status.status.weak, 1);
                    GetComponent<Character>().AddArmor(15);
                }
                else if (intention == 2){
                    GetComponent<Animator>().Play("108_attack");
                    yield return new WaitForSeconds(0.4f);
                    GetComponent<Character>().Attack(player, 16);
                    player_character.AddStatus(Status.status.vulnerable, 2);
                }
                else if (intention == 3){
                    GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.555f);
                    effects.Play(player, "thief hand");
                    yield return new WaitForSeconds(0.375f);
                    player_character.AddStatus(Status.status.information_erase, 1);
                }
                else if (intention == 4){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddStatus(Status.status.strength, 4);

                }
                break;

            case 201: // 羔羊
                if(intention == 0){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 9);
                }
                else if (intention == 1){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Heal(10);
                }
                break;
            case 202: // 烈焰蝙蝠
                if(intention == 0){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 10);
                    deck.AddCardToHand(deck.GetCard(205));
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
                    List<GameObject> list202 = new();
                    foreach(GameObject card in Deck.GetHand())
                        if (card.GetComponent<CardDisplay>().thisCard.id == 205) list202.Add(card);
                    foreach(GameObject card in list202){
                        deck.RemoveCard(card);
                        deck.AddCardToHand(deck.GetCard(206));
                    }
                }
                break;
            case 203: // 冷蛛
                if(intention == 0){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    deck.AddCardToHand(deck.GetCard(207));
                }
                else if (intention == 1){
                    // GetComponent<Animator>().Play("108_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 11);
                    if (player_character.GetArmor() == 0) player_character.AddStatus(Status.status.vulnerable, 1);
                }
                else if(intention == 2){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    player_character.AddStatus(Status.status.weak, 3);
                }
                break;
            case 204: // 樹妖
                if(intention == 0){
                    GetComponent<Animator>().Play("204_skill");
                    yield return new WaitForSeconds(0.4f);
                    GetComponent<Character>().AddArmor(10);
                    player_character.AddStatus(Status.status.weak, 2);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("204_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 10);
                    GetComponent<Character>().AddStatus(Status.status.strength, 1);
                }
                else if(intention == 2){
                    GetComponent<Animator>().Play("204_triple attack");
                    yield return new WaitForSeconds(3 / 14f);
                    GetComponent<Character>().Attack(player, 4);
                    yield return new WaitForSeconds(4 / 14f);
                    GetComponent<Character>().Attack(player, 4);
                    yield return new WaitForSeconds(4 / 14f);
                    GetComponent<Character>().Attack(player, 4);
                }
                break;
            case 205: // 盜匪
                if(intention == 0){
                    GetComponent<Animator>().Play("205_attack1");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 9);
                    player_character.AddStatus(Status.status.bleed, 3);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("205_attack1");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 13);
                    GetComponent<Character>().AddStatus(Status.status.vulnerable, 1);
                }
                else if(intention == 2){
                    GetComponent<Animator>().Play("205_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddStatus(Status.status.strength, 2);
                    //下次造成的傷害+5
                }
                else if(intention == 3){
                    // GetComponent<Animator>().Play("205_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(8);
                }
                break;
            case 206: // 烈火屠夫
                if(intention == 0){
                    // GetComponent<Animator>().Play("206_attack");
                    yield return new WaitForSeconds(0.5f);
                    if (GameObject.Find("Fire spirit 1") == null){
                        GameObject fireSpirit1 = FindObjectOfType<BattleController>().SpwanEnemyAt(30, new Vector3(180 + 300, 0, 0));
                        fireSpirit1.name = "Fire spirit 1";
                    }
                    else{
                        GameObject.Find("Fire spirit 1").GetComponent<Character>().AddArmor(10);
                        GameObject.Find("Fire spirit 1").GetComponent<Character>().AddStatus(Status.status.strength, 5);
                    }
                    if (GameObject.Find("Fire spirit 2") == null){
                        GameObject fireSpirit2 = FindObjectOfType<BattleController>().SpwanEnemyAt(30, new Vector3(180 + 600, 0, 0));
                        fireSpirit2.name = "Fire spirit 2";
                    }
                    else{
                        GameObject.Find("Fire spirit 2").GetComponent<Character>().AddArmor(10);
                        GameObject.Find("Fire spirit 2").GetComponent<Character>().AddStatus(Status.status.strength, 5);
                    }
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("206_buff");
                    yield return new WaitForSeconds(9 / 23f);
                    GetComponent<Character>().AddArmor(20);
                    int power_level206_1 = GetComponent<Character>().GetStatus(Status.status.strength);
                    GetComponent<Character>().AddStatus(Status.status.power_compete, 20+2*power_level206_1);
                }
                else if (intention == 2){
                    // GetComponent<Animator>().Play("206_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddStatus(Status.status.strength, 5);
                }
                else if(intention == 3){
                    GetComponent<Animator>().Play("206_attack");
                    yield return new WaitForSeconds(0.6f);
                    GetComponent<Character>().Attack(player, 20);
                    int power_level206_2 = GetComponent<Character>().GetStatus(Status.status.power_compete);
                    if (power_level206_2 > 0){
                        GetComponent<Character>().AddStatus(Status.status.power_compete, -power_level206_2);
                        GetComponent<Character>().AddStatus(Status.status.strength, 10);
                    }
                }
                else if(intention == 4){
                    // GetComponent<Animator>().Play("206_demage");
                    yield return new WaitForSeconds(0.5f);
                    foreach(GameObject enemy in BattleController.GetAllEnemy()){
                        enemy.GetComponent<Character>().Heal(12);
                    }
                    //所有人恢復血量
                }
                break;
            case 207: // 雙生暗影(A)
                if(intention == 0){
                    GetComponent<Animator>().Play("207_attack");
                    yield return new WaitForSeconds(2 / 18f);
                    GetComponent<Character>().Attack(player, 4);
                    yield return new WaitForSeconds(7 / 18f);
                    GetComponent<Character>().Attack(player, 4);
                    yield return new WaitForSeconds(5 / 18f);
                    GetComponent<Character>().Attack(player, 4);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("207_skill");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.frail, 2);
                }
                else if(intention == 2){
                    GetComponent<Animator>().Play("207_skill");
                    yield return new WaitForSeconds(0.5f);
                    foreach(GameObject enemy in BattleController.GetAllEnemy()){
                        enemy.GetComponent<Character>().AddArmor(10);
                    }
                }
                break;
            case 208: // 雙生暗影(B)
                if(intention == 0){
                    GetComponent<Animator>().Play("208_attack2");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 2);
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("208_attack");
                    yield return new WaitForSeconds(1 / 3f);
                    GetComponent<Character>().Attack(player, 5);
                    Global.AddSan(-5);
                }
                else if(intention == 2){
                    GetComponent<Animator>().Play("208_attack2");
                    yield return new WaitForSeconds(0.5f);
                    deck.AddCardToDrawPile(deck.GetCard(201));
                    deck.AddCardToDrawPile(deck.GetCard(201));
                }
                break;
            case 209: // 莎布·尼古拉絲
                if(intention == 0){
                    GetComponent<Animator>().Play("209_hurt");
                    yield return new WaitForSeconds(0.25f);
                    if (GameObject.Find("lamb 1") == null && GetComponent<Character>().GetHP() > 20){
                        GameObject lamb1 = FindObjectOfType<BattleController>().SpwanEnemyAt(8, new Vector3(180 + 300, 0, 0));
                        lamb1.name = "lamb 1";
                        GetComponent<Character>().LoseHP(20);
                    }
                    else if (GameObject.Find("lamb 1")){
                        GameObject.Find("lamb 1").GetComponent<Character>().AddMaxHP(13);
                    }
                    if (GameObject.Find("lamb 2") == null && GetComponent<Character>().GetHP() > 20){
                        GameObject fireSpirit2 = FindObjectOfType<BattleController>().SpwanEnemyAt(8, new Vector3(180 + 600, 0, 0));
                        fireSpirit2.name = "lamb 2";
                        GetComponent<Character>().LoseHP(20);
                    }
                    else if (GameObject.Find("lamb 2")){
                        GameObject.Find("lamb 2").GetComponent<Character>().AddMaxHP(13);
                    }
                }
                else if (intention == 1){
                    GetComponent<Animator>().Play("209_walk");
                    yield return new WaitForSeconds(0.5f);
                    foreach(GameObject enemy in BattleController.GetAllEnemy()){
                        enemy.GetComponent<Character>().AddArmor(12);
                        enemy.GetComponent<Character>().AddStatus(Status.status.strength, 3);
                    }
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
                    Global.AddSan(-6);
                }
                else if(intention == 4){
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
                    yield return new WaitForSeconds(8 / 13f);
                    GetComponent<Character>().Attack(player,5);
                    if (player_character.GetArmor() == 0) player_character.AddStatus(Status.status.burn, 3);
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
                    intentionObj.transform.GetChild(0).GetComponent<TMP_Text>().text = BattleController.ComputeDamage(gameObject, GetPlayer(), value).ToString();
                break;
            case 2:
                intentionObj.GetComponent<Image>().sprite = defendImg;
                if (value > 0)
                    intentionObj.transform.GetChild(0).GetComponent<TMP_Text>().text = value.ToString();
                break;
            case 3:
                intentionObj.GetComponent<Image>().sprite = othersImg;
                break;
            default:
                Debug.Log("ShowIntention: Unknown type " + type.ToString());
                break;
        }
        if (value == 0) intentionObj.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
    }
    void ShowIntention(int type, int value, int times){
        if (intentionObj == null){
            intentionObj = Instantiate(intentionTemplate, transform);
            intentionObj.transform.localPosition = new Vector3(0, 200, 0);
        }
        if (type != 1) return;
        intentionObj.GetComponent<Image>().sprite = attackImg;
        if (value > 0)
            intentionObj.transform.GetChild(0).GetComponent<TMP_Text>().text = BattleController.ComputeDamage(gameObject, GetPlayer(), value).ToString() + " x " + times.ToString();
        if (value == 0) intentionObj.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
    }

    public int GetIntentionType(){
        if (intentionObj.GetComponent<Image>().sprite == attackImg) return 1;
        if (intentionObj.GetComponent<Image>().sprite == defendImg) return 2;
        if (intentionObj.GetComponent<Image>().sprite == othersImg) return 3;
        return -1;
    }
}
