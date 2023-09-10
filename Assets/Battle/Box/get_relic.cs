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
            while(!illegal_relics(relic_index)){
                relic_index = UnityEngine.Random.Range(0, Int32.MaxValue) % relic_sprite.Length;
            }
            relic_img.sprite = relic_sprite[relic_index];
            id = Int32.Parse(relic_sprite[relic_index].name);
        }  
    }
    bool illegal_relics(int relic_id){
        List<int> relic_list = new List <int> ();
        relic_list = Global.Return_All_Relic();
        for(int i = 0; i < relic_list.Count; i++){
            if(relic_list[i] == relic_id){
                return false;
                break;
            }
        }
        switch(relic_id){
            case 1:
                return false;
                break;
            case 2:
                return false;
                break;
            case 3:
                return false;
                break;
            case 4:
                return false;
                break;
            case 5:
                return false;
                break;
            case 6:
                return false;
                break;
            case 7:
                return false;
                break;
            case 8:
                return false;
                break;
            case 9:
                return false;
                break;
            case 10:
                return false;
                break;
            case 11:
                return false;
                break;
            case 12:
                return false;
                break;
        }
        return true;
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
