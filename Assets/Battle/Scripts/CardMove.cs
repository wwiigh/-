using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMove : MonoBehaviour
{
    BattleController battleController;
    Cost cost;
    bool draggable = true;
    bool moving = false;
    Vector3 moveDestination;
    bool dragging = false;
    enum ScalingState{
        None,
        Bigger,
        Smaller
    }
    ScalingState scalingState = ScalingState.None;
    Vector3 posSaved;
    Vector3 positionOffset;
    int playCardThreshold = -300;

    private void Start() {
        battleController = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        cost = GameObject.FindGameObjectWithTag("Cost").GetComponent<Cost>();
    }

    public void Drag(){
        if (battleController.GetState() == BattleController.BattleState.SelectEnemy || 
            battleController.GetState() == BattleController.BattleState.SelectCard) return;
        if (!draggable || dragging) return;
        Debug.Log("is dragging");
        moving = false;
        dragging = true;
        positionOffset = transform.localPosition - Input.mousePosition;
        posSaved = transform.localPosition;
    }

    private void Update() {
        if (dragging){
            transform.localPosition = Input.mousePosition + positionOffset;
        }
    }

    public void Drop(){
        if (battleController.GetState() == BattleController.BattleState.SelectEnemy || 
            battleController.GetState() == BattleController.BattleState.SelectCard) return;
        dragging = false;
        if (transform.localPosition.y > playCardThreshold && Cost.GetCost() >= GetComponent<CardDisplay>().thisCard.cost){
            GetComponent<CardState>().state = CardState.State.ReadyToUse;
            CardEffects.Use(gameObject);
            Move(new Vector3(-700, 300, 0));
        }
        else{
            Move(posSaved);
        }
    }

    public void PointerEnter(){
        scalingState = ScalingState.Bigger;
        transform.SetAsLastSibling();
        StartCoroutine(ChangeSize(2.4f, 0.2f));
    }

    public void PointerExit(){
        scalingState = ScalingState.Smaller;
        StartCoroutine(ChangeSize(2.0f, 0.2f));
    }

    public void Move(Vector3 destnation, float speed){
        StartCoroutine(SlideTo(destnation, speed));
    }

    public void Move(Vector3 destnation){
        StartCoroutine(SlideTo(destnation, 0.3f));
    }

    IEnumerator SlideTo(Vector3 destination, float speed){
        // Debug.Log("slide to(): dst: " + destination.ToString());
        moveDestination = destination;
        if (moving) yield break;
        moving = true;
        while((moveDestination - transform.localPosition).magnitude > 0.1f && moving){
            transform.localPosition = Vector3.Lerp(transform.localPosition, moveDestination, speed);
            yield return new WaitForSeconds(0.02f);
        }
        moving = false;
    }

    IEnumerator ChangeSize(float size, float speed){
        ScalingState originalState = scalingState;
        Vector3 targetSize = new Vector3(size, size, 1);
        while(Mathf.Abs(transform.localScale.x - size) > 0.1f && scalingState == originalState){
            transform.localScale = Vector3.Lerp(transform.localScale, targetSize, speed);
            yield return new WaitForSeconds(0.02f);
        }
        if (scalingState == originalState) scalingState = ScalingState.None;
    }
}
