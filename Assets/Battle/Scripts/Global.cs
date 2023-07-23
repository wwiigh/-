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
        public float money_addition;
    }
    public static int player_max_hp = 100;
    public static int player_hp = 100;
    public static int money = 0;
    public static int max_sanity = 100;
    public static int sanity = 100;
    public static float money_addition = 1;
    public static List<Card> player_deck = new List<Card>();
    public static void init()
    {
        player_hp = 100;
        player_max_hp = 100;
        money = 0;
        max_sanity = 100;
        sanity = 100;
        money_addition = 1;
        PlayerDeckInit();
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



    public static void PlayerDeckInit(){
        if (player_deck.Count != 0) return;
        AllCards allcards = GameObject.FindGameObjectWithTag("AllCards").GetComponent<AllCards>();
        List<Card> basicCards = allcards.GetBasicCards();
        for (int i = 0; i < 4; i++){
            player_deck.Add(basicCards[0]);
        }
        for (int i = 0; i < 4; i++){
            player_deck.Add(basicCards[1]);
        }
        for (int i = 0; i < 2; i++){
            player_deck.Add(basicCards[2]);
        }
    }
    public static void PlayerDeck_Add(Card card){
        player_deck.Add(card);
    }
    public static void PlayerDeck_Remove(Card card){
        player_deck.Remove(card);
    }
    public static List<Card> GetPlayerDeck(){
        return player_deck;
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
    /// <summary>
    /// 會回傳是否成功加入，有可能背包滿了放不了
    /// </summary>
    /// <param name="id">傳入物品 ID</param>
    /// <param name="type">relic or equipment or item</param>
    
    public static bool AddItemToBag(int id,string type)
    {
        Bag_System bag_System = FindObjectOfType<Bag_System>();
        bool success_add = bag_System.Add_Item(id.ToString(),type);
        return success_add;
        
    }
    /// <summary>
    /// 刪除物品(道具裝備遺物皆可)
    /// </summary>
    /// <param name="id">傳入物品 ID</param>
    /// <param name="type">relic or equipment or item</param>
    public static void RemoveItemFromBag(int id,string type)
    {
        Bag_System bag_System = FindObjectOfType<Bag_System>();
        bag_System.Bag_del_item(id,type);
    }
    ///<summary>回傳正在裝備中的裝備</summary>
    public static List<int> Return_Equipment()
    {
        Bag_System bag_System = FindObjectOfType<Bag_System>();
        return bag_System.Return_Equipment();
    }
    ///<summary>回傳所有裝備(包含未裝備以及裝備中)</summary>
    public static  List<int> Return_All_Equipment()
    {
        Bag_System bag_System = FindObjectOfType<Bag_System>();
        return bag_System.Return_All_Equipment();
    }
    ///<summary>
    ///回傳所有遺物
    ///</summary>
    public static  List<int> Return_All_Relic()
    {
        Bag_System bag_System = FindObjectOfType<Bag_System>();
        return bag_System.Return_All_Relic();
    }
    ///<summary>
    ///回傳正在背包裡的所有物品，包含裝備和道具
    ///其中回傳list<list<int>>
    ///list[0]內放入背包內道具
    ///list[1]內放入背包內裝備
    ///</summary>
    public static List<List<int>> Return_All_Item_In_Bag()
    {
        Bag_System bag_System = FindObjectOfType<Bag_System>();
        return bag_System.Return_All_Item_In_Bag();
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
