using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitAnimation : MonoBehaviour
{
    Vector2 original_position = Vector2.zero;
    bool isPlaying = false;
    Coroutine current_animation = null;
    public void Play(){
        if (original_position == Vector2.zero) original_position = transform.localPosition;
        if (isPlaying) StopCoroutine(current_animation);
        current_animation = StartCoroutine(_Play());
    }

    IEnumerator _Play(){
        isPlaying = true;
        transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
        if (tag == "Player") transform.localPosition = original_position + Vector2.left * 100;
        else transform.localPosition = original_position + Vector2.right * 100;
        float percent = 0;
        while(1 - percent > 0.001f){
            percent = Mathf.Lerp(percent, 1, 0.1f);
            transform.GetChild(0).GetComponent<Image>().color = new Color(1, percent, percent, 1);
            if (tag == "Player") transform.localPosition = original_position + Vector2.left * 100 * (1 - percent);
            else transform.localPosition = original_position + Vector2.right * 100 * (1 - percent);
            yield return new WaitForSeconds(0.02f);
        }

        transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        transform.localPosition = original_position;
        isPlaying = false;
    }
}
