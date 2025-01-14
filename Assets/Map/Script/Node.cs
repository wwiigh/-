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
    [Header("地圖控制")]
    public Map_Generate map;
    [Header("顯示商店")]
    public GameObject shop_object;
    [Header("顯示祭壇")]
    public GameObject altar_object;
    [Header("顯示戰鬥")]
    public GameObject battle_object;
    [Header("顯示戰鬥裝備")]
    public GameObject battle_object_equipment;
    [Header("顯示寶箱")]
    public GameObject treasure_object;
    [Header("顯示事件")]
    public GameObject event_object;
    public List<GameObject> All_Battle_Obj;
    public static Node ActiveNode;
    public GameObject circle;
    Transition transition;
    public void check()
    {
        ActiveNode = this;
        if(transition==null)transition = FindObjectOfType<Transition>();
        // print("check");
        // print(height.ToString()+" "+width.ToString());
        switch(type)
        {
            case 's':
                // Invoke(nameof(click_action_shop),0.6f);
                click_action_shop();
                break;
            case 'f':
                Invoke(nameof(click_action_battle_normal),0.6f);
                // click_action_battle_normal();
                transition.Play();
                break;
            case 'F':
                Invoke(nameof(click_action_battle_Elite),0.6f);
                // click_action_battle_Elite(); 
                transition.Play();
                break;
            case 'e':
                Invoke(nameof(click_action_event),0.6f);
                // click_action_event();
                transition.Play();
                break;
            case 'm':
                Invoke(nameof(click_action_story),0.6f);
                transition.Play();
                break;
            case 't':
                // Invoke(nameof(click_action_treasure),0.6f);
                click_action_treasure();
                break;
            case 'n':
                
                break;
            case 'b':
                Invoke(nameof(click_action_battle_Boss),0.6f);
                // click_action_battle_Boss();
                transition.Play();
                break;
            case 'h':
                Invoke(nameof(click_action_altar),0.6f);
                // click_action_altar();
                transition.Play();
                break;
        }
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
    public void click_action_shop()
    {
        shop_object.SetActive(true);
        FindObjectOfType<Map_System>().Change_state(Map_System.map_state.shop);
        StartCoroutine(wait_finish(shop_object));
    }
    public void click_action_altar()
    {
        altar_object.SetActive(true);
        FindObjectOfType<Map_System>().Change_state(Map_System.map_state.altar);
        StartCoroutine(wait_finish(altar_object));
    }
    public void click_action_battle_normal()
    {
        battle_object.SetActive(true);
        battle_object_equipment.SetActive(true);
        foreach (var item in All_Battle_Obj)
        {
            item.SetActive(true);
        }
        FindObjectOfType<Map_System>().Change_state(Map_System.map_state.fight);
        // Map_System.now_state = Map_System.map_state.fight;
        StartCoroutine(start_battle());
        // Relic_Implement.Handle_Relic_Before_Battle();
        FindObjectOfType<Equipment_Control>().Enable_Equipment();
        FindObjectOfType<BattleController>().EnterBattle(BattleController.BattleType.Normal);
        StartCoroutine(wait_finish(battle_object));
    }
    public void click_action_battle_Elite()
    {
        battle_object.SetActive(true);
        battle_object_equipment.SetActive(true);
        foreach (var item in All_Battle_Obj)
        {
            item.SetActive(true);
        }
        // Map_System.now_state = Map_System.map_state.fight;
        StartCoroutine(start_battle());
        
        FindObjectOfType<Equipment_Control>().Enable_Equipment();
        FindObjectOfType<Map_System>().Change_state(Map_System.map_state.fight);
        FindObjectOfType<BattleController>().EnterBattle(BattleController.BattleType.Elite);
        StartCoroutine(wait_finish(battle_object));
    }
    public void click_action_battle_Boss()
    {
        battle_object.SetActive(true);
        battle_object_equipment.SetActive(true);
        foreach (var item in All_Battle_Obj)
        {
            item.SetActive(true);
        }
        // Map_System.now_state = Map_System.map_state.fight;
        // Relic_Implement.Handle_Relic_Before_Battle();
        StartCoroutine(start_battle());
        FindObjectOfType<Equipment_Control>().Enable_Equipment();
        FindObjectOfType<Map_System>().Change_state(Map_System.map_state.fight);
        FindObjectOfType<BattleController>().EnterBattle(BattleController.BattleType.Boss);
        StartCoroutine(wait_finish(battle_object));
    }
    public void click_action_treasure()
    {
        treasure_object.SetActive(true);
        FindObjectOfType<Map_System>().Change_state(Map_System.map_state.treasure);
        StartCoroutine(wait_finish(treasure_object));
    }
    public void click_action_event()
    {
        event_object.SetActive(true);
        Random.InitState((int)Time.time);
        int id = Event_Select.Get_Event();
        event_object.GetComponent<GameEvent>().LoadEvent(id,EventClass.Type.normal);
        FindObjectOfType<Map_System>().Change_state(Map_System.map_state.events);
        StartCoroutine(wait_finish(event_object));
    }
    public void ending()
    {
        event_object.SetActive(true);
        if(FindObjectOfType<Bag_System>().Have_Item("relic","3"))
        {
            event_object.GetComponent<GameEvent>().LoadEvent(501,EventClass.Type.story);
        }
        else
        {
            event_object.GetComponent<GameEvent>().LoadEvent(401,EventClass.Type.story);
        }
        FindObjectOfType<Map_System>().Change_state(Map_System.map_state.ending);
    }
    public void click_action_story()
    {
        int level = Map_Generate.now_level;
        event_object.SetActive(true);
        switch (level)
        {
            case 1:
                event_object.GetComponent<GameEvent>().LoadEvent(101,EventClass.Type.story);
                break;
            case 2:
                event_object.GetComponent<GameEvent>().LoadEvent(201,EventClass.Type.story);
                break;
            case 3:
                event_object.GetComponent<GameEvent>().LoadEvent(301,EventClass.Type.story);
                break;
            default:
                break;
        }
        FindObjectOfType<Map_System>().Change_state(Map_System.map_state.events);
        StartCoroutine(wait_finish(event_object));
    }
    IEnumerator start_battle()
    {
        // yield return new WaitForSeconds(1);
        yield return null;
        Relic_Implement.Handle_Relic_Before_Battle();
    }
    IEnumerator wait_finish(GameObject _ob)
    {
        while(_ob.activeSelf == true)
        {
            // if(Map_System.now_state == Map_System.map_state.fight)
            // {
            //     FindObjectOfType<Map_Node_Action>().Check_Battle_State();
            // }
            yield return new WaitForEndOfFrame();
        }
        if(transition==null)transition = FindObjectOfType<Transition>();
        if(type == 'b'||type == 'F'||type == 'f')
        {
            transition.Play();
            yield return new WaitForSeconds(0.6f);
        }
        foreach (var item in All_Battle_Obj)
        {
            item.SetActive(false);
        }
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Card");
        foreach (var item in tmp)
        {
            Destroy(item);
        }
        tmp = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in tmp)
        {
            Destroy(item);
        }
        tmp = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var item in tmp)
        {
            Destroy(item);
        }
        map.Go_to_next();
        FindObjectOfType<Map_System>().Change_state(Map_System.map_state.normal);
    }
    
}
