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
            case 8:
                battleController.SelectEnemy();
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
                    deck.Draw(card_info.Args[1]);
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
            case 26:
                player_character.AddStatus(Status.status.lock_on_prepare, 1);
                EffectEnd();
                break;
            case 27:
                foreach(GameObject enemy in battleController.GetAllEnemy()){
                    player_character.Attack(enemy, card_info.Args[0]);
                    enemy.GetComponent<Character>().AddStatus(Status.status.burn, card_info.Args[1]);
                }
                EffectEnd();
                break;
            case 28:
                List<GameObject> cardsDrawn = deck.Draw(card_info.Args[0]);
                if (cardsDrawn.Count > 0) deck.Discard(cardsDrawn[Random.Range(0, cardsDrawn.Count)]);
                EffectEnd();
                break;
            case 29:
                List<Card> list = new List<Card>();
                Card tmp1 = new Card();
                tmp1.rarity = Card.Rarity.common;
                tmp1.cardName = "";
                tmp1.description = new List<string>();
                tmp1.Args = new List<int>();
                tmp1.cost_original = 0;
                list.Add(tmp1);
                Card tmp2 = Card.Copy(tmp1);
                tmp2.cost_original = 1;
                list.Add(tmp2);
                Card tmp3 = Card.Copy(tmp2);
                tmp3.cost_original = 2;
                list.Add(tmp3);
                Card tmp4 = Card.Copy(tmp3);
                tmp4.cost_original = 3;
                list.Add(tmp4);

                Global.SelectCardsFrom(GameObject.FindGameObjectWithTag("Player").transform.parent.parent, list, Callback_29, true);
                break;
            case 30:
                battleController.SelectEnemy();
                break;
            case 31:
                player_character.AddBlock(card_info.Args[0]);
                player_character.AddStatus(Status.status.fire_armor, card_info.Args[1]);
                EffectEnd();
                break;
            case 32:
                player_character.AddStatus(Status.status.fire_enchantment, card_info.Args[0]);
                EffectEnd();
                break;
            case 34:
                battleController.SelectEnemy();
                break;
            case 35:
                battleController.SelectEnemy();
                break;
            case 36:
                battleController.SelectEnemy();
                break;
            case 37:
                player_character.AddStatus(Status.status.doppelganger, 3);
                EffectEnd();
                break;
            case 38:
                player_character.AddArmor(card_info.Args[0]);
                // not finished
                EffectEnd();
                break;
            case 40:
                foreach(GameObject enemy in battleController.GetAllEnemy()){
                    enemy.GetComponent<Character>().AddStatus(Status.status.burn, card_info.Args[0]);
                    int tmp = enemy.GetComponent<Character>().GetStatus(Status.status.burn);
                    if (tmp >= card_info.Args[1]){
                        enemy.GetComponent<Character>().AddStatus(Status.status.burn, -tmp);
                        enemy.GetComponent<Character>().LoseHP(tmp * 3);
                    }
                }
                EffectEnd();
                break;
            case 41:
                List<GameObject> drawnCards = deck.Draw(3);
                int costSaved = -9;
                bool success = true;
                foreach(GameObject cardObj in drawnCards){
                    if (costSaved == -9) costSaved = cardObj.GetComponent<CardDisplay>().thisCard.cost;
                    else if (cardObj.GetComponent<CardDisplay>().thisCard.cost != costSaved){
                        success = false;
                        break;
                    }
                }
                if (success)
                    foreach(GameObject enemy in battleController.GetAllEnemy()){
                        player_character.Attack(enemy, card_info.Args[0]);
                    }
                EffectEnd();
                break;
            case 42:
                battleController.SelectEnemy();
                break;
            case 43:
                battleController.SelectEnemy();
                break;
            case 44:
                battleController.SelectEnemy();
                break;
            case 45:
                List<Card> list45 = new List<Card>();
                list45.Add(deck.GetCard(43));
                list45.Add(deck.GetCard(44));
                Global.SelectCardsFrom(GameObject.FindGameObjectWithTag("Player").transform.parent.parent, list45, Callback_45, true);
                break;
            case 46:
                foreach(GameObject enemy in battleController.GetAllEnemy()){
                    player_character.Attack(enemy, card_info.Args[0]);
                }
                if (!card_info.once_used){
                    GameObject tmp = deck.AddCardToHand(deck.GetCard(58));
                    if (card_info.upgraded) tmp.GetComponent<CardDisplay>().thisCard.upgraded = true;
                    tmp.GetComponent<CardDisplay>().thisCard.exhaust = true;
                    card_info.once_used = true;
                }
                if (battleController.Played46ThisTurn()){
                    foreach(GameObject enemy in battleController.GetAllEnemy()){
                        player_character.Attack(enemy, card_info.Args[0]);
                    }
                }
                EffectEnd();
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
            case 8:
                t1 = enemy.GetComponent<EnemyMove>().GetIntentionType();
                if (t1 == 1) deck.AddCardToHand(deck.GetRandomSkillCard());
                else deck.AddCardToHand(deck.GetRandomAttackCard());
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
            case 30:
                player_character.Attack(enemy, card_info.Args[0]);
                deck.Draw();
                battleController.SelectCard(1, true, false);
                break;
            case 34:
                player_character.Attack(enemy, card_info.Args[0]);
                EffectEnd();
                break;
            case 35:
                player_character.Attack(enemy, card_info.Args[0]);
                player_character.Heal( (int)(player_character.GetMaxHP() * card_info.Args[1] * 0.01f) );
                EffectEnd();
                break;
            case 36:
                if (card_saved == deck.GetHand()[0]) player_character.Attack(enemy, card_info.Args[1]);
                else player_character.Attack(enemy, card_info.Args[0]);
                EffectEnd();
                break;
            case 42:
                for (int i = 0; i < card_info.Args[1]; i++) player_character.Attack(enemy, card_info.Args[0]);
                EffectEnd();
                break;
            case 43:
                player_character.Attack(enemy, card_info.Args[0]);
                enemy_character.AddStatus(Status.status.weak, card_info.Args[1]);
                if (battleController.GetLastCardPlayed().cost == 0) Cost.ChangeCost(1);
                EffectEnd();
                break;
            case 44:
                player_character.Attack(enemy, card_info.Args[0]);
                enemy_character.AddStatus(Status.status.vulnerable, card_info.Args[1]);
                if (battleController.GetLastCardPlayed().cost == 0) Cost.ChangeCost(1);
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
                // not finished
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
            case 30:
                deck.MoveFromHandToDrawPile(cards[0]);
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
        battleController.PlayedCard(card_saved.GetComponent<CardDisplay>().thisCard);
        deck.CardUsed(card_saved);
        Cost.ChangeCost(-card_saved.GetComponent<CardDisplay>().thisCard.cost);
    }

    static void Callback_29(int n){
        Deck deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        // Debug.Log("Callback_29, selected number " + n.ToString());
        GameObject tmp = deck.Draw();
        if (!card_saved.GetComponent<CardDisplay>().thisCard.once_used && n == tmp.GetComponent<CardDisplay>().thisCard.cost){
            Cost.ChangeCost(1);
            deck.Draw();
            card_saved.GetComponent<CardDisplay>().thisCard.once_used = true;
        }
        EffectEnd();
    }

    static void Callback_45(int n){
        Deck deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        GameObject tmp = null;
        if (n == 0) tmp = deck.AddCardToHand(deck.GetCard(43));
        if (n == 1) tmp = deck.AddCardToHand(deck.GetCard(44));
        if (tmp){
            tmp.GetComponent<CardDisplay>().thisCard.exhaust = true;
            tmp.GetComponent<CardDisplay>().thisCard.disappear = true;
        }
        EffectEnd();
    }
}
