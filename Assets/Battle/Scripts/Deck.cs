using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Deck : MonoBehaviour
{
    BattleController battleController;
    [SerializeField] List<Card> drawPile = new List<Card>();
    [SerializeField] List<Card> trash = new List<Card>();
    [SerializeField] List<GameObject> hand = new List<GameObject>();
    [SerializeField] GameObject card_template;
    TMP_Text drawPileNumber;
    TMP_Text trashNumber;
    int hand_limit = 8;

    private void Start() {
        battleController = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        drawPileNumber = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        trashNumber = transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        // Init();
    }

    public void Init(List<Card> playerDeck){
        for (int i = 0; i < playerDeck.Count; i++){
            drawPile.Add( Card.Copy( playerDeck[i] ) );
        }
        Shuffle(drawPile);
    }

    public void Init(){
        AllCards allcards = Object.FindObjectOfType<AllCards>();
        int totalCards = AllCards.GetCount();
        RemoveChild();
        drawPile.Clear();
        hand.Clear();
        trash.Clear();
        // foreach(Card card in allcards.GetAllCards()){
        //     drawPile.Add( Card.Copy( card ) );
        // }
        // foreach(Card card in allcards.GetBasicCards()){
        //     drawPile.Add( Card.Copy( card ) );
        // }
        // foreach(Card card in allcards.GetSpecialCards()){
        //     drawPile.Add( Card.Copy( card ) );
        // }
        // Shuffle(drawPile);
        foreach(Card card in Global.GetPlayerDeck())
        {
            drawPile.Add( Card.Copy(card) );
        }
        // drawPile.Add( AllCards.GetCard(903) );
        // drawPile.Add( AllCards.GetCard(37) );
        // drawPile.Add( AllCards.GetCard(101) );
        // drawPile.Add( AllCards.GetCard(9) );
        // drawPile.Add( AllCards.GetCard(12) );
        // drawPile.Add( AllCards.GetCard(13) );
    }


    
    bool Enemy312_Effect1 = false;
    bool Enemy312_Effect2 = false;
    bool Enemy312_Effect3 = false;

    static public void Use312Effect(int n){
        Deck deck = FindObjectOfType<Deck>();
        if (n == 1) deck.Enemy312_Effect1 = true;
        if (n == 2) deck.Enemy312_Effect2 = true;
        if (n == 3) deck.Enemy312_Effect3 = true;
    }

    // public void Init(){
    //     List<Card> allcards = allcards_class.GetAllCards();
    //     int totalCards = allcards_class.GetCount();
    //     for (int i = 0; i < 20; i++){
    //         drawPile.Add( Card.Copy( allcards[Random.Range(0, totalCards)] ) );
    //     }
    //     Shuffle(drawPile);
    // }

    void RemoveChild(){
        List<GameObject> list = new List<GameObject>();
        foreach(Transform child in transform){
            if (child.tag == "Card") list.Add(child.gameObject);
        }
        foreach(GameObject obj in list) Destroy(obj);
    }

    public void CardUsed(GameObject card){
        Character player_character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        hand.Remove(card);

        if (Global.Check_Relic_In_Bag(20) && !BattleController.PlayedCardThisTurn()){
            drawPile.Add(card.GetComponent<CardDisplay>().thisCard);
            Shuffle(drawPile);
        }
        else if (player_character.GetStatus(Status.status.bounce_back) > 0 && card.GetComponent<CardDisplay>().thisCard.id != 12){
            player_character.AddStatus(Status.status.bounce_back, -1);
            drawPile.Add(card.GetComponent<CardDisplay>().thisCard);
            Shuffle(drawPile);
        }
        else if (!card.GetComponent<CardDisplay>().thisCard.exhaust) trash.Add(card.GetComponent<CardDisplay>().thisCard);

        if (player_character.GetStatus(Status.status.imitate) > 0 && card.GetComponent<CardDisplay>().thisCard.id != 48){
            AddCardToHand(card.GetComponent<CardDisplay>().thisCard);
            player_character.AddStatus(Status.status.imitate, -1);
        }

        Destroy(card);
        Rearrange();
    }

    // Triggers discard effects
    public void Discard(GameObject card){
        Character player_character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        hand.Remove(card);
        trash.Add(card.GetComponent<CardDisplay>().thisCard);
        Destroy(card);
        Rearrange();
        
        int tmp = player_character.GetStatus(Status.status.second_weapon);
        if (tmp > 0 && !battleController.DiscardedThisTurn())
            Draw(tmp);
        
        tmp = player_character.GetStatus(Status.status.fast_hand);
        if (tmp > 0) BattleController.GetRandomEnemy().GetComponent<Character>().GetHit(tmp);
        
        battleController.Discarded();
    }

    // Does not trigger discard effects
    public void SpecialDiscard(GameObject card){
        hand.Remove(card);
        trash.Add(card.GetComponent<CardDisplay>().thisCard);
        Destroy(card);
        Rearrange();
    }

    public void RemoveCard(GameObject card){
        Card card_info = card.GetComponent<CardDisplay>().thisCard;
        hand.Remove(card);

        if (card_info.id == 22){
            GameObject target = battleController.GetEnemyWithLowestHP();
            Character player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            player.Attack(target, card_info.Args[1]);
        }

        if (card_info.id == 23){
            AddCardToHand(card_info);
        }

        if (card_info.id == 24){
            Character player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            player.AddStatus(Status.status.strength, card_info.Args[1]);
        }

        if (card_info.id == 209){
            BattleController.GetRandomEnemy().GetComponent<Character>().LoseHP(30);
        }

        if (card_info.id == 210){
            AddCardToHand(AllCards.GetCard(211));
        }

        Destroy(card);
        Rearrange();
    }



    public GameObject AddCardToHand(Card card){
        if (hand.Count == hand_limit) return null;
        GameObject tmp = MakeCard(card);
        hand.Add(tmp);
        Rearrange();
        return tmp;
    }
    public Card AddCardToDrawPile(Card card){
        drawPile.Add(card);
        Shuffle(drawPile);
        return card;
    }



    public void MoveFromHandToDrawPile(GameObject card){
        hand.Remove(card);
        drawPile.Insert(0, card.GetComponent<CardDisplay>().thisCard);
        Destroy(card);
        Rearrange();
    }
    public void MoveFromDrawPileToHand(Card card){
        drawPile.Remove(card);
        AddCardToHand(card);
    }
    public void MoveFromTrashToHand(Card card){
        trash.Remove(card);
        AddCardToHand(card);
    }



    public Card GetRandomSkillCard(){
        return AllCards.GetRandomSkillCard();
        // return allcards_obj.GetComponent<AllCards>().GetRandomSkillCard();
    }
    public Card GetRandomAttackCard(){
        return AllCards.GetRandomAttackCard();
        // return allcards_obj.GetComponent<AllCards>().GetRandomAttackCard();
    }



    public void AnEyeForAnEye(int dmgReceived){
        foreach(GameObject card in hand){
            Card card_info = card.GetComponent<CardDisplay>().thisCard;
            if (card_info.id == 34){
                card_info.Args[0] += dmgReceived * card_info.Args[1];
                card.GetComponent<CardDisplay>().LoadCard();
            }
        }
    }

    public GameObject Card207InHand(){
        GameObject cardFound = null;
        foreach(GameObject card in hand){
            if (card.GetComponent<CardDisplay>().thisCard.id == 207){
                cardFound = card;
                break;
            }
        }
        return cardFound;
    }



    static bool firstCardDrawn = false;
    public void TurnStart(){
        firstCardDrawn = false;
        int draw_less_level = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().GetStatus(Status.status.draw_less);
        Draw(5 - draw_less_level);
        if (Global.Check_Relic_In_Bag(10)) Draw();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().AddStatus(Status.status.draw_less, -draw_less_level);

        if (Enemy312_Effect1){
            foreach(GameObject card in hand) card.GetComponent<CardDisplay>().thisCard.cost_change_before_play++;
            Enemy312_Effect1 = false;
        }
        if (Enemy312_Effect2){
            hand[Random.Range(0, hand.Count)].GetComponent<CardDisplay>().thisCard.cost_change += 10;
            Enemy312_Effect2 = false;
        }
        if (Enemy312_Effect3){
            List<GameObject> unmodified_cards = new(hand);
            int randomIdx = Random.Range(0, unmodified_cards.Count);
            GameObject cardSaved = unmodified_cards[randomIdx];
            unmodified_cards.RemoveAt(randomIdx);
            while(unmodified_cards.Count > 0){
                randomIdx = Random.Range(0, unmodified_cards.Count);
                Card card1 = cardSaved.GetComponent<CardDisplay>().thisCard;
                Card card2 = unmodified_cards[randomIdx].GetComponent<CardDisplay>().thisCard;
                (card1.cost_change, card2.cost_change) = (card2.cost_change, card1.cost_change);
                (card1.cost_change_before_play, card2.cost_change_before_play) = (card2.cost_change_before_play, card1.cost_change_before_play);
                (card1.cost, card2.cost) = (card2.cost, card1.cost);
                cardSaved = unmodified_cards[randomIdx];
                unmodified_cards.RemoveAt(randomIdx);
            }
            Enemy312_Effect3 = false;
        }
        UpdateHand();
    }
    public void TurnEnd(){
        Character player_character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        int tmp = Random.Range(0, hand.Count);
        if (player_character.GetStatus(Status.status.information_erase) > 0)
            hand[tmp].GetComponent<CardDisplay>().thisCard = AllCards.GetCard(204);

        tmp = Random.Range(0, hand.Count);
        if (player_character.GetStatus(Status.status.swallow) > 0){
            FindObjectOfType<BattleController>().SetSwallowedCard(hand[tmp].GetComponent<CardDisplay>().thisCard);
            Destroy(hand[tmp]);
            hand.RemoveAt(tmp);
        }

        OnceEffectReactivate();
        int idx = 0;
        while(idx < hand.Count){
            Card card_info = hand[idx].GetComponent<CardDisplay>().thisCard;

            if (card_info.disappear){
                RemoveCard(hand[idx]);
                idx++;
                continue;
            }

            if (card_info.costDecreaseOnTurnEnd) card_info.cost--;

            if (card_info.id == 201 || card_info.id == 202){
                if (hand.Count == 1){
                    idx++;
                    continue;
                }
                int removeIdx = Random.Range(0, hand.Count - 1);
                if (removeIdx >= idx) removeIdx++;
                RemoveCard(hand[removeIdx]);
                if (removeIdx > idx) idx++;
                continue;
            }

            if (card_info.id == 203){
                GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().AddStatus(Status.status.weak, 1);
                idx++;
                continue;
            }

            if (card_info.id == 205){
                GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().AddStatus(Status.status.burn, 2);
                idx++;
                continue;
            }
            
            if (card_info.id == 206){
                GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().AddStatus(Status.status.burn, 5);
                idx++;
                continue;
            }

            idx++;
        }
        for (int i = hand.Count - 1; i >= 0; i--){
            if (!hand[i].GetComponent<CardDisplay>().thisCard.keep && !hand[i].GetComponent<CardDisplay>().thisCard.keepBeforeUse){
                SpecialDiscard(hand[i]);
            }
        }
        UpdateHand();
    }
    void OnceEffectReactivate(){
        foreach(Card card in drawPile){
            if (card.once_used) card.once_used = false;
        }
        foreach(Card card in trash){
            if (card.once_used) card.once_used = false;
        }
        foreach(GameObject card in hand){
            if (card.GetComponent<CardDisplay>().thisCard.once_used) card.GetComponent<CardDisplay>().thisCard.once_used = false;
        }
    }

    static public GameObject Draw(){
        Deck deck = FindAnyObjectByType<Deck>();
        List<GameObject> hand = deck.hand;
        List<Card> drawPile = GetDeck();
        List<Card> trash = GetTrash();

        if (hand.Count == deck.hand_limit) return null;
        GameObject cardObj = null;
        if (drawPile.Count == 0){
            foreach(Card card in trash) drawPile.Add(card);
            trash.Clear();
            Shuffle(drawPile);
        }
        if (drawPile.Count > 0){
            cardObj = deck.MakeCard(drawPile[0]);
            hand.Add(cardObj);
            drawPile.RemoveAt(0);
        }
        Rearrange();
        if (Global.Check_Relic_In_Bag(15) && firstCardDrawn == false){
            firstCardDrawn = true;
            if (cardObj.GetComponent<CardDisplay>().thisCard.type == Card.Type.attack){
                foreach(GameObject enemy in BattleController.GetAllEnemy())
                    enemy.GetComponent<Character>().GetHit(3);
            }
            else GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().AddArmor(4);
        }
        return cardObj;
    }

    static public List<GameObject> Draw(int n){
        List<GameObject> list = new();
        for (int i = 0; i < n; i++){
            GameObject tmp = Draw();
            if (tmp != null) list.Add(tmp);
        }
        return list;
    }

    static public void Rearrange(){
        Deck deck = FindObjectOfType<Deck>();
        int gap = 200;
        deck.UpdateDeckNumbers();
        for (int i = 0; i < deck.hand.Count; i++){
            if (deck.hand[i].GetComponent<CardState>().state == CardState.State.ReadyToUse)
                deck.hand[i].GetComponent<CardMove>().Move(new Vector3(-700, 300, 0));
            else deck.hand[i].GetComponent<CardMove>().Move(new Vector3(-gap/2 * (deck.hand.Count-1) + gap * i, -400, 0));
            deck.hand[i].GetComponent<CardDisplay>().LoadCard();
        }
    }

    public void ResetHand(){
        for (int i = 0; i < hand.Count; i++){
            if (hand[i].GetComponent<CardState>().state == CardState.State.ReadyToUse)
                hand[i].GetComponent<CardState>().state = CardState.State.Normal;
            if (hand[i].GetComponent<CardState>().state == CardState.State.Selected)
                hand[i].GetComponent<CardState>().Unselect();
        }
    }

    public void UpdateHand(){
        for (int i = 0; i < hand.Count; i++){
            hand[i].GetComponent<CardDisplay>().LoadCard();
        }
    }

    static public List<GameObject> GetHand(){
        return FindObjectOfType<Deck>().hand;
    }
    static public List<Card> GetDeck(){
        return FindObjectOfType<Deck>().drawPile;
    }
    static public List<Card> GetTrash(){
        return FindObjectOfType<Deck>().trash;
    }
    static public List<Card> GetAll(){
        List<Card> list = new();
        foreach(Card card in GetDeck()) list.Add(card);
        foreach(Card card in GetTrash()) list.Add(card);
        foreach(GameObject card in GetHand()) list.Add(card.GetComponent<CardDisplay>().thisCard);
        return list;
    }

    static void Shuffle(List<Card> deck){
        System.Random rng = new();
        int n = deck.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            (deck[k], deck[n]) = (deck[n], deck[k]);
            // Card tmp = deck[k];
            // deck[k] = deck[n];
            // deck[n] = tmp;
        }
    }

    void UpdateDeckNumbers(){
        drawPileNumber.text = drawPile.Count.ToString();
        trashNumber.text = trash.Count.ToString();
    }

    GameObject MakeCard(Card c){
        return card_template.GetComponent<CardDisplay>().Make(Card.Copy(c), transform);
    }
}
