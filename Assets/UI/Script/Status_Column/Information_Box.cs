using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Information_Box : MonoBehaviour
{
    public Image box;
    public TMP_Text information;
    int count;
    public void add_count(int c)
    {
        count = c;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void set_box_size()
    {

        if(count<=3)
        box.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(3.42f,1.1f);
        else
        box.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(3.42f,1.1f + (count-3)*0.4f);

        // print(Time.time.ToString()+" "+count.ToString());
        // print(box);
    }
    
}
