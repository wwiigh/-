using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentionIcon : MonoBehaviour
{
    GameObject box = null;
    public void PointerIn(){
        GameObject character = transform.parent.gameObject;
        box = DescriptionBox.ShowIntention(character.GetComponent<EnemyMove>().GetIntention(), transform);
    }

    public void PointerOut(){
        if (box != null) Destroy(box);
    }
}
