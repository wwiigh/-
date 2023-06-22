using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class Card : ScriptableObject
{
    public int id;
    public string cardName;
    public Sprite image;
    public int cost_original;
    public int cost;
    public List<string> description;
    public List<int> Args;
    public enum Select{
        None,
        Enemy,
        Card
    }
    public Select select;
    public enum Rarity{
        common,
        uncommon,
        rare
    }
    public Rarity rarity;
    public enum Type{
        attack,
        skill,
        special
    }
    public Type type;
    public bool keep;
    public bool keepBeforeUse;
    public bool exhaust;
    public bool upgraded;
    public int fontSize = 10;

    public static Card Copy(Card c){
        return new Card{
            id = c.id,
            cardName = c.cardName,
            image = c.image,
            cost_original = c.cost_original,
            cost = c.cost,
            description = c.description,
            Args = c.Args,
            select = c.select,
            rarity = c.rarity,
            type = c.type,
            exhaust = c.exhaust,
            fontSize = c.fontSize
        };
    }
}
