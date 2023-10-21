using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitAnimation : MonoBehaviour
{
    Vector3 original_position = Vector3.zero;
    bool dying = false;
    bool color_changing = false;
    Color target_color = new Color(1, 1, 1, 1);
    bool position_changing = false;
    Vector3 target_position = Vector3.zero;
    public void Play(bool knockback=true, bool dead=false){
        if (original_position == Vector3.zero) original_position = transform.localPosition;
        Animation(knockback, dead);
    }

    void FixedUpdate()
    {
        if (color_changing){
            transform.GetChild(0).GetComponent<Image>().color = Color.Lerp(
                transform.GetChild(0).GetComponent<Image>().color, target_color, 0.1f);
            Color diff = transform.GetChild(0).GetComponent<Image>().color - target_color;
            float diff_value = Mathf.Abs(diff.r) + Mathf.Abs(diff.g) + Mathf.Abs(diff.b) + Mathf.Abs(diff.a);
            if (diff_value < 0.001f){
                transform.GetChild(0).GetComponent<Image>().color = target_color;
                color_changing = false;
            }
        }
        
        if (position_changing){
            transform.localPosition = Vector3.Lerp(transform.localPosition, target_position, 0.1f);
            if ((transform.localPosition - target_position).magnitude < 0.001f){
                if (dying){
                    GetComponent<Character>().SafeDestroy();
                    return;
                }
                transform.localPosition = target_position;
                position_changing = false;
            }
        }
    }

    void Animation(bool knockback=true, bool dead=false){
        if (dead) dying = true;

        transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
        if (!dead) target_color = new Color(1, 1, 1, 1);
        else target_color = new Color(0, 0, 0, 0);
        color_changing = true;

        if (!knockback && !dead) return;
        if (!dead){
            if (knockback){
                if (tag == "Player") transform.localPosition = original_position + Vector3.left * 100;
                else transform.localPosition = original_position + Vector3.right * 100;
                target_position = original_position;
                position_changing = true;
            }
        }
        else{
            target_position = original_position + Vector3.right * 50;
            if (tag == "Player") target_position = original_position + Vector3.left * 50;
            position_changing = true;
        }
    }
}
