using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentionIcon : MonoBehaviour
{
    bool showing = false;
    GameObject box = null;
    public void PointerIn(){
        GameObject character = transform.parent.gameObject;
        (string a, string b) ret = character.GetComponent<EnemyMove>().GetIntention();
        box = DescriptionBox.Show(ret.a, ret.b);
        showing = true;
    }

    public void PointerOut(){
        if (box != null) Destroy(box);
        showing = false;
    }

    private void Update() {
        if (showing) box.GetComponent<DescriptionBox>().UpdatePosition();
    }

    public void UpdateText(string name, string description){
        if (box != null) box.GetComponent<DescriptionBox>().UpdateText(name, description);
    }

    public void DestroyBox(){
        if (box) Destroy(box);
    }
}
