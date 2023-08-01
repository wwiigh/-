using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescriptionBox : MonoBehaviour
{
    public Item item;
    [SerializeField] GameObject box;
    [SerializeField] TMP_Text boxText;
    // [SerializeField] TMP_Text typeText;
    // [SerializeField] TMP_Text rarityText;
    // [SerializeField] TMP_Text effectText;
    // [SerializeField] TMP_Text descriptionText;
    // public void UpdateText(){
    //     nameText.text = item.itemName;
    //     switch(item.type){
    //         case "weapon":
    //             typeText.text = "武器";
    //             break;
    //         case "hat":
    //             typeText.text = "帽子";
    //             break;
    //         case "armor":
    //             typeText.text = "衣服";
    //             break;
    //         case "boots":
    //             typeText.text = "鞋子";
    //             break;
    //         case "accesory":
    //             typeText.text = "飾品";
    //             break;
    //         case "item":
    //             typeText.text = "道具";
    //             break;
    //         case "":
    //             typeText.text = "";
    //             break;
    //         default:
    //             Debug.Log("Type Error");
    //             break;
    //     }
    //     switch(item.rarity){
    //         case 1:
    //             rarityText.text = "普通";
    //             rarityText.color = new Color32(125, 125, 125, 255);
    //             break;
    //         case 2:
    //             rarityText.text = "罕見";
    //             rarityText.color = new Color32(0, 200, 255, 255);
    //             break;
    //         case 3:
    //             rarityText.text = "稀有";
    //             rarityText.color = new Color32(255, 255, 0, 255);
    //             break;
    //         case -1:
    //             rarityText.text = "";
    //             break;
    //         default:
    //             Debug.Log("Rarity Error");
    //             break;
    //     }
    //     effectText.text = item.effectText;
    //     descriptionText.text = item.description;
    // }

    private void Start() {
        // UpdateText();
    }

    public void Show(Item a, Transform t){
        transform.SetSiblingIndex(transform.parent.childCount - 1);
        item = a;
        // // UpdateText();
        box.SetActive(true);
        // Debug.Log("position: " + t.position.ToString());
        // box.transform.position = t.position + new Vector3(200, -180, 0);
        box.transform.position = t.position + new Vector3(2, -1.3f, 0);
        // Debug.Log("box position: " + box.transform.position.ToString());
    }

    public void Hide(){
        box.SetActive(false);
    }



    static public GameObject ShowIntention((string name, string description) param, Transform caller){
        GameObject boxObj = Instantiate(GameObject.Find("DescriptionBox2"), GameObject.Find("BattleController").transform);
        boxObj.transform.GetChild(0).gameObject.SetActive(true);
        DescriptionBox box = boxObj.GetComponent<DescriptionBox>();

        box.boxText.text = param.name + "\n" + param.description;
        // box.nameText.text = param.name;
        // box.effectText.text = param.description;
        // box.rarityText.text = "";
        // box.typeText.text = "";
        // box.descriptionText.text = "";

        float width = box.boxText.preferredWidth + 100;
        float height = box.boxText.preferredHeight + 100;
        // boxObj.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

        Vector2 panelSize = boxObj.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        // boxObj.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        // boxObj.transform.position.z = 0;
        boxObj.transform.position = caller.position;
        Debug.Log("caller: " + caller.position.ToString());
        Debug.Log("after: " + boxObj.transform.position.ToString());
        Debug.Log("local: " + boxObj.transform.localPosition.ToString());
        Debug.Log("panel object: " + boxObj.transform.GetChild(0).gameObject);
        Debug.Log("panel size: " + panelSize.ToString());
        boxObj.transform.localPosition += new Vector3(panelSize.x / 2, -panelSize.y / 2) * 5;
        Debug.Log("after: " + boxObj.transform.localPosition.ToString());
        Debug.Log("mouse pos: " + Input.mousePosition);
        Debug.Log("in world: " + GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition));
        return boxObj;
    }
}
