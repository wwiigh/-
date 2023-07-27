using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
public class get_card : MonoBehaviour
{
    // Start is called before the first frame update
    public Image cardIMG;
    public Sprite[] cardsprite;
    int current_card_index = 0;
    void OnEnable()
    {
        got_cards();
    }
    public void got_cards(){
        if(cardsprite.Length > 0){
            long currentTicks = DateTime.Now.Ticks;
            current_card_index = (int)currentTicks % cardsprite.Length;
            cardIMG.sprite = cardsprite[current_card_index];
            // Debug.Log("----");
            // Debug.Log(current_card_index);
            // Debug.Log("----");
        }
    }
    
}
