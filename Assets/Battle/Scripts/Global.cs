using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Global
{
    [System.Serializable]
    public struct Data{ //用來放需要存的資料
        public int player_max_hp;
        public int player_hp;
        public int money;
        public int max_sanity;
        public int sanity;
        public float money_addition;
    }
    public static int player_max_hp = 100;
    public static int player_hp = 100;
    public static int money = 0;
    public static int max_sanity = 100;
    public static int sanity = 100;
    public static float money_addition = 1;
    public static void init()
    {
        player_hp = 100;
        player_max_hp = 100;
        money = 0;
        max_sanity = 100;
        sanity = 100;
        money_addition = 1;
        SaveData();
    }

    public static void AddSan(int value){
        sanity += value;
        if(sanity > max_sanity)sanity = max_sanity;
    }
    public static void AddMaxSan(int value){
        max_sanity += value;
    }
    public static void AddHp(int value){
        player_hp += value;
        if(player_hp>player_max_hp)player_hp = player_max_hp;
    }
    public static void AddMaxHp(int value){
        player_max_hp += value;
    }
    public static void AddMoney(int value){
        if(value > 0)
        {
            money += (int)(value*money_addition);
        }
        else
        {
            money += value;
        }
    }
    //用來存檔
    public static void SaveData()
    {
        Data save_data = new Data{
            player_max_hp = Global.player_max_hp,
            player_hp = Global.player_hp,
            money = Global.money,
            max_sanity = Global.max_sanity,
            sanity = Global.sanity,
            money_addition = Global.money_addition
        };
        string jsonInfo = JsonUtility.ToJson(save_data,true);
        File.WriteAllText(Application.dataPath+"/Save_Data/Player_Data", jsonInfo);
    }
    //用來讀檔
    public static void ReadData()
    {
        string LoadData = File.ReadAllText(Application.dataPath+"/Save_Data/Player_Data");
        Data Load = JsonUtility.FromJson<Data>(LoadData);
        Global.player_max_hp = Load.player_max_hp;
        Global.player_hp = Load.player_hp;
        Global.money = Load.money;
        Global.max_sanity = Load.max_sanity;
        Global.sanity = Load.sanity;
        Global.money_addition = Load.money_addition;
    }
}
