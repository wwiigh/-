using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardState : MonoBehaviour
{
    BattleController battleController;
    public enum State{
        Normal,
        ReadyToUse,
        Selected
    }
    public State state = State.Normal;
    private void Start() {
        battleController = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
    }



    public void Click(){
        if (battleController.GetState() == BattleController.BattleState.SelectCard){
            if (state == State.ReadyToUse) return;
            if (state == State.Selected){
                Unselect();
            }
            else{
                if (!battleController.SelectCard_Availible()) return;
                Select();
            }
        }
    }

    public void Select(){
        GameObject border = transform.GetChild(4).gameObject;
        state = State.Selected;
        border.SetActive(true);
        battleController.SelectCard_Add(gameObject);
    }

    public void Unselect(){
        GameObject border = transform.GetChild(4).gameObject;
        state = State.Normal;
        border.SetActive(false);
        battleController.SelectCard_Remove(gameObject);
    }
}
