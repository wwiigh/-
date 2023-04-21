using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Equipments")]
public class EquipmentClass : ScriptableObject
{
    public int id;
    public string _name;
    public Sprite image;
    public enum Rarity{
        Common,
        Uncommon,
        Rare
    }
    public Rarity rarity;
    public string description;
    public int initialCharge;
    public int maxCharge;
    public enum ChargeWhen{
        None,
        EnemyDefeated,
        GainArmor,
        DealDamage,
        PlayCards,
        UseEnergy,
        TakeDamage
    }
    public ChargeWhen chargeTiming;
    public int cost;
    public int cooldown;
}
