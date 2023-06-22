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
    public TMP_Text costText;
    public Card thisCard;

    public GameObject Make(Card c, Transform t){
        GameObject a =  Instantiate(gameObject, t);
        c.cost = c.cost_original;
        a.GetComponent<CardDisplay>().thisCard = c;
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
        descriptionText.fontSize = thisCard.fontSize;
        img.sprite = thisCard.image;
        int ArgIdx = 0;
        descriptionText.text = "";
        if (thisCard.keep || thisCard.keepBeforeUse) descriptionText.text += "保留。";
        costText.text = thisCard.cost.ToString();
        foreach (string s in thisCard.description){
            if (s == "#A"){
                //descriptionText.text += thisCard.Args[ArgIdx] + " ";
                descriptionText.text += thisCard.Args[ArgIdx];
                ArgIdx++;
            }
            else if (s == "#N"){
                //descriptionText.text += s + " ";
                descriptionText.text += "\n";
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
