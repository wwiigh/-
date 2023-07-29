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
    public Sprite[] equiSprite;
    private int current_equi_index;
    void OnEnable()
    {
        got_equi();
    }
    public void got_equi(){
        if(equiSprite.Length > 0){
            print("hi\n");
            long currentTicks = DateTime.Now.Ticks;
            print("ticks is " + (int)currentTicks);
            current_equi_index = (int)currentTicks % (equiSprite.Length);
            if(current_equi_index < 0){
                current_equi_index -= 2 * current_equi_index;
            }
            // print(current_equi_index);
            equiIMG.sprite = equiSprite[current_equi_index];
            int id = Int32.Parse(equiSprite[current_equi_index].name);
            print(id);
            // Global.AddItemToBag(id, "equipment");
        }
    }
}
