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
                EffectEnd();
                break;
            case 6:
                player_character.AddStatus(Status.status.fortify, card_info.Args[0]);
                EffectEnd();
                break;
            case 7:
                player_character.AddArmor(BattleController.ComputeArmor(card_info.Args[0]));
                deck.Draw();
                battleController.SelectCard(1, true, false);
                break;
            case 9:
                battleController.SelectEnemy();
                break;
            case 10:
                player_character.AddStatus(Status.status.fast_hand, card_info.Args[0]);
                EffectEnd();
                break;
            case 11:
                player_character.AddStatus(Status.status.second_weapon, 1);
                EffectEnd();
                break;
            case 12:
                battleController.SelectEnemy();
                break;
            case 13:
                battleController.SelectEnemy();
                break;
            case 14:
                player_character.AddArmor(BattleController.ComputeArmor(card_info.Args[0]));
                if (!battleController.PlayedCardThisTurn()){
                    Cost.ChangeCost(1);
                    deck.Draw(card_info.Args[0]);
                }
                EffectEnd();
                break;
            case 15:
                Cost.ChangeCost(battleController.GetCurrentTurn());
                EffectEnd();
                break;
            case 16:
                battleController.SelectEnemy();
                break;
            case 17:
                player_character.AddStatus(Status.status.counter, card_info.Args[0]);
                EffectEnd();
                break;
            case 18:
                battleController.SelectEnemy();
                break;
            case 19:
                t1 = player_character.GetArmor();
                t1 += player_character.GetBlock();
                player_character.AddArmor(BattleController.ComputeArmor(card_info.Args[0]));
                if (t1 == 0) player_character.AddBlock(BattleController.ComputeArmor(card_info.Args[1]));
                EffectEnd();
                break;
            case 20:
                Cost.ChangeCost(Cost.GetCost() - card_saved.GetComponent<CardDisplay>().thisCard.cost);
                EffectEnd();
                break;
            case 21:
                player_character.AddStatus(Status.status.temporary_strength, 2);
                if (card_info.upgraded) player_character.AddStatus(Status.status.temporary_dexterity, 2);
                EffectEnd();
                break;
            case 22:
                player_character.Attack(battleController.ReturnRandomEnemy(), card_info.Args[0]);
                EffectEnd();
                break;
            case 23:
                battleController.SelectEnemy();
                break;
            case 24:
                for (int i = 0; i < 2; i++){
                    foreach(GameObject enemy in battleController.GetAllEnemy()){
                        player_character.Attack(enemy, card_info.Args[0]);
                    }
                }
                EffectEnd();
                break;
            case 25:
                battleController.SelectCard(1, true, true);
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
                EffectEnd();
                break;
            case 3:
                enemy_character.TriggerBurn(false);
                EffectEnd();
                break;
            case 9:
                player_character.Attack(enemy, card_info.Args[0]);
                if (battleController.PlayedAttackAndSkillThisTurn()){
                    Cost.ChangeCost(1);
                    deck.Draw();
                }
                EffectEnd();
                break;
            case 12:
                player_character.Attack(enemy, card_info.Args[0]);
                player_character.AddStatus(Status.status.bounce_back, 1);
                EffectEnd();
                break;
            case 13:
                player_character.Attack(enemy, card_info.Args[0]);
                player_character.Attack(enemy, card_info.Args[0]);
                EffectEnd();
                break;
            case 16:
                player_character.Attack(enemy, card_info.Args[0]);
                if (battleController.TookDmgLastTurn()){
                    Cost.ChangeCost(1);
                    deck.Draw();
                }
                EffectEnd();
                break;
            case 18:
                player_character.Attack(enemy, card_info.Args[0]);
                battleController.SelectCard(3, false, false);
                break;
            case 23:
                player_character.Attack(enemy, card_info.Args[0]);
                player_character.AddBlock(BattleController.ComputeArmor(card_info.Args[1]));
                if (card_info.upgraded) player_character.AddArmor(BattleController.ComputeArmor(2));
                Cost.ChangeCost(1);
                EffectEnd();
                break;
            case 57:
                enemy_character.AddStatus(Status.status.burn, card_info.Args[0]);
                EffectEnd();
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
                EffectEnd();
                break;
            case 4:
                cards[0].GetComponent<CardDisplay>().thisCard.keep = true;
                cards[0].GetComponent<CardState>().Unselect();
                deck.UpdateHand();
                EffectEnd();
                break;
            case 7:
                deck.Discard(cards[0]);
                EffectEnd();
                break;
            case 18:
                for (int i = cards.Count - 1; i >= 0; i--){
                    cards[i].GetComponent<CardDisplay>().thisCard.keepBeforeUse = true;
                    cards[i].GetComponent<CardState>().Unselect();
                }
                deck.UpdateHand();
                EffectEnd();
                break;
            case 25:
                Card tmp = cards[0].GetComponent<CardDisplay>().thisCard;
                deck.RemoveCard(cards[0]);
                deck.AddCardToHand(tmp);
                EffectEnd();
                break;
            default:
                Debug.Log("CardEffects CardSelected(): Unknown id " + id.ToString());
                break;
        }
    }

    static void EffectEnd(){
        BattleController battleController = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        Deck deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        if (card_saved.GetComponent<CardDisplay>().thisCard.type == Card.Type.attack) battleController.PlayedAttack();
        if (card_saved.GetComponent<CardDisplay>().thisCard.type == Card.Type.skill) battleController.PlayedSkill();
        battleController.PlayedCard();
        deck.CardUsed(card_saved);
        Cost.ChangeCost(-card_saved.GetComponent<CardDisplay>().thisCard.cost);
    }
}
