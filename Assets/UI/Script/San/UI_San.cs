using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_San : MonoBehaviour
{
    public TMP_Text san_num;
    int san_now;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        san_now = Global.sanity;
        san_num.text = san_now + "/" + Global.max_sanity;
        GetComponent<UI_Control>().Change_text("理智 (" + san_now + "/" + Global.max_sanity + ")");
    }
}
