using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Bag_Save
{
    [System.Serializable]
    public struct Data{ //用來放需要存的資料
        public  List<string> bag_type ;
        public  List<string> bag_name ;
        public  List<string> relic_type ;
        public  List<string> relic_name ;
        public  List<string> equipment_type ;
        public  List<string> equipment_name ;
    }
    public static List<string> bag_type = new List<string>();
    public static List<string> bag_name = new List<string>();
    public static List<string> relic_type = new List<string>();
    public static List<string> relic_name = new List<string>();
    public static List<string> equipment_type = new List<string>();
    public static List<string> equipment_name = new List<string>();
    public static void  Save_Data(string type,List<GameObject> list)
    {
        
        clear(type);
        Debug.Log(type);
        Debug.Log(list.Count);
        for(int i=0;i<list.Count;i++)
        {
            Item_Effect tmp = list[i].GetComponent<Item_Effect>();
            if(type=="bag")
            {
                bag_name.Add(tmp._name);
                bag_type.Add(tmp._type);
            }
            if(type=="relic")
            {
                relic_name.Add(tmp._name);
                relic_type.Add(tmp._type);
            }
            if(type=="equipment")
            {
                equipment_name.Add(tmp._name);
                equipment_type.Add(tmp._type);
            }
        }
        Data save_data = new Data{
            bag_name = bag_name,
            bag_type = bag_type,
            relic_name = relic_name,
            relic_type = relic_type,
            equipment_name = equipment_name,
            equipment_type = equipment_type           
        };
        string jsonInfo = JsonUtility.ToJson(save_data,true);
        File.WriteAllText(Application.dataPath+"/Save_Data/UI_Data", jsonInfo);
    }
    public static void  Load_Data(Bag_System bag_System)
    {
        if(!File.Exists(Application.dataPath+"/Save_Data/UI_Data"))return;
        bag_System.clear();
        string LoadData = File.ReadAllText(Application.dataPath+"/Save_Data/UI_Data");
        Data Load = JsonUtility.FromJson<Data>(LoadData);
        bag_name = Load.bag_name;
        bag_type = Load.bag_type;
        relic_name = Load.relic_name;
        relic_type = Load.relic_type;
        equipment_name = Load.equipment_name;
        equipment_type = Load.equipment_type;    
        for(int i=0;i<bag_type.Count;i++)
        {
            bag_System.Add_Item(bag_name[i],bag_type[i]);
        }
        for(int i=0;i<equipment_type.Count;i++)
        {
            bag_System.Add_Item(equipment_name[i],equipment_type[i],true);
            bag_System.wear_equipment(bag_type.Count+i);
        }
        for(int i=0;i<relic_type.Count;i++)
        {
            bag_System.Add_Item(relic_name[i],relic_type[i]);
        }
    }
    
    public static void clear(string type)
    {
        // node_arr.Clear();
        if(type == "bag")
        {       
            bag_name.Clear();
            bag_type.Clear();
        
        }
        if(type == "relic")
        {       
            relic_name.Clear();
            relic_type.Clear();
        }
        if(type == "equipment")
        {
            equipment_name.Clear();
            equipment_type.Clear();
        }
        
    }
}
