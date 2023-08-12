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
        public List<int> card_id;
        public List<bool> card_up;
    }
    public static int player_max_hp = 100;
    public static int player_hp = 100;
    public static int money = 0;
    public static int max_sanity = 100;
    public static int sanity = 100;
    public static float money_addition = 1;
    public static int current_level = 1;
    public static List<Card> player_deck = new List<Card>();
    public static List<int> card_id = new List<int>();
    public static List<bool> card_up = new List<bool>();
    public static Card select_card;
    public static void init()
    {
        player_hp = 100;
        player_max_hp = 100;
        money = 0;
        max_sanity = 100;
        sanity = 100;
        money_addition = 1;
        player_deck.Clear();
        card_id.Clear();
        card_up.Clear();
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
        if(player_hp > player_max_hp) player_hp = player_max_hp;
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
            player_deck.Add(Card.Copy(basicCards[0]));
        }
        for (int i = 0; i < 4; i++){
            player_deck.Add(Card.Copy(basicCards[1]));
        }
        for (int i = 0; i < 2; i++){
            player_deck.Add(Card.Copy(basicCards[2]));
        }
    }
    public static void PlayerDeck_Add(Card card){
        player_deck.Add(Card.Copy(card));
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
        Global.card_id = new List<int>();
        Global.card_up = new List<bool>();
        for(int i=0;i<Global.player_deck.Count;i++)
        {
            card_id.Add(Global.player_deck[i].id);
            card_up.Add(Global.player_deck[i].upgraded);
        }
        Data save_data = new Data{
            player_max_hp = Global.player_max_hp,
            player_hp = Global.player_hp,
            money = Global.money,
            max_sanity = Global.max_sanity,
            sanity = Global.sanity,
            money_addition = Global.money_addition,
            card_id = Global.card_id,
            card_up = Global.card_up
        };
        string jsonInfo = JsonUtility.ToJson(save_data,true);
        PlayerPrefs.SetString("Player_Data",jsonInfo);
        // File.WriteAllText(Application.dataPath+"/Save_Data/Player_Data", jsonInfo);
    }
    //用來讀檔
    public static void ReadData()
    {
        string LoadData = PlayerPrefs.GetString("Player_Data","");
        if(LoadData == "")
        {
            init();
            return;
        }
        Data Load = JsonUtility.FromJson<Data>(LoadData);
        Global.player_max_hp = Load.player_max_hp;
        Global.player_hp = Load.player_hp;
        Global.money = Load.money;
        Global.max_sanity = Load.max_sanity;
        Global.sanity = Load.sanity;
        Global.money_addition = Load.money_addition;
        Global.card_id = Load.card_id;
        Global.card_up = Load.card_up;
        AllCards allcards = GameObject.FindGameObjectWithTag("AllCards").GetComponent<AllCards>();
        Global.player_deck.Clear();
        for(int i=0;i<Global.card_id.Count;i++)
        {
            Global.player_deck.Add(Card.Copy(allcards.GetCard(card_id[i])));
            if(Global.card_up[i]==true)UpgradeCard(player_deck[i]);
            // Global.player_deck[i].upgraded = Global.card_up[i];
        }
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
    ///<summary>
    ///回傳某遺物玩家是否擁有
    /// <param name="id">傳入物品 ID</param>
    ///</summary>
    public static bool Check_Relic_In_Bag(int id)
    {
        Bag_System bag_System = FindObjectOfType<Bag_System>();
        return bag_System.Have_Item("relic",id.ToString());
    }
    ///<summary>
    ///回傳某道具玩家是否擁有
    /// <param name="id">傳入物品 ID</param>
    ///</summary>
    public static bool Check_Item_In_Bag(int id)
    {
        Bag_System bag_System = FindObjectOfType<Bag_System>();
        return bag_System.Have_Item("item",id.ToString());
    }
    ///<summary>
    ///回傳某裝備玩家是否擁有
    /// <param name="id">傳入物品 ID</param>
    ///</summary>
    public static bool Check_Equipment_In_Bag(int id)
    {
        Bag_System bag_System = FindObjectOfType<Bag_System>();
        return bag_System.Have_Item("epuipment",id.ToString());
    }

    public delegate void MyDelegate(int n);
    public delegate void CardFunction(Card card);
    static MyDelegate callback_saved;
    static CardFunction card_function;
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



    public static void UpgradeCard(GameObject cardObj){
        UpgradeCard(cardObj.GetComponent<CardDisplay>().thisCard);
    }
    public static void UpgradeCard(Card card){
        if (card.upgraded) return;

        switch(card.id){
            case 1:
                card.Args[0] = 5;
                break;
            case 2:
                card.cost_original = 0;
                break;
            case 3:
                card.cost_original = 0;
                break;
            case 4:
                card.Args[0] = 3;
                break;
            case 5:
                card.Args[0] = 4;
                break;
            case 6:
                card.Args[0] = 3;
                break;
            case 7:
                card.Args[0] = 6;
                break;
            case 8:
                card.cost_original = 0;
                break;
            case 9:
                card.Args[0] = 8;
                break;
            case 10:
                card.Args[0] = 8;
                break;
            case 11:
                card.cost_original = 0;
                break;
            case 12:
                card.Args[0] = 10;
                break;
            case 13:
                card.Args[0] = 5;
                break;
            case 14:
                card.Args[1] = 2;
                break;
            case 15:
                card.cost_original = 2;
                break;
            case 16:
                card.Args[0] = 16;
                break;
            case 17:
                card.Args[0] = 12;
                break;
            case 18:
                card.Args[0] = 40;
                break;
            case 19:
                card.Args[0] = 20;
                card.Args[1] = 12;
                break;
            case 20:
                card.cost_original = 0;
                break;
            case 21:
                card.Args[1] = 2;
                break;
            case 22:
                card.Args[0] = 13;
                card.Args[1] = 13;
                break;
            case 23:
                card.Args[2] = 2;
                break;
            case 24:
                card.Args[0] = 4;
                card.Args[1] = 3;
                break;
            case 25:
                card.keep = true;
                break;
            case 26:
                card.cost_original = 1;
                break;
            case 27:
                card.Args[1] = 7;
                break;
            case 28:
                card.Args[0] = 4;
                break;
            case 29:
                card.Args[0] = 2;
                break;
            case 30:
                card.Args[0] = 10;
                break;
            case 31:
                card.Args[0] = 13;
                card.Args[1] = 5;
                break;
            case 32:
                card.Args[0] = 3;
                break;
            case 33:
                // not implemented yet
                break;
            case 34:
                card.Args[1] = 3;
                break;
            case 35:
                card.Args[1] = 7;
                break;
            case 36:
                card.Args[0] = 9;
                card.Args[1] = 27;
                break;
            case 37:
                card.cost_original = 1;
                break;
            case 38:
                card.Args[1] = 2;
                card.Args[2] = 1;
                break;
            case 39:
                // not implemented yet
                break;
            case 40:
                card.Args[1] = 30;
                break;
            case 41:
                card.Args[0] = 17;
                break;
            case 42:
                card.Args[0] = 8;
                break;
            case 43:
                card.Args[1] = 2;
                break;
            case 44:
                card.Args[1] = 2;
                break;
            case 45:
                card.Args[0] = 5;
                break;
            case 46:
                break;
            case 47:
                break;
            case 48:
                card.cost_original = 0;
                break;
            case 49:
                card.cost_original = 1;
                break;
            case 50:
                card.cost_original = 1;
                break;
            case 51:
                card.cost_original = 1;
                break;
            case 52:
                card.Args[0] = 8;
                break;
            case 53:
                card.Args[0] = 10;
                break;
            case 54:
                card.Args[0] = 5;
                card.Args[1] = 6;
                break;
            case 55:
                card.Args[1] = 5;
                break;
            case 56:
                card.Args[1] = 4;
                break;
            case 57:
                card.Args[0] = 10;
                break;
            case 58:
                card.Args[0] = 1;
                break;
            case 59:
                card.Args[0] = 1;
                card.Args[2] = 6;
                break;
            case 60:
                card.Args[1] = 2;
                break;

            case 101:
                card.Args[0] = 9;
                break;
            case 102:
                card.Args[0] = 6;
                break;
            case 103:
                Debug.Log("test");
                card.Args[0] = 9;
                break;
            default:
                Debug.Log("UpgradeCard: Unknown id " + card.id.ToString());
                break;
        }

        card.upgraded = true;
    
    }
    public static void ShowPlayerCards(List<Card> card_list, CardFunction card_fun,bool can_select)
    {
        select_card = null;
        card_function = card_fun;
        Card_List show_list = FindObjectOfType<Card_List>();
        show_list.Init(card_list,card_fun,can_select);
        
    }
    public static void DoCardAction(Card card)
    {
        card_function(card);
    }
}
