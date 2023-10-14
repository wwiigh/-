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
    [SerializeField] GameObject ShowPanel;
    [SerializeField] TMP_Text ShowText;
    [SerializeField] AudioSource audioSource;

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
        ShowPanel.SetActive(false);
        
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
        audioSource.Play();
        switch (event_loaded.id)
        {
            case 101:   
                Reset();
                LoadEvent(102,EventClass.Type.story);
                break;
            case 102:   
                Global.AddSan(-10);
                FindObjectOfType<Bag_System>().Add_Item("1","relic",true);
                StartCoroutine(ShowInformation_NOTDisabe("理智減少了10，獲得遺物"+getRelicName(1)));
                Reset();
                LoadEvent(103,EventClass.Type.story);
                break;
            case 103: 
                StartCoroutine(nameof(transition)); 
                break;
            case 104: 
                StartCoroutine(nameof(transition)); 
                break;
            case 105:
                StartCoroutine(nameof(transition)); 
                break;
            case 201:   
                Reset();
                LoadEvent(202,EventClass.Type.story);
                break;
            case 202:   
                FindObjectOfType<Bag_System>().Add_Item("2","relic",true);
                StartCoroutine(ShowInformation_NOTDisabe("獲得遺物"+getRelicName(2)));
                Reset();
                LoadEvent(203,EventClass.Type.story);
                break;
            case 203:   
                Global.AddMoney(-100);
                StartCoroutine(ShowInformation_NOTDisabe("減少了金錢100"));
                Reset();
                LoadEvent(204,EventClass.Type.story);
                break;
            case 204:   
                StartCoroutine(nameof(transition)); 
                break;
            case 205:   
                StartCoroutine(nameof(transition)); 
                break;
            case 301:   
                Global.AddSan(-10);
                FindObjectOfType<Bag_System>().Add_Item("4","relic",true);
                StartCoroutine(ShowInformation_NOTDisabe("理智減少了10，獲得遺物"+getRelicName(4)));
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
                StartCoroutine(ShowInformation("理智減少了10，獲得遺物"+getRelicName(3)));
                StartCoroutine(nameof(transition)); 
                break;
            case 401:   
                StartCoroutine(nameof(transition)); 
                SceneManager.LoadScene("StartMenu");
                break;
            case 501:   
                StartCoroutine(nameof(transition)); 
                SceneManager.LoadScene("StartMenu");
                break;
            case 1000:
                FindObjectOfType<Bag_System>().Show_Equipment_list(1,1);
                break;
            case 2000:
                if(random_value<0.33)
                {
                    string text2000 = "";
                    random_value = Random.value;
                    if(random_value<0.45)
                    {
                        Global.AddMoney(30);
                        text2000 += "獲得了金錢30";
                    }else if(random_value<0.70 || FindObjectOfType<Bag_System>().Bag_Full()==true)
                    {
                        int randomrelic = FindObjectOfType<Bag_System>().Add_Random_Relic();
                        text2000 += "獲得了遺物"+getRelicName(randomrelic);
                    }
                    else 
                    {
                        int randomequip = FindObjectOfType<Bag_System>().Add_Random_Equipment();
                        text2000 += "獲得了裝備"+getRelicName(randomequip);
                    }
                    StartCoroutine(ShowInformation(text2000));
                }
                else if(random_value<0.66) 
                {
                    Global.AddSan(-10);
                    StartCoroutine(ShowInformation("理智減少了10"));
                }
                else 
                {
                    Debug.Log("進入了戰鬥");
                    Node.ActiveNode.StopAllCoroutines();
                    Node.ActiveNode.click_action_battle_normal();
                    
                    StartCoroutine(nameof(transition)); 
                }
                break;
            case 3000:
                if(random_value<0.3)
                {
                    Global.AddSan(-10);
                    StartCoroutine(ShowInformation_NOTDisabe("你往前了，但理智減少了10"));
                }
                else
                {
                    StartCoroutine(ShowInformation_NOTDisabe("你往前了"));
                }
                Reset();
                LoadEvent(3001,EventClass.Type.normal);
                break;
            case 3001:
                if(random_value<0.6)
                {
                    Global.AddSan(-20);
                    StartCoroutine(ShowInformation_NOTDisabe("你往前了，但理智減少了20"));
                }
                else
                {
                    StartCoroutine(ShowInformation_NOTDisabe("你往前了"));
                }
                Reset();
                LoadEvent(3002,EventClass.Type.normal);
                break;
            case 3002:
                if(random_value<0.99)
                {
                    Global.AddSan(-40);
                    StartCoroutine(ShowInformation_NOTDisabe("你往前了，但理智減少了40"));
                }
                else
                {
                    StartCoroutine(ShowInformation_NOTDisabe("你往前了"));
                }
                Reset();
                LoadEvent(3003,EventClass.Type.normal);
                break;
            case 3003:
                FindObjectOfType<Bag_System>().Add_Item("5","relic");
                string text3000 = "獲得了遺物"+getRelicName(5);
                StartCoroutine(ShowInformation(text3000));


                // this.gameObject.SetActive(false);
                break;
            case 4000:
                if(Global.sanity >= 50) 
                {
                    Reset();
                    LoadEvent(4001,EventClass.Type.normal);
                }
                else 
                StartCoroutine(nameof(transition)); 
                break;
            case 4001:
                StartCoroutine(nameof(transition)); 
                break;
            case 5000:
                Reset();
                LoadEvent(5001,EventClass.Type.normal);
                break;
            case 5001:
                Global.AddSan(-10);
                Global.AddItemToBag(6,"relic");
                string text5001 = "獲得了遺物"+getRelicName(6)+"，理智減少了10";
                StartCoroutine(ShowInformation(text5001));
                // FindObjectOfType<Bag_System>().Add_Item("6","relic");
                // this.gameObject.SetActive(false);
                break;
            case 6000:
                if(FindObjectOfType<Bag_System>().Bag_Full())return;
                FindObjectOfType<Bag_System>().Add_Item("301","item");
                string text6000 = "獲得了道具"+getItemName(301);
                // random_value = 0;
                // Debug.Log("random value"+random_value);
                if(random_value>0.5f)
                {
                    Global.AddMaxHp(10);
                    text6000 += "，最大生命值提升10";
                }
                else
                {
                    Card card6000 = Update_Card();
                    text6000 += "，升級了" + card6000.cardName;
                }// this.gameObject.SetActive(false);
                StartCoroutine(ShowInformation(text6000));
                break;
            case 6001:
                if(FindObjectOfType<Bag_System>().Bag_Full())return;
                FindObjectOfType<Bag_System>().Add_Item("302","item");
                string text6001 = "獲得了道具"+getItemName(302);
                if(random_value>0.5f)
                {
                    Global.AddMaxHp(10);
                    text6001 += "，最大生命值提升10";
                }
                else
                {
                    Card card6000 = Update_Card();
                    text6001 += "，升級了" + card6000.cardName;
                }// this.gameObject.SetActive(false);
                StartCoroutine(ShowInformation(text6001));
                // this.gameObject.SetActive(false);
                break;
            case 6002:
                if(FindObjectOfType<Bag_System>().Bag_Full())return;
                FindObjectOfType<Bag_System>().Add_Item("303","item");
                string text6002 = "獲得了道具"+getItemName(303);
                if(random_value>0.5f)
                {
                    Global.AddMaxHp(10);
                    text6002 += "，最大生命值提升10";
                }
                else
                {
                    Card card6000 = Update_Card();
                    text6002 += "，升級了" + card6000.cardName;
                }// this.gameObject.SetActive(false);
                
                bool relic_1 = FindObjectOfType<Bag_System>().Have_Item("item",301.ToString());
                bool relic_2 = FindObjectOfType<Bag_System>().Have_Item("item",302.ToString());
                bool relic_3 = FindObjectOfType<Bag_System>().Have_Item("item",303.ToString());
                if(relic_1&&relic_2&&relic_3)
                {
                    FindObjectOfType<Bag_System>().Bag_del_item(301,"item");
                    FindObjectOfType<Bag_System>().Bag_del_item(302,"item");
                    FindObjectOfType<Bag_System>().Bag_del_item(303,"item");
                    text6002 += "，聖人骨骸消失了";
                    Reset();
                    LoadEvent(6003,EventClass.Type.normal);
                    StartCoroutine(ShowInformation_NOTDisabe(text6002));
                }
                else
                {
                    StartCoroutine(ShowInformation(text6002));

                }
                break;
            case 6003:
                FindObjectOfType<Bag_System>().Add_Item("7","relic");
                string text6003 = "獲得了遺物"+getRelicName(7);
                StartCoroutine(ShowInformation(text6003));
                // this.gameObject.SetActive(false);
                break;
            case 7000:
                Global.AddHp(10);
                Global.AddSan(10);
                StartCoroutine(ShowInformation("血量恢復了10，理智恢復了10"));
                // this.gameObject.SetActive(false);
                break;
            case 8000:
                FindObjectOfType<Bag_System>().Add_Item("8","relic");
                Global.AddSan(-5);
                string text8000 = "獲得了遺物"+getRelicName(8) + "，理智減少了5";
                StartCoroutine(ShowInformation(text8000));
                
                // this.gameObject.SetActive(false);

                break;
            case 8001:
                StartCoroutine(nameof(transition)); 
                break;
            case 9000:
                Reset();
                if(random_value<0.5)
                {
                    Global.AddSan(-10);
                    string text9000 = "理智減少了10";
                    Card card9000 = Update_Card();
                    text9000 += "，升級了"+card9000.cardName;
                    card9000 = Update_Card();
                    text9000 += "，升級了"+card9000.cardName;

                    StartCoroutine(ShowInformation_NOTDisabe(text9000));
                    LoadEvent(9001,EventClass.Type.normal);
                }
                else
                {
                    Card card9000 = Update_Card();
                    Global.AddHp(-10);
                    string text9000 = "血量減少了10";
                    text9000 += "，升級了"+card9000.cardName;
                    StartCoroutine(ShowInformation_NOTDisabe(text9000));
                    LoadEvent(9002,EventClass.Type.normal);
                }
                break;
            case 9001:
                StartCoroutine(nameof(transition)); 
                break;
            case 9002:
                StartCoroutine(nameof(transition)); 
                break;
            case 10000:
                string text10000 = "";
                if(random_value<0.5)
                {
                    Global.AddMoney(30);
                    text10000 += "獲得了金錢30";
                }
                else
                {
                    Global.AddHp(-10);
                    text10000 += "血量減少了10";
                }
                StartCoroutine(ShowInformation(text10000));
                // this.gameObject.SetActive(false);
                break;
            case 11000:
                Global.AddSan(-10);
                StartCoroutine(ShowInformation_NOTDisabe("理智減少了10"));
                Reset();
                LoadEvent(11001,EventClass.Type.normal);
                break;
            case 11001:
                StartCoroutine(nameof(transition)); 
                break;
            case 11002:
                Card card11002 = Update_Card();
                Global.AddSan(-10);
                StartCoroutine(ShowInformation("升級了"+card11002.cardName+"，理智減少了10"));
                // this.gameObject.SetActive(false);
                break;
            case 12000:
                if(FindObjectOfType<Bag_System>().Bag_Full())return;
                FindObjectOfType<Bag_System>().Add_Item("304","item");
                // random_value = 0;
                // Debug.Log("random value"+random_value);
                string text12000 = "獲得了道具"+getItemName(304);
                // random_value = 0;
                // Debug.Log("random value"+random_value);
                if(random_value>0.5f)
                {
                    Global.AddMaxHp(10);
                    text12000 += "，最大生命值提升10";
                }
                else
                {
                    Card card6000 = Update_Card();
                    text12000 += "，升級了" + card6000.cardName;
                }// this.gameObject.SetActive(false);
                StartCoroutine(ShowInformation(text12000));
                // this.gameObject.SetActive(false);
                
                break;
            case 12001:
                if(FindObjectOfType<Bag_System>().Bag_Full())return;
                FindObjectOfType<Bag_System>().Add_Item("305","item");
                string text12001 = "獲得了道具"+getItemName(305);
                // random_value = 0;
                // Debug.Log("random value"+random_value);
                if(random_value>0.5f)
                {
                    Global.AddMaxHp(10);
                    text12001 += "，最大生命值提升10";
                }
                else
                {
                    Card card6000 = Update_Card();
                    text12001 += "，升級了" + card6000.cardName;
                }// this.gameObject.SetActive(false);
                StartCoroutine(ShowInformation(text12001));
                // random_value = 0;
                // Debug.Log("random value"+random_value);

                
                break;
            case 12002:
                if(FindObjectOfType<Bag_System>().Bag_Full())return;
                FindObjectOfType<Bag_System>().Add_Item("306","item");
                // random_value = 0;
                // Debug.Log("random value"+random_value);
                string text12002 = "獲得了道具"+getItemName(306);
                // random_value = 0;
                // Debug.Log("random value"+random_value);
                if(random_value>0.5f)
                {
                    Global.AddMaxHp(10);
                    text12002 += "，最大生命值提升10";
                }
                else
                {
                    Card card6000 = Update_Card();
                    text12002 += "，升級了" + card6000.cardName;
                }// this.gameObject.SetActive(false);
                bool item_1 = FindObjectOfType<Bag_System>().Have_Item("item",304.ToString());
                bool item_2 = FindObjectOfType<Bag_System>().Have_Item("item",305.ToString());
                bool item_3 = FindObjectOfType<Bag_System>().Have_Item("item",306.ToString());
                if(item_1&&item_2&&item_3)
                {
                    FindObjectOfType<Bag_System>().Bag_del_item(304,"item");
                    FindObjectOfType<Bag_System>().Bag_del_item(305,"item");
                    FindObjectOfType<Bag_System>().Bag_del_item(306,"item");
                    text12002 += "，手稿消失了";
                    Reset();
                    StartCoroutine(ShowInformation_NOTDisabe(text12002));
                    LoadEvent(12003,EventClass.Type.normal);
                }
                else
                {
                    // this.gameObject.SetActive(false);
                    StartCoroutine(ShowInformation(text12002));
                
                }
                break;
            case 12003:
                FindObjectOfType<Bag_System>().Add_Item("9","relic");
                string text12003 = "獲得了遺物"+getRelicName(9);
                StartCoroutine(ShowInformation(text12003));
                // this.gameObject.SetActive(false);
                break;
            case 13000:
                Global.AddSan(-10);
                StartCoroutine(ShowInformation("理智減少了10"));
                break;
            case 14000:
                Global.AddSan(-10);
                Reset();
                StartCoroutine(ShowInformation_NOTDisabe("理智減少10"));
                LoadEvent(14001,EventClass.Type.normal);
                break;
            case 14001:
                StartCoroutine(nameof(transition)); 
                break;
            case 14002:
                StartCoroutine(nameof(transition)); 
                break;
            case 14003:
                StartCoroutine(nameof(transition)); 
                break;
            case 15000:
                Global.AddHp(-10);
                Reset();
                StartCoroutine(ShowInformation_NOTDisabe("生命減少10"));
                LoadEvent(15001,EventClass.Type.normal);
                break;
            case 15001:
                StartCoroutine(nameof(transition)); 
                break;
            case 15002:
                StartCoroutine(nameof(transition)); 
                break;
            case 16000:
                StartCoroutine(nameof(transition)); 
                break;
            case 18000:
                Card card18000 = Update_Card();
                Global.AddSan(-10);
                StartCoroutine(ShowInformation("理智減少了10，"+"升級了"+card18000.cardName));
                // this.gameObject.SetActive(false);
                break;
            case 19000:
                Global.AddSan(10);
                int relic19000= FindObjectOfType<Bag_System>().Remove_Random_Relic();
                // this.gameObject.SetActive(false);
                StartCoroutine(ShowInformation("理智增加了10，"+"移除了"+getRelicName(relic19000)));
                break;
            case 20000:
                Card card2000 = Add_Attack_Card();
                StartCoroutine(ShowInformation("得到了"+card2000.cardName));
                // this.gameObject.SetActive(false);
                break;
            case 21000:
                StartCoroutine(nameof(transition)); 
                break;
            case 22000:
                Global.AddSan(-10);
                StartCoroutine(ShowInformation("理智減少了10"));
                // this.gameObject.SetActive(false);
                break;
            case 23000:
                Global.AddSan(-10);
                StartCoroutine(ShowInformation("理智減少了10"));
                // this.gameObject.SetActive(false);
                break;
            case 24000:
                FindObjectOfType<Bag_System>().Add_Item("12","relic");
                StartCoroutine(ShowInformation("獲得了遺物"+getRelicName(12)));
                // this.gameObject.SetActive(false);
                break;
            case 25000:
                string text25000 = "";
                int san = 0;
                int hp = 0;
                if((float)Global.sanity/Global.max_sanity > 0.75f)
                {
                    // Global.AddSan(-10);
                    san += -10;
                    // text25000 += "理智減少了10，";
                }
                if((float)Global.player_hp/Global.player_max_hp > 0.75f)
                {
                    // Global.AddHp(-10);
                    hp += -10;
                    // text25000 += "血量減少了10";
                }
                else 
                {
                    // Global.AddSan(10);
                    // Global.AddHp(10);
                    hp+=10;
                    san+=10;
                    // text25000 += "血量恢復了10，理智減少了10";
                }
                Global.AddSan(san);
                Global.AddHp(hp);
                if(hp>0)text25000 += "血量恢復了"+hp;
                else if(hp<0)text25000 += "血量減少了"+hp;
                if(san>0)text25000 += " 理智恢復了"+san;
                else if(san<0)text25000 += " 理智減少了"+san;
                StartCoroutine(ShowInformation(text25000));
                // this.gameObject.SetActive(false);
                break;
            case 26000:
                StartCoroutine(nameof(transition)); 
                break;
            case 27000:
                string text27000 = "";
                float p = (float)Global.sanity/Global.max_sanity;
                if(p>0.5)
                {
                    Global.AddSan(-10);
                    text27000 += "理智減少了10";
                }else
                {
                    Card card = Add_Card();
                    Global.AddMoney(100);
                    text27000 += "得到了卡片"+card.cardName + "，得到了金幣100";
                }
                StartCoroutine(ShowInformation(text27000));
                // this.gameObject.SetActive(false);
                break;
            case 28000:
                StartCoroutine(nameof(transition)); 
                break;
            case 29000:
                StartCoroutine(nameof(transition)); 
                break;
            case 30000:
                StartCoroutine(nameof(transition)); 
                break;
            default:
                break;
        }

    }
    public void ButtonPressed2(){
        audioSource.Play();
        float random_value = Random.value;
        switch (event_loaded.id)
        {
            case 101:   
                Reset();
                LoadEvent(105,EventClass.Type.story);
                break;
            case 102:   
                Global.AddSan(-5);
                StartCoroutine(ShowInformation_NOTDisabe("理智減少了5"));
                Reset();
                LoadEvent(104,EventClass.Type.story);
                break;
            case 203: 
                Global.AddHp(-10);
                StartCoroutine(ShowInformation_NOTDisabe("血量減少了10"));
                Reset();
                LoadEvent(205,EventClass.Type.story);  
                break;
            case 301:   
                StartCoroutine(nameof(transition)); 
                break;
            case 303: 
                StartCoroutine(nameof(transition)); 
                break;
            case 1000:
                FindObjectOfType<Bag_System>().Show_Equipment_list(2,2);
                break;
            case 2000: 
                StartCoroutine(nameof(transition)); 
                break;
            case 3000: 
                StartCoroutine(nameof(transition)); 
                break;
            case 3001: 
                StartCoroutine(nameof(transition)); 
                break;
            case 3002: 
                StartCoroutine(nameof(transition)); 
                break;
            case 3003:
                StartCoroutine(nameof(transition)); 
                break;
            case 5000:
                StartCoroutine(nameof(transition)); 
                break;
            case 5001:
                random_value = Random.value;
                bool v = FindObjectOfType<Bag_System>().Bag_Full();
                if(random_value < 0.5 && v==false)
                {
                    int id5001 = FindObjectOfType<Bag_System>().Add_Random_Equipment();
                    string text5001 = "獲得了裝備"+getEquipmentName(id5001);
                    StartCoroutine(ShowInformation(text5001));
                }else
                {
                    int id5001 = FindObjectOfType<Bag_System>().Add_Random_Relic();
                    string text5001 = "獲得了遺物"+getRelicName(id5001);
                    StartCoroutine(ShowInformation(text5001));
                }// this.gameObject.SetActive(false);
                break;
            case 6000:
                StartCoroutine(nameof(transition));
                break;
            case 6001:
                StartCoroutine(nameof(transition)); 
                break;
            case 6002:
                StartCoroutine(nameof(transition)); 
                break;
            case 8000:
                Global.AddHp(-5);
                Reset();
                string text8000 = "生命減少了5";
                StartCoroutine(ShowInformation_NOTDisabe(text8000));
                LoadEvent(8001,EventClass.Type.normal);
                break;
            case 9000:
                StartCoroutine(nameof(transition)); 
                break;
            case 10000:
                StartCoroutine(nameof(transition)); 
                break;
            case 11000:
                StartCoroutine(nameof(transition)); 
                break;
            case 11002:
                Card card11002 = Remove_Card();
                Global.AddSan(-15);
                
                int id11002 = FindObjectOfType<Bag_System>().Remove_Random_Equipment();
                string text11002 = "移除了"+card11002.cardName + "，理智減少了15";
                if(id11002!=0)
                {
                    Debug.Log("移除了"+id11002);
                    text11002 += "，移除了"+getEquipmentName(id11002);
                }
                StartCoroutine(ShowInformation(text11002));
                // this.gameObject.SetActive(false);text11002
                break;
            case 12000:
                StartCoroutine(nameof(transition)); 
                break;
            case 12001:
               StartCoroutine(nameof(transition)); 
                break;
            case 12002:
                StartCoroutine(nameof(transition)); 
                break;
            case 13000:
                //待做
                if(FindObjectOfType<Bag_System>().Bag_Full()==true)return;
                FindObjectOfType<Bag_System>().Add_Item("202","item");
                StartCoroutine(ShowInformation("獲得了道具"+getItemName(202)));
                // this.gameObject.SetActive(false);
                break;
            case 14000:
                Global.AddSan(10);
                Global.AddHp(10);
                Reset();
                StartCoroutine(ShowInformation_NOTDisabe("理智恢復10，血量恢復10"));
                LoadEvent(14002,EventClass.Type.normal);
                break;
            case 15000:
                Reset();
                LoadEvent(15002,EventClass.Type.normal);
                break;
            case 16000:
                StartCoroutine(nameof(transition)); 
                break;
            case 18000:
                StartCoroutine(nameof(transition)); 
                break;
            case 19000:
                int relic19000 = FindObjectOfType<Bag_System>().Remove_Random_Equipment();
                StartCoroutine(ShowInformation("移除了"+getRelicName(relic19000)));
                // this.gameObject.SetActive(false);
                break;
            case 20000:
                Card card2000 = Add_Skill_Card();
                StartCoroutine(ShowInformation("得到了"+card2000.cardName));
                // this.gameObject.SetActive(false);
                break;
            case 25000:
                
                StartCoroutine(nameof(transition)); 
                break;
            case 27000:
                string text27000 = "";
                float p = (float)Global.sanity/Global.max_sanity;
                if(p>0.5)
                {
                    Global.AddSan(-10);
                    text27000 += "理智減少了10";
                }
                else 
                {
                    Global.AddSan(10);
                    text27000 += "理智恢復了10";

                }StartCoroutine(ShowInformation(text27000));
                // this.gameObject.SetActive(false);
                break;
            case 28000:
                StartCoroutine(nameof(transition)); 
                break;
            case 30000:
               StartCoroutine(nameof(transition)); 
                break;
            default:
                break;
        }
    }
    public void ButtonPressed3(){
        audioSource.Play();
        switch (event_loaded.id)
        {
            case 1000:
                FindObjectOfType<Bag_System>().Show_Equipment_list(1,3);
                break;
            case 5001:
                StartCoroutine(nameof(transition)); 
                break;
            case 14000:
                Reset();
                LoadEvent(14003,EventClass.Type.normal);
                break;
            case 16000:
                StartCoroutine(nameof(transition)); 
                break;
            case 19000:
                float rv = Random.value;
                string text19000 = "";
                if(rv > 0.5f)
                {
                    int r = FindObjectOfType<Bag_System>().Add_Random_Relic();
                    text19000 += "得到了"+getRelicName(r);
                }else 
                {
                    Card card = Add_Card();
                    text19000 += "得到了"+card.cardName;
                }
                StartCoroutine(ShowInformation(text19000));
                break;
            case 28000:
                StartCoroutine(nameof(transition)); 
                break;
            default:
                break;
        }
    }
    public void ButtonPressed4(){
        audioSource.Play();
        switch (event_loaded.id)
        {
            case 1000:
                StartCoroutine(nameof(transition)); 
                break;
            case 16000:
                StartCoroutine(nameof(transition)); 
                break;
            case 19000:
                Global.AddHp(-10);
                Global.AddSan(10);
                StartCoroutine(ShowInformation("血量減少了10，理智增加了10"));
                // this.gameObject.SetActive(false);
                break;
            case 28000:
               StartCoroutine(nameof(transition)); 
                break;
            default:
                break;
        }
    }
    public void ButtonPressed5(){
        audioSource.Play();
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

    Card Update_Card()
    {
        List<Card> player_deck = Global.GetPlayerDeck();
        int index = Random.Range(0,player_deck.Count);
        int tmp = index;
        Card card = player_deck[index];
        while(card.upgraded == true)
        {
            index += 1;
            if(index >= player_deck.Count)index = 0;
            card = player_deck[index];
            if(tmp == index)break;
        }
        Global.UpgradeCard(card);
        return card;
    }
    Card Add_Card()
    {
        Card card;
        float rv = Random.value;
        if(rv > 0.4)card = Add_Skill_Card();
        else card = Add_Attack_Card();
        return card;
    }
    Card Add_Attack_Card()
    {
        int index = Random.Range(0,all_attack_cards.Count);
        Card card = all_attack_cards[index];
        Global.PlayerDeck_Add(card);
        return card;
    }
    Card Add_Skill_Card()
    {
        int index = Random.Range(0,all_skill_cards.Count);
        Card card = all_skill_cards[index];
        Global.PlayerDeck_Add(card);
        return card;
    }
    Card Remove_Card()
    {
        List<Card> player_deck = Global.GetPlayerDeck();
        Card card = player_deck[Random.Range(0,player_deck.Count)];
        Global.PlayerDeck_Remove(card);
        return card;
    }
    public void Remove_Card_19000(Card card)
    {
        Global.PlayerDeck_Remove(card);
        StartCoroutine(ShowInformation("移除了"+card.cardName));
        // this.gameObject.SetActive(false);
    }
    public void ShowText1000(int pre,int to,int pre2 = -1)
    {
        string text = "";
        if(pre2==-1)
        {
            text = getEquipmentName(pre) + "變成了" + getEquipmentName(to);
        }
        else
        {
            text = getEquipmentName(pre) + "和" + getEquipmentName(pre2) + "變成了" + getEquipmentName(to);
        }
        StartCoroutine(ShowInformation(text));
    }
    public void ShowText1000Del(int pre)
    {
        string text = "";
        text = getEquipmentName(pre) +"消失了";
        StartCoroutine(ShowInformation(text));
    }
    public void ShowText1000Nothing()
    {
        string text = "甚麼都沒有發生";
        StartCoroutine(ShowInformation(text));
    }
    IEnumerator ShowInformation(string text)
    {
        ShowPanel.SetActive(true);
        ShowText.text = text;
        yield return new WaitForSeconds(2);
        ShowPanel.SetActive(false);
        StartCoroutine(nameof(transition)); 
    }
    IEnumerator ShowInformation_NOTDisabe(string text)
    {
        ShowPanel.SetActive(true);
        ShowText.text = text;
        yield return new WaitForSeconds(2);
        ShowPanel.SetActive(false);
        // this.gameObject.SetActive(false);
    }
    string getEquipmentName(int id)
    {
        string e_name = "";
        switch(id)
        {
            case 1:
                e_name = "虛無之刃(稀有)";
                break;
            case 2:
                e_name = "殞命之旅(稀有)";
                break;
            case 3:
                e_name = "銀色手鐲(普通)";
                break;
            case 4:
                e_name = "克圖格亞的圖騰(普通)";
                break;
            case 5:
                e_name = "妖刀村正(罕見)";
                break;
            case 6:
                e_name = "俄羅斯輪盤(罕見)";
                break;
            case 7:
                e_name = "黑貓頭顱(稀有)";
                break;
            case 8:
                e_name = "妖精護身符(稀有)";
                break;
            case 9:
                e_name = "幽靈護手(稀有)";
                break;
            case 10:
                e_name = "破爛草衣(普通)";
                break;
            case 11:
                e_name = "吸血刃(稀有)";
                break;
            case 12:
                e_name = "打火機(普通)";
                break;
            case 13:
                e_name = "淨化御守(罕見)";
                break;
            case 14:
                e_name = "鏽刀(普通)";
                break;
            case 15:
                e_name = "電磁砲(罕見)";
                break;
            case 16:
                e_name = "韻律手環(普通)";
                break;
            case 17:
                e_name = "動力裝甲(罕見)";
                break;
            case 18:
                e_name = "疾行靴(罕見)";
                break;
            case 19:
                e_name = "流浪者(稀有)";
                break;
            case 20:
                e_name = "苦行(罕見)";
                break;
            case 21:
                e_name = "簡約胸章(普通)";
                break;
            case 22:
                e_name = "火藥(普通)";
                break;
            case 23:
                e_name = "護身符(普通)";
                break;
            case 24:
                e_name = "純白誓約(稀有)";
                break;
            case 25:
                e_name = "漆黑法典(罕見)";
                break;
            case 26:
                e_name = "雙節棍(罕見)";
                break;
            case 27:
                e_name = "鋼盾(罕見)";
                break;
            case 28:
                e_name = "日冕(稀有)";
                break;
            case 29:
                e_name = "月影(稀有)";
                break;
            case 30:
                e_name = "紅寶石戒指(普通)";
                break;
            default:
                break;
        }
        return e_name;
    }
    string getRelicName(int id)
    {
        string e_name = "";
        switch(id)
        {
            case 1:
                e_name = "精緻的寶石(罕見)";
                break;
            case 2:
                e_name = "白紙(罕見)";
                break;
            case 3:
                e_name = "照片(罕見)";
                break;
            case 4:
                e_name = "日記(罕見)";
                break;
            case 5:
                e_name = "扇子(稀有)";
                break;
            case 6:
                e_name = "格赫羅斯(罕見)";
                break;
            case 7:
                e_name = "聖人的名諱(稀有)";
                break;
            case 8:
                e_name = "分靈體(稀有)";
                break;
            case 9:
                e_name = "完整的手稿(稀有)";
                break;
            case 10:
                e_name = "畢宿五的祝福(罕見)";
                break;
            case 11:
                e_name = "號角(罕見)";
                break;
            case 12:
                e_name = "道羅斯(罕見)";
                break;
            case 13:
                e_name = "燒火棍(普通)";
                break;
            case 14:
                e_name = "平衡鳥(普通)";
                break;
            case 15:
                e_name = "塔羅牌(普通)";
                break;
            case 16:
                e_name = "渾沌物質(罕見)";
                break;
            case 17:
                e_name = "鐵匠錘(普通)";
                break;
            case 18:
                e_name = "鐮刀(罕見)";
                break;
            case 19:
                e_name = "拳套(罕見)";
                break;
            case 20:
                e_name = "迴力鏢(普通)";
                break;
            case 21:
                e_name = "香爐(普通)";
                break;
            case 22:
                e_name = "南瓜頭(稀有)";
                break;
            case 23:
                e_name = "限制器(罕見)";
                break;
            case 24:
                e_name = "氧化劑(普通)";
                break;
            case 25:
                e_name = "物理聖杖(普通)";
                break;
            case 26:
                e_name = "金幣(普通)";
                break;
            case 27:
                e_name = "章魚雕像(普通)";
                break;
            case 28:
                e_name = "正20面骰(稀有)";
                break;
            case 29:
                e_name = "古神的褻語(稀有)";
                break;
            case 30:
                e_name = "哈絲塔的祝福(罕見)";
                break;
            default:
                break;
        }
        return e_name;
    }
    string getItemName(int id)
    {
        string e_name = "";
        switch(id)
        {
            case 101:
                e_name = "史萊姆泥";
                break;
            case 102:
                e_name = "斷裂的長矛";
                break;
            case 103:
                e_name = "透明的絲捐";
                break;
            case 104:
                e_name = "獠牙";
                break;
            case 105:
                e_name = "蟹甲";
                break;
            case 106:
                e_name = "普通石塊";
                break;
            case 107:
                e_name = "舍利子";
                break;
            case 108:
                e_name = "史萊姆球";
                break;
            case 109:
                e_name = "飛矢";
                break;
            case 110:
                e_name = "誘人的肉";
                break;
            case 111:
                e_name = "病毒";
                break;
            case 112:
                e_name = "毒液";
                break;
            case 113:
                e_name = "有生機的枯木";
                break;
            case 114:
                e_name = "火";
                break;
            case 115:
                e_name = "樹皮";
                break;
            case 116:
                e_name = "絲滑如娟布的暗影";
                break;
            case 117:
                e_name = "祈禱聲";
                break;
            case 118:
                e_name = "骨灰";
                break;
            case 119:
                e_name = "破舊的紅布";
                break;
            case 120:
                e_name = "肉塊";
                break;
            case 201:
                e_name = "生命藥水(普通)";
                break;
            case 202:
                e_name = "力量藥水(普通)";
                break;
            case 203:
                e_name = "理智藥水(罕見)";
                break;
            case 204:
                e_name = "飛劍(普通)";
                break;
            case 205:
                e_name = "詛咒(普通)";
                break;
            case 206:
                e_name = "瘋癲藥水(罕見)";
                break;
            case 207:
                e_name = "薩滿超載(稀有)";
                break;
            case 208:
                e_name = "腦子(罕見)";
                break;
            case 209:
                e_name = "另一個腦子(罕見)";
                break;
            case 210:
                e_name = "左眼(罕見)";
                break;
            case 211:
                e_name = "右眼(罕見)";
                break;
            case 301:
                e_name = "聖人頭骨";
                break;
            case 302:
                e_name = "聖人手骨";
                break;
            case 303:
                e_name = "聖人腿骨";
                break;
            case 304:
                e_name = "手稿1";
                break;
            case 305:
                e_name = "手稿2";
                break;
            case 306:
                e_name = "手稿3";
                break;
            default:
                break;
        }
        return e_name;
    }
   
    IEnumerator transition()
    {
        FindObjectOfType<Transition>().Play();
        yield return new WaitForSeconds(0.6f);
        this.gameObject.SetActive(false);
    }
}
