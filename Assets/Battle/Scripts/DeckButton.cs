using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckButton : MonoBehaviour
{
    bool zooming = false;
    Vector3 targetSize = new Vector3(1, 1, 1);

    void Update()
    {
        if ((transform.localScale - targetSize).magnitude > 0.0001f){
            transform.localScale = Vector3.Lerp(transform.localScale, targetSize, 0.1f);
        }
        else transform.localScale = targetSize;
    }

    public void HoverIn(){
        targetSize = new Vector3(1.2f, 1.2f, 1);
    }
    public void HoverOut(){
        targetSize = new Vector3(1, 1, 1);
    }
    public void Click(){
        if (name == "draw pile"){
            Global.ShowPlayerCards(Deck.GetDeck(), null, false);
        }
        if (name == "trash"){
            Global.ShowPlayerCards(Deck.GetTrash(), null, false);
        }
    }
}
