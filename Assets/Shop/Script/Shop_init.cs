using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_init : MonoBehaviour
{
    [System.Serializable]
    public struct Product_Information
    {
        public Sprite picture;
        public string type;
        public string name;
        public int price;
        public UI_Show_Text description;
    }
    public GameObject bag_system;
    public GameObject Product;
    public int num_per_row;
    public GameObject background;
    public Product_Information[] product_list;
    public List<GameObject> sell_list = new List<GameObject>();
    public Dictionary<string,UI_Show_Text> equipment = new Dictionary<string, UI_Show_Text>();
    public Dictionary<string,UI_Show_Text> relic = new Dictionary<string, UI_Show_Text>();
    public Dictionary<string,UI_Show_Text> item = new Dictionary<string, UI_Show_Text>();
    public Dictionary<string,Sprite> equipment_image = new Dictionary<string, Sprite>();
    public Dictionary<string,Sprite> relic_image = new Dictionary<string, Sprite>();
    public Dictionary<string,Sprite> item_image = new Dictionary<string, Sprite>();
    // Start is called before the first frame update
    void Awake()
    {
        init();

    }
    void Start()
    {
        
        
    }
    void OnEnable()
    {
        Generate_product();
    }
    void OnDisable()
    {
        for(int i=0;i<sell_list.Count;i++)
        {
            Destroy(sell_list[i]);
        }
        sell_list.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void Add_Product(string name, string type,int price)
    {
        if(type == "item")
        {
            int index = sell_list.Count % num_per_row;
            float x = index * 1.8f;
            if(index == 8)x -= 0.4f;
            UI_Show_Text _item = item[name];
            GameObject new_item = Instantiate(Product,new Vector3(0,0,0),Quaternion.identity,background.transform);
            new_item.GetComponentInChildren<UI_Control>().UI_information = _item;
            new_item.GetComponentInChildren<Image>().sprite = item_image[name];
            new_item.GetComponent<Transform>().SetParent(background.transform);
            new_item.GetComponentInChildren<Shop_Buy>().shop_list_index = sell_list.Count;
            new_item.GetComponentInChildren<Shop_Buy>()._type = type;
            new_item.GetComponentInChildren<Shop_Buy>()._name = name;
            new_item.GetComponentInChildren<Shop_Buy>().shop = this;
            new_item.GetComponentInChildren<Shop_Buy>().price = price;
            if(sell_list.Count<num_per_row) new_item.GetComponentInChildren<UI_Control>().reset_pos(this.transform,new Vector3(x,-4f,0));
            if(sell_list.Count>=num_per_row) new_item.GetComponentInChildren<UI_Control>().reset_pos(this.transform,new Vector3(x,-7f,0));
            sell_list.Add(new_item);
        }
        if(type == "relic")
        {
            int index = sell_list.Count % num_per_row;
            float x = index * 1.8f;
            if(index == 8)x -= 0.4f;
            UI_Show_Text _item = relic[name];
            GameObject new_item = Instantiate(Product,new Vector3(0,0,0),Quaternion.identity,background.transform);
            new_item.GetComponentInChildren<UI_Control>().UI_information = _item;
            new_item.GetComponentInChildren<Image>().sprite = relic_image[name];
            new_item.GetComponent<Transform>().SetParent(background.transform);
            new_item.GetComponentInChildren<Shop_Buy>().shop_list_index = sell_list.Count;
            new_item.GetComponentInChildren<Shop_Buy>()._type = type;
            new_item.GetComponentInChildren<Shop_Buy>()._name = name;
            new_item.GetComponentInChildren<Shop_Buy>().shop = this;
            new_item.GetComponentInChildren<Shop_Buy>().price = price;
            if(sell_list.Count<num_per_row) new_item.GetComponentInChildren<UI_Control>().reset_pos(this.transform,new Vector3(x,-4f,0));
            if(sell_list.Count>=num_per_row) new_item.GetComponentInChildren<UI_Control>().reset_pos(this.transform,new Vector3(x,-7f,0));
            sell_list.Add(new_item);
        }
        if(type == "equipment")
        {
            int index = sell_list.Count % num_per_row;
            float x = index * 1.8f;
            if(index == 8)x -= 0.4f;
            UI_Show_Text _item = equipment[name];
            GameObject new_item = Instantiate(Product,new Vector3(0,0,0),Quaternion.identity,background.transform);
            new_item.GetComponentInChildren<UI_Control>().UI_information = _item;
            new_item.GetComponentInChildren<Image>().sprite = equipment_image[name];
            new_item.GetComponent<Transform>().SetParent(background.transform);
            new_item.GetComponentInChildren<Shop_Buy>().shop_list_index = sell_list.Count;
            new_item.GetComponentInChildren<Shop_Buy>()._type = type;
            new_item.GetComponentInChildren<Shop_Buy>()._name = name;
            new_item.GetComponentInChildren<Shop_Buy>().shop = this;
            new_item.GetComponentInChildren<Shop_Buy>().price = price;
            if(sell_list.Count<num_per_row) new_item.GetComponentInChildren<UI_Control>().reset_pos(this.transform,new Vector3(x,-4f,0));
            if(sell_list.Count>=num_per_row) new_item.GetComponentInChildren<UI_Control>().reset_pos(this.transform,new Vector3(x,-7f,0));
            sell_list.Add(new_item);
        }
        
    }
    void init()
    {
        foreach(var i in product_list)
        {
            if(i.type == "equipment")
            {
                equipment.Add(i.name,i.description);
                equipment_image.Add(i.name,i.picture);
            }
            if(i.type == "item")
            {
                item.Add(i.name,i.description);
                item_image.Add(i.name,i.picture);
            }
            if(i.type == "relic")
            {
                relic.Add(i.name,i.description);
                relic_image.Add(i.name,i.picture);
            }
        }
    }
    void Add_Item()
    {
        int i = Random.Range(0,product_list.Length);
        while(product_list[i].type=="relic"&&(Have_Relic(0,product_list[i].name)
        ||Have_Relic(1,product_list[i].name)))
        {
            i+=1;
            if(i>=product_list.Length)i=0;
        }
        Add_Product(product_list[i].name,product_list[i].type,product_list[i].price);
    }
    public void Buy(int index)
    {
        //扣掉錢
        //..........
        
        GameObject o = sell_list[index];
        string type = o.GetComponentInChildren<Shop_Buy>()._type;
        string name = o.GetComponentInChildren<Shop_Buy>()._name;
        int money = o.GetComponentInChildren<Shop_Buy>().price;
        if(Global.money<money)return;
        else Global.AddMoney(-money);
        bool buy_success = bag_system.GetComponent<Bag_System>().Add_Item(name,type);
        if(buy_success==true)
        {
            Button[] tmp = o.GetComponentsInChildren<Button>();
            for(int i=0;i<tmp.Length;i++)tmp[i].gameObject.SetActive(false);
        }
        
    }
    //0 for bag 1 for shop
    bool Have_Relic(int position,string name)
    {
        if(position==0)
        {
            return bag_system.GetComponent<Bag_System>().Have_Item("relic",name);
        }
        else
        {
            for(int i=0;i<sell_list.Count;i++)
            {
                string t = sell_list[i].GetComponentInChildren<Shop_Buy>()._type;
                string n = sell_list[i].GetComponentInChildren<Shop_Buy>()._name;
                if(t=="relic"&&n==name)return true;
            }
            return false;
        }
    }
    bool Have_Item(string type,string name)
    {
        
        for(int i=0;i<sell_list.Count;i++)
        {
            string t = sell_list[i].GetComponentInChildren<Shop_Buy>()._type;
            string n = sell_list[i].GetComponentInChildren<Shop_Buy>()._name;
            if(t==type&&n==name)return true;
        }
        return false;
    }
    void Generate_product()
    {
        for(int i=0;i<5;i++)
        {
            //generate card
        }
        for(int i=0;i<3;i++)
        {
            int index = Random.Range(0,11);
            while(Have_Item(product_list[index].type,product_list[index].name))
            {
                index+=1;
                if(index>=11)index=0;
            }
            Add_Product(product_list[index].name,product_list[index].type,product_list[index].price);
        }
        for(int i=0;i<3;i++)
        {
            int index = Random.Range(11,29);
            while(Have_Relic(0,product_list[index].name)||Have_Relic(1,product_list[index].name))
            {
                index+=1;
                if(index>=29)index=11;
            }
            Add_Product(product_list[index].name,product_list[index].type,product_list[index].price);
        }
        for(int i=0;i<3;i++)
        {
            int index = Random.Range(29,59);
            while(Have_Item(product_list[index].type,product_list[index].name))
            {
                index+=1;
                if(index>=59)index=29;
            }
            Add_Product(product_list[index].name,product_list[index].type,product_list[index].price);
        }
    }
}
