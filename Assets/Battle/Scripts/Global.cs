using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Global : MonoBehaviour
{
    [System.Serializable]
    public struct Data{ //用來放需要存的資料
        public int player_max_hp;
        public int player_hp;
        public int money;
        public int max_sanity;
        public int sanity;
    }
    public static int player_max_hp = 100;
    public static int player_hp = 100;
    public static int money = 0;
    public static int max_sanity = 100;
    public static int sanity = 100;

    public static void AddSan(int value){
        sanity += value;
    }
    public static void AddMaxSan(int value){
        max_sanity += value;
    }
    public static void AddHp(int value){
        player_hp += value;
    }
    public static void AddMaxHp(int value){
        player_max_hp += value;
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
    }


    public delegate void MyDelegate(int n);
    static MyDelegate callback_saved;
    static List<GameObject> cardObjs = new List<GameObject>();
    static GameObject panelObj;
    static GameObject cancelButtonObj;
    public static void SelectCardsFrom(Transform parent, List<Card> cards, MyDelegate callback, bool cancellable){
        GlobalAssets asset = GameObject.FindGameObjectWithTag("Global").GetComponent<GlobalAssets>();
        GameObject panelTemplate = asset.panelTemplate;
        GameObject cancelButtonTemplate = asset.cancelButtonTemplate;
        GameObject cardTemplate = asset.cardTemplate;
        callback_saved = callback;
        cardObjs.Clear();
        int gap = 250;
        int idx = 0;
        panelObj = Instantiate(panelTemplate, parent);
        foreach(Card card in cards){
            GameObject tmp = cardTemplate.GetComponent<CardDisplay>().Make(card, parent);
            cardObjs.Add(tmp);
            tmp.GetComponent<CardMove>().clickReturnNumber = idx;
            tmp.GetComponent<CardMove>().OnClick += SelectCardsFrom_Return;
            tmp.GetComponent<CardMove>().Move(new Vector3(-gap/2 * (cards.Count-1) + gap * idx, 0, 0));
            idx++;
        }
        cancelButtonObj = Instantiate(cancelButtonTemplate, parent);
        cancelButtonObj.transform.localPosition = new Vector3(0, -250, 0);
        if (!cancellable) cancelButtonObj.GetComponent<Button>().interactable = false;
    }
    public static void SelectCardsFrom_Return(int n){
        foreach(GameObject obj in cardObjs) Destroy(obj);
        Destroy(panelObj);
        Destroy(cancelButtonObj);
        callback_saved(n);
    }
}
