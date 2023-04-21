using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cost : MonoBehaviour
{
    [SerializeField] Text costText;
    int maxCost;
    int cost;
    void Start()
    {
        Init();
    }

    public void Init(){
        maxCost = 3;
        cost = 3;
        updateText();
    }

    public int GetCost(){
        return cost;
    }

    public void ChangeCost(int value){
        cost += value;
        updateText();
    }

    void updateText(){
        costText.text = cost.ToString() + " / " + maxCost.ToString();
    }
}
