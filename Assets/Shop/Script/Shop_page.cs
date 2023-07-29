using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_page : MonoBehaviour
{
    public GameObject normal_product;
    public GameObject card_product;
    public GameObject page1;
    public GameObject page2;
    int page = 1;
    void OnEnable()
    {
        page = 1;
        Press_Page1();
    }

    public void Press_Page1()
    {
        page = 1;
        page1.GetComponent<Image>().color = Color.white;
        page2.GetComponent<Image>().color = Color.gray;
        normal_product.SetActive(true);
        card_product.SetActive(false);
    }
    public void Press_Page2()
    {
        page = 2;
        page1.GetComponent<Image>().color = Color.gray;
        page2.GetComponent<Image>().color = Color.white;
        normal_product.SetActive(false);
        card_product.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
