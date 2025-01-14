using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionBox : MonoBehaviour
{
    static public GameObject Show(string name, string description){
        // Debug.Log("Show() is called");
        GameObject boxObj = Instantiate(FindAnyObjectByType<GlobalAssets>().descriptionBoxTemplate, GameObject.FindGameObjectWithTag("UI canvas").transform);
        boxObj.transform.GetChild(0).gameObject.SetActive(true);
        DescriptionBox box = boxObj.GetComponent<DescriptionBox>();

        // boxObj.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = name + "\n" + description;

        box.UpdateText(name, description);
        box.UpdatePosition();
        
        return boxObj;
    }
    public void UpdateText(string name, string description){
        transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = name + "\n" + description;
    }
    public void UpdatePosition(){
        LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.transform.GetChild(0).GetComponent<RectTransform>());
        Vector2 panelSize = gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;

        gameObject.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        Vector3 offset = new Vector3(30, -30);
        // Debug.Log("mouse position: " + transform.localPosition);
        // Debug.Log("right end: " + (transform.localPosition + (Vector3)panelSize + offset));
        Vector2 right_end = transform.localPosition + (Vector3)panelSize + offset;

        if (right_end.x >= 950){
            gameObject.transform.localPosition += new Vector3(-panelSize.x / 2, -panelSize.y / 2);
            offset.x = -offset.x;
            gameObject.transform.localPosition += offset;
        }
        else{
            gameObject.transform.localPosition += new Vector3(panelSize.x / 2, -panelSize.y / 2);
            gameObject.transform.localPosition += offset;
        }
        // Debug.Log(transform.localPosition + new Vector3(panelSize.x / 2 + 30, -panelSize.y / 2 - 30));
        // gameObject.transform.GetChild(0).localPosition += new Vector3(panelSize.x / 2, -panelSize.y / 2);
    }
}
