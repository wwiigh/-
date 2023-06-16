using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shop_hide : MonoBehaviour
{
    int status = 0;
    public Color origin_background;
    public GameObject shop;
    public Button button;
    public Image background;
    public GameObject leave;
    public GameObject product;
    public GameObject map_move;
    public TMP_Text text;
    public void hide()
    {
        if(status == 0)
        {
            text.text = "show";
            background.color = new Color(1,1,1,0);
            leave.SetActive(false);
            product.SetActive(false);
            status = 1;
            map_move.GetComponent<Map_Move>().enabled = true;
        }
        else
        {
            status = 0;
            text.text = "hide";
            background.color = origin_background;
            leave.SetActive(true);
            product.SetActive(true);
            map_move.GetComponent<Map_Move>().enabled = false;
        }
    }
    public void leave_shop()
    {
        map_move.GetComponent<Map_Move>().enabled = true;
        shop.SetActive(false);   
    }
    // Start is called before the first frame update
    void Start()
    {
        map_move.GetComponent<Map_Move>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
