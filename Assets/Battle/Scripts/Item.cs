using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string type;
    public Sprite image;
    public string effectText;
    public string description;
    public int rarity;
}
