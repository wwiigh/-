using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusIcon : MonoBehaviour
{
    GameObject battleController;
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
        battleController = GameObject.FindGameObjectWithTag("BattleController");
        parent = transform.parent.gameObject;
        Debug.Log("battleController is " + battleController.ToString());
        descriptionBox = battleController.GetComponent<BattleController>().descriptionBox;
    }
    public void UpdateIcon(int idx, (Status.status _status, int level) pack){
        if (parent == null) Init();
        status = pack._status;
        level = pack.level;
        Vector3 defaultPosotion = new Vector3(-100, -215, 0);
        transform.localPosition = defaultPosotion + Vector3.right * 50 * (idx % 5) + Vector3.down * 60 * (idx / 5);
        img.sprite = battleController.GetComponent<Status>().GetImage(status);
        if (Status.IsCountable(status)){
            levelText.enabled = true;
            levelText.text = pack.level.ToString();
        }
        else{
            levelText.enabled = false;
        }
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
