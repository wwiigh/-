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
            relic_img.sprite = relic_sprite[relic_index];
            id = Int32.Parse(relic_sprite[relic_index].name);
        }  
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
