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
    public void Show(int damage){
        GetComponent<TMP_Text>().text = damage.ToString();
        speed = new Vector3(Random.Range(3f, 6f), Random.Range(4f, 6f), 0);
        if (transform.parent.tag == "Player") speed.x = -speed.x;
        playing = true;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (playing){
            transform.localPosition += speed;
            speed += accel;
            GetComponent<TMP_Text>().color += new Color(0, 0, 0, -fadeSpeed);
            if (GetComponent<TMP_Text>().color.a <= 0) Destroy(gameObject);
        }
    }
}
