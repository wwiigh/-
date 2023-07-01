using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Node:MonoBehaviour
{

    //設定第幾層第幾個點
    int height;
    int width;

    // 查看是否為可用格子
    bool is_valid;


    // 分辨事件，戰鬥等
    // s 商店
    // f 一般戰鬥
    // F 菁英
    // e 事件
    // t 寶箱
    // n none
    // b boss
    // h 祭壇
    // m 主線
    char type;
    //現在持有的點
    public Button node;

    //生成事件時是否能使用
    public bool can_assign;
    //下面接了那些點
    public List<int> next;
    //上面接了那些點
    public List<int> parent;
    //是不是可以點擊
    public bool can_go;
    public Sprite little_monster;
    public Sprite big_monster;
    public Sprite events;
    public Sprite treasure;
    public Sprite altar;
    public Sprite boss;
    public Sprite shop;
    public void check()
    {
        print("check");
        print(height.ToString()+" "+width.ToString());
        
    }

    public bool Return_valid()
    {
        return is_valid;
    }
    public void Set_valid()
    {
        is_valid = true;
        node.image.color = Color.white;
    }
    public void Set_type(char t)
    {
        type = t;
        Set_button(t);
    }
    public void Set_height(int h)
    {
        height = h;
    }
    public void Set_width(int w)
    {
        width = w;
    }
    public int Get_height()
    {
        return height;
    }
    public int Get_width()
    {
        return width;
    }
    public char Return_type()
    {
        return type;
    }
    public void Init()
    {
        can_assign = true;
        height = 0;
        width = 0;
        is_valid = false;
        type = 'n';
        node.image.color = Color.black;
        next = new List<int>();
        parent = new List<int>();
        return;
    }
    //設定點的大小
    public void Set_node_size(Vector2 node_size)
    {
        node.GetComponent<RectTransform>().sizeDelta = node_size;
    }

    void Set_button(char _type)
    {
        switch(_type)
        {
            case 's':
                node.GetComponent<Image>().sprite = shop;
                break;
            case 'f':
                node.GetComponent<Image>().sprite = little_monster;
                break;
            case 'F':
                node.GetComponent<Image>().sprite = big_monster;
                break;
            case 'e':
                node.GetComponent<Image>().sprite = events;
                break;
            case 'm':
                node.GetComponent<Image>().sprite = events;
                break;
            case 't':
                node.GetComponent<Image>().sprite = treasure;
                break;
            case 'n':
                node.GetComponent<Image>().sprite = null;
                break;
            case 'b':
                node.GetComponent<Image>().sprite = boss;
                break;
            case 'h':
                node.GetComponent<Image>().sprite = altar;
                break;
        }
    }
    public int return_height()
    {
        return height;
    }
    public int return_width()
    {
        return width;
    }
    public void copy(Node node)
    {
        Init();
        height = node.return_height();
        width = node.return_width();
        is_valid = node.Return_valid();
        type = node.Return_type();
        can_assign = node.can_assign;
        foreach (var item in node.next)
        {
            next.Add(item);
        }
        foreach (var item in node.parent)
        {
            parent.Add(item);
        }
        little_monster = node.little_monster;
        big_monster = node.big_monster;
        events = node.events;
        treasure = node.treasure;
        altar = node.altar;
        boss = node.boss;
        shop = node.shop;
        Set_button(type);
    }
}
