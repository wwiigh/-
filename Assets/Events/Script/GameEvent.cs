using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    public void Reset(){
        List<GameObject> tmp = new List<GameObject>();
        foreach(Transform child in canvas.transform){
            if (child.name == "optionButton") tmp.Add(child.gameObject);
        }
        foreach(GameObject obj in tmp){
            Destroy(obj);
        }
        description.text = "";
        optionCount=0;
        eventName.text = "";
    }

    public void ButtonPressed1(){
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
                Global.money -= 100;
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
                break;
            case 501:   
                break;
            default:
                break;
        }

    }
    public void ButtonPressed2(){
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
            default:
                break;
        }
    }
    public void ButtonPressed3(){
        
    }
    public void ButtonPressed4(){
        
    }
    public void ButtonPressed5(){
        
    }
}
