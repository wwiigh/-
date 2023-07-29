using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameEvent : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject optionButton;
    EventClass event_loaded;
    [SerializeField] TMP_Text eventName;
    [SerializeField] Image image;
    [SerializeField] TMP_Text description;
    int optionCount;
    [SerializeField] List<EventClass> all_events;
    [SerializeField] List<Card> all_attack_cards;
    [SerializeField] List<Card> all_skill_cards;

    List<EventClass> normal_events = new List<EventClass>();
    List<EventClass> story_events = new List<EventClass>();
    Dictionary<int,EventClass> normal_events_dict = new Dictionary<int,EventClass>();
    Dictionary<int,EventClass> story_events_dict = new Dictionary<int,EventClass>();

    private void Start() {
        foreach(EventClass _event in all_events){
            if (_event.type == EventClass.Type.normal) normal_events.Add(_event);
            else if (_event.type == EventClass.Type.story) story_events.Add(_event);
            if (_event.type == EventClass.Type.normal) normal_events_dict[_event.id] = _event;
            else if (_event.type == EventClass.Type.story) story_events_dict[_event.id] = _event;
        }
    }
    
    void OnEnable(){
        normal_events.Clear();
        story_events.Clear();
        normal_events_dict.Clear();
        story_events_dict.Clear();
        foreach(EventClass _event in all_events){
            if (_event.type == EventClass.Type.normal) normal_events.Add(_event);
            else if (_event.type == EventClass.Type.story) story_events.Add(_event);
            if (_event.type == EventClass.Type.normal) normal_events_dict[_event.id] = _event;
            else if (_event.type == EventClass.Type.story) story_events_dict[_event.id] = _event;
        }
    }
    public void EnterEvent(){
        int idx = Random.Range(0, normal_events.Count);
        event_loaded = normal_events[idx];
        LoadEvent();
    }

    void LoadEvent(){
        eventName.text = event_loaded.eventName;
        image.sprite = event_loaded.image;
        description.text = event_loaded.description;
        optionCount = event_loaded.optionText.Count;

        for (int i = 0; i < optionCount; i++){
            GameObject tmp = Instantiate(optionButton, canvas.transform);
            tmp.transform.position += new Vector3(500, -450 + (optionCount - i - 1) * 80, 0);
            tmp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = event_loaded.optionText[i];
            tmp.name = "optionButton";
        }
    }
    public void LoadEvent(int event_id, EventClass.Type type){
        Reset();
        if(type == EventClass.Type.normal){
            event_loaded = normal_events_dict[event_id];
        }
        else{
            event_loaded = story_events_dict[event_id];
        }
        eventName.text = event_loaded.eventName;
        image.sprite = event_loaded.image;
        description.text = event_loaded.description;
        optionCount = event_loaded.optionText.Count;

        for (int i = 0; i < optionCount; i++){
            GameObject tmp = Instantiate(optionButton, canvas.transform);
            tmp.transform.position = Vector3.zero;
            tmp.transform.localPosition = Vector3.zero;
            tmp.transform.localPosition += new Vector3(500, -450 + (optionCount - i - 1) * 80, 0);
            tmp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = event_loaded.optionText[i];
            tmp.name = "optionButton";
            if(i==0)
                tmp.GetComponent<Button>().onClick.AddListener(delegate { ButtonPressed1(); });
            else if(i==1)
                tmp.GetComponent<Button>().onClick.AddListener(delegate { ButtonPressed2(); });
            else if(i==2)
                tmp.GetComponent<Button>().onClick.AddListener(delegate { ButtonPressed3(); });
            else if(i==3)
                tmp.GetComponent<Button>().onClick.AddListener(delegate { ButtonPressed4(); });
            if(i==4)
                tmp.GetComponent<Button>().onClick.AddListener(delegate { ButtonPressed5(); });
        }
        Button_event_check();
    }

    public void Reset(){
        List<GameObject> tmp = new List<GameObject>();
        foreach(Transform child in canvas.transform){
            if (child.name == "optionButton") tmp.Add(child.gameObject);
        }
        foreach(GameObject obj in tmp){
            obj.SetActive(false);
            Destroy(obj);
        }
        description.text = "";
        optionCount=0;
        eventName.text = "";
    }

    public void ButtonPressed1(){
        float random_value = Random.value;
        switch (event_loaded.id)
        {
            case 101:   
                Reset();
                LoadEvent(102,EventClass.Type.story);
                break;
            case 102:   
                Global.AddSan(-10);
                FindObjectOfType<Bag_System>().Add_Item("1","relic",true);
                Reset();
                LoadEvent(103,EventClass.Type.story);
                break;
            case 103: 
                this.gameObject.SetActive(false);  
                break;
            case 104: 
                this.gameObject.SetActive(false);  
                break;
            case 105:
                this.gameObject.SetActive(false);
                break;
            case 201:   
                Reset();
                LoadEvent(202,EventClass.Type.story);
                break;
            case 202:   
                FindObjectOfType<Bag_System>().Add_Item("2","relic",true);
                Reset();
                LoadEvent(203,EventClass.Type.story);
                break;
            case 203:   
                Global.AddMoney(-100);
                Reset();
                LoadEvent(204,EventClass.Type.story);
                break;
            case 204:   
                this.gameObject.SetActive(false);
                break;
            case 205:   
                this.gameObject.SetActive(false);
                break;
            case 301:   
                Global.AddSan(-10);
                FindObjectOfType<Bag_System>().Add_Item("4","relic",true);
                Reset();
                LoadEvent(302,EventClass.Type.story);
                break;
            case 302:   
                Reset();
                LoadEvent(303,EventClass.Type.story);
                break;
            case 303: 
                FindObjectOfType<Bag_System>().Add_Item("3","relic",true);
                Global.AddSan(-10);
                this.gameObject.SetActive(false);
                break;
            case 401:   
                this.gameObject.SetActive(false);
                SceneManager.LoadScene("StartMenu");
                break;
            case 501:   
                this.gameObject.SetActive(false);
                SceneManager.LoadScene("StartMenu");
                break;
            case 1000:
                FindObjectOfType<Bag_System>().Show_Equipment_list(1,1);
                break;
            case 2000:
                if(random_value<0.33)
                {
                    random_value = Random.value;
                    if(random_value<0.45)Global.AddMoney(30);
                    else if(random_value<0.70 || FindObjectOfType<Bag_System>().Bag_Full()==true)
                    {
                        FindObjectOfType<Bag_System>().Add_Random_Relic();
                    }
                    else FindObjectOfType<Bag_System>().Add_Random_Equipment();
                }
                else if(random_value<0.66) Global.AddSan(-10);
                else FindObjectOfType<Map_Node_Action>().click_action_battle();
                this.gameObject.SetActive(false);
                break;
            case 3000:
                if(random_value<0.3)Global.AddSan(-10);
                Reset();
                LoadEvent(3001,EventClass.Type.normal);
                break;
            case 3001:
                if(random_value<0.6)Global.AddSan(-20);
                Reset();
                LoadEvent(3002,EventClass.Type.normal);
                break;
            case 3002:
                if(random_value<0.99)Global.AddSan(-40);
                Reset();
                LoadEvent(3003,EventClass.Type.normal);
                break;
            case 3003:
                FindObjectOfType<Bag_System>().Add_Item("5","relic");
                this.gameObject.SetActive(false);
                break;
            case 4000:
                if(Global.sanity >= 50) 
                {
                    Reset();
                    LoadEvent(4001,EventClass.Type.normal);
                }
                else 
                this.gameObject.SetActive(false);
                break;
            case 4001:
                this.gameObject.SetActive(false);
                break;
            case 5000:
                Reset();
                LoadEvent(5001,EventClass.Type.normal);
                break;
            case 5001:
                Global.AddSan(-10);
                Global.AddItemToBag(6,"relic");
                // FindObjectOfType<Bag_System>().Add_Item("6","relic");
                this.gameObject.SetActive(false);
                break;
            case 6000:
                if(FindObjectOfType<Bag_System>().Bag_Full())return;
                FindObjectOfType<Bag_System>().Add_Item("301","item");
                // random_value = 0;
                // Debug.Log("random value"+random_value);
                if(random_value>0.5f) Global.AddMaxHp(10);
                else Update_Card();
                this.gameObject.SetActive(false);
                break;
            case 6001:
                if(FindObjectOfType<Bag_System>().Bag_Full())return;
                FindObjectOfType<Bag_System>().Add_Item("302","item");
                if(random_value>0.5f) Global.AddMaxHp(10);
                else Update_Card();
                this.gameObject.SetActive(false);
                break;
            case 6002:
                if(FindObjectOfType<Bag_System>().Bag_Full())return;
                FindObjectOfType<Bag_System>().Add_Item("303","item");
                if(random_value>0.5f) Global.AddMaxHp(10);
                else Update_Card();
                
                bool relic_1 = FindObjectOfType<Bag_System>().Have_Item("item",301.ToString());
                bool relic_2 = FindObjectOfType<Bag_System>().Have_Item("item",302.ToString());
                bool relic_3 = FindObjectOfType<Bag_System>().Have_Item("item",303.ToString());
                if(relic_1&&relic_2&&relic_3)
                {
                    FindObjectOfType<Bag_System>().Bag_del_item(301,"item");
                    FindObjectOfType<Bag_System>().Bag_del_item(302,"item");
                    FindObjectOfType<Bag_System>().Bag_del_item(303,"item");
                    Reset();
                    LoadEvent(6003,EventClass.Type.normal);
                }
                else this.gameObject.SetActive(false);
                break;
            case 6003:
                FindObjectOfType<Bag_System>().Add_Item("7","relic");
                this.gameObject.SetActive(false);
                break;
            case 7000:
                Global.AddHp(10);
                Global.AddSan(10);
                this.gameObject.SetActive(false);
                break;
            case 8000:
                FindObjectOfType<Bag_System>().Add_Item("8","relic");
                Global.AddSan(-5);
                this.gameObject.SetActive(false);
                break;
            case 8001:
                this.gameObject.SetActive(false);
                break;
            case 9000:
                Reset();
                if(random_value<0.5)
                {
                    Global.AddSan(-10);
                    Update_Card();
                    Update_Card();
                    LoadEvent(9001,EventClass.Type.normal);
                }
                else
                {
                    Update_Card();
                    Global.AddHp(-10);
                    LoadEvent(9002,EventClass.Type.normal);
                }
                break;
            case 9001:
                this.gameObject.SetActive(false);
                break;
            case 9002:
                this.gameObject.SetActive(false);
                break;
            case 10000:
                if(random_value<0.5)
                {
                    Global.AddMoney(30);
                }
                else
                {
                    Global.AddHp(-10);
                }
                this.gameObject.SetActive(false);
                break;
            case 11000:
                Global.AddSan(-10);
                Reset();
                LoadEvent(11001,EventClass.Type.normal);
                break;
            case 11001:
                this.gameObject.SetActive(false);
                break;
            case 11002:
                Update_Card();
                Global.AddSan(-10);
                this.gameObject.SetActive(false);
                break;
            case 12000:
                if(FindObjectOfType<Bag_System>().Bag_Full())return;
                FindObjectOfType<Bag_System>().Add_Item("304","item");
                // random_value = 0;
                // Debug.Log("random value"+random_value);
                if(random_value>0.5f) Global.AddMaxHp(10);
                else Update_Card();
                this.gameObject.SetActive(false);
                
                break;
            case 12001:
                if(FindObjectOfType<Bag_System>().Bag_Full())return;
                FindObjectOfType<Bag_System>().Add_Item("305","item");
                // random_value = 0;
                // Debug.Log("random value"+random_value);
                if(random_value>0.5f) Global.AddMaxHp(10);
                else Update_Card();
                this.gameObject.SetActive(false);
                
                break;
            case 12002:
                if(FindObjectOfType<Bag_System>().Bag_Full())return;
                FindObjectOfType<Bag_System>().Add_Item("306","item");
                // random_value = 0;
                // Debug.Log("random value"+random_value);
                if(random_value>0.5f) Global.AddMaxHp(10);
                else Update_Card();
                
                bool item_1 = FindObjectOfType<Bag_System>().Have_Item("item",304.ToString());
                bool item_2 = FindObjectOfType<Bag_System>().Have_Item("item",305.ToString());
                bool item_3 = FindObjectOfType<Bag_System>().Have_Item("item",306.ToString());
                if(item_1&&item_2&&item_3)
                {
                    FindObjectOfType<Bag_System>().Bag_del_item(304,"item");
                    FindObjectOfType<Bag_System>().Bag_del_item(305,"item");
                    FindObjectOfType<Bag_System>().Bag_del_item(306,"item");
                    Reset();
                    LoadEvent(12003,EventClass.Type.normal);
                }
                else this.gameObject.SetActive(false);
                break;
            case 12003:
                FindObjectOfType<Bag_System>().Add_Item("9","relic");
                this.gameObject.SetActive(false);
                break;
            case 13000:
                Global.AddSan(-10);
                this.gameObject.SetActive(false);
                break;
            case 14000:
                Global.AddSan(-10);
                Reset();
                LoadEvent(14001,EventClass.Type.normal);
                break;
            case 14001:
                this.gameObject.SetActive(false);
                break;
            case 14002:
                this.gameObject.SetActive(false);
                break;
            case 14003:
                this.gameObject.SetActive(false);
                break;
            case 15000:
                Global.AddHp(-10);
                Reset();
                LoadEvent(15001,EventClass.Type.normal);
                break;
            case 15001:
                this.gameObject.SetActive(false);
                break;
            case 15002:
                this.gameObject.SetActive(false);
                break;
            case 16000:
                this.gameObject.SetActive(false);
                break;
            case 18000:
                Update_Card();
                Global.AddSan(-10);
                this.gameObject.SetActive(false);
                break;
            case 19000:
                Global.AddSan(10);
                FindObjectOfType<Bag_System>().Remove_Random_Relic();
                this.gameObject.SetActive(false);
                break;
            case 20000:
                Add_Attack_Card();
                this.gameObject.SetActive(false);
                break;
            case 21000:
                this.gameObject.SetActive(false);
                break;
            case 22000:
                Global.AddSan(-10);
                this.gameObject.SetActive(false);
                break;
            case 23000:
                Global.AddSan(-10);
                this.gameObject.SetActive(false);
                break;
            case 24000:
                FindObjectOfType<Bag_System>().Add_Item("12","relic");
                this.gameObject.SetActive(false);
                break;
            case 25000:
                if((float)Global.sanity/Global.max_sanity > 0.75f)Global.AddSan(-10);
                if((float)Global.player_hp/Global.player_max_hp > 0.75f)Global.AddHp(-10);
                else 
                {
                    Global.AddSan(10);
                    Global.AddHp(10);
                }
                this.gameObject.SetActive(false);
                break;
            case 26000:
                this.gameObject.SetActive(false);
                break;
            case 27000:
                float p = (float)Global.sanity/Global.max_sanity;
                if(p>0.5)Global.AddSan(-10);
                else
                {
                    Add_Card();
                    Global.AddMoney(100);
                }
                this.gameObject.SetActive(false);
                break;
            case 28000:
                this.gameObject.SetActive(false);
                break;
            case 29000:
                this.gameObject.SetActive(false);
                break;
            case 30000:
                this.gameObject.SetActive(false);
                break;
            default:
                break;
        }

    }
    public void ButtonPressed2(){
        float random_value = Random.value;
        switch (event_loaded.id)
        {
            case 101:   
                Reset();
                LoadEvent(105,EventClass.Type.story);
                break;
            case 102:   
                Global.AddSan(-5);
                Reset();
                LoadEvent(104,EventClass.Type.story);
                break;
            case 203: 
                Global.AddHp(-10);
                Reset();
                LoadEvent(205,EventClass.Type.story);  
                break;
            case 301:   
                this.gameObject.SetActive(false);
                break;
            case 303: 
                this.gameObject.SetActive(false);
                break;
            case 1000:
                FindObjectOfType<Bag_System>().Show_Equipment_list(2,2);
                break;
            case 2000: 
                this.gameObject.SetActive(false);
                break;
            case 3000: 
                this.gameObject.SetActive(false);
                break;
            case 3001: 
                this.gameObject.SetActive(false);
                break;
            case 3002: 
                this.gameObject.SetActive(false);
                break;
            case 3003:
                this.gameObject.SetActive(false);
                break;
            case 5000:
                this.gameObject.SetActive(false);
                break;
            case 5001:
                random_value = Random.value;
                bool v = FindObjectOfType<Bag_System>().Bag_Full();
                if(random_value < 0.5 && v==false)FindObjectOfType<Bag_System>().Add_Random_Equipment();
                else FindObjectOfType<Bag_System>().Add_Random_Relic();
                this.gameObject.SetActive(false);
                break;
            case 6000:
                this.gameObject.SetActive(false);
                break;
            case 6001:
                this.gameObject.SetActive(false);
                break;
            case 6002:
                this.gameObject.SetActive(false);
                break;
            case 8000:
                Global.AddHp(-5);
                Reset();
                LoadEvent(8001,EventClass.Type.normal);
                break;
            case 9000:
                this.gameObject.SetActive(false);
                break;
            case 10000:
                this.gameObject.SetActive(false);
                break;
            case 11000:
                this.gameObject.SetActive(false);
                break;
            case 11002:
                Remove_Card();
                Global.AddSan(-15);
                FindObjectOfType<Bag_System>().Remove_Random_Equipment();
                this.gameObject.SetActive(false);
                break;
            case 12000:
                this.gameObject.SetActive(false);
                break;
            case 12001:
                this.gameObject.SetActive(false);
                break;
            case 12002:
                this.gameObject.SetActive(false);
                break;
            case 13000:
                //待做
                this.gameObject.SetActive(false);
                break;
            case 14000:
                Global.AddSan(10);
                Global.AddHp(10);
                Reset();
                LoadEvent(14002,EventClass.Type.normal);
                break;
            case 15000:
                Reset();
                LoadEvent(15002,EventClass.Type.normal);
                break;
            case 16000:
                this.gameObject.SetActive(false);
                break;
            case 18000:
                this.gameObject.SetActive(false);
                break;
            case 19000:
                FindObjectOfType<Bag_System>().Remove_Random_Equipment();
                this.gameObject.SetActive(false);
                break;
            case 20000:
                Add_Skill_Card();
                this.gameObject.SetActive(false);
                break;
            case 25000:
                
                this.gameObject.SetActive(false);
                break;
            case 27000:
                float p = (float)Global.sanity/Global.max_sanity;
                if(p>0.5)Global.AddSan(-10);
                else Global.AddSan(10);
                this.gameObject.SetActive(false);
                break;
            case 28000:
                this.gameObject.SetActive(false);
                break;
            case 30000:
                this.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    public void ButtonPressed3(){
        switch (event_loaded.id)
        {
            case 1000:
                FindObjectOfType<Bag_System>().Show_Equipment_list(1,3);
                break;
            case 5001:
                this.gameObject.SetActive(false);
                break;
            case 14000:
                Reset();
                LoadEvent(14003,EventClass.Type.normal);
                break;
            case 16000:
                this.gameObject.SetActive(false);
                break;
            case 19000:
                float rv = Random.value;
                if(rv > 0.5f)FindObjectOfType<Bag_System>().Add_Random_Relic();
                else Add_Card();
                this.gameObject.SetActive(false);
                break;
            case 28000:
                this.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    public void ButtonPressed4(){
        switch (event_loaded.id)
        {
            case 1000:
                this.gameObject.SetActive(false);
                break;
            case 16000:
                this.gameObject.SetActive(false);
                break;
            case 19000:
                Global.AddHp(-10);
                Global.AddSan(10);
                this.gameObject.SetActive(false);
                break;
            case 28000:
                this.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    public void ButtonPressed5(){
        switch (event_loaded.id)
        {
            case 19000:
                Global.ShowPlayerCards(Global.GetPlayerDeck(),Remove_Card_19000,true);
                // this.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    void Button_event_check()
    {
        List<GameObject> tmp = new List<GameObject>();
        foreach(Transform child in canvas.transform){
            if (child.name == "optionButton"&&child.gameObject.activeSelf==true) tmp.Add(child.gameObject);
        }
        Debug.Log("now count "+tmp.Count);
        switch (event_loaded.id)
        {
            case 203:
                if(Global.money<100)
                {
                    
                    tmp[0].GetComponent<Button>().interactable = false;
                }
                break;
            case 1000:
                int[] r_list = FindObjectOfType<Bag_System>().equipment_type_num();
                if(r_list[0]<2&&r_list[1]<2&&r_list[2]<2)
                {
                    tmp[1].GetComponent<Button>().interactable = false;
                }
                List<int> tmp_list = FindObjectOfType<Bag_System>().Return_All_Equipment();
                if(tmp_list.Count==0)
                {
                    for(int i=0;i<3;i++)
                    {
                        tmp[i].GetComponent<Button>().interactable = false;
                    }
                }
                break;
            case 19000:
                List<int> r = FindObjectOfType<Bag_System>().Return_All_Relic();
                if(r.Count == 0)tmp[0].GetComponent<Button>().interactable = false;
                r = FindObjectOfType<Bag_System>().Return_All_Equipment();
                if(r.Count == 0)tmp[1].GetComponent<Button>().interactable = false;

                break;
            default:
                break;
        }
    }
    IEnumerator wait_one_frame()
    {
        yield return new WaitForEndOfFrame();
    }

    void Update_Card()
    {
        List<Card> player_deck = Global.GetPlayerDeck();
        Card card = player_deck[Random.Range(0,player_deck.Count)];
        Global.UpgradeCard(card);
    }
    void Add_Card()
    {
        float rv = Random.value;
        if(rv > 0.4)Add_Skill_Card();
        else Add_Attack_Card();
    }
    void Add_Attack_Card()
    {
        int index = Random.Range(0,all_attack_cards.Count);
        Card card = all_attack_cards[index];
        Global.PlayerDeck_Add(card);
    }
    void Add_Skill_Card()
    {
        int index = Random.Range(0,all_skill_cards.Count);
        Card card = all_skill_cards[index];
        Global.PlayerDeck_Add(card);
    }
    void Remove_Card()
    {
        List<Card> player_deck = Global.GetPlayerDeck();
        Card card = player_deck[Random.Range(0,player_deck.Count)];
        Global.PlayerDeck_Remove(card);
    }
    public void Remove_Card_19000(Card card)
    {
        Global.PlayerDeck_Remove(card);
        this.gameObject.SetActive(false);
    }
}
