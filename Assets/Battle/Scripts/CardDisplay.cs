using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] Sprite normal;
    [SerializeField] Sprite uncommon;
    [SerializeField] Sprite rare;
    [SerializeField] Sprite special;

    public Image cardBase;
    [SerializeField] Image topFrame;
    public Image img;
    [SerializeField] Image bottomFrame;
    public TMP_Text descriptionText;
    [SerializeField] Image nameFrame;
    public TMP_Text nameText;
    [SerializeField] Image borderFrame;
    [SerializeField] GameObject costIcon;
    public TMP_Text costText;
    public Card thisCard;

    public GameObject Make(Card c, Transform t){
        GameObject a =  Instantiate(gameObject, t);
        // c.cost = c.cost_original;
        a.GetComponent<CardDisplay>().thisCard = Card.Copy(c);
        a.tag = "Card";
        a.GetComponent<CardDisplay>().LoadCard();
        // a.SetActive(true);
        return a;
    }

    public int GetCost(){
        int cost = thisCard.cost + thisCard.cost_change + thisCard.cost_change_before_play;

        if (FindAnyObjectByType<Deck>()){
            int count208 = 0;
            foreach(GameObject card in Deck.GetHand()){
                if (!card) continue;
                if (card.GetComponent<CardDisplay>().thisCard.id == 208) count208++;
            }
            if (Deck.GetHand().Contains(gameObject) && GetComponent<CardDisplay>().thisCard.type != Card.Type.special) cost += count208 * 2;
        }
        
        if (GameObject.FindWithTag("Player") == null) return cost;
        if (GameObject.FindWithTag("Player").GetComponent<Character>().GetStatus(Status.status.void_sword) > 0) return 0;
        if (Global.Check_Relic_In_Bag(23) && cost > 2) cost = 2;
        return cost;
    }

    public void LoadCard(){
        LoadCard(false);
    }
    public void LoadCard(bool no_modification){
        if (thisCard.rarity == Card.Rarity.common) cardBase.sprite = normal;
        else if (thisCard.rarity == Card.Rarity.uncommon) cardBase.sprite = uncommon;
        else cardBase.sprite = rare;

        nameText.text = thisCard.cardName;
        if (thisCard.upgraded) nameText.text += "+";

        descriptionText.fontSize = thisCard.fontSize;

        if (thisCard.image == null) img.enabled = false;
        else img.sprite = thisCard.image;
        
        descriptionText.text = "";
        if (thisCard.keep || thisCard.keepBeforeUse) descriptionText.text += "保留。";
        if (thisCard.exhaust) descriptionText.text += "移除。";
        if (thisCard.disappear) descriptionText.text += "消逝。";

        if (thisCard.cost != -1){
            if (!no_modification && GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().GetStatus(Status.status.dream) > 0)
                costText.text = "";
            else costText.text = GetCost().ToString();
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
                if (no_modification){
                    descriptionText.text += thisCard.Args[ArgIdx].ToString();
                    ArgIdx++;
                    continue;
                }

                if (player.GetComponent<Character>().GetStatus(Status.status.void_sword) > 0){
                    if (thisCard.id == 47){
                        if (thisCard.upgraded) tmp = BattleController.ComputeDamage(player, 1, 3, 5);
                        else tmp = BattleController.ComputeDamage(player, 1, 3, 3);
                    }
                    else tmp = BattleController.ComputeDamage(player, 1);
                }
                else{
                    if (thisCard.id == 47){
                        if (thisCard.upgraded) tmp = BattleController.ComputeDamage(player, thisCard.Args[ArgIdx], 3, 5);
                        else tmp = BattleController.ComputeDamage(player, thisCard.Args[ArgIdx], 3, 3);
                    }
                    else tmp = BattleController.ComputeDamage(player, thisCard.Args[ArgIdx]);
                }

                if (tmp > thisCard.Args[ArgIdx]) descriptionText.text += "<color=green>" + tmp.ToString() + "</color>";
                else if (tmp < thisCard.Args[ArgIdx]) descriptionText.text += "<color=red>" + tmp.ToString() + "</color>";
                else descriptionText.text += tmp;
                ArgIdx++;
            }
            else if (s == "#D"){
                if (no_modification){
                    descriptionText.text += thisCard.Args[ArgIdx].ToString();
                    ArgIdx++;
                    continue;
                }
                
                if (player.GetComponent<Character>().GetStatus(Status.status.void_sword) > 0){
                    tmp = BattleController.ComputeArmor(1);
                }
                else tmp = BattleController.ComputeArmor(thisCard.Args[ArgIdx]);

                if (tmp > thisCard.Args[ArgIdx]) descriptionText.text += "<color=green>" + tmp.ToString() + "</color>";
                else if (tmp < thisCard.Args[ArgIdx]) descriptionText.text += "<color=red>" + tmp.ToString() + "</color>";
                else descriptionText.text += tmp;
                ArgIdx++;
            }
            else if (s == "#O"){
                descriptionText.text += thisCard.Args[ArgIdx];
                ArgIdx++;
            }
            else if (s == "#N"){
                //descriptionText.text += s + " ";
                descriptionText.text += "\n";
            }
            else if (s == "#turn"){
                descriptionText.text += BattleController.GetCurrentTurn();
            }
            else if (s == "#once_start"){
                if (thisCard.once_used) descriptionText.text += "<color=grey>";
            }
            else if (s == "#once_end"){
                if (thisCard.once_used) descriptionText.text += "</color>";
            }
            else{
                descriptionText.text += s;
            }
        }
    }

    public void SetPos(Vector3 v){
        Debug.Log("set pos to " + v);
        transform.localPosition = v;
        Debug.Log("now pos: " + transform.localPosition);
    }

    bool fading = false;
    float fadingSpeed = 0.1f;
    public void FadeOut(){
        fading = true;
    }
    private void FixedUpdate() {
        if (fading){
            cardBase.color = Color.Lerp(cardBase.color, new Color(0, 0, 0, 0), fadingSpeed);
            topFrame.color = Color.Lerp(topFrame.color, new Color(0, 0, 0, 0), fadingSpeed);
            img.color = Color.Lerp(img.color, new Color(0, 0, 0, 0), fadingSpeed);
            bottomFrame.color = Color.Lerp(bottomFrame.color, new Color(0, 0, 0, 0), fadingSpeed);
            descriptionText.color = Color.Lerp(descriptionText.color, new Color(0, 0, 0, 0), fadingSpeed);
            nameFrame.color = Color.Lerp(nameFrame.color, new Color(0, 0, 0, 0), fadingSpeed);
            nameText.color = Color.Lerp(nameText.color, new Color(0, 0, 0, 0), fadingSpeed);
            borderFrame.color = Color.Lerp(borderFrame.color, new Color(0, 0, 0, 0), fadingSpeed);
            costIcon.GetComponent<Image>().color = Color.Lerp(costIcon.GetComponent<Image>().color, new Color(0, 0, 0, 0), fadingSpeed);
            costText.color = Color.Lerp(costText.color, new Color(0, 0, 0, 0), fadingSpeed);

            if (cardBase.color.a < 0.01f){
                Destroy(gameObject);
            }
        }
    }
    public bool IsFading(){
        return fading;
    }
}
