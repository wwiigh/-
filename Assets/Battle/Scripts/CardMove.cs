using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMove : MonoBehaviour
{
    BattleController battleController;
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
    public delegate void OnClickDelegate(int n);
    public event OnClickDelegate OnClick;
    public int clickReturnNumber = -1;
    Vector3 targetSize = new(2, 2, 1);
    Vector3 targetPosition = Vector3.zero;
    float movingSpeed = 0.3f;

    private void Start() {
        battleController = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
    }

    public void Drag(){
        if (battleController.GetState() == BattleController.BattleState.SelectEnemy || 
            battleController.GetState() == BattleController.BattleState.SelectCard) return;
        if (!draggable || dragging) return;
        // Debug.Log("is dragging");
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
        if (OnClick != null){
            OnClick.Invoke(clickReturnNumber);
            return;
        }
        if (battleController.GetState() == BattleController.BattleState.SelectEnemy || 
            battleController.GetState() == BattleController.BattleState.SelectCard) return;
        dragging = false;
        if (transform.localPosition.y > playCardThreshold && Cost.GetCost() >= GetComponent<CardDisplay>().GetCost() &&
            GetComponent<CardDisplay>().thisCard.cost != -1){
            GetComponent<CardState>().state = CardState.State.ReadyToUse;
            CardEffects.Use(gameObject);
            Move(new Vector3(0, 250, 0));
        }
        else{
            Deck.Rearrange();
        }
    }

    public void PointerEnter(){
        // scalingState = ScalingState.Bigger;
        transform.SetAsLastSibling();
        // StartCoroutine(ChangeSize(2.4f, 0.2f));

        if (!discarding && !GetComponent<CardDisplay>().IsFading()) targetSize = new(2.4f, 2.4f, 1);
    }

    public void PointerExit(){
        // scalingState = ScalingState.Smaller;
        // StartCoroutine(ChangeSize(2.0f, 0.2f));

        if (!discarding && !GetComponent<CardDisplay>().IsFading()) targetSize = new(2, 2, 1);
    }

    public void Move(Vector3 destination, float speed){
        // StartCoroutine(SlideTo(destination, speed));
        targetPosition = destination;
        movingSpeed = speed;
    }

    public void Move(Vector3 destination){
        // StartCoroutine(SlideTo(destination, 0.3f));
        Move(destination, 0.3f);
    }

    bool discarding = false;
    Vector3 discardDestination = Vector3.zero;
    float discardSpeed = 0.1f;
    private void FixedUpdate() {
        if ((transform.localScale - targetSize).magnitude > 0.0001f){
            transform.localScale = Vector3.Lerp(transform.localScale, targetSize, 0.2f);
        }
        else transform.localScale = targetSize;

        if ((targetPosition - transform.localPosition).magnitude > 0.1f){
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, movingSpeed);
        }
        else transform.localPosition = targetPosition;

        if (discarding){
            transform.localPosition = Vector3.Lerp(transform.localPosition, discardDestination, discardSpeed);
            if (transform.localPosition.x - discardDestination.x < 1){
                Destroy(gameObject);
                foreach (DeckButton icon in FindObjectsOfType<DeckButton>()){
                    if (icon.name == "trash") icon.ItemIn();
                }
            }
        }
    }

    public void Discard(float speed, Vector3 destination){
        discardSpeed = speed;
        discardDestination = destination;
        discarding = true;
        targetSize = new(0.3f, 0.3f, 1);
    }

    // IEnumerator DiscardAnimation(float height, float speed, Vector3 destination){
    //     while((destination - transform.localPosition).magnitude > 0.1f){
    //         transform.localPosition = Vector3.Lerp(transform.localPosition, moveDestination, speed);
    //         yield return new WaitForSeconds(0.02f);
    //     }
    //     moving = false;
    // }

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
        if (scalingState == originalState) {
            scalingState = ScalingState.None;
            transform.localScale = targetSize;
        }
    }
}
