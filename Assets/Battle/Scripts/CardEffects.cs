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
        BattleEffects effects = GameObject.FindGameObjectWithTag("BattleEffects").GetComponent<BattleEffects>();
        
        if (BattleController.GetBattleID() == 312 && card_saved.GetComponent<CardDisplay>().GetCost() >= 3){
            Cost.ChangeCost(-card_saved.GetComponent<CardDisplay>().GetCost());
            deck.RemoveCard(card_saved);
            return;
        }

        GameObject card207 = deck.Card207InHand();
        if (card207 != null && card_info.type == Card.Type.attack){
            Cost.ChangeCost(-card_saved.GetComponent<CardDisplay>().GetCost());
            deck.SpecialDiscard(card);
            deck.RemoveCard(card207);
            return;
        }

        switch(id){
            case 1:
                battleController.SelectCard(CardSelected, 1, true, true);
                break;
            case 2:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 3:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 4:
                Deck.Draw(card_info.Args[0]);
                battleController.SelectCard(CardSelected, 1, true, false);
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
                player_character.AddArmor(card_info.Args[0]);
                Deck.Draw();
                battleController.SelectCard(CardSelected, 1, true, false);
                break;
            case 8:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 9:
                battleController.SelectEnemy(EnemySelected);
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
                battleController.SelectEnemy(EnemySelected);
                break;
            case 13:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 14:
                player_character.AddArmor(card_info.Args[0]);
                if (!BattleController.PlayedCardThisTurn()){
                    Cost.ChangeCost(1);
                    Deck.Draw(card_info.Args[1]);
                }
                EffectEnd();
                break;
            case 15:
                Cost.ChangeCost(BattleController.GetCurrentTurn());
                EffectEnd();
                break;
            case 16:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 17:
                player_character.AddStatus(Status.status.counter, card_info.Args[0]);
                EffectEnd();
                break;
            case 18:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 19:
                t1 = player_character.GetArmor();
                t1 += player_character.GetBlock();
                player_character.AddArmor(card_info.Args[0]);
                if (t1 == 0) player_character.AddBlock(card_info.Args[1]);
                EffectEnd();
                break;
            case 20:
                Cost.ChangeCost(Cost.GetCost() - card_saved.GetComponent<CardDisplay>().thisCard.cost);
                EffectEnd();
                break;
            case 21:
                player_character.AddStatus(Status.status.temporary_strength, card_info.Args[0]);
                player_character.AddStatus(Status.status.temporary_dexterity, card_info.Args[1]);
                EffectEnd();
                break;
            case 22:
                player_character.Attack(BattleController.GetRandomEnemy(), card_info.Args[0]);
                EffectEnd();
                break;
            case 23:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 24:
                for (int i = 0; i < 2; i++){
                    foreach(GameObject enemy in BattleController.GetAllEnemy()){
                        player_character.Attack(enemy, card_info.Args[0]);
                    }
                }
                EffectEnd();
                break;
            case 25:
                battleController.SelectCard(CardSelected, 1, true, true);
                break;
            case 26:
                player_character.AddStatus(Status.status.lock_on_prepare, 1);
                EffectEnd();
                break;
            case 27:
                Debug.Log("call effects by card 27, " + effects.GetInstanceID());
                effects.Play(null, "sting_AOE");
                foreach(GameObject enemy in BattleController.GetAllEnemy()){
                    player_character.Attack(enemy, card_info.Args[0]);
                    enemy.GetComponent<Character>().AddStatus(Status.status.burn, card_info.Args[1]);
                    if (Global.Check_Relic_In_Bag(24)) enemy.GetComponent<Character>().AddStatus(Status.status.burn, 5);
                }
                EffectEnd();
                break;
            case 28:
                List<GameObject> cardsDrawn = Deck.Draw(card_info.Args[0]);
                if (cardsDrawn.Count > 0) deck.Discard(cardsDrawn[Random.Range(0, cardsDrawn.Count)]);
                EffectEnd();
                break;
            case 29:
                List<Card> list = new List<Card>();
                Card tmp1 = new Card();
                tmp1.rarity = Card.Rarity.common;
                tmp1.cardName = "";
                tmp1.description = new List<string>();
                tmp1.description.Add("消費為0");
                tmp1.Args = new List<int>();
                tmp1.cost = 0;
                list.Add(tmp1);

                Card tmp2 = Card.Copy(tmp1);
                tmp2.description[0] = "消費為1";
                tmp2.cost = 1;
                list.Add(tmp2);

                Card tmp3 = Card.Copy(tmp2);
                tmp3.description[0] = "消費為2";
                tmp3.cost = 2;
                list.Add(tmp3);

                Card tmp4 = Card.Copy(tmp3);
                tmp4.description[0] = "消費為3";
                tmp4.cost = 3;
                list.Add(tmp4);

                Global.SelectCardsFrom(GameObject.FindGameObjectWithTag("Player").transform.parent.parent, list, Callback_29, true);
                break;
            case 30:
                battleController.SelectEnemy(EnemySelected);
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
            case 33:
                GameObject card33 = Deck.GetHighestCostCardInHand();
                if (card33){
                    int cost33 = card33.GetComponent<CardDisplay>().GetCost();
                    deck.RemoveCard(card33);
                    Deck.Draw(cost33);
                }
                EffectEnd();
                break;
            case 34:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 35:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 36:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 37:
                player_character.AddStatus(Status.status.doppelganger, 3);
                EffectEnd();
                break;
            case 38:
                player_character.AddArmor(card_info.Args[0]);
                if (Deck.GetDeck().Count > 0) Global.ShowPlayerCards(Deck.GetDeck(), Callback_38, true);
                else EffectEnd();
                break;
            case 39:
                player_character.AddArmor(card_info.Args[0]);
                if (!BattleController.PlayedCardThisTurn()) player_character.AddBlock(card_info.Args[1]);
                EffectEnd();
                break;
            case 40:
                foreach(GameObject enemy in BattleController.GetAllEnemy()){
                    enemy.GetComponent<Character>().AddStatus(Status.status.burn, card_info.Args[0]);
                    if (Global.Check_Relic_In_Bag(24)) enemy.GetComponent<Character>().AddStatus(Status.status.burn, 5);
                    int tmp = enemy.GetComponent<Character>().GetStatus(Status.status.burn);
                    if (tmp >= card_info.Args[1]){
                        enemy.GetComponent<Character>().AddStatus(Status.status.burn, -tmp);
                        enemy.GetComponent<Character>().LoseHP(tmp * 3);
                    }
                }
                EffectEnd();
                break;
            case 41:
                List<GameObject> drawnCards = Deck.Draw(3);
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
                    foreach(GameObject enemy in BattleController.GetAllEnemy()){
                        player_character.Attack(enemy, card_info.Args[0]);
                    }
                EffectEnd();
                break;
            case 42:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 43:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 44:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 45:
                List<Card> list45 = new List<Card>();
                list45.Add(AllCards.GetCard(43));
                list45.Add(AllCards.GetCard(44));
                Global.SelectCardsFrom(GameObject.FindGameObjectWithTag("Player").transform.parent.parent, list45, Callback_45, true);
                break;
            case 46:
                GameObject obj46 = GameObject.Find("Battle effects handler");
                obj46.GetComponent<CardEffects>().Effect_46(card_info);
                // foreach(GameObject enemy in battleController.GetAllEnemy()){
                //     player_character.Attack(enemy, card_info.Args[0]);
                // }
                // if (!card_info.once_used){
                //     GameObject tmp = deck.AddCardToHand(deck.GetCard(58));
                //     if (card_info.upgraded) tmp.GetComponent<CardDisplay>().thisCard.upgraded = true;
                //     tmp.GetComponent<CardDisplay>().thisCard.exhaust = true;
                //     card_info.once_used = true;
                // }
                // if (battleController.Played46ThisTurn()){
                //     foreach(GameObject enemy in battleController.GetAllEnemy()){
                //         player_character.Attack(enemy, card_info.Args[0]);
                //     }
                // }
                // EffectEnd();
                break;
            case 47:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 48:
                player_character.AddStatus(Status.status.imitate, 1);
                EffectEnd();
                break;
            case 49:
                player_character.AddStatus(Status.status.remnant, 1);
                EffectEnd();
                break;
            case 50:
                player_character.AddStatus(Status.status.turtle_stance, 5);
                EffectEnd();
                break;
            case 51:
                player_character.AddStatus(Status.status.dragon_stance, 5);
                EffectEnd();
                break;
            case 52:
                player_character.AddBlock(card_info.Args[0]);
                if (player_character.GetStatus(Status.status.temporary_dexterity) == 0) player_character.AddStatus(Status.status.temporary_dexterity, 2);
                else player_character.AddStatus(Status.status.dexterity, 1);
                EffectEnd();
                break;
            case 53:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 54:
                battleController.SelectCard(CardSelected, 1, true, true);
                break;
            case 55:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 56:
                player_character.AddArmor(card_info.Args[0]);
                if (card_info.upgraded) player_character.AddBlock(card_info.Args[1]);
                Global.AddSan( (int)(Global.max_sanity * 0.07f) );
                battleController.EndTurn();
                EffectEnd();
                break;
            case 57:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 58:
                List<Card> cardList58 = new();
                foreach(Card _card in Deck.GetTrash())
                    if (_card.type == Card.Type.attack &&_card.cost == 1) cardList58.Add(_card);
                if (cardList58.Count != 0){
                    Global.ShowPlayerCards(cardList58, Callback_58, true);
                }
                else EffectEnd();
                break;
            case 59:
                battleController.SelectCard(CardSelected, card_info.Args[0], true, true);
                break;
            case 60:  
                battleController.SelectEnemy(EnemySelected);
                break;

            case 101:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 102:
                player_character.AddArmor(card_info.Args[0]);
                EffectEnd();
                break;
            case 103:
                player_character.AddBlock(card_info.Args[0]);
                EffectEnd();
                break;

            case 203:
                EffectEnd();
                break;
            case 208:
                player_character.LoseHP(4);
                EffectEnd();
                break;
            case 209:
                player_character.GetHit(20);
                card_info.exhaust = true;
                EffectEnd();
                break;
            case 210:
                Global.AddSan(-10);
                card_info.exhaust = true;
                EffectEnd();
                break;
            case 211:
                Global.AddSan(4);
                battleController.SelectCard(CardSelected, 1, false, false);
                break;

            case 901:
                battleController.SelectCard(CardSelected, 8, false, true);
                break;
            case 902:
                battleController.SelectEnemy(EnemySelected);
                break;
            case 903:
                Cost.ChangeCost(90);
                Deck.Draw(99);
                EffectEnd();
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
        BattleEffects effects = GameObject.FindGameObjectWithTag("BattleEffects").GetComponent<BattleEffects>();

        int id = card_saved.GetComponent<CardDisplay>().thisCard.id;
        Card card_info = card_saved.GetComponent<CardDisplay>().thisCard;

        switch(id){
            case 2:
                int tmp2 = enemy_character.GetStatus(Status.status.burn);
                enemy_character.AddStatus(Status.status.burn, tmp2);
                if (Global.Check_Relic_In_Bag(24)) enemy_character.AddStatus(Status.status.burn, 5);
                EffectEnd();
                break;
            case 3:
                enemy_character.TriggerBurn(false);
                if (Global.Check_Relic_In_Bag(24)) enemy_character.AddStatus(Status.status.burn, 5);
                EffectEnd();
                break;
            case 8:
                int tmp3 = enemy.GetComponent<EnemyMove>().GetIntentionType();
                if (tmp3 == 1) deck.AddCardToHand(AllCards.GetRandomSkillCard());
                else deck.AddCardToHand(deck.GetRandomAttackCard());
                EffectEnd();
                break;
            case 9:
                effects.Play(enemy, "slash1");
                player_character.Attack(enemy, card_info.Args[0]);
                if (battleController.PlayedAttackAndSkillThisTurn()){
                    Cost.ChangeCost(1);
                    Deck.Draw();
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
                    Deck.Draw();
                }
                EffectEnd();
                break;
            case 18:
                player_character.Attack(enemy, card_info.Args[0]);
                battleController.SelectCard(CardSelected, 3, false, false);
                break;
            case 23:
                player_character.Attack(enemy, card_info.Args[0]);
                player_character.AddBlock(card_info.Args[1]);
                player_character.AddArmor(card_info.Args[2]);
                Cost.ChangeCost(1);
                EffectEnd();
                break;
            case 30:
                player_character.Attack(enemy, card_info.Args[0]);
                Deck.Draw();
                battleController.SelectCard(CardSelected, 1, true, false);
                break;
            case 34:
                player_character.Attack(enemy, card_info.Args[0]);
                card_info.Args[0] = 9;
                EffectEnd();
                break;
            case 35:
                player_character.Attack(enemy, card_info.Args[0]);
                player_character.Heal( (int)(player_character.GetMaxHP() * card_info.Args[1] * 0.01f) );
                EffectEnd();
                break;
            case 36:
                if (card_saved == Deck.GetHand()[0]) player_character.Attack(enemy, card_info.Args[1]);
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
                if (battleController.GetLastCardPlayed() != null && battleController.GetLastCardPlayed().cost == 0) Cost.ChangeCost(1);
                EffectEnd();
                break;
            case 44:
                player_character.Attack(enemy, card_info.Args[0]);
                enemy_character.AddStatus(Status.status.vulnerable, card_info.Args[1]);
                if (battleController.GetLastCardPlayed() != null && battleController.GetLastCardPlayed().cost == 0) Cost.ChangeCost(1);
                EffectEnd();
                break;
            case 47:
                if (card_info.upgraded) player_character.Attack(enemy, card_info.Args[0], 3, 5);
                else player_character.Attack(enemy, card_info.Args[0], 3, 3);
                if (!battleController.PlayedAttackThisTurn()) Cost.ChangeCost(1);
                EffectEnd();
                break;
            case 53:
                player_character.Attack(enemy, card_info.Args[0]);
                if (player_character.GetStatus(Status.status.temporary_strength) == 0) player_character.AddStatus(Status.status.temporary_strength, 2);
                else player_character.AddStatus(Status.status.strength, 1);
                EffectEnd();
                break;
            case 55:
                bool alive55 = player_character.Attack(enemy, card_info.Args[0]);
                if (!alive55 && enemy_character.GetStatus(Status.status.summoned) == 0) Global.AddSan( (int)(Global.max_sanity * card_info.Args[1] * 0.01f) );
                EffectEnd();
                break;
            case 57:
                enemy_character.AddStatus(Status.status.burn, card_info.Args[0]);
                if (Global.Check_Relic_In_Bag(24)) enemy.GetComponent<Character>().AddStatus(Status.status.burn, 5);
                EffectEnd();
                break;
            case 59:
                player_character.Attack(enemy, card_info.Args[1]);
                player_character.Attack(enemy, card_info.Args[1]);
                player_character.Attack(enemy, card_info.Args[1]);
                EffectEnd();
                break;
            case 60:
                player_character.Attack(enemy, card_info.Args[0]);
                if (enemy_character.GetArmor() + enemy_character.GetBlock() == 0) enemy_character.AddStatus(Status.status.vulnerable, card_info.Args[1]);
                EffectEnd();
                break;
            case 101:
                effects.Play(enemy, "slash1");
                player_character.Attack(enemy, card_info.Args[0]);
                EffectEnd();
                break;

            case 902:
                effects.Play(enemy, "slash1");
                player_character.Attack(enemy, 999);
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
        int id = card_saved.GetComponent<CardDisplay>().thisCard.id;
        Card card_info = card_saved.GetComponent<CardDisplay>().thisCard;
        switch(id){
            case 1:
                deck.Discard(cards[0]);
                player_character.AddArmor(card_info.Args[0]);
                List<Card> cardList1 = new();
                foreach(Card card in Deck.GetDeck())
                    if (card.cost == 0) cardList1.Add(card);
                if (cardList1.Count != 0){
                    foreach(Card card in cardList1) Debug.Log(card.cardName + " is in list");
                    Global.ShowPlayerCards(cardList1, Callback_1, true);
                }
                else EffectEnd();
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
            case 54:
                deck.Discard(cards[0]);
                player_character.AddArmor(card_info.Args[0]);
                player_character.AddBlock(card_info.Args[1]);
                EffectEnd();
                break;
            case 59:
                foreach(GameObject card in cards) deck.Discard(card);
                battleController.SelectEnemy(EnemySelected);
                break;

            case 211:
                if (cards.Count > 0) deck.RemoveCard(cards[0]);
                EffectEnd();
                break;

            case 901:
                foreach(GameObject card in cards){
                    Global.UpgradeCard(card);
                }
                deck.ResetHand(false);
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
        Character player_character = GameObject.FindWithTag("Player").GetComponent<Character>();

        Cost.ChangeCost(-card_saved.GetComponent<CardDisplay>().GetCost());
        card_saved.GetComponent<CardDisplay>().thisCard.cost_change_before_play = 0;
        card_saved.GetComponent<CardDisplay>().thisCard.keepBeforeUse = false;

        if (player_character.GetStatus(Status.status.doppelganger) > 0 &&
            !card_saved.GetComponent<CardDisplay>().thisCard.produced_by_doppelganger &&
            card_saved.GetComponent<CardDisplay>().thisCard.id != 37){

            Card tmpCard = deck.AddCardToHand(card_saved.GetComponent<CardDisplay>().thisCard).GetComponent<CardDisplay>().thisCard;
            Card.SetCost(tmpCard, 0);
            tmpCard.exhaust = true;
            tmpCard.disappear = true;

            int idx = 0;
            foreach(string s in tmpCard.description){
                if (s == "#A" || s == "#D") tmpCard.Args[idx] /= 2;
                if (s == "#A" || s == "#D" || s == "#O") idx++;
            }

            tmpCard.produced_by_doppelganger = true;
            player_character.AddStatus(Status.status.doppelganger, -1);
        }

        if (player_character.GetStatus(Status.status.void_sword) > 0){
            card_saved.GetComponent<CardDisplay>().thisCard.exhaust = true;
            player_character.AddStatus(Status.status.void_sword, -1);
        }

        Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.UseEnegy, card_saved.GetComponent<CardDisplay>().GetCost());

        deck.CardUsed(card_saved);
        battleController.PlayedCard(card_saved.GetComponent<CardDisplay>().thisCard);
    }



    static void Callback_1(Card card){
        FindObjectOfType<Deck>().MoveFromDrawPileToHand(card);
        EffectEnd();
    }
    static void Callback_29(int n){
        Deck deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        // Debug.Log("Callback_29, selected number " + n.ToString());
        GameObject tmp = Deck.Draw();
        if (tmp && !card_saved.GetComponent<CardDisplay>().thisCard.once_used && n == tmp.GetComponent<CardDisplay>().thisCard.cost){
            Cost.ChangeCost(1);
            Deck.Draw();
            card_saved.GetComponent<CardDisplay>().thisCard.once_used = true;
        }
        EffectEnd();
    }
    static void Callback_38(Card card){
        GameObject cardObj = FindObjectOfType<Deck>().MoveFromDrawPileToHand(card);
        Card card_info = cardObj.GetComponent<CardDisplay>().thisCard;
        card_info.keep = true;
        if (card_info.cost != -1) card_info.cost += card_saved.GetComponent<CardDisplay>().thisCard.upgraded? 1:2;
        card_info.costDecreaseOnTurnEnd = true;
        EffectEnd();
    }
    static void Callback_45(int n){
        Deck deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        GameObject tmp = null;
        if (n == 0) tmp = deck.AddCardToHand(AllCards.GetCard(43));
        if (n == 1) tmp = deck.AddCardToHand(AllCards.GetCard(44));
        if (tmp){
            tmp.GetComponent<CardDisplay>().thisCard.exhaust = true;
            tmp.GetComponent<CardDisplay>().thisCard.disappear = true;
        }
        EffectEnd();
    }
    static void Callback_58(Card card){
        FindObjectOfType<Deck>().MoveFromTrashToHand(card);
        EffectEnd();
    }


    void Effect_46(Card card_info){
        StartCoroutine(_Effect_46(card_info));
    }
    IEnumerator _Effect_46(Card card_info){
        BattleController battleController = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        Character player_character = battleController.GetPlayer().GetComponent<Character>();
        Deck deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        BattleEffects effects = GameObject.FindGameObjectWithTag("BattleEffects").GetComponent<BattleEffects>();

        battleController.GetPlayer().GetComponent<Animator>().Play("player_attack");
        yield return new WaitForSeconds(0.25f);
        effects.Play(null, "sword wave");
        foreach(GameObject enemy in BattleController.GetAllEnemy()){
            player_character.Attack(enemy, card_info.Args[0]);
        }
        if (!card_info.once_used){
            GameObject tmp = deck.AddCardToHand(AllCards.GetCard(58));
            if (card_info.upgraded) tmp.GetComponent<CardDisplay>().thisCard.upgraded = true;
            tmp.GetComponent<CardDisplay>().thisCard.exhaust = true;
            card_info.once_used = true;
        }
        yield return new WaitForSeconds(0.25f);

        if (battleController.Played46ThisTurn()){
            battleController.GetPlayer().GetComponent<Animator>().Play("player_attack");
            yield return new WaitForSeconds(0.25f);
            effects.Play(null, "sword wave");
            foreach(GameObject enemy in BattleController.GetAllEnemy()){
                player_character.Attack(enemy, card_info.Args[0]);
            }
        }
        EffectEnd();
    }
}
