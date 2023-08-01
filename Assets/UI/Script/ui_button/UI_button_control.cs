using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if(Map_System.now_state == Map_System.map_state.normal)return;
        map_canvas.GetComponent<Canvas>().sortingOrder = (map_canvas.GetComponent<Canvas>().sortingOrder==0 ?2:0);
        map_disable_move.SetActive(!map_disable_move.activeSelf);
        if(map_disable_move.activeSelf)
        {
            Map_System.now_ui = Map_System.map_ui.open;
        }
        else
        {
            Map_System.now_ui = Map_System.map_ui.close;
        }
    }

    public void change_setting_active()
    {
        setting.SetActive(!setting.activeSelf);
    }
    public void Setting_Return_title()
    {
        FindObjectOfType<Save>().save();
        SceneManager.LoadScene("StartMenu");
    }
    public void testhealth()
    {
        Global.AddHp(-10);
        Global.AddSan(-10);
        Global.money += 100;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
