using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Select : MonoBehaviour
{
    public GameObject border;
    public static GameObject select;
    bool can_select = true;
    public void Select()
    {
        if(can_select == false)return;
        // if(Card_List.now_select == 1 && border.activeSelf == false)return;
        if(border.activeSelf == true)
        {
            Card_List.now_select = 0;
            Card_List.now_select_card = null;
            select = null;
            border.SetActive(false);
        }
        else
        {
            Card_List.now_select = 1;
            if(select != null)
            {
                select.GetComponent<Card_Select>().border.SetActive(false);
            }
            select = this.gameObject;
            Card_List.now_select_card = gameObject.GetComponent<CardDisplay>().thisCard;
            border.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NotSelect()
    {
        can_select = false;
    }

}
