using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEffects : MonoBehaviour
{
    public enum effectType{
        slash,
        sting
    }



    public void Play(GameObject character, string animation_name){
        Play(character, Vector3.zero, animation_name);
    }
    public void Play(GameObject character, Vector3 offset, string animation_name){
        StartCoroutine(_Play(character, offset, animation_name));
    }
    IEnumerator _Play(GameObject character, Vector3 offset, string animation_name){
        GameObject clone = Instantiate(gameObject, transform.parent);
        clone.tag = "Untagged";
        if (character != null) clone.transform.localPosition = character.transform.localPosition;
        clone.GetComponent<Animator>().Play(animation_name);
        yield return new WaitForSeconds(1);
        Destroy(clone);
    }
}
