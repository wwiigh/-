using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_List : MonoBehaviour
{
    [SerializeField] Sprite normal;
    [SerializeField] Sprite uncommon;
    [SerializeField] Sprite rare;
    [SerializeField] Sprite special;
    [SerializeField] GameObject costIcon;
    public GameObject card_list;
    public GameObject card;
    public  List<Card> player_deck;
    public GameObject confirm_button;
    public static int now_select = 0;
    public static Card now_select_card;
    List<GameObject> cards = new List<GameObject>();
    Global.CardFunction card_fun;
    // void OnEnable()
    // {
    //     Global.PlayerDeckInit();
    //     Global.ShowPlayerCards(Global.GetPlayerDeck(),null,true);
    //     // Init(true);
    // }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(now_select == 1)confirm_button.GetComponent<Button>().interactable = true;
        else confirm_button.GetComponent<Button>().interactable = false;
    }
    public void Clear()
    {
        for(int i=0;i<this.gameObject.transform.childCount;i++)
        {
            GameObject g = this.gameObject.transform.GetChild(i).gameObject;
            g.SetActive(false);
        }
        foreach (var item in cards)
        {
            Destroy(item);
        }
        cards.Clear();
    }
    public void Init(List<Card> player_deck,Global.CardFunction card_fun,bool can_select)
    {
        if(player_deck.Count==0&&can_select==true)return;
        now_select = 0;
        this.card_fun = card_fun;
        // player_deck = Global.GetPlayerDeck();
        for(int i=0;i<this.gameObject.transform.childCount;i++)
        {
            GameObject g = this.gameObject.transform.GetChild(i).gameObject;
            g.SetActive(true);
        }
        foreach(var item in player_deck)
        {
            GameObject a =  Instantiate(card, card_list.transform);
            cards.Add(a);
            item.cost = item.cost_original;
            a.GetComponent<CardDisplay>().thisCard = item;
            if(can_select == false) a.GetComponent<Card_Select>().NotSelect();
            a.tag = "Card";
            a.GetComponent<CardDisplay>().LoadCard(true);
            // card.GetComponent<CardDisplay>().Make(item,card_list.transform);
        }
        if(can_select == true)confirm_button.SetActive(true);
        else confirm_button.SetActive(false);
    }

    public void OnClick()
    {
        Global.select_card = now_select_card;
        if(card_fun != null)
        {
            Global.DoCardAction(Global.select_card);
        }
        Debug.Log("now card is "+Global.select_card);
        for(int i=0;i<this.gameObject.transform.childCount;i++)
        {
            GameObject g = this.gameObject.transform.GetChild(i).gameObject;
            g.SetActive(false);
        }
        foreach (var item in cards)
        {
            Destroy(item);
        }
        cards.Clear();
    }

    void show_card_text(CardDisplay a,Card thisCard)
    {
        if (thisCard.rarity == Card.Rarity.common) a.cardBase.sprite = normal;
        else if (thisCard.rarity == Card.Rarity.uncommon) a.cardBase.sprite = uncommon;
        else a.cardBase.sprite = rare;

        a.nameText.text = thisCard.cardName;
        if (thisCard.upgraded) a.nameText.text += "+";

        a.descriptionText.fontSize = thisCard.fontSize;

        if (thisCard.image == null) a.img.enabled = false;
        else a.img.sprite = thisCard.image;
        
        a.descriptionText.text = "";
        if (thisCard.keep || thisCard.keepBeforeUse) a.descriptionText.text += "保留。";
        if (thisCard.exhaust) a.descriptionText.text += "移除。";
        if (thisCard.disappear) a.descriptionText.text += "消逝。";

        if (thisCard.cost != -1){
            a.costText.text = thisCard.cost.ToString();
        }
        else{
            costIcon.SetActive(false);
        }
        

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int ArgIdx = 0;
        int tmp = 0;
        bool upgraded_text = false;
        foreach (string s in thisCard.description){
            if (s == "#upgrade_start"){
                upgraded_text = true;
                continue;
            }
            else if (s == "#upgrade_end"){
                upgraded_text = false;
                continue;
            }

            if (upgraded_text && !thisCard.upgraded) continue;

            if (s == "#A"){
                if (thisCard.id == 47){
                    if (thisCard.upgraded) tmp =  thisCard.Args[ArgIdx];
                    else tmp = thisCard.Args[ArgIdx];
                }
                else tmp = thisCard.Args[ArgIdx];

                if (tmp > thisCard.Args[ArgIdx]) a.descriptionText.text += "<color=green>" + tmp.ToString() + "</color>";
                else if (tmp < thisCard.Args[ArgIdx]) a.descriptionText.text += "<color=red>" + tmp.ToString() + "</color>";
                else a.descriptionText.text += tmp;
                ArgIdx++;
            }
            else if (s == "#D"){
                tmp = thisCard.Args[ArgIdx];
                if (tmp > thisCard.Args[ArgIdx]) a.descriptionText.text += "<color=green>" + tmp.ToString() + "</color>";
                else if (tmp < thisCard.Args[ArgIdx]) a.descriptionText.text += "<color=red>" + tmp.ToString() + "</color>";
                else a.descriptionText.text += tmp;
                ArgIdx++;
            }
            else if (s == "#O"){
                a.descriptionText.text += thisCard.Args[ArgIdx];
                ArgIdx++;
            }
            else if (s == "#N"){
                //descriptionText.text += s + " ";
                a.descriptionText.text += "\n";
            }
            else if (s == "#turn"){
                a.descriptionText.text += Object.FindObjectOfType<BattleController>().GetCurrentTurn();
            }
            else if (s == "#once_start"){
                if (thisCard.once_used) a.descriptionText.text += "<color=grey>";
            }
            else if (s == "#once_end"){
                if (thisCard.once_used) a.descriptionText.text += "</color>";
            }
            else{
                a.descriptionText.text += s;
            }
        }
    }
}
