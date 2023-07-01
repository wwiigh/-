using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_button_control : MonoBehaviour
{
    public string now_visit;
    public GameObject map_canvas;
    public GameObject map_disable_move;
    public GameObject setting;
    
    // Start is called before the first frame update
    
    public void change_map_active()
    {
        // map.SetActive(!map.activeSelf);
        map_canvas.GetComponent<Canvas>().sortingOrder = (map_canvas.GetComponent<Canvas>().sortingOrder==0 ?2:0);
        map_disable_move.SetActive(!map_disable_move.activeSelf);
    }

    public void change_setting_active()
    {
        setting.SetActive(!setting.activeSelf);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
