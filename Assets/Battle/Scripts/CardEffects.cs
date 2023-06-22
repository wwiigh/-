using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffects : MonoBehaviour
{
    static GameObject card_saved;
    public static void Use(GameObject card){
        BattleController battleController = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        Character player_character = battleController.GetPlayer().GetComponent<Character>();
        Deck deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        int t1;
        card_saved = card;
        int id = card_saved.GetComponent<CardDisplay>().thisCard.id;
        Card card_info = card_saved.GetComponent<CardDisplay>().thisCard;
        switch(id){
            case 1:
                battleController.SelectCard(1, true, true);
                break;
            case 2:
                battleController.SelectEnemy();
                break;
            case 3:
                battleController.SelectEnemy();
                break;
            case 4:
                deck.Draw(card_info.Args[0]);
                battleController.SelectCard(1, true, false);
                break;
            case 5:
                player_character.AddStatus(Status.status.prepare, card_info.Args[0]);
                deck.CardUsed(card_saved);
                break;
            case 9:
                battleController.SelectEnemy();
                break;
            case 57:
                battleController.SelectEnemy();
                break;
            default:
                Debug.Log("CardEffects Use(): Unknown id " + id.ToString());
                break;
        }
    }

    public static void EnemySelected(GameObject enemy){
        BattleController battleController = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        Character player_character = battleController.GetPlayer().GetComponent<Character>();
        Character enemy_character = enemy.GetComponent<Character>();
        Deck deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        int t1;
        int id = card_saved.GetComponent<CardDisplay>().thisCard.id;
        Card card_info = card_saved.GetComponent<CardDisplay>().thisCard;
        switch(id){
            case 2:
                t1 = enemy_character.GetStatus(Status.status.burn);
                enemy_character.AddStatus(Status.status.burn, t1);
                deck.CardUsed(card_saved);
                break;
            case 3:
                enemy_character.TriggerBurn(false);
                deck.CardUsed(card_saved);
                break;
            case 9:
                player_character.Attack(enemy, card_info.Args[0]);
                deck.CardUsed(card_saved);
                break;
            case 57:
                enemy_character.AddStatus(Status.status.burn, card_info.Args[0]);
                deck.CardUsed(card_saved);
                break;
            default:
                Debug.Log("CardEffects EnemySelected(): Unknown id " + id.ToString());
                break;
        }
    }

    public static void CardSelected(List<GameObject> cards){
        BattleController battleController = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        Character player_character = battleController.GetPlayer().GetComponent<Character>();
        Deck deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        int t1;
        int id = card_saved.GetComponent<CardDisplay>().thisCard.id;
        Card card_info = card_saved.GetComponent<CardDisplay>().thisCard;
        switch(id){
            case 1:
                player_character.AddArmor(card_info.Args[0]);
                deck.Discard(cards[0]);
                deck.CardUsed(card_saved);
                break;
            case 4:
                cards[0].GetComponent<CardDisplay>().thisCard.keep = true;
                cards[0].GetComponent<CardState>().Unselect();
                deck.UpdateHand();
                deck.CardUsed(card_saved);
                break;
            default:
                Debug.Log("CardEffects CardSelected(): Unknown id " + id.ToString());
                break;
        }
    }
}
