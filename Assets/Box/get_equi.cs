using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;


public class get_equi : MonoBehaviour
{
    // Start is called before the first frame update
    public Image equiIMG;
    public Sprite[] cardsprite;
    private int current_equi_index;
    void OnEnable()
    {
        got_equi();
    }
    public void got_equi(){
        if(cardsprite.Length > 0){
            long currentTicks = DateTime.Now.Ticks;
            current_equi_index = (int)currentTicks % cardsprite.Length;
            equiIMG.sprite = cardsprite[current_equi_index];
        }
    }
}
