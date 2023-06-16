using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_button_control : MonoBehaviour
{
    public GameObject map;
    // Start is called before the first frame update
    
    public void change_map_active()
    {
        map.SetActive(!map.activeSelf);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
