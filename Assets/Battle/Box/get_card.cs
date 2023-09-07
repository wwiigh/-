using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
public class get_card : MonoBehaviour
{
    public Image cardIMG;
    public Sprite[] cardsprite;
    public Card[] cards;
    int current_card_index = 0;
    int id;
    public Button button;
    public GameObject card;
    int is_gotten = 0;
    void Start (){
        button.onClick.AddListener(cardonClick);
    }
    void OnEnable()
    {
        got_cards();
        is_gotten = 0;
    }
    public void got_cards(){
        if(cardsprite.Length > 0){
            long currentTicks = DateTime.Now.Ticks;
            current_card_index = (int)currentTicks % (cardsprite.Length);
            if(current_card_index < 0){
                current_card_index -= 2 * current_card_index;
            }
            cardIMG.sprite = cardsprite[current_card_index];
            string cardname = cardsprite[current_card_index].name;
            if(current_card_index < 10){
                id = (int)cardname[0] - '0';
            }
            else{
                id = 10 * (cardname[0] - '0') + (cardname[1] - '0');
                
            }
        }
    }
    public void cardonClick(){
        if(is_gotten == 0){
            Global.PlayerDeck_Add(cards[id-1]);
            is_gotten = 1;
            card.SetActive(false);
        }
        else{
            is_gotten = 0;
        }
    }
    
}
