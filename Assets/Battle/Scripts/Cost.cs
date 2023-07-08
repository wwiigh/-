using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cost : MonoBehaviour
{
    static Text costText;
    static int maxCost;
    static int cost;

    static public void Init(){
        costText = GameObject.FindGameObjectWithTag("Cost").transform.GetChild(0).GetComponent<Text>();
        maxCost = 3;
        cost = 3;
        updateText();
    }

    static public int GetCost(){
        return cost;
    }

    static public void ChangeCost(int value){
        cost += value;
        updateText();
    }

    static public void Refill(int additional){
        cost = maxCost + additional;
        updateText();
    }

    static void updateText(){
        costText.text = cost.ToString() + " / " + maxCost.ToString();
    }
}
