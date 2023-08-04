using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic_Implement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<int> Relic_Immediate = new List<int>();
    [SerializeField] List<int> Relic_After_Card = new List<int>();
    [SerializeField] List<int> Relic_After_Action = new List<int>();
    [SerializeField] List<int> Relic_Before_Action = new List<int>();
    GameObject player;
    List<GameObject> enemys;
    List<GameObject> all_hands_cards;
    Card first_card;
    Card throw_card;
    int throw_card_num;
    int now_turn;
    int kill_enemy;
    int attack_card_num = 0;
    void Awake()
    {
    }
    // void Use_Relic(int id)
    // {
    //     switch (id)
    //     {
    //         case 1:
    //             break;
    //         case 2:
    //             break;
    //         case 3:
    //             break;
    //         case 4:
    //             break;
    //         case 5:
    //             break;
    //         case 6:
    //             //力量
    //             Global.AddHp(5);
    //             break;
    //         case 7:
    //             Global.AddMaxHp(10);
    //             break;
    //         case 8:
    //             break;
    //         case 9:
    //             break;
    //         case 10:
    //             break;
    //         case 11:
    //             //力量
    //             Global.AddMaxHp(4);
    //             break;
    //         case 12:
    //             //力量
    //             Global.AddMaxHp(6);
    //             break;
    //         case 13:
    //             break;
    //         case 14:
    //             break;
    //         case 15:
    //             break;
    //         case 16:
    //             //力量
    //             Global.AddMaxSan(-10);
    //             Global.AddMaxHp(10);
    //             break;
    //         case 17:
    //             break;
    //         case 18:
    //             break;
    //         case 19:
    //             break;
    //         case 20:
    //             break;
    //         case 21:
    //             break;
    //         case 22:
    //             break;
    //         case 23:
    //             break;
    //         case 24:
    //             break;
    //         case 25:
    //             break;
    //         case 26:
    //             break;
    //         case 27:
    //             break;
    //         case 28:
    //             break;
    //         case 29:
    //             break;
    //         case 30:
    //             Global.AddMaxSan(10);
    //             break;
    //         default:
    //             break;
    //     }
    // }
    ///<summary>玩家打出一張牌後</summary>
    /// <param name="_player">傳入玩家</param>
    /// <param name="_enemys">傳入所有敵人</param>
    /// <param name="_all_hands_cards">傳入玩家手牌</param>
    /// <param name="_now_turn">傳入當前回合數</param>
    /// <param name="_kill_enemy">傳入打出這張牌後擊殺敵人數</param>
    /// <param name="_throw_card">傳入打出的牌</param>
    public void Handle_Relic_After_Card(
        GameObject _player,
        List<GameObject> _enemys,
        List<GameObject> _all_hands_cards,
        int _now_turn,
        int _kill_enemy,
        Card _throw_card)
    {
        player = _player;
        enemys = _enemys;
        all_hands_cards = _all_hands_cards;
        now_turn = _now_turn;
        kill_enemy = _kill_enemy;
        throw_card = _throw_card;
        List<int> relic_list = GetComponent<Bag_System>().Return_All_Relic();
        if(relic_list.Contains(19))attack_card_num+=1;
        foreach(int id in relic_list)
        {
            
            if(Relic_After_Card.Contains(id)==true)
            {
                Use_Relic_After_Card(id);
                // break;
            }
        }

    }
    ///<summary>玩家結束回合後，敵人行動前</summary>
    /// <param name="_player">傳入玩家</param>
    /// <param name="_enemys">傳入所有敵人</param>
    /// <param name="_all_hands_cards">傳入玩家手牌</param>
    /// <param name="_now_turn">傳入當前回合數</param>
    public void Handle_Relic_After_Action(
        GameObject _player,
        List<GameObject> _enemys,
        List<GameObject> _all_hands_cards,
        int _now_turn)
    {
        player = _player;
        enemys = _enemys;
        all_hands_cards = _all_hands_cards;
        now_turn = _now_turn;
        // List<int> need_to_handle = new List<int>();
        List<int> relic_list = GetComponent<Bag_System>().Return_All_Relic();
        foreach(int id in relic_list)
        {
            
            if(Relic_After_Action.Contains(id)==true)
            {
                Use_Relic_After_Action(id);
                // break;
            }
        }
        // for(int i=0;i<Relic_Before_Action.Count;i++)
        // {
        //     if(GetComponent<Bag_System>().Have_Item("relic",Relic_Before_Action[i].ToString())==true)
        //     {
        //         need_to_handle.Add(Relic_Before_Action[i]);
        //     }
        // }

    }
    ///<summary>玩家開始出牌前</summary>
    /// <param name="_player">傳入玩家</param>
    /// <param name="_enemys">傳入所有敵人</param>
    /// <param name="_all_hands_cards">傳入玩家手牌</param>
    /// <param name="_first_card">本回合抽到的第一張牌</param>
    /// <param name="_now_turn">傳入當前回合數</param>
    public void Handle_Relic_Before_Action(
        GameObject _player,
        List<GameObject> _enemys,
        List<GameObject> _all_hands_cards,
        Card _first_card,
        int _now_turn)
    {
        player = _player;
        enemys = _enemys;
        all_hands_cards = _all_hands_cards;
        first_card = _first_card;
        now_turn = _now_turn;
        List<int> relic_list = GetComponent<Bag_System>().Return_All_Relic();
        foreach(int id in relic_list)
        {
            
            if(Relic_Before_Action.Contains(id)==true)
            {
                Use_Relic_Before_Action(id);
                // break;
            }
        }

    }
    ///<summary>玩家一拿到遺物</summary>
    public void Handle_Relic_immediate(int relic_id)
    {
        foreach(int id in Relic_Immediate)
        {
            if(relic_id == id)
            {
                Use_Relic_Immediate(relic_id);
                break;
            }
        }

    }
    void Use_Relic_Immediate(int id)
    {
        switch (id)
        {
            case 6:
                Global.AddHp(5);
                break;
            case 7:
                Global.AddMaxHp(10);
                break;
            case 11:
                Global.AddMaxHp(4);
                break;
            case 12:
                Global.AddMaxHp(6);
                break;
            case 13:
                //燃燒待做
                break;
            case 16:
                Global.AddMaxSan(-10);
                Global.AddMaxHp(10);
                break;
            case 26:
                Global.money_addition = 1.25f;
                break;
            case 30:
                Global.AddMaxSan(10);
                break;
            default:
                break;
        }
    }
    void Use_Relic_Before_Action(int id)
    {
        switch (id)
        {
            case 6:
                if(now_turn==1)player.GetComponent<Character>().AddStatus(Status.status.strength,5);
                break;
            case 8:
                //死亡待做
                break;
            case 9:
                if(now_turn==1)
                {
                    player.GetComponent<Character>().AddStatus(Status.status.strength,3);
                    player.GetComponent<Character>().AddArmor(10);
                }
                break;
            case 10:
                //多抽卡待做
                FindObjectOfType<Deck>().Draw(1);
                break;
            case 11:
                if(now_turn==1)player.GetComponent<Character>().AddStatus(Status.status.strength,6);
                break;
            case 12:
                if(now_turn==1)player.GetComponent<Character>().AddStatus(Status.status.strength,4);
                break;
            case 15:
                if(first_card.type == Card.Type.attack)
                {
                    foreach (var enemy in enemys)
                    {
                        enemy.GetComponent<Character>().GetHit(3);
                    }
                }
                else 
                {
                    player.GetComponent<Character>().AddArmor(4);
                }
                break;
            case 16:
                if(now_turn==1)player.GetComponent<Character>().AddStatus(Status.status.strength,2);
                break;
            case 17:
                if(now_turn==1)
                {
                    List<Card> player_deck = Global.GetPlayerDeck();
                    int index = Random.Range(0,player_deck.Count);
                    int init = index;
                    while(player_deck[index].upgraded == true)
                    {
                        index ++;
                        if(index >= player_deck.Count)index = 0;
                        if(init == index)break;
                    }
                    if(player_deck[index].upgraded == true)break;
                    Global.UpgradeCard(player_deck[index]);
                }
                break;
            case 21:
                if(now_turn==1)
                {
                    player.GetComponent<Character>().AddStatus((Status.status)Random.Range(0,37),1);
                    player.GetComponent<Character>().AddStatus((Status.status)Random.Range(37,48),1);
                }
                break;
            case 23:
                //2費以上變2費
                break;
            case 27:
                if(now_turn==1)player.GetComponent<Character>().AddStatus(Status.status.dexterity,2);
                break;
            case 28:
                //卡傷害待做
                break;
            case 29:
                if(now_turn==1)
                {
                    foreach (var enemy in enemys)
                    {
                        enemy.GetComponent<Character>().AddStatus(Status.status.strength,-3);
                    }
                }
                break;
            default:
                break;
        }
    }
    void Use_Relic_After_Card(int id)
    {
        switch (id)
        {
            case 14:
                if(throw_card_num==1&&throw_card.type == Card.Type.attack)
                {
                    player.GetComponent<Character>().AddStatus(Status.status.temporary_dexterity,1);
                }
                else
                {
                    player.GetComponent<Character>().AddStatus(Status.status.temporary_strength,1);
                }
                break;
            case 18:
                player.GetComponent<Character>().AddStatus(Status.status.temporary_strength,kill_enemy);
                break;
            case 19:
                if(attack_card_num%2==0)
                {
                    player.GetComponent<Character>().AddStatus(Status.status.strength,1);
                }
                break;
            case 20:
                if(throw_card_num==1);//把卡片加回手牌
                break;
            case 24:
                //燃燒待做
                break;
            case 25:
                if(throw_card.type == Card.Type.attack)
                {
                    foreach (var enemy in enemys)
                    {
                        enemy.GetComponent<Character>().GetHit(3);
                    }
                }
                break;
            default:
                break;
        }
    }
    void Use_Relic_After_Action(int id)
    {
        switch (id)
        {
            case 5:
                break;
            case 22:
                break;
            default:
                break;
        }
    }
}
