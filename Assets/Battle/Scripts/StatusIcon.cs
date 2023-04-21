using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusIcon : MonoBehaviour
{
    GameObject battle;
    GameObject parent;
    [SerializeField] Image img;
    [SerializeField] TMP_Text levelText;
    // [SerializeField] GameObject descriptionBoxTemplate;
    [SerializeField] GameObject descriptionBox;
    Status.status status;
    int level;
    private void Start() {
        if (parent == null) Init();
        // descriptionBox = Instantiate(descriptionBoxTemplate, transform);
        // descriptionBox.SetActive(false);
    }
    void Init(){
        parent = transform.parent.gameObject;
        battle = parent.transform.parent.gameObject;
        descriptionBox = battle.GetComponent<Battle>().descriptionBox;
    }
    public void UpdateIcon(int idx, (Status.status _status, int level) pack){
        if (parent == null) Init();
        status = pack._status;
        level = pack.level;
        Vector3 defaultPosotion = new Vector3(-100, -215, 0);
        transform.localPosition = defaultPosotion + Vector3.right * 50 * (idx % 6) + Vector3.down * 60 * (idx / 6);
        img.sprite = battle.GetComponent<Status>().GetImage(status);
        levelText.text = pack.level.ToString();
    }

    public void PointerIn(){
        // descriptionBox.SetActive(true);
        Item item = ScriptableObject.CreateInstance<Item>();
        // Item item = new Item();
        item.itemName = Status.GetName(status);
        item.type = "";
        item.effectText = Status.GetDescription(status, level);
        item.description = "";
        item.rarity = -1;
        descriptionBox.GetComponent<DescriptionBox>().Show(item, transform);
    }

    public void PointerOut(){
        descriptionBox.GetComponent<DescriptionBox>().Hide();
    }
}
