using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public bool disappear;
    public bool upgraded;
    public bool once_used;
    public bool costDecreaseOnTurnEnd;
    public int fontSize = 10;

    public static Card Copy(Card c){
        return new Card{
            id = c.id,
            cardName = c.cardName,
            image = c.image,
            cost_original = c.cost_original,
            cost = c.cost,
            description = c.description.ToList<string>(),
            Args =c.Args.ToList<int>(),
            select = c.select,
            rarity = c.rarity,
            type = c.type,
            keep = c.keep,
            keepBeforeUse = c.keepBeforeUse,
            exhaust = c.exhaust,
            disappear = c.disappear,
            upgraded = c.upgraded,
            once_used = c.once_used,
            costDecreaseOnTurnEnd = c.costDecreaseOnTurnEnd,
            fontSize = c.fontSize
        };
    }
}
