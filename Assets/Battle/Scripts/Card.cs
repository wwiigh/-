using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class Card : ScriptableObject
{
    public int id;
    public string cardName;
    public Sprite image;
    public int cost;
    public List<string> description;
    public List<int> Args;
    public int select;
    public int rarity;
    public bool exhaust;
    public int fontSize;
}
