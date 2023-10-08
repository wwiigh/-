using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Button : MonoBehaviour
{
    [SerializeField] GameObject card_list;
    bool open = false;
    Map_System.map_state tmp;
    public void On_Click()
    {
        if(open)
        {
            Clear(null);
            return;    
        }
        Global.ShowPlayerCards(Global.GetPlayerDeck(),Clear,false);
        tmp = Map_System.now_state;
        Map_System.now_state = Map_System.map_state.open_ui;
        open = true;
    }
    public void Clear(Card card)
    {
        open = false;
        FindObjectOfType<Card_List>().Clear();
        Map_System.now_state = tmp;
    }
}
