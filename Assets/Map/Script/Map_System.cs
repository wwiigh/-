using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Map_System : MonoBehaviour
{
    public enum map_state{
        normal,
        fight,
        shop,
        events,
        altar,
        treasure,
        ending,
        open_ui
    }
    public enum map_ui{
        close,
        open
    }
    public static map_state now_state;
    public static map_ui now_ui;
    public void Change_state(map_state state)
    {
        now_state = state;
    }
    public static void New_Game()
    {
        Map_Save save_data = new Map_Save{
            is_new = 1,
            now_level = 1,
            now_height = 9,
            now_width = 0,
            node_assign_arr = null,
            node_parent_arr = null,
            node_parent_index_arr = null,
            node_next_arr = null,
            node_next_index_arr = null,
            node_valid_arr = null,
            node_type_arr = null
        };
        string jsonInfo = JsonUtility.ToJson(save_data,true);
        PlayerPrefs.SetString("Map_Data",jsonInfo);
        // File.WriteAllText(Application.dataPath+"/Save_Data/Map_Data", jsonInfo);
        
    }
}
