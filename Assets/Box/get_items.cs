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
    public Button button;
    public int id;
    public GameObject item;
    public int is_gotten = 0;
    void Start(){
        button.onClick.AddListener(item_onClick);
    }
    void OnEnable()
    {
        item.SetActive(true);
        got_items();
        is_gotten = 0;
    }
    
    public void got_items(){
        if(items_IMGs.Length > 0){
            long currentTicks = DateTime.Now.Ticks;
            current_index = (int)(currentTicks) % items_IMGs.Length;
            if(current_index < 0){
                current_index -= 2 * current_index;
            }
            // print("idx is " + current_index);
            img.sprite = items_IMGs[current_index];
            // print("item idx id is " + current_index);
            // print("item name is " + items_IMGs[current_index].name);
            id = Int32.Parse(items_IMGs[current_index].name);
            
        }
    }
    public void item_onClick(){
        if(is_gotten == 0){
            Global.AddItemToBag(id, "item");
            is_gotten = 1;
        }
    }
}
