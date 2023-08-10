using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Implement : MonoBehaviour
{
    [SerializeField] List<int> normal_equipment_list = new List<int>();
    int use_2 = -1;
    bool infight = false;
    GameObject player;
    GameObject[] enemys;
    int now_charge;
    void Update()
    {
        if(Map_System.now_state == Map_System.map_state.fight)
        {
            if(infight==false)
            {
                infight = true;
            }
        }
        else 
        {
            if(infight == true)
            {
                infight = false;
                if(use_2!=-1)
                {
                    player.GetComponent<Character>().Heal(use_2);
                    use_2 = -1;
                }
            }
        }
    }

    public void Use_Equipment(int id,int charge)
    {
        now_charge = charge;
        player = GameObject.FindGameObjectWithTag("Player");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        switch (id)
        {
            case 1:
                //待做
                break;
            case 2:
                int demage = (int)(Global.player_max_hp * 0.05f);
                player.GetComponent<Character>().LoseHP(demage);
                player.GetComponent<Character>().AddArmor(demage * 2);
                use_2 = demage;
                break;
            case 3:
                player.GetComponent<Character>().AddArmor(5);
                break;
            case 4:
                player.GetComponent<Character>().AddStatus(Status.status.fire_enchantment,1);
                break;
            case 5:
                player.GetComponent<Character>().AddStatus(Status.status.strength,1);
                List<Card> deck_5 = Deck.GetDeck();
                List<Card> attack = new List<Card>();
                for(int i=0;i<deck_5.Count;i++)
                {
                    if(deck_5[i].cost_original==1&&deck_5[i].type == Card.Type.attack)
                    {
                        attack.Add(deck_5[i]);
                    }
                }
                Global.ShowPlayerCards(attack,callback_5,true);
                break;
            case 6:
                FindObjectOfType<BattleController>().SelectEnemy(callback_6);
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
                FindObjectOfType<BattleController>().SelectEnemy(callback_12);
                break;
            case 13:
                List<(Status.status _status,int level)> status = player.GetComponent<Character>().GetAllStatus();
                List<(Status.status _status,int level)> tmp = new List<(Status.status, int level)>();
                foreach (var item in status)
                {
                    if(item._status >= Status.status.burn && item._status <= Status.status.compress)
                    {
                        tmp.Add(item);
                    }
                }
                int random_13 = Random.Range(0,tmp.Count);
                player.GetComponent<Character>().AddStatus(tmp[random_13]._status,-tmp[random_13].level);
                break;
            case 14:
                player.GetComponent<Character>().AddStatus(Status.status.temporary_strength,1);
                break;
            case 15:
                FindObjectOfType<BattleController>().SelectEnemy(callback_15);
                break;
            case 16:
                player.GetComponent<Character>().AddArmor(4);
                break;
            case 17:
                Cost.ChangeCost(1);
                break;
            case 18:
                player.GetComponent<Character>().AddStatus(Status.status.dexterity,3);
                break;
            case 19:
                Deck deck = FindObjectOfType<Deck>();
                List<GameObject> handcard_19 = Deck.GetHand();
                List<GameObject> handcard_19_copy = new List<GameObject>();
                foreach (var item in handcard_19)
                {
                    handcard_19_copy.Add(item);
                }
                foreach (var item in handcard_19_copy)
                {
                    deck.Discard(item);
                }
                Deck.Draw(7);
                break;
            case 20:
                FindObjectOfType<BattleController>().SelectEnemy(callback_20);
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
                player.GetComponent<Character>().AddStatus(Status.status.strength,-s);
                player.GetComponent<Character>().AddStatus(Status.status.temporary_strength,3*s);
                break;
            case 23:
                Deck deck_23 = FindObjectOfType<Deck>();
                List<GameObject> handcard_23 = Deck.GetHand();
                deck_23.RemoveCard(handcard_23[Random.Range(0,handcard_23.Count)]);
                break;
            case 24:
                Deck deck_24 = FindObjectOfType<Deck>();
                List<GameObject> handcard_24 = Deck.GetHand();
                List<GameObject> handcard_24_copy = new List<GameObject>();
                int num = handcard_24_copy.Count;
                foreach (var item in handcard_24)
                {
                    handcard_24_copy.Add(item);
                }
                foreach (var item in handcard_24_copy)
                {
                    deck_24.Discard(item);
                }
                Global.AddSan(num*2);
                break;
            case 25:
                Global.AddSan(-10);
                foreach (var enemy in enemys)
                {
                    enemy.GetComponent<Character>().GetHit(10);
                }
                break;
            case 26:
                player.GetComponent<Character>().AddStatus(Status.status.strength,1);
                break;
            case 27:
                player.GetComponent<Character>().AddStatus(Status.status.dexterity,1);
                break;
            case 28:
                //給敵人狀態
                foreach (var enemy in enemys)
                {
                    enemy.GetComponent<Character>().AddStatus(Status.status.burn,13);
                }
                List<int> equipment_list_28 = GetComponent<Bag_System>().Return_All_Equipment();
                if(equipment_list_28.Contains(29))
                {
                    foreach (var enemy in enemys)
                    {
                        enemy.GetComponent<Character>().AddStatus(Status.status.weak,1);
                    }
                }
                break;
            case 29:
                foreach (var enemy in enemys)
                {
                    List<(Status.status,int)> allstatus = enemy.GetComponent<Character>().GetAllStatus();
                    for(int i=0;i<allstatus.Count;i++)
                    {
                        if(allstatus[i].Item1 < Status.status.compress && allstatus[i].Item1 >= Status.status.burn)
                        {
                            player.GetComponent<Character>().AddArmor(4);
                        }
                    }
                }
                break;
            case 30:
                Deck.Draw(2);
                break;
            default:
                break;
        }
    }

    public void callback_5(Card card)
    {
        card.exhaust = true;
    }
    public void callback_6(GameObject enemy)
    {
        enemy.GetComponent<Character>().GetHit(40);
    }
    public void callback_12(GameObject enemy)
    {
        enemy.GetComponent<Character>().AddStatus(Status.status.burn,5);
        enemy.GetComponent<Character>().TriggerBurn(false);
    }
    public void callback_15(GameObject enemy)
    {
        enemy.GetComponent<Character>().LoseHP(11);
    }
    public void callback_20(GameObject enemy)
    {
        enemy.GetComponent<Character>().LoseHP(now_charge);
    }
}
