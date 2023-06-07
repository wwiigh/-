using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// [CreateAssetMenu(fileName = "New Map_Save", menuName = "Map_Save")]
public class Map_Save
{
    [Header("是否為新的地圖")]
    //0 for not new, 1 for new
    public int is_new = 1;    
    [Header("第幾關")]
    public int now_level;

    [Header("第幾層")]
    public int now_height;
    // [Header("button perfab")]
    // public Button button;
    // int height = 10;
    // int width = 10;
    // public List<Node> node_arr = new List<Node>();
    public List<int> node_parent_arr = new List<int>();
    public List<int> node_parent_index_arr = new List<int>();
    public List<int> node_next_arr = new List<int>();
    public List<int> node_next_index_arr = new List<int>();
    public List<bool> node_assign_arr = new List<bool>();
    public List<bool> node_valid_arr = new List<bool>();
    public List<char> node_type_arr = new List<char>();
    public Sprite little_monster;
    public Sprite big_monster;
    public Sprite events;
    public Sprite treasure;
    public Sprite altar;
    public Sprite boss;
    public Sprite shop;
    // Init();
    //     
    //     is_valid = node.Return_valid();
    //     type = node.Return_type();
    //     can_assign = node.can_assign;
    
   
    //     Set_button(type);
    public void save_level_height(int save_level,int save_height)
    {
        now_height = save_height;
        now_level = save_level;
    }
    public int return_level()
    {
        return now_level;
    }
    public int return_height()
    {
        return now_height;
    }
    public void save_node(List<Node> nodes)
    {
        clear();
        foreach (var item in nodes)
        {
            
            // Node tmp = new Node();
            // tmp.node = button;
            // tmp.copy(item);
            // height = item.return_height();
            // width = item.return_width();
            node_next_index_arr.Add(item.next.Count);
            foreach (var item2 in item.next)
            {
                node_next_arr.Add(item2);
            }
            node_parent_index_arr.Add(item.parent.Count);

            foreach (var item2 in item.parent)
            {
                node_parent_arr.Add(item2);
            }
            little_monster = item.little_monster;
            big_monster = item.big_monster;
            events = item.events;
            treasure = item.treasure;
            altar = item.altar;
            boss = item.boss;
            shop = item.shop;
            node_valid_arr.Add(item.Return_valid());
            node_type_arr.Add(item.Return_type());
            node_assign_arr.Add(item.can_assign);
            // node_arr.Add(tmp);    
        }
    }
    public void copy(List<Node> nodes)
    {
        int index_parent = 0;
        int index_next = 0;
        for(int i=0;i<node_assign_arr.Count;i++)
        {
            // nodes[i].Set_height(height);
            // nodes[i].Set_width(width);
            // level = node_parent_arr.Count;
            for(int j=0;j<node_parent_index_arr[i];j++)
            {
                nodes[i].parent.Add(node_parent_arr[index_parent+j]);
            }
            index_parent += node_parent_index_arr[i];
            for(int j=0;j<node_next_index_arr[i];j++)
            {
                nodes[i].next.Add(node_next_arr[index_next+j]);
            }
            index_next += node_next_index_arr[i];
            
            if(node_valid_arr[i]==true)
                nodes[i].Set_valid();
            nodes[i].Set_type(node_type_arr[i]);
            nodes[i].can_assign = node_assign_arr[i];
        }
    }
    public void clear()
    {
        // node_arr.Clear();
        node_parent_arr.Clear();
        node_parent_index_arr.Clear();
        node_next_index_arr.Clear();
        node_next_arr.Clear();
        node_assign_arr.Clear();
        node_valid_arr.Clear();
        node_type_arr.Clear();
    }
}
