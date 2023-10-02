using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardState : MonoBehaviour
{
    public enum State{
        Normal,
        ReadyToUse,
        Selected
    }
    public State state = State.Normal;



    public void Click(){
        if (FindObjectOfType<BattleController>().GetState() == BattleController.BattleState.SelectCard){
            if (state == State.ReadyToUse) return;
            if (state == State.Selected){
                Unselect();
            }
            else{
                if (!FindObjectOfType<BattleController>().SelectCard_Availible()) return;
                Select();
            }
        }
    }

    public void Select(){
        GameObject border = transform.GetChild(4).gameObject;
        state = State.Selected;
        border.SetActive(true);
        FindObjectOfType<BattleController>().SelectCard_Add(gameObject);
    }

    public void Unselect(){
        GameObject border = transform.GetChild(4).gameObject;
        state = State.Normal;
        border.SetActive(false);
        FindObjectOfType<BattleController>().SelectCard_Remove(gameObject);
    }
}
