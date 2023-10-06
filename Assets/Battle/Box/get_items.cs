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
    public static int id;
    public GameObject item;
    int is_gotten = 0;
    bool bag_full = false;
    void Start(){
        button.onClick.AddListener(item_onClick);
    }
    void OnEnable()
    {
        // item.SetActive(true);
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
            img.sprite = items_IMGs[current_index];
            id = Int32.Parse(items_IMGs[current_index].name);
        }
    }
    public void item_onClick(){
        if(is_gotten == 0){
            if(Global.AddItemToBag(id, "item")){
                is_gotten = 1;
                item.SetActive(false);
            }
        }
    }
}
