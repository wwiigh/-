using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] Sprite[] image;
    public enum status{
        strenth,
        dexterity
    }
    public static string GetName(status _status){
        switch(_status){
            case status.strenth:
                return "力量";
            case status.dexterity:
                return "敏捷";
            default:
                return "Unknown";
        }
    }

    public static string GetDescription(status _status, int level){
        switch(_status){
            case status.strenth:
                return "造成的攻擊傷害" + ( (level > 0)? "<color=green>+":"<color=red>") + level.ToString() + "</color>";
            case status.dexterity:
                return "從卡牌中獲得的護甲與格擋" + ( (level > 0)? "<color=green>+":"<color=red>") + level.ToString() + "</color>";
            default:
                return "Unknown";
        }
    }

    public Sprite GetImage(status _status){
        return image[(int) _status];
    }
}
