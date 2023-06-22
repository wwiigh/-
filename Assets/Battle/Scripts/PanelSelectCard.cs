using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelSelectCard : MonoBehaviour
{
    BattleController battleController;
    [SerializeField] TMP_Text title;
    [SerializeField] GameObject confirmButton;
    [SerializeField] GameObject cancelButton;

    private void Start() {
        battleController = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
    }

    public void Confirm(){
        battleController.SelectCard_Confirm();
    }

    public void UpdateAll(int current, int total, bool isEqual, bool cancellable){
        if (cancellable){
            cancelButton.GetComponent<Button>().interactable = true;
            cancelButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else{
            cancelButton.GetComponent<Button>().interactable = false;
            cancelButton.GetComponent<Image>().color = new Color32(137, 137, 137, 255);
        }
        if (isEqual){
            title.text = string.Format("選擇{0}張牌 ( {1}/{2} )", total, current, total);
            if (current == total){
                confirmButton.GetComponent<Button>().interactable = true;
                confirmButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            else{
                confirmButton.GetComponent<Button>().interactable = false;
                confirmButton.GetComponent<Image>().color = new Color32(137, 137, 137, 255);
            }
        }
        else{
            title.text = string.Format("選擇最多{0}張牌 ( {1}/{2} )", total, current, total);
            if (current <= total){
                confirmButton.GetComponent<Button>().interactable = true;
                confirmButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            else{
                confirmButton.GetComponent<Button>().interactable = false;
                confirmButton.GetComponent<Image>().color = new Color32(137, 137, 137, 255);
            }
        }
    }

    public void Cancel(){
        battleController.SelectCard_Cancel();
    }
}
