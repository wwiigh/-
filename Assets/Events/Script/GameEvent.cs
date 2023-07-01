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

    private void Start() {
        foreach(EventClass _event in all_events){
            if (_event.type == EventClass.Type.normal) normal_events.Add(_event);
            else if (_event.type == EventClass.Type.story) story_events.Add(_event);
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

    public void Reset(){
        List<GameObject> tmp = new List<GameObject>();
        foreach(Transform child in canvas.transform){
            if (child.name == "optionButton") tmp.Add(child.gameObject);
        }
        foreach(GameObject obj in tmp){
            Destroy(obj);
        }
    }
}
