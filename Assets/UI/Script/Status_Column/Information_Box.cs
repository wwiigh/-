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
    public void set_box_size(GameObject canvas)
    {

        if(count<=3)
        box.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(3.42f,1.1f);
        else
        box.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(3.42f,1.1f + (count-3)*0.4f);

        // print(Time.time.ToString()+" "+count.ToString());
        // print(box);
        float p_x = previousX(this.transform);
        float nowx = this.transform.localPosition.x + p_x;
        float leftx = nowx - box.gameObject.GetComponent<RectTransform>().sizeDelta.x/2*108 ;
        float rightx = nowx + box.gameObject.GetComponent<RectTransform>().sizeDelta.x/2*108;

        float width = canvas.GetComponent<RectTransform>().rect.width;
        Debug.Log("left " + leftx + " right " + rightx + " screen/2 " + width/2);
        if(leftx<-width/2)
        {
            this.transform.localPosition = new Vector3(-width/2 + 4*108/2f - p_x,this.transform.localPosition.y,this.transform.localPosition.z);
        }
        else if(rightx >width/2)
        {
            this.transform.localPosition = new Vector3(width/2 - 4*108/2f - p_x,this.transform.localPosition.y,this.transform.localPosition.z);

        }
    }
    float previousX(Transform box)
    {
        float x = 0;
        while(box.parent.GetComponent<Canvas>()==null)
        {
            x+= box.parent.localPosition.x;
            box = box.parent;
        }
        return x;
    }
    
}
