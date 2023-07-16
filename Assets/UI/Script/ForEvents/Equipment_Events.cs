using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment_Events : MonoBehaviour
{
    [System.Serializable]
    public struct equipment_data
    {
        public int id;
        public int rarity;
    }
    public GameObject events;
    public GameObject button;
    public List<equipment_data> data_list = new List<equipment_data>();
    // public List<List<equipment_data>> data_list = new List<List<equipment_data>>();
    int Select_num;
    int now_num;
    int option_num;
    int[] equipment_rarity = new int[2];
    int[] equipment_list_index = new int[2];
    List<int> select_object = new List<int>();
    List<List<equipment_data>> rarity_list = new List<List<equipment_data>>();
    
    void OnEnable()
    {
        button.SetActive(true);
        init();
    }
    void OnDisable()
    {
        select_object.Clear();
        button.SetActive(false);
        events.SetActive(false);
        clear();
    }
    void Update()
    {
        if(now_num == Select_num)
        {
            if(now_num==2&&(equipment_rarity[0]!=equipment_rarity[1]))
            button.GetComponent<Button>().interactable = false;
            else
            button.GetComponent<Button>().interactable = true;

        }
        else 
        button.GetComponent<Button>().interactable = false;

    }

    public void Set_Num(int Equipment_num,int Option_num)
    {
        Select_num = Equipment_num;
        now_num = 0;
        option_num = Option_num;
    }
    public void Set_active(GameObject _gameObject,int rarity,int id)
    {
        // print("count:"+_gameObject.transform.childCount.ToString());
        for(int i=0;i<_gameObject.transform.childCount;i++)
        {
            GameObject child = _gameObject.transform.GetChild(i).gameObject;
            if(child.activeSelf == false && now_num == Select_num)return;
            if(child.name == "board")
                child.SetActive(!child.activeSelf);
            if(child.activeSelf == true)
            {
                equipment_rarity[select_object.Count] = rarity;
                equipment_list_index[select_object.Count] = _gameObject.GetComponent<Item_Effect>().list_index;                now_num += 1;
                select_object.Add(id);
            }
            else 
            {
                now_num -= 1;
                select_object.Remove(id);
            }
        }
    }
    void init()
    {
        rarity_list.Clear();
        for(int i=0;i<3;i++)
            rarity_list.Add(new List<equipment_data>());
        foreach(var item in data_list)
        {
            rarity_list[item.rarity-1].Add(item);
        }
    }
    public void Change_Equipment()
    {
        int g = select_object[0];
        int r = equipment_rarity[0];
        int index = Random.Range(0,rarity_list[r-1].Count);
        switch (option_num)
        {
            case 1:
                FindObjectOfType<Bag_System>().Change_Equipment(g,rarity_list[r-1][index].id);
                Debug.Log("now change to"+rarity_list[r-1][index].id.ToString());
                Debug.Log("now change form"+g);
                break;
            case 2:
                if(FindObjectOfType<Bag_System>().Bag_Full()==true)return;
                int g2 = select_object[1];
                int r2 = equipment_rarity[1];
                int __r = r == 3 ? 2 : r;
                if(r<3)index = Random.Range(0,rarity_list[r].Count);
                FindObjectOfType<Bag_System>().del_item(equipment_list_index[0]);
                FindObjectOfType<Bag_System>().del_item(equipment_list_index[1]);
                FindObjectOfType<Bag_System>().Add_Item(rarity_list[__r][index].id.ToString(),"equipment");
                
                break;
            case 3:
                float p = Random.value;
                int _r = r == 3 ? 2 : r;
                if(r<3)index = Random.Range(0,rarity_list[r].Count);
                if(p<0.45)FindObjectOfType<Bag_System>().Change_Equipment(g,rarity_list[_r][index].id);
                else if(p<0.8)break;
                else FindObjectOfType<Bag_System>().del_item(equipment_list_index[0]);
                break;
            default:
                break;
        }
        this.gameObject.SetActive(false);
    }
    void clear()
    {
        foreach(Transform item in this.transform)
        {
            Destroy(item.gameObject);
        }
    }
}
