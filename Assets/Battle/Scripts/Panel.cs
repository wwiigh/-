using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    GameObject currentActive;
    BattleController.BattleState stateSaved;
    public GameObject Show(BattleController.BattleState state, int current, int total, bool isEqual, bool cancellable){
        stateSaved = state;
        gameObject.SetActive(true);
        if (stateSaved == BattleController.BattleState.SelectCard){
            currentActive = transform.GetChild(0).gameObject;
            currentActive.SetActive(true);
            UpdateAll(current, total, isEqual, cancellable);
            return currentActive;
        }
        Debug.Log("Panel.cs: shouldn't appear");
        return null;
    }

    public void Hide(){
        currentActive.SetActive(false);
        gameObject.SetActive(false);
    }

    public void UpdateAll(int current, int total, bool isEqual, bool cancellable){
        if (stateSaved == BattleController.BattleState.SelectCard) currentActive.GetComponent<PanelSelectCard>().UpdateAll(current, total, isEqual, cancellable);
        
    }
}
