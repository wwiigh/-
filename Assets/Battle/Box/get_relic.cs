using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class get_relic : MonoBehaviour
{
    // Start is called before the first frame update
    public Image relic_img;
    public Sprite[] relic_sprite;
    public GameObject relic;
    public Button button;
    int relic_index;
    int is_gotten = 0;
    int id;
    void Start()
    {
        button.onClick.AddListener(relic_onClick);       
    }

    void OnEnable() {
        got_relic();
        is_gotten = 0;
    }
    void got_relic(){
        if(relic_sprite.Length > 0){
            relic_index = UnityEngine.Random.Range(0, Int32.MaxValue) % relic_sprite.Length;
            id = Int32.Parse(relic_sprite[relic_index].name);
            while (repeat_relic(id) || illegal_relics(id)){
                relic_index = UnityEngine.Random.Range(0, Int32.MaxValue) % relic_sprite.Length;
                id = Int32.Parse(relic_sprite[relic_index].name);
            }
            relic_img.sprite = relic_sprite[relic_index];
            id = Int32.Parse(relic_sprite[relic_index].name);
        }  
    }
    bool repeat_relic(int relic_id){
        List<int> bag_relic_list = new List <int> ();
        bag_relic_list = Global.Return_All_Relic();
        for(int i = 0; i < bag_relic_list.Count; i++){
            if(bag_relic_list[i] == relic_id) return true;
        }
        return false;    
    }
    bool illegal_relics(int relic_id){      //ture if relic is illegal 
        switch(relic_id){
            case 1:
                return true;
            case 2:
                return true;
            case 3:
                return true;
            case 4:
                return true;    
            case 5:
                return true;
            case 6:
                return true;
            case 7:
                return true;
            case 8:
                return true;
            case 9:
                return true;
            case 10:
                return true;
            case 11:
                return true;
            case 12:
                return true;
        }
        return false;
    }
    public void relic_onClick(){
        if(is_gotten == 0){
            if(Global.AddItemToBag(id, "relic")){
                is_gotten = 1;
                relic.SetActive(false);
            }
        }
    }
}
