using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
public class get_items : MonoBehaviour
{
    public Image img;
    public Sprite[] items_IMGs;
    private int current_index = 0;
    void OnEnable()
    {
        got_items();
    }
    
    public void got_items(){
        if(items_IMGs.Length > 0){
            long currentTicks = DateTime.Now.Ticks;
            current_index = (int) currentTicks % items_IMGs.Length;
            img.sprite = items_IMGs[current_index];
        }
    }
}
