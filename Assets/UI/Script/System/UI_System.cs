using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class UI_System : MonoBehaviour
{
    void OnEnable()
    {
        Global.ReadData();
    }
    public static void New_Game()
    {
        Bag_Save.Data save_data = new Bag_Save.Data{
            bag_name = null,
            bag_type = null,
            relic_name = null,
            relic_type = null,
            equipment_name = null,
            equipment_type = null           
        };
        string jsonInfo = JsonUtility.ToJson(save_data,true);
        File.WriteAllText(Application.dataPath+"/Save_Data/UI_Data", jsonInfo);
        
    }
}
