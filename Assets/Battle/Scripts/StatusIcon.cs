using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusIcon : MonoBehaviour
{
    GameObject battleController;
    GameObject parent;
    [SerializeField] Image img;
    [SerializeField] TMP_Text levelText;
    Status.status status;
    int level;
    private void Start() {
        if (parent == null) Init();
    }
    void Init(){
        battleController = GameObject.FindGameObjectWithTag("BattleController");
        parent = transform.parent.gameObject;
        descriptionBox = battleController.GetComponent<BattleController>().descriptionBox;
    }
    public void UpdateIcon(int idx, (Status.status _status, int level) pack, int numberPerRow){
        if (parent == null) Init();
        status = pack._status;
        level = pack.level;
        Vector3 defaultPosotion = new Vector3(-100, -215, 0);
        if (numberPerRow == 3) defaultPosotion = new Vector3(-50, -215, 0);
        transform.localPosition = defaultPosotion + Vector3.right * 50 * (idx % numberPerRow) + Vector3.down * 60 * (idx / numberPerRow);
        img.sprite = battleController.GetComponent<Status>().GetImage(status);
        if (Status.IsCountable(status)){
            levelText.enabled = true;
            levelText.text = pack.level.ToString();
        }
        else{
            levelText.enabled = false;
        }
    }



    GameObject descriptionBox;
    bool showing = false;
    public void PointerIn(){
        descriptionBox = DescriptionBox.Show(Status.GetName(status), Status.GetDescription(status, level));
        showing = true;
    }
    public void PointerOut(){
        if (descriptionBox != null) Destroy(descriptionBox);
        showing = false;
    }
    private void Update() {
        if (showing) descriptionBox.GetComponent<DescriptionBox>().UpdatePosition();
    }



    public void Destroy(){
        if (descriptionBox != null) Destroy(descriptionBox);
        Destroy(gameObject);
    }
}
