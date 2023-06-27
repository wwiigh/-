using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag_System : MonoBehaviour
{
    [System.Serializable]
    public struct Item_information
    {
        public Sprite picture;
        public string type;
        public string name;
        public UI_Show_Text description;
    }
    public Button[] buttons;
    public Item_information[] item_list;
    public GameObject grid;
    public GameObject Bag;
    public GameObject Relic;
    public GameObject Equipment;
    public Dictionary<string,UI_Show_Text> equipment = new Dictionary<string, UI_Show_Text>();
    public Dictionary<string,UI_Show_Text> relic = new Dictionary<string, UI_Show_Text>();
    public Dictionary<string,UI_Show_Text> item = new Dictionary<string, UI_Show_Text>();
    public Dictionary<string,Sprite> equipment_image = new Dictionary<string, Sprite>();
    public Dictionary<string,Sprite> relic_image = new Dictionary<string, Sprite>();
    public Dictionary<string,Sprite> item_image = new Dictionary<string, Sprite>();
    public Dictionary<int,GameObject> id_to_item = new Dictionary<int, GameObject>();
    int id = 0;
    List<GameObject> bag_list = new List<GameObject>();
    List<GameObject> equipment_list = new List<GameObject>();
    List<GameObject> relic_list = new List<GameObject>();
    public void _add_item()
    {
        int i = Random.Range(0,item_list.Length);
        Add_Item(item_list[i].name,item_list[i].type);
        // Add_Item("s1","item");
    }
    // Start is called before the first frame update
    void Awake()
    {
        init();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        Bag_Save.Load_Data(this);
    }
    void OnDisable()
    {
        Bag_Save.Save_Data("bag",bag_list);
        Bag_Save.Save_Data("relic",relic_list);
        Bag_Save.Save_Data("equipment",equipment_list);
    }

    public bool Add_Item(string name, string type,bool force = false)
    {
        if(type == "equipment")
        {
            if(force == false && bag_list.Count==12)return false;
            int index = bag_list.Count % 6;
            float x = index * 0.8f;
            UI_Show_Text _item = equipment[name];
            GameObject new_item = Instantiate(grid,new Vector3(0,0,0),Quaternion.identity,Bag.transform);
            new_item.GetComponent<UI_Control>().UI_information = _item;
            new_item.GetComponent<Image>().sprite = equipment_image[name];
            new_item.GetComponent<Transform>().SetParent(Bag.transform);
            new_item.GetComponent<Item_Effect>().item_type = Item_Effect.Type.equipment_off;
            new_item.GetComponent<Item_Effect>().use = buttons[0];
            new_item.GetComponent<Item_Effect>().close = buttons[1];
            new_item.GetComponent<Item_Effect>().del = buttons[2];
            new_item.GetComponent<Item_Effect>().list_index = id;
            new_item.GetComponent<Item_Effect>()._name = name;
            new_item.GetComponent<Item_Effect>()._type = type;
            if(bag_list.Count<6) new_item.GetComponent<UI_Control>().reset_pos(this.transform,new Vector3(x,0,0));
            if(bag_list.Count>=6) new_item.GetComponent<UI_Control>().reset_pos(this.transform,new Vector3(x,-0.8f,0));
            id_to_item[id] = new_item;
            id+=1;
            bag_list.Add(new_item);
        }
        if(type == "item")
        {
            if(force == false &&bag_list.Count==12)return false;
            int index = bag_list.Count % 6;
            float x = index * 0.8f;
            UI_Show_Text _item = item[name];
            GameObject new_item = Instantiate(grid,new Vector3(0,0,0),Quaternion.identity,Bag.transform);
            new_item.GetComponent<UI_Control>().UI_information = _item;
            new_item.GetComponent<Image>().sprite = item_image[name];
            new_item.GetComponent<Transform>().SetParent(Bag.transform);
            new_item.GetComponent<Item_Effect>().item_type = Item_Effect.Type.item;
            new_item.GetComponent<Item_Effect>().use = buttons[0];
            new_item.GetComponent<Item_Effect>().close = buttons[1];
            new_item.GetComponent<Item_Effect>().del = buttons[2];
            new_item.GetComponent<Item_Effect>().list_index = id;
            new_item.GetComponent<Item_Effect>()._name = name;
            new_item.GetComponent<Item_Effect>()._type = type;
            if(bag_list.Count<6) new_item.GetComponent<UI_Control>().reset_pos(this.transform,new Vector3(x,0,0));
            if(bag_list.Count>=6) new_item.GetComponent<UI_Control>().reset_pos(this.transform,new Vector3(x,-0.8f,0));
            id_to_item[id] = new_item;
            id+=1;
            bag_list.Add(new_item);

        }
        if(type == "relic")
        {
            UI_Show_Text _item = relic[name];
            int index = relic_list.Count % 12;
            float x = 14 - 6.4f + index * 0.8f;
            if(x >= 14)x = 14;
            GameObject new_item = Instantiate(grid,new Vector3(0,0,0),Quaternion.identity,Relic.transform);
            new_item.GetComponent<UI_Control>().UI_information = _item;
            new_item.GetComponent<Image>().sprite = relic_image[name];
            new_item.GetComponent<Transform>().SetParent(Relic.transform);
            new_item.GetComponent<Item_Effect>().item_type = Item_Effect.Type.relic;
            new_item.GetComponent<Item_Effect>().use = buttons[0];
            new_item.GetComponent<Item_Effect>().close = buttons[1];
            new_item.GetComponent<Item_Effect>().del = buttons[2];
            new_item.GetComponent<Item_Effect>()._name = name;
            new_item.GetComponent<Item_Effect>()._type = type;
            if(relic_list.Count<12) new_item.GetComponent<UI_Control>().reset_pos(this.transform,new Vector3(x,0,0));
            if(relic_list.Count>=12) new_item.GetComponent<UI_Control>().reset_pos(this.transform,new Vector3(x,-0.8f,0));
            relic_list.Add(new_item);
        }
        return true;
    }
    void init()
    {
        foreach(var i in item_list)
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
    public void del_item(int _id)
    {
        // print("now delete");
        bag_list.Remove(id_to_item[_id]);
        Destroy(id_to_item[_id]);
        reset_pos();
    }

    void reset_pos()
    {
        for(int i=0;i<bag_list.Count;i++)
        {

            int index = i % 6;
            float x = index * 0.8f;
            GameObject new_item =  bag_list[i];
            if(i<6) new_item.GetComponent<UI_Control>().reset_pos_old(this.transform,new Vector3(x,0,0));
            if(i>=6) new_item.GetComponent<UI_Control>().reset_pos_old(this.transform,new Vector3(x,-0.8f,0));
        }
        for(int i=0;i<equipment_list.Count;i++)
        {
            int index = i % 6;
            float x = index * 0.8f;
            GameObject new_item =  equipment_list[i];
            new_item.GetComponent<UI_Control>().reset_pos_old(this.transform,new Vector3(x+0.8f*6,0,0));
        }
    }
    public bool wear_equipment(int _id)
    {
        if(equipment_list.Count==3)return false;
        GameObject ob = id_to_item[_id];
        bag_list.Remove(id_to_item[_id]);
        equipment_list.Add(id_to_item[_id]);
        id_to_item[_id].transform.parent = Equipment.transform;
        id_to_item[_id].GetComponent<Item_Effect>().item_type = Item_Effect.Type.equipment_on;
        reset_pos();
        return true;

    }
    public bool clear_equipment(int _id)
    {
        if(bag_list.Count==12)return false;
        GameObject ob = id_to_item[_id];
        equipment_list.Remove(id_to_item[_id]);
        bag_list.Add(id_to_item[_id]);
        id_to_item[_id].transform.parent = Bag.transform;
        id_to_item[_id].GetComponent<Item_Effect>().item_type = Item_Effect.Type.equipment_off;
        reset_pos();
        return true;
    }

    public void change_button_pos(Vector3 pos)
    {
        int _index = 0;
        foreach(var i in buttons)
        {
            i.transform.position = pos + new Vector3(0.5f,-0.8f-0.7f*_index,0);
            _index++;
        }
    }
    public bool Have_Item(string type,string name)
    {
        if(type == "equipment")
        {
            for(int i=0;i<bag_list.Count;i++)
            {
                string n = bag_list[i].GetComponent<UI_Control>().UI_information.name;
                if(n == name)return true;
            }
            for(int i=0;i<equipment_list.Count;i++)
            {
                string n = equipment_list[i].GetComponent<UI_Control>().UI_information.name;
                if(n == name)return true;
            }
            return false;
        }
        if(type == "item")
        {
            for(int i=0;i<bag_list.Count;i++)
            {
                string n = bag_list[i].GetComponent<UI_Control>().UI_information.name;
                if(n == name)return true;
            }
            return false;
        }
        if(type == "relic")
        {
            for(int i=0;i<relic_list.Count;i++)
            {
                string n = relic_list[i].GetComponent<UI_Control>().UI_information.name;
                if(n == name)return true;
            }
            return false;
        }
        return true;
    }
    public void Buy()
    {

    }
}
