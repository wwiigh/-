using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] GameObject deck;
    [SerializeField] Sprite normal;
    [SerializeField] Sprite uncommon;
    [SerializeField] Sprite rare;

    public Text nameText;
    public Image img;
    public Text descriptionText;
    public Text costText;
    public int index;
    public Card thisCard;

    public GameObject Make(Card c, int idx, Transform t){
        GameObject a =  Instantiate(gameObject, transform.parent);
        a.GetComponent<CardDisplay>().thisCard = c;
        a.GetComponent<CardDisplay>().index = idx;
        a.tag = "Card";
        a.GetComponent<CardDisplay>().LoadCard();
        a.transform.SetParent(t);
        a.SetActive(true);
        return gameObject;
    }

    public void LoadCard(){
        if (thisCard.rarity == 0) GetComponent<Image>().sprite = normal;
        else if (thisCard.rarity == 1) GetComponent<Image>().sprite = uncommon;
        else GetComponent<Image>().sprite = rare;
        nameText.text = thisCard.cardName;
        descriptionText.fontSize = thisCard.fontSize;
        img.sprite = thisCard.image;
        int ArgIdx = 0;
        descriptionText.text = "";
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

    public int GetIndex(){
        return index;
    }

    public void SetPos(Vector3 v){
        Debug.Log("set pos to " + v);
        transform.localPosition = v;
        Debug.Log("now pos: " + transform.localPosition);
    }
}
