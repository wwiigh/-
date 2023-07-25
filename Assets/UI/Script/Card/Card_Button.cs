using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Button : MonoBehaviour
{
    [SerializeField] GameObject card_list;
    bool open = false;
    public void On_Click()
    {
        if(open)
        {
            open = false;
            FindObjectOfType<Card_List>().Clear();
        }
        else
        {
            Global.ShowPlayerCards(Global.GetPlayerDeck(),null,false);
            open = true;
            
        }
    }
}
