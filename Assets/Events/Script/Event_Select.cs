using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Event_Select : MonoBehaviour
{
    [System.Serializable]
    public struct Data{ //用來放需要存的資料
        public List<int> Events_array ;
        public int last_event;
    }
    public static List<int> Events_array = new List<int>();
    static int last_event;
    public static void init()
    {
        for(int i=1;i<=30;i++)
        {
            if(i==17)continue;
            Events_array.Add(i*1000);
        }
        Events_array.Add(11002);
        last_event = -1;
        Save_Data();
    }
    public static int Get_Event()
    {
        Load_Data();
        if(last_event == 6000 || last_event == 6001 || last_event == 12001 || last_event == 12000)
        {
            last_event = last_event + 1;
            Save_Data();
            return last_event;
        }
        int index = Random.Range(0,Events_array.Count);
        int id = Events_array[index];
        Events_array.Remove(id);
        last_event = id;
        Save_Data();
        return id;
    }

    static void Save_Data()
    {
        Data save_data = new Data{
            Events_array = Events_array,        
            last_event = last_event
        };
        string jsonInfo = JsonUtility.ToJson(save_data,true);
        File.WriteAllText(Application.dataPath+"/Save_Data/Events_Data", jsonInfo);
    }
    static void Load_Data()
    {
        string LoadData = File.ReadAllText(Application.dataPath+"/Save_Data/Events_Data");
        Data Load = JsonUtility.FromJson<Data>(LoadData);
        Events_array = Load.Events_array;
        last_event = Load.last_event;
    }
}
