using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
using UnityEngine.EventSystems;

public class get_booty: MonoBehaviour{
    
    [System.Serializable]
    public struct Award{
        public int id;
        public string type;
        public int enemy_id;
        public Sprite picture;
        public UI_Show_Text description;
        public bool is_gotten;
    }
    public Award[] award;
    [System.Serializable]
    public struct Get
    {
        public GameObject obj;
        public bool is_used;
        public Image picture;
        public string type;
        public int id;
        public Button button;
        
    }
    public Get[] get = new Get[6];
    BattleController.BattleType enemy_type = 0;
    int[] enemy_id;
    int number_of_probability = 101;
    public Text gold_text;
    public GameObject booty;
    public Button booty_close_button;
    [System.Serializable]
    public struct Award_card{
        public GameObject obj;
        public Cardarray cardobj;
        public Image picture;
        public int card_id;
        public Button button;
    }
    [System.Serializable]
    public struct Cardarray{
        public Sprite picture;
        public Card cardobj;
        public int id;
        
    }
    public Cardarray[] cards;
    public Award_card[] award_card;
    public Button CardBootyCloseButton;
    public GameObject booty_card;
    void Start(){
        get[0].button.onClick.AddListener(() => click_get_item(0));
        get[1].button.onClick.AddListener(() => click_get_item(1));
        get[2].button.onClick.AddListener(() => click_get_item(2));
        get[3].button.onClick.AddListener(() => click_get_item(3));
        get[4].button.onClick.AddListener(() => click_get_item(4));
        get[5].button.onClick.AddListener(() => click_get_item(5));
        booty_close_button.onClick.AddListener(close_booty);
        award_card[0].button.onClick.AddListener(() => click_get_card(0));
        award_card[1].button.onClick.AddListener(() => click_get_card(1));
        award_card[2].button.onClick.AddListener(() => click_get_card(2));
        CardBootyCloseButton.onClick.AddListener(close_card_booty);
    }
    void OnEnable(){
        enemy_id = new int[]{101, 102, 103};
        enemy_type = 0;
        // enemy_id = PlayerPrefs.GetInt("enemyid", enemyid);
        golden();
        for(int i = 0; i < get.Length; i++){
            get[i].obj.SetActive(false);
        }
        booty_card.SetActive(false);
        switch(enemy_type){
            case BattleController.BattleType.Normal:
                for(int i = 0; i < enemy_id.Length; i++){
                    if(award_is_drop(number_of_probability)){
                        normal_drop(enemy_id[i]);
                    }
                }
                break;
            case BattleController.BattleType.Elite:
                for(int i = 0; i < enemy_id.Length; i++){
                    if(award_is_drop(number_of_probability)){
                        normal_drop(enemy_id[i]);
                    }
                }
                break;
            case BattleController.BattleType.Boss:
                for(int i = 0; i < enemy_id.Length; i++){
                    if(award_is_drop(number_of_probability)){
                        normal_drop(enemy_id[i]);
                    }
                }
                break;
        }
        card_drop();
    }
    int find_drop(int find_enemy_id){
        for(int i = 0; i < award.Length; i++){
            if(award[i].enemy_id == find_enemy_id){
                return i;
                break;   //return the award's index
            }
        }
        return -1;
    }
    void normal_drop(int id){
        int drop = find_drop(id);
        if(drop > -1){
            // print(drop);
            for(int i = 0; i < get.Length; i++){
                if(!get[i].is_used){
                    get[i].is_used = true;
                    get[i].picture.sprite = award[drop].picture;
                    get[i].type = award[drop].type;
                    get[i].id = award[drop].id;
                    get[i].obj.SetActive(true);
                    break;
                }
            }
        }
    }
    void elite_drop(int id){
        
    }
    void boss_drop(int id){

    }
    void card_drop(){
        int[] arr = new int[3];
        for(int i = 0; i < 3; i++){
            int x = UnityEngine.Random.Range(0, 60);
            if(i > 0){
                if(x == arr[i-1]){
                    x = UnityEngine.Random.Range(0, 60);
                }
                arr[i] = x;
            }
        }
        for(int i = 0; i < 3; i++){
            award_card[i].picture.sprite = cards[arr[i]].picture;
            award_card[i].card_id = cards[arr[i]].id;
        }
    }
    void click_get_card(int idx){
        Global.PlayerDeck_Add(cards[award_card[idx].card_id - 1].cardobj);
    }
    void click_get_item(int index){
        // print(get[index].id);
        // print(get[index].type);
        if(Global.AddItemToBag(get[index].id, get[index].type)){
            print("get " + get[index].id);
            get[index].obj.SetActive(false);
        }
    }
    
    bool award_is_drop(int number){
        int rnd = UnityEngine.Random.Range(0, 100);
        if(rnd <= number){
            return true;
        }
        else {
            return false;
        }
    }
    void golden(){
        int x = UnityEngine.Random.Range(100, 200);
        gold_text.text = "" + x;
        Global.AddMoney(x);
    }
    public void close_booty(){
        for(int i = 0; i < get.Length; i++){
            get[i].obj.SetActive(true);
        }
        booty.SetActive(false);
        booty_card.SetActive(true);
    }
    public void close_card_booty(){
        booty_card.SetActive(false);
    }
    public bool ShowLoot(int[] enemy_ids, BattleController.BattleType type){
        enemy_id = enemy_ids;
        enemy_type = type;
        open_settlement();
        return true;
        
    }
    public bool illegal_relics(int relic_id){
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
    public void open_settlement(){
        //打開戰利品畫面
    }
}
