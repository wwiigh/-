using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic_Implement : MonoBehaviour
{
    public enum Type
    {
        UseAttackCard,UseSkillCard,TurnStart
    }
    [SerializeField] static List<int> Relic_Immediate = new List<int>();
    [SerializeField] static List<int> Relic_After_Card = new List<int>();
    [SerializeField] static List<int> Relic_After_Action = new List<int>();
    [SerializeField] static List<int> Relic_Before_Action = new List<int>();
    [SerializeField] static List<int> Relic_Before_Battle = new List<int>();
    [SerializeField] static List<int> Relic_Dead = new List<int>();
    [SerializeField] static Bag_System bag_System;
    static GameObject player;
    static GameObject[] enemys;
    static List<GameObject> all_hands_cards;
    Card first_card;
    Card throw_card;
    int throw_card_num;
    int now_turn;
    int kill_enemy;
    int attack_card_num = 0;
    void Awake()
    {
        Relic_Immediate.Add(6);
        Relic_Immediate.Add(7);
        Relic_Immediate.Add(11);
        Relic_Immediate.Add(12);
        Relic_Immediate.Add(13);
        Relic_Immediate.Add(16);
        Relic_Immediate.Add(26);
        Relic_Immediate.Add(30);
        bag_System = FindObjectOfType<Bag_System>();
        Relic_Before_Battle.Add(5);
        Relic_Before_Battle.Add(6);
        Relic_Before_Battle.Add(9);
        Relic_Before_Battle.Add(11);
        Relic_Before_Battle.Add(12);
        Relic_Before_Battle.Add(16);
        Relic_Before_Battle.Add(17);
        Relic_Before_Battle.Add(21);
        Relic_Before_Battle.Add(22);
        Relic_Before_Battle.Add(23);
        Relic_Before_Battle.Add(27);
        Relic_Before_Battle.Add(28);
        Relic_Before_Battle.Add(29);
    }
    
    public static void Update_Relic(Type type)
    {
        List<int> relic_list = bag_System.Return_All_Relic();
        player = GameObject.FindGameObjectWithTag("Player");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        all_hands_cards = Deck.GetHand();
        BattleController battleController = FindObjectOfType<BattleController>();
        if(BattleController.PlayedCardThisTurn()==false && relic_list.Contains(14))
        {
            if(type == Type.UseAttackCard)
            {
                player.GetComponent<Character>().AddStatus(Status.status.temporary_dexterity,1);
            }
            else
            {
                player.GetComponent<Character>().AddStatus(Status.status.temporary_strength,1);
            }
        }
        if(relic_list.Contains(19)&&type == Type.UseAttackCard)
        {
            Global.relic_19+=1;
            if(Global.relic_19==2)
            {
                player.GetComponent<Character>().AddStatus(Status.status.strength,1);
                Global.relic_19 = 0;
            }
            bag_System.Update_Relic_Count(Global.relic_19);
        }
        if(relic_list.Contains(25)&&type == Type.UseAttackCard)
        {
            foreach (var item in enemys)
            {
                item.GetComponent<Character>().LoseHP(3);
            }
        }
        if(relic_list.Contains(28)&&type == Type.TurnStart)
        {
            int relic_28 = Random.Range(-4,6);
        }

    }

    // }
    // ///<summary>玩家結束回合後，敵人行動前</summary>
    // /// <param name="_player">傳入玩家</param>
    // /// <param name="_enemys">傳入所有敵人</param>
    // /// <param name="_all_hands_cards">傳入玩家手牌</param>
    // /// <param name="_now_turn">傳入當前回合數</param>
    // public void Handle_Relic_After_Action(
    //     GameObject _player,
    //     List<GameObject> _enemys,
    //     List<GameObject> _all_hands_cards,
    //     int _now_turn)
    // {
    //     player = _player;
    //     enemys = _enemys;
    //     all_hands_cards = _all_hands_cards;
    //     now_turn = _now_turn;
    //     // List<int> need_to_handle = new List<int>();
    //     List<int> relic_list = GetComponent<Bag_System>().Return_All_Relic();
    //     foreach(int id in relic_list)
    //     {
            
    //         if(Relic_After_Action.Contains(id)==true)
    //         {
    //             Use_Relic_After_Action(id);
    //             // break;
    //         }
    //     }
    //     // for(int i=0;i<Relic_Before_Action.Count;i++)
    //     // {
    //     //     if(GetComponent<Bag_System>().Have_Item("relic",Relic_Before_Action[i].ToString())==true)
    //     //     {
    //     //         need_to_handle.Add(Relic_Before_Action[i]);
    //     //     }
    //     // }

    // }
    // ///<summary>玩家開始出牌前</summary>
    // /// <param name="_player">傳入玩家</param>
    // /// <param name="_enemys">傳入所有敵人</param>
    // /// <param name="_all_hands_cards">傳入玩家手牌</param>
    // /// <param name="_first_card">本回合抽到的第一張牌</param>
    // /// <param name="_now_turn">傳入當前回合數</param>
    // public void Handle_Relic_Before_Action(
    //     GameObject _player,
    //     List<GameObject> _enemys,
    //     List<GameObject> _all_hands_cards,
    //     Card _first_card,
    //     int _now_turn)
    // {
    //     player = _player;
    //     enemys = _enemys;
    //     all_hands_cards = _all_hands_cards;
    //     first_card = _first_card;
    //     now_turn = _now_turn;
    //     List<int> relic_list = GetComponent<Bag_System>().Return_All_Relic();
    //     foreach(int id in relic_list)
    //     {
            
    //         if(Relic_Before_Action.Contains(id)==true)
    //         {
    //             Use_Relic_Before_Action(id);
    //             // break;
    //         }
    //     }

    // }

    ///<summary>戰鬥一開始</summary>
    public static void Handle_Relic_Before_Battle()
    {
        List<int> relic_list = bag_System.Return_All_Relic();
        player = GameObject.FindGameObjectWithTag("Player");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(" Num "+player + " " + enemys.Length);
        all_hands_cards = Deck.GetHand();
        foreach(int id in relic_list)
        {
            if(Relic_Before_Battle.Contains(id)==true)
            {
                Use_Relic_Before_Battle(id);
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
    public enum DeadType
    {
        Enemy,Player
    }
    ///<summary>玩家或敵人死亡</summary>
    public static void Handle_Relic_Dead(DeadType deadType)
    {
        List<int> relic_list = bag_System.Return_All_Relic();
        player = GameObject.FindGameObjectWithTag("Player");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        all_hands_cards = Deck.GetHand();
        if(deadType == DeadType.Player &&relic_list.Contains(8)==true)
        {
            Debug.Log("use dead");
            Use_Relic_Dead(8);
        }
        else if(deadType == DeadType.Enemy &&relic_list.Contains(18)==true)
        {
            Use_Relic_Dead(18);
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
    static void Use_Relic_Before_Battle(int id)
    {
        Debug.Log("now check id" + id);
        switch (id)
        {
            case 5:
                foreach (var enemy in enemys)
                {
                    enemy.GetComponent<Character>().AddStatus(Status.status.immobile,1);
                } 
                break;
            case 6:
                player.GetComponent<Character>().AddStatus(Status.status.strength,5);
                break;
            case 9:
                player.GetComponent<Character>().AddStatus(Status.status.strength,3);
                player.GetComponent<Character>().AddArmor(10);
                break;
            case 10:
                //多抽卡待做
                Deck.Draw(1);
                break;
            case 11:
                player.GetComponent<Character>().AddStatus(Status.status.strength,6);
                break;
            case 12:
                player.GetComponent<Character>().AddStatus(Status.status.strength,4);
                break;
            case 15:
                // if(first_card.type == Card.Type.attack)
                // {
                //     foreach (var enemy in enemys)
                //     {
                //         enemy.GetComponent<Character>().GetHit(3);
                //     }
                // // }
                // else 
                // {
                //     player.GetComponent<Character>().AddArmor(4);
                // }
                break;
            case 16:
                player.GetComponent<Character>().AddStatus(Status.status.strength,2);
                break;
            case 17:
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
                
                break;
            case 21:
                
                    player.GetComponent<Character>().AddStatus((Status.status)Random.Range(0,37),1);
                    player.GetComponent<Character>().AddStatus((Status.status)Random.Range(37,48),1);
                
                break;
            case 22:
                foreach (var enemy in enemys)
                {
                    enemy.GetComponent<Character>().AddStatus(Status.status.unfortune,10);
                } 
                break;
            case 23:
                List<Card> player_deck_23 = Deck.GetDeck();
                foreach (var item in player_deck_23)
                {
                    Debug.Log("card cost "+item.cost_original);
                    if(item.cost_original>=2)item.cost_original = 2;
                }
                break;
            case 27:
                player.GetComponent<Character>().AddStatus(Status.status.dexterity,2);
                break;
            case 28:
                player.GetComponent<Character>().AddStatus(Status.status.dice20,1);
                break;
            case 29:
                foreach (var enemy in enemys)
                {
                    enemy.GetComponent<Character>().AddStatus(Status.status.strength,-3);
                }            
                break;
            default:
                break;
        }
    }
    static void  Use_Relic_Dead(int id)
    {
        switch (id)
        {
            case 8:
                Global.player_hp = Global.player_max_hp;
                Global.sanity = Global.max_sanity;
                bag_System.Relic_Del_item(8);
                break;
            case 18:
                player.GetComponent<Character>().AddStatus(Status.status.strength,1);
                break;
            default:
                break;
        }
    }
    // void Use_Relic_After_Action(int id)
    // {
    //     switch (id)
    //     {
    //         case 5:
    //             break;
    //         case 22:
    //             break;
    //         default:
    //             break;
    //     }
    // }
}
