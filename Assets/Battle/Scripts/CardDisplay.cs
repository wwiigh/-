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
    public TMP_Text nameText;
    public Image img;
    public TMP_Text descriptionText;
    [SerializeField] GameObject costIcon;
    public TMP_Text costText;
    public Card thisCard;

    public GameObject Make(Card c, Transform t){
        GameObject a =  Instantiate(gameObject, t);
        c.cost = c.cost_original;
        a.GetComponent<CardDisplay>().thisCard = Card.Copy(c);
        a.tag = "Card";
        a.GetComponent<CardDisplay>().LoadCard();
        // a.SetActive(true);
        return a;
    }

    public void LoadCard(){
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
            costText.text = thisCard.cost.ToString();
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
                    if (thisCard.upgraded) tmp = BattleController.ComputeDamage(player, thisCard.Args[ArgIdx], 3, 5);
                    else tmp = BattleController.ComputeDamage(player, thisCard.Args[ArgIdx], 3, 3);
                }
                else tmp = BattleController.ComputeDamage(player, thisCard.Args[ArgIdx]);

                if (tmp > thisCard.Args[ArgIdx]) descriptionText.text += "<color=green>" + tmp.ToString() + "</color>";
                else if (tmp < thisCard.Args[ArgIdx]) descriptionText.text += "<color=red>" + tmp.ToString() + "</color>";
                else descriptionText.text += tmp;
                ArgIdx++;
            }
            else if (s == "#D"){
                tmp = BattleController.ComputeArmor(thisCard.Args[ArgIdx]);
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
                descriptionText.text += Object.FindObjectOfType<BattleController>().GetCurrentTurn();
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
}
