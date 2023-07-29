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
    public GameObject Events_grid;
    public GameObject Bag;
    public GameObject Relic;
    public GameObject Equipment;
    public GameObject Equipment_list;
    Dictionary<string,UI_Show_Text> equipment = new Dictionary<string, UI_Show_Text>();
    Dictionary<string,UI_Show_Text> relic = new Dictionary<string, UI_Show_Text>();
    Dictionary<string,UI_Show_Text> item = new Dictionary<string, UI_Show_Text>();
    Dictionary<string,Sprite> equipment_image = new Dictionary<string, Sprite>();
    Dictionary<string,Sprite> relic_image = new Dictionary<string, Sprite>();
    Dictionary<string,Sprite> item_image = new Dictionary<string, Sprite>();
    Dictionary<string,int> equipment_rarity = new Dictionary<string, int>();
    Dictionary<string,int> relic_rarity = new Dictionary<string, int>();
    Dictionary<string,int> item_rarity = new Dictionary<string, int>();
    Dictionary<int,GameObject> id_to_item = new Dictionary<int, GameObject>();
    int id = 0;
    List<GameObject> bag_list = new List<GameObject>();
    List<GameObject> equipment_list = new List<GameObject>();
    List<GameObject> relic_list = new List<GameObject>();
    List<int> id_list = new List<int>();
    ///<summary>for test</summary>
    public void _add_item()
    {
        // int i = Random.Range(0,item_list.Length);
        Add_Item("107","item");
        Add_Item("119","item");
        // Add_Item("s1","item");

        // Map_System.New_Game();

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
        // List<int> tmp = Return_Equipment();
        // foreach(var i in tmp)
        // {
        //     print("equipment"+i.ToString());
        // }
        // tmp = Return_Relic();
        // foreach(var i in tmp)
        // {
        //     print("relic"+i.ToString());
        // }
    }
    void OnDisable()
    {
        save();   
    }
    public void save()
    {
        Bag_Save.Save_Data("bag",bag_list);
        Bag_Save.Save_Data("relic",relic_list);
        Bag_Save.Save_Data("equipment",equipment_list);
    }
    public bool Bag_Full()
    {
        if(bag_list.Count==12)return true;
        else return false;
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
            GetComponent<Relic_Implement>().Handle_Relic_immediate(int.Parse(name));
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
                equipment_rarity.Add(i.name,i.description.rarity);
            }
            if(i.type == "item")
            {
                item.Add(i.name,i.description);
                item_image.Add(i.name,i.picture);
                item_rarity.Add(i.name,i.description.rarity);
            }
            if(i.type == "relic")
            {
                relic.Add(i.name,i.description);
                relic_image.Add(i.name,i.picture);
                relic_rarity.Add(i.name,i.description.rarity);
            }
        }
    }
    /// <summary>
    /// 若要刪除背包物品別用這個，用Bag_del_item(int id);
    /// </summary>

    public void del_item(int _id)
    {
        // print("now delete");
        if(bag_list.Contains(id_to_item[_id])) bag_list.Remove(id_to_item[_id]);
        else equipment_list.Remove(id_to_item[_id]);
        Destroy(id_to_item[_id]);
        reset_pos();
    }
    /// <summary>
    /// 若要刪除背包物品用這個
    /// </summary>
    /// <param name="id">傳入物品 ID</param>
    /// <param name="type">relic or equipment or item</param>
    public void Bag_del_item(int id,string type)
    {
        switch(type)
        {
            case "relic":
                for(int i=0;i<relic_list.Count;i++)
                {
                    if(relic_list[i].GetComponent<Item_Effect>()._name == id.ToString())
                    {
                        Relic_Del_item(id);
                        break;
                    }
                }
                break;
            case "equipment":
                for(int i=0;i<equipment_list.Count;i++)
                {
                    if(equipment_list[i].GetComponent<Item_Effect>()._name == id.ToString())
                    {
                        del_item(equipment_list[i].GetComponent<Item_Effect>().list_index);
                        break;
                    }
                }
                for(int i=0;i<bag_list.Count;i++)
                {
                    if(bag_list[i].GetComponent<Item_Effect>()._type == "item")continue;
                    if(bag_list[i].GetComponent<Item_Effect>()._name == id.ToString())
                    {
                        del_item(bag_list[i].GetComponent<Item_Effect>().list_index);
                        break;
                    }
                }
                break;
            case "item":
                for(int i=0;i<bag_list.Count;i++)
                {
                    if(bag_list[i].GetComponent<Item_Effect>()._name == id.ToString())
                    {
                        del_item(bag_list[i].GetComponent<Item_Effect>().list_index);
                        break;
                    }
                }
                break;
            default:
                break;
        }
    }
    public void Relic_Del_item(int id)
    {
        for(int i=0;i<relic_list.Count;i++)
        {
            if(relic_list[i].GetComponent<Item_Effect>()._name == id.ToString())
            {
                Destroy(relic_list[i]);
                relic_list.Remove(relic_list[i]);
                break;
            }
        }
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
        for(int i=0;i<relic_list.Count;i++)
        {
            GameObject new_item =  relic_list[i];
            int index = relic_list.Count % 12;
            float x = 14 - 6.4f + index * 0.8f;
            if(x >= 14)x = 14;
            if(relic_list.Count<12) new_item.GetComponent<UI_Control>().reset_pos(this.transform,new Vector3(x,0,0));
            if(relic_list.Count>=12) new_item.GetComponent<UI_Control>().reset_pos(this.transform,new Vector3(x,-0.8f,0));
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
    ///<summary>回傳正在裝備中的裝備</summary>
    public List<int> Return_Equipment()
    {
        List<int> equipment = new List<int>();
        for(int i=0;i<equipment_list.Count;i++)
        {
            string n = equipment_list[i].GetComponent<UI_Control>().UI_information.name;
            equipment.Add(int.Parse(n));
        }
        return equipment;
    }
    ///<summary>
    ///回傳正在背包裡的所有物品，包含裝備和道具
    ///其中回傳list<list<int>>
    ///list[0]內放入背包內道具
    ///list[1]內放入背包內裝備
    ///</summary>
    public List<List<int>> Return_All_Item_In_Bag()
    {
        List<List<int>> ans = new List<List<int>>();
        ans[0] = new List<int>();
        ans[1] = new List<int>();
        for(int i=0;i<bag_list.Count;i++)
        {
            if(bag_list[i].GetComponent<Item_Effect>().item_type == Item_Effect.Type.equipment_off)
            {
                string n = bag_list[i].GetComponent<UI_Control>().UI_information.name;
                ans[1].Add(int.Parse(n));
            }
            else if(bag_list[i].GetComponent<Item_Effect>().item_type == Item_Effect.Type.item)
            {
                string n = bag_list[i].GetComponent<UI_Control>().UI_information.name;
                ans[0].Add(int.Parse(n));
            }
        }
        return ans;
    }
    ///<summary>回傳所有裝備(包含未裝備)</summary>
    public List<int> Return_All_Equipment()
    {
        id_list.Clear();
        List<int> equipment = new List<int>();
        for(int i=0;i<equipment_list.Count;i++)
        {
            string n = equipment_list[i].GetComponent<UI_Control>().UI_information.name;
            equipment.Add(int.Parse(n));
            id_list.Add(equipment_list[i].GetComponent<Item_Effect>().list_index);
        }
        for(int i=0;i<bag_list.Count;i++)
        {
            if(bag_list[i].GetComponent<Item_Effect>().item_type == Item_Effect.Type.equipment_off)
            {
                string n = bag_list[i].GetComponent<UI_Control>().UI_information.name;
                id_list.Add(bag_list[i].GetComponent<Item_Effect>().list_index);
                equipment.Add(int.Parse(n));
            }
        }
        return equipment;
    }

    public List<int> Return_All_Relic()
    {
        List<int> relic = new List<int>();
        for(int i=0;i<relic_list.Count;i++)
        {
            string n = relic_list[i].GetComponent<UI_Control>().UI_information.name;
            relic.Add(int.Parse(n));
        }
        return relic;
    }
    public void clear()
    {
        foreach (var item in bag_list)
        {
            Destroy(item);
        }
        bag_list.Clear();
        foreach (var item in equipment_list)
        {
            Destroy(item);
        }
        equipment_list.Clear();
        foreach (var item in relic_list)
        {
            Destroy(item);
        }
        relic_list.Clear();
    }

    public void Show_Equipment_list(int select_num,int option_num)
    {
        Equipment_list.SetActive(true);
        Equipment_list.GetComponent<Equipment_Events>().Set_Num(select_num,option_num);
        List<int> tmp = Return_All_Equipment();
        float index = 0;
        int i = 0;
        foreach (var item in tmp)
        {

            UI_Show_Text _item = equipment[item.ToString()];
            GameObject new_item = Instantiate(Events_grid,new Vector3(0,0,0),Quaternion.identity,Equipment_list.transform);
            new_item.GetComponent<UI_Control>().UI_information = _item;
            new_item.GetComponent<Button>().onClick.AddListener(delegate{
                Equipment_list.GetComponent<Equipment_Events>().Set_active(new_item,equipment_rarity[item.ToString()],item);
            } );
            
            new_item.GetComponent<Image>().sprite = equipment_image[item.ToString()];
            new_item.GetComponent<Transform>().SetParent(Equipment_list.transform);
            new_item.GetComponent<Item_Effect>().list_index = id_list[i];
            new_item.GetComponent<UI_Control>().reset_pos(this.transform,new Vector3(index,-3.6f,0));
            index += 0.8f;
            i+=1;
        }

    }
    public void Change_Equipment(int from,int to)
    {
        for(int i=0;i<bag_list.Count;i++)
        {
            if(bag_list[i].GetComponent<Item_Effect>()._type != "equipment")continue;
            Debug.Log("here"+bag_list[i].GetComponent<Item_Effect>().name);
            if(bag_list[i].GetComponent<Item_Effect>()._name == from.ToString())
            {
                UI_Show_Text _item = equipment[to.ToString()];
                bag_list[i].GetComponent<UI_Control>().UI_information = _item;
                bag_list[i].GetComponent<UI_Control>().Change_description();
                bag_list[i].GetComponent<Image>().sprite = equipment_image[to.ToString()];
                bag_list[i].GetComponent<Item_Effect>()._name = to.ToString();
                print("now is here");
                return;
            }
        }
        for(int i=0;i<equipment_list.Count;i++)
        {
            if(equipment_list[i].GetComponent<Item_Effect>()._type != "equipment")continue;
            if(equipment_list[i].GetComponent<Item_Effect>()._name == from.ToString())
            {
                UI_Show_Text _item = equipment[to.ToString()];
                equipment_list[i].GetComponent<UI_Control>().UI_information = _item;
                equipment_list[i].GetComponent<UI_Control>().Change_description();
                equipment_list[i].GetComponent<Image>().sprite = equipment_image[to.ToString()];
                equipment_list[i].GetComponent<Item_Effect>()._name = to.ToString();
                return;
            }
        }
    }
    public int[] equipment_type_num()
    {
        int[] r_list = new int[3];
        r_list[0] = 0;
        r_list[1] = 0;
        r_list[2] = 0;
        for(int i=0;i<equipment_list.Count;i++)
        {
            string n = equipment_list[i].GetComponent<UI_Control>().UI_information.name;
            r_list[equipment_rarity[n]-1] += 1;
        }
        for(int i=0;i<bag_list.Count;i++)
        {
            if(bag_list[i].GetComponent<Item_Effect>().item_type == Item_Effect.Type.equipment_off)
            {
                string n = bag_list[i].GetComponent<UI_Control>().UI_information.name;
                r_list[equipment_rarity[n]-1] += 1;
            }
        }
        return r_list;
    }
    public void Add_Random_Equipment()
    {
        int e_id = Random.Range(1,31);
        Add_Item(e_id.ToString(), "equipment");
    }
    public void Remove_Random_Equipment()
    {
        if(equipment_list.Count==0)
        {
            for(int i=0;i<bag_list.Count;i++)
            {
                if(bag_list[i].GetComponent<Item_Effect>()._type == "equipment")
                {
                    del_item(bag_list[i].GetComponent<Item_Effect>().list_index);
                    break;
                }
            }
        }
        else
        {
            int r = Random.Range(0,equipment_list.Count);
            del_item(equipment_list[r].GetComponent<Item_Effect>().list_index);
        }
    }
    public void Remove_Random_Relic()
    {
        int r = Random.Range(0,relic_list.Count);
        Relic_Del_item(int.Parse(relic_list[r].GetComponent<Item_Effect>()._name));
    }
    public void Add_Random_Relic()
    {
        int e_id = Random.Range(1,31);
        while(Have_Item("relic",e_id.ToString()))
        {
            e_id += 1;
            if(e_id>30)e_id = 1;
        }
        Add_Item(e_id.ToString(),"relic");
    }
}
