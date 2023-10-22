using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageText : MonoBehaviour
{
    bool playing = false;
    Vector3 accel = new Vector3(0, -0.5f, 0);
    Vector3 speed;
    float fadeSpeed = 0.02f;
    bool hasSource = false;
    public void Show(int damage, bool _hasSource){
        hasSource = _hasSource;
        GetComponent<TMP_Text>().text = damage.ToString();

        if (hasSource){
            speed = new Vector3(Random.Range(3f, 6f), Random.Range(4f, 6f), 0);
            if (transform.parent.tag == "Player") speed.x = -speed.x;
        }
        else speed = new Vector3(0, 10f, 0);
        playing = true;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (playing){
            transform.localPosition += speed;
            if (hasSource){
                speed += accel;
            }
            else{
                speed *= 0.95f;
            }
            GetComponent<TMP_Text>().color += new Color(0, 0, 0, -fadeSpeed);
            if (GetComponent<TMP_Text>().color.a <= 0) Destroy(gameObject);
        }
    }
}
