using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Deck : MonoBehaviour
{
    BattleController battleController;
    [SerializeField] List<Card> drawPile = new List<Card>();
    [SerializeField] List<Card> trash = new List<Card>();
    [SerializeField] List<GameObject> hand = new List<GameObject>();
    [SerializeField] Card temp;
    [SerializeField] GameObject card_template;
    [SerializeField] GameObject allcards_obj;
    AllCards allcards_class;
    TMP_Text drawPileNumber;
    TMP_Text trashNumber;
    int hand_limit = 8;
    private void Start() {
        battleController = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        allcards_class = allcards_obj.GetComponent<AllCards>();
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
        int totalCards = allcards_class.GetCount();
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
        drawPile.Add( GetCard(29) );
        drawPile.Add( GetCard(29) );
        drawPile.Add( GetCard(29) );
        drawPile.Add( GetCard(29) );
        drawPile.Add( GetCard(29) );
        drawPile.Add( GetCard(102) );
        drawPile.Add( GetCard(102) );
        drawPile.Add( GetCard(102) );
        drawPile.Add( GetCard(102) );
        drawPile.Add( GetCard(102) );
        drawPile.Add( GetCard(102) );
        drawPile.Add( GetCard(102) );
        drawPile.Add( GetCard(102) );
        drawPile.Add( GetCard(102) );
        drawPile.Add( GetCard(102) );

        // for testing
        // AddCardToHand(GetCard(29));
        // AddCardToHand(GetCard(29));
        // AddCardToHand(GetCard(29));
    }

    // public void Init(){
    //     List<Card> allcards = allcards_class.GetAllCards();
    //     int totalCards = allcards_class.GetCount();
    //     for (int i = 0; i < 20; i++){
    //         drawPile.Add( Card.Copy( allcards[Random.Range(0, totalCards)] ) );
    //     }
    //     Shuffle(drawPile);
    // }

    public Card GetCard(int id){
        return allcards_class.GetCard(id);
    }

    void RemoveChild(){
        List<GameObject> list = new List<GameObject>();
        foreach(Transform child in transform){
            if (child.tag == "Card") list.Add(child.gameObject);
        }
        foreach(GameObject obj in list) Destroy(obj);
    }

    public void CardUsed(GameObject card){
        hand.Remove(card);
        trash.Add(card.GetComponent<CardDisplay>().thisCard);
        Destroy(card);
        Rearrange();
    }

    // Triggers discard effects
    public void Discard(GameObject card){
        Character player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        hand.Remove(card);
        trash.Add(card.GetComponent<CardDisplay>().thisCard);
        Destroy(card);
        Rearrange();
        
        int tmp = player.GetStatus(Status.status.second_weapon);
        if (tmp > 0 && !battleController.DiscardedThisTurn())
            GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>().Draw(tmp);
        
        tmp = player.GetStatus(Status.status.fast_hand);
        if (tmp > 0) battleController.GetRandomEnemy().GetComponent<Character>().GetHit(tmp);
        
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
            battleController.GetRandomEnemy().GetComponent<Character>().LoseHP(30);
        }

        if (card_info.id == 210){
            AddCardToHand(GetCard(211));
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

    public void MoveFromHandToDrawPile(GameObject card){
        hand.Remove(card);
        drawPile.Insert(0, card.GetComponent<CardDisplay>().thisCard);
        Destroy(card);
        Rearrange();
    }

    public Card GetRandomSkillCard(){
        return allcards_obj.GetComponent<AllCards>().GetRandomSkillCard();
    }
    public Card GetRandomAttackCard(){
        return allcards_obj.GetComponent<AllCards>().GetRandomAttackCard();
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

    public GameObject card207InHand(){
        GameObject cardFound = null;
        foreach(GameObject card in hand){
            if (card.GetComponent<CardDisplay>().thisCard.id == 207){
                cardFound = card;
                break;
            }
        }
        return cardFound;
    }

    public void TurnEnd(){
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

    public GameObject Draw(){
        if (hand.Count == hand_limit) return null;
        GameObject cardObj = null;
        if (drawPile.Count > 0){
            cardObj = MakeCard(drawPile[0]);
            hand.Add(cardObj);
            drawPile.RemoveAt(0);
        }
        Rearrange();
        return cardObj;
    }

    public List<GameObject> Draw(int n){
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < n; i++){
            GameObject tmp = Draw();
            if (tmp != null) list.Add(tmp);
        }
        return list;
    }

    public void Rearrange(){
        int gap = 200;
        UpdateDeckNumbers();
        for (int i = 0; i < hand.Count; i++){
            if (hand[i].GetComponent<CardState>().state == CardState.State.ReadyToUse)
                hand[i].GetComponent<CardMove>().Move(new Vector3(-700, 300, 0));
            else hand[i].GetComponent<CardMove>().Move(new Vector3(-gap/2 * (hand.Count-1) + gap * i, -400, 0));
            hand[i].GetComponent<CardDisplay>().LoadCard();
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

    public List<GameObject> GetHand(){
        return hand;
    }

    void Shuffle(List<Card> deck){
        System.Random rng = new System.Random();
        int n = deck.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            Card tmp = deck[k];
            deck[k] = deck[n];
            deck[n] = tmp;
        }
    }

    void UpdateDeckNumbers(){
        drawPileNumber.text = drawPile.Count.ToString();
        trashNumber.text = trash.Count.ToString();
    }

    GameObject MakeCard(Card c){
        Card tmp = Card.Copy(c);
        return card_template.GetComponent<CardDisplay>().Make(tmp, transform);
    }
}
