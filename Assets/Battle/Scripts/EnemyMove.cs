using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
// using System;

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
    // int previous_state = -1;
    public List<int> list = new();
    GameObject GetPlayer(){
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        return player;
    }
    public void ChangeState(){
        int id = GetComponent<Character>().GetEnemyID();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Character player_character = player.GetComponent<Character>();

        switch(id){
            case 101: // 史萊姆
                state = Random.Range(0, 3);
                break;
            case 102: // 哥布林
                state = Random.Range(0, 3);
                break;
            case 103: // 幻影
                if (state == -1) state = 0;
                else if (state == 0) state = 1;
                else if (state == 1) state = 2;
                else if (state == 2) state = 1;
                break;
            case 104: //岩蟹
                // int hp = GetComponent<Character>().GetHP();
                // int maxHP = GetComponent<Character>().GetMaxHP();
                if (GetComponent<Character>().IsFullHealth()) state = Random.Range(1, 3);
                else state = 0;
                break;
            case 105: // 石像
                if (state == -1) state = 0;
                else if (state >= 0 && state <= 2) state += 1;
                else if (state == 3) state = 1;
                break;
            case 106: // 石巨人
                if (state == -1) state = 0;
                else if (GetComponent<Character>().GetHP() <= GetComponent<Character>().GetMaxHP() / 2 &&
                         GetComponent<Character>().GetStatus(Status.status.rock_solid) < 2){
                    state = 3;
                }
                else if (state == 0) state = 1;
                else if (state == 1) state = 2;
                else if (state == 2) state = 0;
                else if (state == 3) state = 2;
                break;
            case 107: // 巨型史萊姆
                if (state == -1) state = 0;
                else if (state == 5) state = 0;
                else state += 1;
                break;
            case 108: // 盜匪首領
                if (state == -1) state = 0;
                else if (state == 4) state = 0;
                else state += 1;
                break;
            case 201: // 羔羊
                // int hp_201 = GetComponent<Character>().GetHP();
                // int maxHP_201 = GetComponent<Character>().GetMaxHP();
                if (GetComponent<Character>().IsFullHealth()) state = 0;
                else{
                    if(Random.value < 0.34) state = 1;
                    else state = 0;
                }
                break;
            case 202: // 烈焰蝙蝠
                if (state == -1) state = 0;
                else if (state == 4) state = 0;
                else state += 1;
                break;
            case 203: // 冷蛛
                if (state == -1) state = 0;
                else if (state == 2) state = 0;
                else state += 1;
                break;
            case 204: // 樹妖
                if (state == -1) state = 0;
                else if (state == 2) state = 0;
                else state += 1;
                break;
            case 205: // 盜匪
                state = Random.Range(1,4);
                if(player_character.GetStatus(Status.status.bleed) == 0 && state == 1) state = 0;
                break;
            case 206: // 烈火屠夫
                if (state == -1) state = 0;
                else if (state == 2 && GetComponent<Character>().GetStatus(Status.status.power_compete) > 0) state = 3;
                else if (state == 2 && GetComponent<Character>().GetStatus(Status.status.power_compete) == 0) state = 4;
                else if (state == 3 || state == 4) state = 0;
                else state += 1;
                break;
            case 207: // 雙生暗影(A)
                if (state == -1) state = 0;
                else if (state == 3) state = 0;
                else state += 1;
                break;
            case 208: // 雙生暗影(B)
                if (state == -1) state = 0;
                else if (state == 4) state = 0;
                else state += 1;
                break;
            case 209: // 莎布·尼古拉絲
                if (state == -1) state = 0;
                else if (state == 4) state = 0;
                else state += 1;
                break;
            case 301: // 幽靈馬
                if (state == -1) state = 0;
                else if (state == 2) state = 0;
                else state += 1;
                break;
            case 302: // 觀察者
                state = 0;
                break;
            case 303: // 護衛者
                if (GetComponent<Character>().GetStatus(Status.status.accumulation) < 20) state = 0;
                else state = 1;
                break;
            case 304: // 追蹤者
                if (state == -1) state = 0;
                else if (state == 2) state = 0;
                else state += 1;
                break;
            case 305: // 騎士骷髏
                if (state == -1) state = 0;
                else if (state == 1 && GetComponent<Character>().GetArmor() >= 10) state = 2;
                else if (state == 1 && GetComponent<Character>().GetArmor() < 10) state = 3;
                else if (state == 3) state = 0;
                else state += 1;
                break;
            case 306: // 屠刀骷髏
                if (state == -1) state = 0;
                else if (state == 2) state = 0;
                else state += 1;
                break;
            case 307: // 長矛骷髏
                if (state == -1) state = 0;
                else if (state == 3) state = 0;
                else state += 1;
                break;
            case 308: // 裂嘴
                if (state == -1) state = 0;
                else if (state == 3) state = 0;
                else state += 1;
                break;
            case 309: // 扭曲團塊(A)
                if (state == -1) state = 0;
                else if (state == 1) state = 0;
                else state += 1;
                break;
            case 310: // 扭曲團塊(B)
                if (state == -1) state = 0;
                else if (state == 1) state = 0;
                else state += 1;
                break;
            case 311: // 恐懼聚合體
                if (state == -1) state = 0;
                else if (state == 3 && BattleController.GetEnemyCount() > 1) state = 4;
                else if (state == 3 && BattleController.GetEnemyCount() == 1) state = 0;
                else if (state == 4) state = 0;
                else state += 1;
                break;
            case 312: // 深淵凝視者
                if (state == -1) state = 0;
                Debug.Log("312 set state");
                if (state / 10 == 111){
                    state = 3;
                    return;
                }
                List<int> moveList = new();
                if (state / 1000 == 0) moveList.Add(1000);
                if (state % 1000 / 100 == 0) moveList.Add(100);
                if (state % 100 / 10 == 0) moveList.Add(10);
                state -= state % 10;
                switch(moveList[Random.Range(0, moveList.Count)]){
                    case 1000:
                        state += 1000;
                        break;
                    case 100:
                        state += 101;
                        break;
                    case 10:
                        state += 12;
                        break;
                }
                break;
            case 313: // 阿薩托斯
                if (state == -1) state = 0;
                else if (GetComponent<Character>().GetHP() <= GetComponent<Character>().GetMaxHP() * 0.5f && state < 10) state = 17;
                else if (state % 10 == 6 || state % 10 == 7) state = 0;
                else state += 1;
                break;
            case 401: // 火精靈
                if (state == -1) state = 0;
                else if (state == 0) state = 1;
                else if (state == 1) state = 0;
                break;
            case 402: // 惡意
                if (state == -1) state = 0;
                else if (state == 2) state = 0;
                else state += 1;
                break;
            case 403: // 編劇家
                if (state == -1) state = 0;
                else if (state == 5) state = 0;
                else state += 1;
                break;
            default:
                Debug.Log("SetIntention: Unknown enemy id " + id.ToString());
                break;
        }
    }
    public void SetIntention(){
        int id = GetComponent<Character>().GetEnemyID();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player==null)return;
        Character player_character = player.GetComponent<Character>();

        // previous_state = state;
        Debug.Log("SetIntention");
        switch(id){
            case 101: // 史萊姆
                if (state == 0) ShowIntention(1, 6);
                else if (state == 1) ShowIntention(1, 10);
                else if (state == 2) ShowIntention(3, 0);
                break;
            case 102: // 哥布林
                if (state == 0) ShowIntention(1, 10);
                else if (state == 1) ShowIntention(1, 6);
                else if (state == 2) ShowIntention(2, 8);
                break;
            case 103: // 幻影
                if (state == 0 || state == 2) ShowIntention(3, 0);
                else if (state == 1) ShowIntention(1, 8);
                break;
            case 104: //岩蟹
                if (state == 0) ShowIntention(3, 0);
                else if (state == 1) ShowIntention(1, 6);
                else if (state == 2) ShowIntention(1, 3);
                break;
            case 105: // 石像
                if (state == 0 || state == 3) ShowIntention(3, 0);
                else if (state == 1 || state == 2) ShowIntention(2, 5);
                break;
            case 106: // 石巨人
                if (state == 0) ShowIntention(1, 7);
                else if (state == 1) ShowIntention(1, 5);
                else if (state == 2) ShowIntention(1, 16);
                else if (state == 3) ShowIntention(3, 0);
                break;
            case 107: // 巨型史萊姆
                switch(state){
                    case 0:
                    case 1:
                    case 3:
                    case 4:
                        ShowIntention(3, 0);
                        break;
                    case 2:
                    case 5:
                        ShowIntention(1, 13);
                        break;
                }
                break;
            case 108: // 盜匪首領
                switch(state){
                    case 0:
                        ShowIntention(1, 9);
                        break;
                    case 3:
                        ShowIntention(1, 16);
                        break;
                    case 1:
                    case 2:
                    case 4:
                        ShowIntention(3, 0);
                        break;
                }
                break;
            case 201: // 羔羊
                if (state == 0) ShowIntention(1, 9);
                else if (state == 1) ShowIntention(3, 0);
                break;
            case 202: // 烈焰蝙蝠
                switch(state){
                    case 0:
                    case 2:
                        ShowIntention(1, 10);
                        break;
                    case 1:
                    case 3:
                        ShowIntention(3, 0);
                        break;
                    case 4:
                        ShowIntention(1, 16);
                        break;
                }
                break;
            case 203: // 冷蛛
                if (state == 0 || state == 1) ShowIntention(3, 0);
                else if (state == 2) ShowIntention(1, 11);
                break;
            case 204: // 樹妖
                if (state == 0) ShowIntention(2, 10);
                else if (state == 1) ShowIntention(1, 4, 3);
                else if (state == 2) ShowIntention(1, 10);
                break;
            case 205: // 盜匪
                if (state == 0) ShowIntention(1, 9);
                else if (state == 1) ShowIntention(1, 13);
                else if (state == 2) ShowIntention(3, 0);
                else if (state == 3) ShowIntention(2, 8);
                break;
            case 206: // 烈火屠夫
                switch(state){
                    case 0:
                    case 2:
                    case 4:
                        ShowIntention(3, 0);
                        break;
                    case 1:
                        ShowIntention(2, 20);
                        break;
                    case 3:
                        ShowIntention(1, 20);
                        break;
                }
                break;
            case 207: // 雙生暗影(A)
                if (state == 0 || state == 2) ShowIntention(1, 4, 3);
                else if(state == 1) ShowIntention(3, 0);
                else if(state == 3) ShowIntention(2, 10);
                break;
            case 208: // 雙生暗影(B)
                switch(state){
                    case 0:
                    case 2:
                        ShowIntention(3, 0);
                        break;
                    case 1:
                    case 3:
                        ShowIntention(1, 5);
                        break;
                }
                break;
            case 209: // 莎布·尼古拉絲
                switch(state){
                    case 0:
                    case 3:
                    case 4:
                        ShowIntention(3, 0);
                        break;
                    case 1:
                        ShowIntention(2, 12);
                        break;
                    case 2:
                        ShowIntention(1, 23);
                        break;
                }
                break;
            case 301: // 幽靈馬
                if (state == 0) ShowIntention(1, 3, 3);
                else if (state == 1) ShowIntention(2, 15);
                else if (state == 2) ShowIntention(1, 13);
                break;
            case 302: // 觀察者
                if (state == 0) ShowIntention(3, 0);
                else if (state == 1) ShowIntention(2, 10);
                else if (state == 2) ShowIntention(3, 0);
                else if (state == 3) ShowIntention(1, 23);
                break;
            case 303: // 護衛者
                if (state == 0) ShowIntention(2,11);
                else if (state == 1) ShowIntention(1, GetComponent<Character>().GetStatus(Status.status.accumulation));
                break;
            case 304: // 追蹤者
                if (state == 0) ShowIntention(3, 0);
                else if (state == 1 || state == 2) ShowIntention(1, 11);
                break;
            case 305: // 騎士骷髏
                if (state == 0) ShowIntention(2, 13);
                else if (state == 1) ShowIntention(1, 8);
                else if (state == 2) ShowIntention(1, GetComponent<Character>().GetArmor());
                else if (state == 3) ShowIntention(3, 0);
                break;
            case 306: // 屠刀骷髏
                if (state == 0 || state == 1) ShowIntention(1, 6, 2);
                else if (state == 2) ShowIntention(1, 18);
                break;
            case 307: // 長矛骷髏
                if (state == 0) ShowIntention(2, 12);
                else if (state == 1 || state == 2) ShowIntention(1, 9);
                else if (state == 3) ShowIntention(1, 4, 3);
                break;
            case 308: // 裂嘴
                switch(state){
                    case 0:
                    case 1:
                    case 3:
                        ShowIntention(3, 0);
                        break;
                    case 2:
                        ShowIntention(1, 25);
                        break;
                }
                break;
            case 309: // 扭曲團塊(A)
                if (state == 0) ShowIntention(3, 0);
                else if (state == 1) ShowIntention(1, 8);
                break;
            case 310: // 扭曲團塊(B)
                if (state == 0) ShowIntention(1,16);
                else if (state == 1) ShowIntention(3, 0);
                break;
            case 311: // 恐懼聚合體
                switch(state){
                    case 0:
                    case 1:
                    case 2:
                        ShowIntention(3, 0);
                        break;
                    case 3:
                        ShowIntention(1, 18);
                        break;
                    case 4:
                        ShowIntention(1, 23 * (BattleController.GetEnemyCount() - 1));
                        break;
                }
                break;
            case 312: // 深淵凝視者
                ShowIntention(3, 0);
                break;
            case 313: // 阿薩托斯
                switch(state % 10){
                    case 0:
                    case 1:
                    case 4:
                    case 7:
                        ShowIntention(3, 0);
                        break;
                    case 2:
                        ShowIntention(1, 21);
                        break;
                    case 3:
                        ShowIntention(2, 18);
                        break;
                    case 5:
                        ShowIntention(1, 14);
                        break;
                    case 6:
                        int count = 0;
                        foreach(Card card in Deck.GetAll())
                            if (card.id == 209 || card.id == 210) count++;
                        ShowIntention(1, 16 * count);
                        break;
                }
                break;
            case 401: // 火精靈
                if (state == 0) ShowIntention(1, 5);
                else if (state == 1) ShowIntention(2, 8);
                break;
            case 402: // 惡意
                if (state == 0) ShowIntention(1,10);
                else if (state == 1 || state == 2) ShowIntention(3,0);
                break;
            case 403: // 編劇家
                switch(state){
                    case 1:
                    case 3:
                    case 5:
                        ShowIntention(3, 0);
                        break;
                    case 0:
                    case 2:
                        ShowIntention(1, 15);
                        break;
                    case 4:
                        ShowIntention(2, 18);
                        break;
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
                if (state == 0) return ("黏液撞擊", "造成<color=red>" + GetDamage(6).ToString() + "</color>點傷害，給予<color=red>1</color>層虛弱");
                if (state == 1) return ("撞擊", "造成<color=red>" + GetDamage(10).ToString() + "</color>點傷害");
                if (state == 2) return ("腐蝕", "給予<color=red>1</color>層虛弱、<color=red>1</color>層脆弱");
                break;
            case 102:
                if (state == 0) return ("戳刺", "造成<color=red>" + GetDamage(10).ToString() + "</color>點傷害");
                if (state == 1) return ("發怒", "造成<color=red>" + GetDamage(6).ToString() + "</color>點傷害，獲得<color=green>2</color>點力量");
                if (state == 2) return ("躲閃", "獲得<color=green>8</color>點護甲");
                break;
            case 103:
                if (state == 0) return ("模糊輪廓", "免疫下次受到的傷害");
                if (state == 1) return ("攻擊", "造成<color=red>" + GetDamage(8).ToString() + "</color>點傷害");
                if (state == 2) return ("驚嚇", "理智<color=red>-2</color>");
                break;
            case 104:
                if (state == 0) return ("躲藏", "恢復<color=green>1</color>點生命");
                if (state == 1) return ("揮擊", "造成<color=red>" + GetDamage(6).ToString() + "</color>點傷害");
                if (state == 2) return ("利螯", "造成<color=red>" + GetDamage(3).ToString() + "</color>點傷害，給予<color=red>3</color>層流血");
                break;
            case 105:
                if (state == 0) return ("路障", "獲得嘲諷與<color=green>3</color>點荊棘");
                if (state == 1 || state == 2) return ("修補", "獲得<color=green>5</color>點護甲");
                if (state == 3) return ("強化", "獲得<color=green>3</color>點荊棘");
                break;
            case 106:
                if (state == 0) return ("揮拳", "造成<color=red>" + GetDamage(7).ToString() + "</color>點傷害");
                if (state == 1) return ("戰吼", "造成<color=red>" + GetDamage(5).ToString() + "</color>點傷害，給予<color=red>1</color>層易傷與<color=red>1</color>層脆弱");
                if (state == 2) return ("巨臂橫掃", "造成<color=red>" + GetDamage(16).ToString() + "</color>點傷害");
                if (state == 3) return ("超硬化", "獲得<color=green>1</color>層堅硬");
                break;
            case 107:
                if (state == 1) return ("黏液噴射", "將1張「腐蝕黏液」加入抽牌堆");
                if (state == 4) return ("黏液噴射", "將2張「虛弱黏液」加入抽牌堆");
                if (state == 0 || state == 3) return ("腐蝕", "給予<color=red>2</color>層虛弱與<color=red>2</color>層脆弱");
                if (state == 2 || state == 5) return ("飛拳", "造成<color=red>" + GetDamage(13).ToString() + "</color>點傷害");
                break;
            case 108:
                if (state == 1) return ("偷竊", "使玩家下回合少抽1張牌。\n若行動時仍有護甲，改為2張");
                if (state == 0) return ("掩護劈砍", "造成<color=red>" + GetDamage(9).ToString() + "</color>點傷害，給予<color=red>1</color>層虛弱，獲得<color=green>15</color>點護甲");
                if (state == 3) return ("強力劈砍", "造成<color=red>" + GetDamage(16).ToString() + "</color>點傷害，給予<color=red>2</color>層易傷");
                if (state == 2) return ("情報抹消", "使玩家下回合結束時，手中一張隨機牌變為「空白」");
                if (state == 4) return ("興奮", "獲得<color=green>4</color>點力量");
                break;
            case 201:
                if (state == 0) return ("撞擊", "造成<color=red>" + GetDamage(9).ToString() + "</color>點傷害");
                if (state == 1) return ("舔舐", "恢復<color=green>10</color>點生命");
                break;
            case 202:
                if (state == 0 || state == 2) return ("火球", "造成<color=red>" + GetDamage(10).ToString() + "</color>點傷害，將1張「火苗」加入玩家手中");
                if (state == 1 || state == 3) return ("飛翔", "免疫下次受到的傷害");
                if (state == 4) return ("噴火", "造成<color=red>" + GetDamage(16).ToString() + "</color>點傷害，將玩家手中所有「火苗」變為「烈焰」");
                break;
            case 203:
                if (state == 0) return ("神經毒素", "給予<color=red>3</color>層虛弱");
                if (state == 1) return ("結網", "將1張「蜘蛛網」加入玩家手中");
                if (state == 2) return ("毒液注入", "造成<color=red>" + GetDamage(11).ToString() + "</color>點傷害，隨後，若玩家沒有護甲，給予<color=red>1</color>層易傷");
                break;
            case 204:
                if (state == 0) return ("樹根纏繞", "獲得<color=green>10</color>點護甲，給予<color=red>2</color>層虛弱");
                if (state == 1) return ("藤蔓長鞭", "造成<color=red>" + GetDamage(4).ToString() + "</color>點傷害<color=red>3</color>次");
                if (state == 2) return ("爪擊", "造成<color=red>" + GetDamage(10).ToString() + "</color>點傷害，獲得<color=green>1</color>點力量");
                break;
            case 205:
                if (state == 0) return ("射擊", "造成<color=red>" + GetDamage(9).ToString() + "</color>點傷害，給予<color=red>3</color>層流血");
                if (state == 1) return ("攻擊弱點", "造成<color=red>" + GetDamage(13).ToString() + "</color>點傷害，給予<color=green>1</color>層易傷");
                if (state == 2) return ("更換箭矢", "獲得<color=green>2</color>點力量，下次造成的傷害<color=green>+5</color>");
                if (state == 3) return ("躲閃", "獲得<color=green>8</color>點護甲");
                break;
            case 206:
                if (state == 0) return ("召喚隨從", "給予所有已在場的「火精靈」<color=green>10</color>點護甲與<color=green>5</color>點力量\n召喚<color=green>2</color>個「火精靈」");
                if (state == 1) return ("烈焰焚身", "獲得<color=green>20</color>點護甲、<color=green>" + (20+2*GetComponent<Character>().GetStatus(Status.status.strength)).ToString() + "</color>層較勁");
                if (state == 2) return ("伸展肌肉", "獲得<color=green>5</color>點力量");
                if (state == 3) return ("碎顱擊", "造成<color=red>" + GetDamage(20).ToString() + "</color>點傷害\n若擁有較勁狀態，清空層數並獲得<color=green>10</color>點力量");
                if (state == 4) return ("重整旗鼓", "所有友軍恢復<color=green>12</color>點生命");
                break;
            case 207:
                if (state == 0 || state == 2) return ("切割", "造成<color=red>" + GetDamage(4).ToString() + "</color>點傷害<color=red>3</color>次");
                if (state == 1) return ("詭譎氣息", "給予<color=red>2</color>層脆弱");
                if (state == 3) return ("暗影庇護", "所有友軍獲得<color=green>10</color>點護甲");
                break;
            case 208:
                if (state == 0) return ("力竭詛咒", "給予<color=red>2</color>層虛弱");
                if (state == 1 || state == 3) return ("噩夢斬擊", "造成<color=red>" + GetDamage(5).ToString() + "</color>點傷害，理智<color=red>-5</color>");
                if (state == 2) return ("精神入侵", "將<color=red>2</color>張「失憶」加入抽牌堆");
                break;
            case 209:
                if (state == 0) return ("孕育", "增加所有「羔羊」13點生命值上限\n召喚兩隻「羔羊」，每召喚一隻就失去<color=red>20</color>點生命\n若生命不足則不會召喚");
                if (state == 1) return ("祝福", "所有友軍獲得<color=green>12</color>點護甲與<color=green>3</color>點力量");
                if (state == 2) return ("觸手鞭笞", "造成<color=red>" + GetDamage(23).ToString() + "</color>點傷害，給予<color=red>2</color>層虛弱");
                if (state == 3) return ("褻語", "理智<color=red>-6</color>");
                if (state == 4) return ("次聲波", "給予「每回合理智<color=red>-1</color>」效果");
                break;
            case 301:
                if (state == 0) return ("踩踏", "造成<color=red>" + GetDamage(3).ToString() + "</color>點傷害<color=red>3</color>次");
                if (state == 1) return ("冥火護身", "獲得<color=green>15</color>點護甲");
                if (state == 2) return ("踢擊", "造成<color=red>" + GetDamage(13).ToString() + "</color>點傷害");
                break;
            case 302:
                if (state == 0) return ("觀察", "根據玩家本回合打出的第一張牌決定行動");
                if (state == 1) return ("警戒", "所有友軍獲得<color=green>10</color>點護甲");
                if (state == 2) return ("弱點鎖定", "給予<color=red>1</color>層易傷");
                if (state == 3) return ("電光石火", "造成<color=red>" + GetDamage(23).ToString() + "</color>點傷害");
                break;
            case 303:
                if (state == 0) return ("護盾充能", "獲得<color=green>11</color>點護甲");
                if (state == 1) return ("反衝", "造成等同蓄能層數的傷害，失去所有蓄能");
                break;
            case 304:
                if (state == 0) return ("陷阱設置", "將一張「捕獸夾」加入抽牌堆");
                if (state == 1 || state == 2) return ("射擊", "造成<color=red>" + GetDamage(11).ToString() + "</color>點傷害");
                break;
            case 305:
                if (state == 0) return ("架盾", "獲得<color=green>13</color>點護甲");
                if (state == 1) return ("劈砍", "造成<color=red>" + GetDamage(8).ToString() + "</color>點傷害");
                if (state == 2) return ("盾擊", "造成等同護甲量的傷害(<color=red>" + GetDamage(GetComponent<Character>().GetArmor()).ToString() + "</color>點)");
                if (state == 3) return ("號令", "所有友軍獲得<color=green>2</color>點力量");
                break;
            case 306:
                if (state == 2) return ("重劈", "造成<color=red>" + GetDamage(18).ToString() + "</color>點傷害，給予<color=red>1</color>層易傷");
                if (state == 0 || state == 1) return ("揮砍", "造成<color=red>" + GetDamage(6).ToString() + "</color>點傷害<color=red>2</color>次");
                break;
            case 307:
                if (state == 0) return ("支援", "給予最左方的友軍<color=green>12</color>點護甲");
                if (state == 3) return ("突刺連擊", "造成<color=red>" + GetDamage(4).ToString() + "</color>點傷害<color=red>3</color>次");
                if (state == 1 || state == 2) return ("突襲", "造成<color=red>" + GetDamage(9).ToString() + "</color>點傷害，給予<color=red>1</color>層虛弱");
                break;
            case 308:
                if (state == 0) return ("吞噬", "給予「下回合結束時，隨機移除一張手中的牌」效果");
                if (state == 1) return ("消化", "若被吞噬的牌為攻擊牌，獲得<color=green>5</color>點力量；\n若為技能牌，恢復<color=green>20</color>點生命；\n若為特殊牌，失去<color=red>30</color>點生命");
                if (state == 2) return ("碎骨撕咬", "造成<color=red>" + GetDamage(25).ToString() + "</color>點傷害，給予<color=red>2</color>層虛弱、<color=red>2</color>層脆弱、<color=red>2</color>層易傷");
                if (state == 3) return ("嘲笑", "玩家每有一個負面狀態使理智<color=red>-3</color>");
                break;
            case 309:
                if (state == 0) return ("脫力毒素", "給予<color=red>2</color>層虛弱");
                if (state == 1) return ("恐懼噬咬", "造成<color=red>" + GetDamage(8).ToString() + "</color>點傷害，理智<color=red>-4</color>");
                break;
            case 310:
                if (state == 0) return ("大力噬咬", "造成<color=red>" + GetDamage(16).ToString() + "</color>點傷害");
                if (state == 1) return ("裝甲腐蝕", "給予<color=red>2</color>層脆弱");
                break;
            case 311:
                if (state == 0 || state == 2) return ("召喚", "召喚2個「惡意」");
                if (state == 1) return ("邪咒", "給予<color=red>2</color>層虛弱");
                if (state == 3) return ("痛擊", "造成<color=red>" + GetDamage(18).ToString() + "</color>點傷害，給予<color=red>2</color>層易傷");
                if (state == 4) return ("引爆", "擊殺所有「惡意」，每擊殺一個就造成<color=red>" + GetDamage(23).ToString() + "</color>傷害並給予<color=red>2</color>層脆弱");
                break;
            case 312:
                if (state % 10 == 0) return ("震懾凝視", "下回合開始時，所有手牌的耗能<color=red>+1</color>，打出後恢復原本費用");
                if (state % 10 == 1) return ("死亡凝視", "下回合結束時，手中隨機1張牌的耗能<color=red>+10</color>");
                if (state % 10 == 2) return ("混亂凝視", "下回合開始時，隨機交換所有手牌的耗能");
                if (state % 10 == 3) return ("恐懼增幅", "給予<color=red>1</color>層精神衰弱，回合結束時造成的傷害<color=red>+4</color>");
                break;
            case 313:
                if (state % 10 == 7) return ("精神改造", "理智<color=red>-30</color>，隨機交換所有卡牌的名稱與圖片");
                if (state % 10 == 0) return ("幻覺", "增加3張偽裝成一般牌的「幻覺」到抽牌堆\n「幻覺」打出時受到<color=red>20</color>點傷害並移除，\n被「幻覺」以外的效果移除時對阿薩托斯造成<color=green>30</color>點傷害");
                if (state % 10 == 1) return ("竄改現實", "增加3張偽裝成一般牌的「虛無」到抽牌堆\n「虛無」打出時理智<color=red>-10</color>並移除，\n被「虛無」以外的效果移除時增加1張「看破」到手牌");
                if (state % 10 == 2) return ("夢境之矛", "造成<color=red>" + GetDamage(21).ToString() + "</color>點傷害，隨後，若玩家沒有護甲，給予<color=red>2</color>層夢境");
                if (state % 10 == 3) return ("空想鎧甲", "獲得<color=green>18</color>點護甲，獲得<color=green>1</color>層無敵");
                if (state % 10 == 4) return ("精神污染", "若理智>30，則理智<color=red>-8</color>，否則給予<color=red>2</color>層易傷");
                if (state % 10 == 5) return ("空間壓縮", "造成<color=red>" + GetDamage(14).ToString() + "</color>點傷害，下回合能量<color=red>-1</color>");
                if (state % 10 == 6) return ("審判", "抽牌堆、棄牌堆、手牌中每存在1張「幻覺」或「虛無」\n就造成1次<color=red>16</color>點傷害並給予<color=red>1</color>層虛弱與<color=red>1</color>層脆弱");
                break;
            case 401:
                if (state == 0) return ("火焰彈", "造成<color=red>" + GetDamage(5).ToString() + "</color>點傷害，隨後，若玩家沒有護甲，給予<color=red>3</color>層燃燒");
                if (state == 1) return ("防護", "獲得<color=green>8</color>點護甲");
                break;
            case 402:
                if (state == 0) return ("撞擊", "造成<color=red>" + GetDamage(10).ToString() + "</color>點傷害");
                if (state == 1) return ("精神干擾", "理智<color=red>-3</color>");
                if (state == 2) return ("爆燃", "給予<color=red>10</color>層燃燒，自身死亡");
                break;
            case 403:
                if (state == 1) return ("惡魔詠唱", "給予<color=red>2</color>層虛弱");
                if (state == 3) return ("褻瀆", "給予<color=red>1</color>層精神衰弱");
                if (state == 0 || state == 2) return ("惡咒", "造成<color=red>" + GetDamage(15).ToString() + "</color>點傷害");
                if (state == 4) return ("魔力障壁", "獲得<color=green>18</color>點護甲");
                if (state == 5) return ("改寫歷史", "將自身生命值變回2回合前的狀態(" + list[list.Count - 2] + ")");
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

        if (GetComponent<Character>().GetStatus(Status.status.immobile) > 0){
            GetComponent<Character>().AddStatus(Status.status.immobile, -1);
            // state = previous_state;
            yield break;
        }

        if (GetComponent<Character>().GetStatus(Status.status.unfortune) > 0 &&
            Random.Range(0, 100) < GetComponent<Character>().GetStatus(Status.status.unfortune)){
            // state = previous_state;
            yield break;
        }

        switch(id){
            case 101: // 史萊姆
                switch(state){
                    case 0:
                        GetComponent<Animator>().Play("101_strike");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().Attack(player, 6);
                        player_character.AddStatus(Status.status.weak, 1);
                        break;
                    case 1:
                        GetComponent<Animator>().Play("101_strike");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().Attack(player, 10);
                        break;
                    case 2:
                        GetComponent<Animator>().Play("101_spit");
                        yield return new WaitForSeconds(0.5f);
                        player_character.AddStatus(Status.status.weak, 1);
                        player_character.AddStatus(Status.status.frail, 1);
                        break;
                }
                break;
            case 102: // 哥布林
                switch(state){
                    case 0:
                        GetComponent<Animator>().Play("102_strike");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().Attack(player, 10);
                        break;
                    case 1:
                        GetComponent<Animator>().Play("102_strike");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().Attack(player, 6);
                        GetComponent<Character>().AddStatus(Status.status.strength, 2);
                        break;
                    case 2:
                        GetComponent<Animator>().Play("102_evade");
                        GetComponent<Character>().AddArmor(8);
                        break;
                }
                break;
            case 103: // 幻影
                switch(state){
                    case 0:
                        GetComponent<Character>().AddStatus(Status.status.invincible, 1);
                        break;
                    case 1:
                        GetComponent<Animator>().Play("103_attack");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().Attack(player, 8);
                        break;
                    case 2:
                        GetComponent<Animator>().Play("103_attack");
                        yield return new WaitForSeconds(0.5f);
                        Global.AddSan(-2);
                        break;
                }
                break;
            case 104: //岩蟹
                switch(state){
                    case 0:
                        GetComponent<Animator>().Play("104_skill");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().Heal(1);
                        break;
                    case 1:
                        GetComponent<Animator>().Play("104_attack1");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().Attack(player, 6);
                        break;
                    case 2:
                        GetComponent<Animator>().Play("104_attack2");
                        yield return new WaitForSeconds(0.71f);
                        GetComponent<Character>().Attack(player, 3);
                        player_character.AddStatus(Status.status.bleed, 3);
                        break;
                }
                break;
            case 105: // 石像
                switch(state){
                    case 0:
                        GetComponent<Animator>().Play("105_reinforce");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().AddStatus(Status.status.taunt, 99);
                        GetComponent<Character>().AddStatus(Status.status.spike, 3);
                        break;
                    case 1:
                    case 2:
                        GetComponent<Animator>().Play("105_repair");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().AddArmor(5);
                        break;
                    case 3:
                        GetComponent<Animator>().Play("105_reinforce");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().AddStatus(Status.status.spike, 3);
                        break;
                }
                break;
            case 106: // 石巨人
                switch(state){
                    case 0:
                        GetComponent<Animator>().Play("106_attack");
                        yield return new WaitForSeconds(0.625f);
                        GetComponent<Character>().Attack(player, 7);
                        break;
                    case 1:
                        GetComponent<Animator>().Play("106_spit");
                        yield return new WaitForSeconds(0.666f);
                        GetComponent<Character>().Attack(player, 5);
                        player_character.AddStatus(Status.status.vulnerable, 1);
                        player_character.AddStatus(Status.status.frail, 1);
                        break;
                    case 2:
                        GetComponent<Animator>().Play("106_swing");
                        yield return new WaitForSeconds(0.625f);
                        GetComponent<Character>().Attack(player, 16);
                        break;
                    case 3:
                        GetComponent<Animator>().Play("106_spit");
                        yield return new WaitForSeconds(0.625f);
                        GetComponent<Character>().AddStatus(Status.status.rock_solid, 1);
                        break;
                }
                break;
            case 107: // 巨型史萊姆
                switch(state){
                    case 0:
                    case 3:
                        GetComponent<Animator>().Play("107_attack");
                        yield return new WaitForSeconds(0.5f);
                        player_character.AddStatus(Status.status.weak, 2);
                        player_character.AddStatus(Status.status.frail, 2);
                        break;
                    case 2:
                    case 5:
                        GetComponent<Animator>().Play("107_attack");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().Attack(player, 13);
                        break;
                    case 1:
                        GetComponent<Animator>().Play("107_attack");
                        yield return new WaitForSeconds(0.5f);
                        deck.AddCardToDrawPile(AllCards.GetCard(202));
                        break;
                    case 4:
                        GetComponent<Animator>().Play("107_attack");
                        yield return new WaitForSeconds(0.5f);
                        deck.AddCardToDrawPile(AllCards.GetCard(203));
                        deck.AddCardToDrawPile(AllCards.GetCard(203));
                        break;
                }
                break;
            case 108: // 盜匪首領
                switch(state){
                    case 0:
                        GetComponent<Animator>().Play("108_attack");
                        yield return new WaitForSeconds(0.4f);
                        GetComponent<Character>().Attack(player, 9);
                        player_character.AddStatus(Status.status.weak, 1);
                        GetComponent<Character>().AddArmor(15);
                        break;
                    case 1:
                        GetComponent<Animator>().Play("108_attack2");
                        yield return new WaitForSeconds(0.555f);
                        effects.Play(player, "thief hand");
                        yield return new WaitForSeconds(0.375f);
                        if (GetComponent<Character>().GetArmor() > 0 || GetComponent<Character>().GetBlock() > 0)
                            player_character.AddStatus(Status.status.draw_less, 2);
                        else player_character.AddStatus(Status.status.draw_less, 1);
                        break;
                    case 2:
                        GetComponent<Animator>().Play("108_attack2");
                        yield return new WaitForSeconds(0.555f);
                        effects.Play(player, "thief hand");
                        yield return new WaitForSeconds(0.375f);
                        player_character.AddStatus(Status.status.information_erase, 1);
                        break;
                    case 3:
                        GetComponent<Animator>().Play("108_attack");
                        yield return new WaitForSeconds(0.4f);
                        GetComponent<Character>().Attack(player, 16);
                        player_character.AddStatus(Status.status.vulnerable, 2);
                        break;
                    case 4:
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().AddStatus(Status.status.strength, 4);
                        break;
                }
                break;

            case 201: // 羔羊
                if (state == 0){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 9);
                }
                else if (state == 1){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Heal(10);
                }
                break;
            case 202: // 烈焰蝙蝠
                switch(state){
                    case 0:
                    case 2:
                        // GetComponent<Animator>().Play("108_attack2");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().Attack(player, 10);
                        deck.AddCardToHand(AllCards.GetCard(205));
                        break;
                    case 1:
                    case 3:
                        // GetComponent<Animator>().Play("108_attack");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().AddStatus(Status.status.invincible, 1);
                        break;
                    case 4:
                        // GetComponent<Animator>().Play("108_attack2");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().Attack(player, 16);
                        List<GameObject> list202 = new();
                        foreach(GameObject card in Deck.GetHand())
                            if (card.GetComponent<CardDisplay>().thisCard.id == 205) list202.Add(card);
                        foreach(GameObject card in list202){
                            deck.RemoveCard(card);
                            deck.AddCardToHand(AllCards.GetCard(206));
                        }
                        break;
                }
                break;
            case 203: // 冷蛛
                if (state == 0){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    player_character.AddStatus(Status.status.weak, 3);
                }
                else if (state == 1){
                    // GetComponent<Animator>().Play("108_attack2");
                    yield return new WaitForSeconds(0.5f);
                    deck.AddCardToHand(AllCards.GetCard(207));
                }
                else if (state == 2){
                    // GetComponent<Animator>().Play("108_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 11);
                    if (player_character.GetArmor() == 0) player_character.AddStatus(Status.status.vulnerable, 1);
                }
                break;
            case 204: // 樹妖
                if (state == 0){
                    GetComponent<Animator>().Play("204_skill");
                    yield return new WaitForSeconds(0.4f);
                    GetComponent<Character>().AddArmor(10);
                    player_character.AddStatus(Status.status.weak, 2);
                }
                else if (state == 1){
                    GetComponent<Animator>().Play("204_triple attack");
                    yield return new WaitForSeconds(3 / 14f);
                    GetComponent<Character>().Attack(player, 4);
                    yield return new WaitForSeconds(4 / 14f);
                    GetComponent<Character>().Attack(player, 4);
                    yield return new WaitForSeconds(4 / 14f);
                    GetComponent<Character>().Attack(player, 4);
                }
                else if (state == 2){
                    GetComponent<Animator>().Play("204_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 10);
                    GetComponent<Character>().AddStatus(Status.status.strength, 1);
                }
                break;
            case 205: // 盜匪
                if (state == 0){
                    GetComponent<Animator>().Play("205_attack1");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 9);
                    player_character.AddStatus(Status.status.bleed, 3);
                }
                else if (state == 1){
                    GetComponent<Animator>().Play("205_attack1");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 13);
                    GetComponent<Character>().AddStatus(Status.status.vulnerable, 1);
                }
                else if (state == 2){
                    GetComponent<Animator>().Play("205_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddStatus(Status.status.strength, 2);
                    GetComponent<Character>().AddStatus(Status.status.explosive_force, 5);
                }
                else if (state == 3){
                    // GetComponent<Animator>().Play("205_attack2");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(8);
                }
                break;
            case 206: // 烈火屠夫
                if (state == 0){
                    // GetComponent<Animator>().Play("206_attack");
                    yield return new WaitForSeconds(0.5f);
                    if (GameObject.Find("Fire spirit 1") == null){
                        GameObject fireSpirit1 = FindObjectOfType<BattleController>().SpawnEnemyAt(30, new Vector3(180 + 300, 0, 0));
                        fireSpirit1.name = "Fire spirit 1";
                        fireSpirit1.GetComponent<Character>().AddStatus(Status.status.summoned, 1);
                    }
                    else{
                        GameObject.Find("Fire spirit 1").GetComponent<Character>().AddArmor(10);
                        GameObject.Find("Fire spirit 1").GetComponent<Character>().AddStatus(Status.status.strength, 5);
                    }
                    if (GameObject.Find("Fire spirit 2") == null){
                        GameObject fireSpirit2 = FindObjectOfType<BattleController>().SpawnEnemyAt(30, new Vector3(180 + 600, 0, 0));
                        fireSpirit2.name = "Fire spirit 2";
                        fireSpirit2.GetComponent<Character>().AddStatus(Status.status.summoned, 1);
                    }
                    else{
                        GameObject.Find("Fire spirit 2").GetComponent<Character>().AddArmor(10);
                        GameObject.Find("Fire spirit 2").GetComponent<Character>().AddStatus(Status.status.strength, 5);
                    }
                }
                else if (state == 1){
                    GetComponent<Animator>().Play("206_buff");
                    yield return new WaitForSeconds(9 / 23f);
                    GetComponent<Character>().AddArmor(20);
                    int power_level206_1 = GetComponent<Character>().GetStatus(Status.status.strength);
                    GetComponent<Character>().AddStatus(Status.status.power_compete, 20+2*power_level206_1);
                }
                else if (state == 2){
                    // GetComponent<Animator>().Play("206_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddStatus(Status.status.strength, 5);
                }
                else if (state == 3){
                    GetComponent<Animator>().Play("206_attack");
                    yield return new WaitForSeconds(0.6f);
                    GetComponent<Character>().Attack(player, 20);
                    int power_level206_2 = GetComponent<Character>().GetStatus(Status.status.power_compete);
                    if (power_level206_2 > 0){
                        GetComponent<Character>().AddStatus(Status.status.power_compete, -power_level206_2);
                        GetComponent<Character>().AddStatus(Status.status.strength, 10);
                    }
                }
                else if (state == 4){
                    // GetComponent<Animator>().Play("206_demage");
                    yield return new WaitForSeconds(0.5f);
                    foreach(GameObject enemy in BattleController.GetAllEnemy()){
                        enemy.GetComponent<Character>().Heal(12);
                    }
                }
                break;
            case 207: // 雙生暗影(A)
                if (state == 0 || state == 2){
                    GetComponent<Animator>().Play("207_attack");
                    yield return new WaitForSeconds(2 / 18f);
                    GetComponent<Character>().Attack(player, 4);
                    yield return new WaitForSeconds(7 / 18f);
                    GetComponent<Character>().Attack(player, 4);
                    yield return new WaitForSeconds(5 / 18f);
                    GetComponent<Character>().Attack(player, 4);
                }
                else if (state == 1){
                    GetComponent<Animator>().Play("207_skill");
                    yield return new WaitForSeconds(0.5f);
                    player_character.AddStatus(Status.status.frail, 2);
                }
                else if (state == 3){
                    GetComponent<Animator>().Play("207_skill");
                    yield return new WaitForSeconds(0.5f);
                    foreach(GameObject enemy in BattleController.GetAllEnemy()){
                        enemy.GetComponent<Character>().AddArmor(10);
                    }
                }
                break;
            case 208: // 雙生暗影(B)
                if (state == 0){
                    GetComponent<Animator>().Play("208_attack2");
                    yield return new WaitForSeconds(0.5f);
                    player_character.AddStatus(Status.status.weak, 2);
                }
                else if (state == 1 || state == 3){
                    GetComponent<Animator>().Play("208_attack");
                    yield return new WaitForSeconds(1 / 3f);
                    GetComponent<Character>().Attack(player, 5);
                    Global.AddSan(-5);
                }
                else if (state == 2){
                    GetComponent<Animator>().Play("208_attack2");
                    yield return new WaitForSeconds(0.5f);
                    deck.AddCardToDrawPile(AllCards.GetCard(201));
                    deck.AddCardToDrawPile(AllCards.GetCard(201));
                }
                break;
            case 209: // 莎布·尼古拉絲
                if (state == 0){
                    GetComponent<Animator>().Play("209_hurt");
                    yield return new WaitForSeconds(0.25f);
                    if (GameObject.Find("lamb 1") == null && GetComponent<Character>().GetHP() > 20){
                        GameObject lamb1 = FindObjectOfType<BattleController>().SpawnEnemyAt(8, new Vector3(180 + 300, 0, 0));
                        lamb1.name = "lamb 1";
                        lamb1.GetComponent<Character>().AddStatus(Status.status.summoned, 1);
                        GetComponent<Character>().LoseHP(20);
                    }
                    else if (GameObject.Find("lamb 1")){
                        GameObject.Find("lamb 1").GetComponent<Character>().AddMaxHP(13);
                    }
                    if (GameObject.Find("lamb 2") == null && GetComponent<Character>().GetHP() > 20){
                        GameObject lamb2 = FindObjectOfType<BattleController>().SpawnEnemyAt(8, new Vector3(180 + 600, 0, 0));
                        lamb2.name = "lamb 2";
                        lamb2.GetComponent<Character>().AddStatus(Status.status.summoned, 1);
                        GetComponent<Character>().LoseHP(20);
                    }
                    else if (GameObject.Find("lamb 2")){
                        GameObject.Find("lamb 2").GetComponent<Character>().AddMaxHP(13);
                    }
                }
                else if (state == 1){
                    GetComponent<Animator>().Play("209_walk");
                    yield return new WaitForSeconds(0.5f);
                    foreach(GameObject enemy in BattleController.GetAllEnemy()){
                        enemy.GetComponent<Character>().AddArmor(12);
                        enemy.GetComponent<Character>().AddStatus(Status.status.strength, 3);
                    }
                }
                else if (state == 2){
                    GetComponent<Animator>().Play("209_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 23);
                    player_character.AddStatus(Status.status.weak, 2);
                }
                else if (state == 3){
                    GetComponent<Animator>().Play("209_walk");
                    yield return new WaitForSeconds(0.5f);
                    Global.AddSan(-6);
                }
                else if (state == 4){
                    GetComponent<Animator>().Play("209_walk");
                    yield return new WaitForSeconds(0.5f);
                    player_character.AddStatus(Status.status.mental_weak, 1);
                }
                break;
            case 301: // 幽靈馬
                if (state == 0){
                    GetComponent<Animator>().Play("301_run");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,3);
                    yield return new WaitForSeconds(0.25f);
                    GetComponent<Character>().Attack(player,3);
                    yield return new WaitForSeconds(0.25f);
                    GetComponent<Character>().Attack(player,3);
                }
                else if (state == 1){
                    GetComponent<Animator>().Play("301_run");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(15);
                }
                else if (state == 2){
                    GetComponent<Animator>().Play("301_run");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 13);
                }
                break;
            case 302: // 觀察者
                if (state == 1){
                    // GetComponent<Animator>().Play("302_attack");
                    yield return new WaitForSeconds(0.5f);
                    foreach(GameObject enemy in BattleController.GetAllEnemy()){
                        enemy.GetComponent<Character>().AddArmor(10);
                    }
                }
                else if (state == 2){
                    // GetComponent<Animator>().Play("302_attack2");
                    yield return new WaitForSeconds(0.5f);
                    player_character.AddStatus(Status.status.vulnerable, 1);

                }
                else if (state == 3){
                    GetComponent<Animator>().Play("302_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 23);
                }
                break;
            case 303: // 護衛者
                if (state == 0){
                    GetComponent<Animator>().Play("303_shield");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(11);
                }
                else if (state == 1){
                    GetComponent<Animator>().Play("303_attack");
                    yield return new WaitForSeconds(0.5f);
                    int accumulation_level = GetComponent<Character>().GetStatus(Status.status.accumulation);
                    GetComponent<Character>().Attack(player, accumulation_level);
                    GetComponent<Character>().AddStatus(Status.status.accumulation, -accumulation_level);
                }
                break;
            case 304: // 追蹤者
                if (state == 0){
                    GetComponent<Animator>().Play("304_skill");
                    yield return new WaitForSeconds(0.5f);
                    deck.AddCardToDrawPile(AllCards.GetCard(208));
                }
                else if (state == 1 || state == 2){
                    GetComponent<Animator>().Play("304_attack");
                    // yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 11);
                }
                break;
            case 305: // 騎士骷髏
                if (state == 0){
                    GetComponent<Animator>().Play("305_shield");
                    yield return new WaitForSeconds(1 / 6f);
                    GetComponent<Character>().AddArmor(13);
                }
                else if (state == 1){
                    GetComponent<Animator>().Play("305_attack");
                    yield return new WaitForSeconds(5 / 8f);
                    GetComponent<Character>().Attack(player,8);
                }
                else if (state == 2){
                    GetComponent<Animator>().Play("305_shield");
                    yield return new WaitForSeconds(1 / 6f);
                    int armor_num = GetComponent<Character>().GetArmor();
                    GetComponent<Character>().Attack(player, armor_num);
                }
                else if (state == 3){
                    GetComponent<Animator>().Play("305_skill");
                    yield return new WaitForSeconds(1 / 3f);
                    foreach(GameObject enemy in BattleController.GetAllEnemy()){
                        enemy.GetComponent<Character>().AddStatus(Status.status.strength, 2);
                    }
                }
                break;
            case 306: // 屠刀骷髏
                if (state == 2){
                    GetComponent<Animator>().Play("306_attack2");
                    yield return new WaitForSeconds(3 / 7f);
                    GetComponent<Character>().Attack(player, 18);
                    player_character.AddStatus(Status.status.vulnerable, 1);
                }
                else if (state == 0 || state == 1){
                    GetComponent<Animator>().Play("306_attack");
                    yield return new WaitForSeconds(1 / 6f);
                    GetComponent<Character>().Attack(player, 6);
                    yield return new WaitForSeconds(1 / 2f);
                    GetComponent<Character>().Attack(player, 6);
                }
                break;
            case 307: // 長矛骷髏
                if (state == 0){
                    // GetComponent<Animator>().Play("307_attack");
                    yield return new WaitForSeconds(0.5f);
                    foreach(GameObject enemy in BattleController.GetAllEnemy()){
                        enemy.GetComponent<Character>().AddArmor(12);
                        break;
                    }
                }
                else if (state == 3){
                    GetComponent<Animator>().Play("307_attack2");
                    yield return new WaitForSeconds(0.2f);
                    GetComponent<Character>().Attack(player, 4);
                    yield return new WaitForSeconds(0.3f);
                    GetComponent<Character>().Attack(player, 4);
                    yield return new WaitForSeconds(0.2f);
                    GetComponent<Character>().Attack(player, 4);
                }
                else if (state == 1 || state == 2){
                    GetComponent<Animator>().Play("307_attack");
                    yield return new WaitForSeconds(1 / 3f);
                    GetComponent<Character>().Attack(player, 9);
                    player_character.AddStatus(Status.status.weak, 1);
                }
                break;
            case 308: // 裂嘴
                if (state == 0){
                    // GetComponent<Animator>().Play("308_attack");
                    yield return new WaitForSeconds(0.5f);
                    player_character.AddStatus(Status.status.swallow, 1);
                }
                else if (state == 1){
                    // GetComponent<Animator>().Play("308_attack");
                    yield return new WaitForSeconds(0.5f);
                    Card.Type cardType = FindObjectOfType<BattleController>().GetSwallowedCard().type;
                    if (cardType == Card.Type.attack) GetComponent<Character>().AddStatus(Status.status.strength, 5);
                    if (cardType == Card.Type.skill) GetComponent<Character>().Heal(20);
                    if (cardType == Card.Type.special) GetComponent<Character>().LoseHP(30);
                }
                else if (state == 2){
                    // GetComponent<Animator>().Play("308_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 25);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 2);
                    player.GetComponent<Character>().AddStatus(Status.status.frail, 2);
                    player.GetComponent<Character>().AddStatus(Status.status.vulnerable, 2);
                }
                else if (state == 3){
                    // GetComponent<Animator>().Play("308_attack");
                    yield return new WaitForSeconds(0.5f);
                    Global.AddSan(-3 * player_character.GetNegativeStatusCount());
                }
                break;
            case 309: // 扭曲團塊(A)
                if (state == 0){
                    // GetComponent<Animator>().Play("309_attack");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 2);
                }
                else if (state == 1){
                    // GetComponent<Animator>().Play("309_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,8);
                    Global.AddSan(-4);
                }
                break;
            case 310: // 扭曲團塊(B)
                if (state == 0){
                    // GetComponent<Animator>().Play("309_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,16);
                }
                else if (state == 1){
                    // GetComponent<Animator>().Play("309_attack");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.frail, 2);
                }
                break;
            case 311: // 恐懼聚合體
                if (state == 0 || state == 2){
                    GetComponent<Animator>().Play("311_demage");
                    yield return new WaitForSeconds(0.5f);
                    Enemy311_Summon();
                }
                else if (state == 1){
                    // GetComponent<Animator>().Play("311_attack");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 2);
                }
                else if (state == 3){
                    GetComponent<Animator>().Play("311_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,18);
                    player.GetComponent<Character>().AddStatus(Status.status.vulnerable, 2);
                }
                else if (state == 4){
                    int count311 = 0;
                    foreach(GameObject enemy in BattleController.GetAllEnemy()){
                        if (enemy.GetComponent<Character>().GetEnemyID() == 402){
                            count311++;
                            enemy.GetComponent<Character>().LoseHP(99);
                        }
                    }
                    if (count311 != 0){
                        GetComponent<Animator>().Play("311_attack");
                        yield return new WaitForSeconds(0.5f);
                        GetComponent<Character>().Attack(player, 23 * count311);
                        player.GetComponent<Character>().AddStatus(Status.status.frail, 2);
                    }
                }
                break;
            case 312: // 深淵凝視者
                if (state % 10 == 0){
                    // GetComponent<Animator>().Play("312_demage");
                    yield return new WaitForSeconds(0.5f);
                    Deck.Use312Effect(1);
                }
                else if (state % 10 == 1){
                    // GetComponent<Animator>().Play("312_attack");
                    yield return new WaitForSeconds(0.5f);
                    Deck.Use312Effect(2);
                }
                else if (state % 10 == 2){
                    // GetComponent<Animator>().Play("312_attack");
                    yield return new WaitForSeconds(0.5f);
                    Deck.Use312Effect(3);
                }
                else if (state % 10 == 3){
                    // GetComponent<Animator>().Play("312_jump");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.mental_weak, 1);
                    GetComponent<Character>().AddStatus(Status.status.oppress, 4);
                }
                break;
            case 313: // 阿薩托斯
                if (state % 10 == 7){
                    GetComponent<Animator>().Play("313_attack1");
                    yield return new WaitForSeconds(0.5f);
                    Global.AddSan(-30);

                    List<Card> cardList313_0 = Deck.GetAll();
                    int randomIdx = Random.Range(0, cardList313_0.Count);
                    Card cardSaved = cardList313_0[randomIdx];
                    cardList313_0.RemoveAt(randomIdx);
                    while(cardList313_0.Count > 0){
                        randomIdx = Random.Range(0, cardList313_0.Count);
                        Card card1 = cardSaved;
                        Card card2 = cardList313_0[randomIdx];
                        (card1.image, card2.image) = (card2.image, card1.image);
                        // (card1.cardName, card2.cardName) = (card2.cardName, card1.cardName);
                        cardSaved = cardList313_0[randomIdx];
                        cardList313_0.RemoveAt(randomIdx);
                    }

                    cardList313_0 = Deck.GetAll();
                    randomIdx = Random.Range(0, cardList313_0.Count);
                    cardSaved = cardList313_0[randomIdx];
                    cardList313_0.RemoveAt(randomIdx);
                    while(cardList313_0.Count > 0){
                        randomIdx = Random.Range(0, cardList313_0.Count);
                        Card card1 = cardSaved;
                        Card card2 = cardList313_0[randomIdx];
                        // (card1.image, card2.image) = (card2.image, card1.image);
                        (card1.cardName, card2.cardName) = (card2.cardName, card1.cardName);
                        cardSaved = cardList313_0[randomIdx];
                        cardList313_0.RemoveAt(randomIdx);
                    }

                    deck.UpdateHand();
                }
                else if (state % 10 == 0){
                    GetComponent<Animator>().Play("313_attack1");
                    yield return new WaitForSeconds(0.5f);
                    List<Card> cardList313_1 = Deck.GetAll();
                    for (int i = 0; i < 3; i++){
                        Card clone = Card.Copy(cardList313_1[Random.Range(0, cardList313_1.Count)]);
                        clone.id = 209;
                        deck.AddCardToDrawPile(clone);
                    }
                }
                else if (state % 10 == 1){
                    GetComponent<Animator>().Play("313_attack1");
                    yield return new WaitForSeconds(0.5f);
                    List<Card> cardList313_2 = Deck.GetAll();
                    for (int i = 0; i < 3; i++){
                        Card clone = Card.Copy(cardList313_2[Random.Range(0, cardList313_2.Count)]);
                        clone.id = 210;
                        deck.AddCardToDrawPile(clone);
                    }
                }
                else if (state % 10 == 2){
                    GetComponent<Animator>().Play("313_attack2");
                    yield return new WaitForSeconds(5 / 8f);
                    GetComponent<Character>().Attack(player,21);
                    if (player_character.GetArmor() == 0) player_character.AddStatus(Status.status.dream, 2);
                }
                else if (state % 10 == 3){
                    GetComponent<Animator>().Play("313_attack1");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(18);
                    GetComponent<Character>().AddStatus(Status.status.invincible,1);
                }
                else if (state % 10 == 4){
                    GetComponent<Animator>().Play("313_attack1");
                    yield return new WaitForSeconds(0.5f);
                    if (Global.sanity > 30) Global.AddSan(-8);
                    else player_character.AddStatus(Status.status.vulnerable, 7);
                }
                else if (state % 10 == 5){
                    GetComponent<Animator>().Play("313_attack2");
                    yield return new WaitForSeconds(5 / 8f);
                    GetComponent<Character>().Attack(player,14);
                    player_character.AddStatus(Status.status.compress, 1);
                }
                else if (state % 10 == 6){
                    GetComponent<Animator>().Play("313_attack2");
                    yield return new WaitForSeconds(5 / 8f);
                    List<Card> cardList313_7 = Deck.GetAll();
                    int count = 0;
                    foreach(Card card in cardList313_7)
                        if (card.id == 209 || card.id == 210) count++;
                    for (int i = 0; i < count; i++){
                        GetComponent<Character>().Attack(player, 16);
                        player_character.AddStatus(Status.status.weak, 1);
                        player_character.AddStatus(Status.status.frail, 1);
                        yield return new WaitForSeconds(0.2f);
                    }
                }
                break;
            case 401: // 火精靈
                if (state == 0){
                    GetComponent<Animator>().Play("401_attack");
                    yield return new WaitForSeconds(8 / 13f);
                    GetComponent<Character>().Attack(player,5);
                    if (player_character.GetArmor() == 0) player_character.AddStatus(Status.status.burn, 3);
                }
                else if (state == 1){
                    GetComponent<Animator>().Play("401_run");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(8);
                }
                break;
            case 402: // 惡意
                if (state == 0){
                    // GetComponent<Animator>().Play("401_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player,10);
                }
                else if (state == 1){
                    // GetComponent<Animator>().Play("401_run");
                    yield return new WaitForSeconds(0.5f);
                    Global.AddSan(-3);
                }
                else if (state == 2){
                    GetComponent<Animator>().Play("402_attack");
                    yield return new WaitForSeconds(0.5f);
                    player_character.AddStatus(Status.status.burn,10);
                    GetComponent<Character>().LoseHP(99);
                }
                break;
            case 403: // 編劇家
                if (state == 1){
                    // GetComponent<Animator>().Play("401_attack");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.weak, 2);
                }
                else if (state == 3){
                    // GetComponent<Animator>().Play("401_run");
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Character>().AddStatus(Status.status.mental_weak, 1);
                }
                else if (state == 0 || state == 2){
                    // GetComponent<Animator>().Play("402_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().Attack(player, 15);
                }
                else if (state == 4){
                    // GetComponent<Animator>().Play("402_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().AddArmor(18);
                }
                else if (state == 5){
                    // GetComponent<Animator>().Play("402_attack");
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<Character>().SetHP(list[list.Count - 3]);
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
            // intentionObj.transform.localPosition = new Vector3(0, 200, 0);
            intentionObj.transform.localPosition = new Vector3(0, BattleController.GetEnemyHeight(GetComponent<Character>().GetEnemyID()) - 150, 0);
        }
        else{
            (string name, string description) = GetIntention();
            intentionObj.GetComponent<IntentionIcon>().UpdateText(name, description);
        }
        switch(type){
            case 1:
                intentionObj.GetComponent<Image>().sprite = attackImg;
                intentionObj.GetComponent<Image>().color = new Color(1, 0, 0, 1);
                intentionObj.GetComponentInChildren<TMP_Text>().color = new Color(1, 0, 0, 1);
                if (value > 0)
                    intentionObj.transform.GetChild(0).GetComponent<TMP_Text>().text = BattleController.ComputeDamage(gameObject, GetPlayer(), value).ToString();
                break;
            case 2:
                intentionObj.GetComponent<Image>().sprite = defendImg;
                intentionObj.GetComponent<Image>().color = new Color(0.3333f, 1, 1, 1);
                intentionObj.GetComponentInChildren<TMP_Text>().color = new Color(0.3333f, 1, 1, 1);
                if (value > 0)
                    intentionObj.transform.GetChild(0).GetComponent<TMP_Text>().text = value.ToString();
                break;
            case 3:
                intentionObj.GetComponent<Image>().sprite = othersImg;
                intentionObj.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                intentionObj.GetComponentInChildren<TMP_Text>().color = new Color(1, 1, 1, 1);
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
            // intentionObj.transform.localPosition = new Vector3(0, 200, 0);
            intentionObj.transform.localPosition = new Vector3(0, BattleController.GetEnemyHeight(GetComponent<Character>().GetEnemyID()) - 150, 0);
        }
        else{
            (string name, string description) = GetIntention();
            intentionObj.GetComponent<IntentionIcon>().UpdateText(name, description);
        }
        if (type != 1) return;
        intentionObj.GetComponent<Image>().sprite = attackImg;
        intentionObj.GetComponent<Image>().color = new Color(1, 0, 0, 1);
        intentionObj.GetComponentInChildren<TMP_Text>().color = new Color(1, 0, 0, 1);
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

    public void ClearIntention(){
        if (intentionObj) intentionObj.GetComponent<IntentionIcon>().DestroyBox();
    }



    public void SetState(int value){
        state = value;
    }



    public void Enemy302_Detect(Card card){
        if (state != 0) return;
        if (card.type == Card.Type.attack){
            state = 1;
            SetIntention();
        }
        if (card.type == Card.Type.skill){
            state = 2;
            SetIntention();
        }
        if (card.type == Card.Type.special){
            state = 3;
            SetIntention();
        }
    }
    public void Enemy303_Detect(){
        if (state == 1) SetIntention();
    }
    public void Enemy305_Detect(){
        if (state == 2) SetIntention();
    }
    public void Enemy311_Detect(){
        if (state == 4) SetIntention();
    }



    void Enemy311_Summon(){
        int count = 2;
        GameObject tmp = null;
        if (GameObject.Find("malice 1") == null && count > 0){
            tmp = FindObjectOfType<BattleController>().SpawnEnemyAt(31, new Vector3(400, 0, 0));
            tmp.name = "malice 1";
            tmp.GetComponent<Character>().AddStatus(Status.status.summoned, 1);
            count--;
        }
        if (GameObject.Find("malice 2") == null && count > 0){
            tmp = FindObjectOfType<BattleController>().SpawnEnemyAt(31, new Vector3(550, 0, 0));
            tmp.name = "malice 2";
            tmp.GetComponent<Character>().AddStatus(Status.status.summoned, 1);
            count--;
        }
        if (GameObject.Find("malice 3") == null && count > 0){
            tmp = FindObjectOfType<BattleController>().SpawnEnemyAt(31, new Vector3(700, 0, 0));
            tmp.name = "malice 3";
            tmp.GetComponent<Character>().AddStatus(Status.status.summoned, 1);
            count--;
        }
        if (GameObject.Find("malice 4") == null && count > 0){
            tmp = FindObjectOfType<BattleController>().SpawnEnemyAt(31, new Vector3(850, 0, 0));
            tmp.name = "malice 4";
            tmp.GetComponent<Character>().AddStatus(Status.status.summoned, 1);
            count--;
        }
    }
}
