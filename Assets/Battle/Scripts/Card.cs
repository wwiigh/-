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
    public int cost_change;
    public int cost_change_before_play;
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
    public bool produced_by_doppelganger = false;
    public int fontSize = 10;

    public static Card Copy(Card c){
        return new Card{
            id = c.id,
            cardName = c.cardName,
            image = c.image,
            cost_original = c.cost_original,
            cost_change = c.cost_change,
            cost_change_before_play = c.cost_change_before_play,
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

    static public void SetCost(Card c, int n){
        c.cost_original = n;
        c.cost = n;
        c.cost_change = 0;
        c.cost_change_before_play = 0;
    }
}
