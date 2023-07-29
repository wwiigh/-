using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop_Buy : MonoBehaviour
{
    public int shop_list_index;
    public string _type;
    public string _name;
    public Card.Rarity card_rarity;
    public int card_id;
    public int price;
    public Shop_init shop;
    public TMP_Text _price;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _price.text = price.ToString();
    }
    public void Buy()
    {
        shop.Buy(shop_list_index);
    }
    public void Buy_card()
    {
        shop.Buy_card(shop_list_index);
    }
}
