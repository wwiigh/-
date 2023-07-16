using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescriptionBox : MonoBehaviour
{
    public Item item;
    [SerializeField] GameObject box;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text typeText;
    [SerializeField] TMP_Text rarityText;
    [SerializeField] TMP_Text effectText;
    [SerializeField] TMP_Text descriptionText;
    public void UpdateText(){
        nameText.text = item.itemName;
        switch(item.type){
            case "weapon":
                typeText.text = "武器";
                break;
            case "hat":
                typeText.text = "帽子";
                break;
            case "armor":
                typeText.text = "衣服";
                break;
            case "boots":
                typeText.text = "鞋子";
                break;
            case "accesory":
                typeText.text = "飾品";
                break;
            case "item":
                typeText.text = "道具";
                break;
            case "":
                typeText.text = "";
                break;
            default:
                Debug.Log("Type Error");
                break;
        }
        switch(item.rarity){
            case 1:
                rarityText.text = "普通";
                rarityText.color = new Color32(125, 125, 125, 255);
                break;
            case 2:
                rarityText.text = "罕見";
                rarityText.color = new Color32(0, 200, 255, 255);
                break;
            case 3:
                rarityText.text = "稀有";
                rarityText.color = new Color32(255, 255, 0, 255);
                break;
            case -1:
                rarityText.text = "";
                break;
            default:
                Debug.Log("Rarity Error");
                break;
        }
        effectText.text = item.effectText;
        descriptionText.text = item.description;
    }

    private void Start() {
        UpdateText();
    }

    public void Show(Item a, Transform t){
        transform.SetSiblingIndex(transform.parent.childCount - 1);
        item = a;
        UpdateText();
        box.SetActive(true);
        // Debug.Log("position: " + t.position.ToString());
        // box.transform.position = t.position + new Vector3(200, -180, 0);
        box.transform.position = t.position + new Vector3(2, -1.3f, 0);
        // Debug.Log("box position: " + box.transform.position.ToString());
    }

    public void Hide(){
        box.SetActive(false);
    }
}
