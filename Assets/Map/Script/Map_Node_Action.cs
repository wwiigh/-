using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Node_Action : MonoBehaviour
{
    [Header("地圖控制")]
    public Map_Generate map;
    [Header("顯示商店")]
    public GameObject shop_object;
    [Header("顯示祭壇")]
    public GameObject altar_object;
    [Header("顯示戰鬥")]
    public GameObject battle_object;
    [Header("顯示戰鬥")]
    public GameObject battle_object_equipment;
    [Header("顯示寶箱")]
    public GameObject treasure_object;
    [Header("顯示事件")]
    public GameObject event_object;

    public void Check_Battle_State()
    {
        GameObject tmp = GameObject.FindGameObjectWithTag("Player");
        if(tmp!=null)
        {
            if(tmp.GetComponent<Character>().GetHP()==0)
            {
                Destroy(tmp);
            }
        }
        GameObject[] tmps = GameObject.FindGameObjectsWithTag("Enemy");
        if(Map_System.now_state == Map_System.map_state.fight && tmps.Length == 0)
        {
            battle_object.SetActive(false);
            battle_object_equipment.SetActive(false);
        }
        if(tmps!=null)
        {
            foreach (var item in tmps)
            {
                if(item.GetComponent<Character>().GetHP()==0)
                {
                    Destroy(item);
                }
            }
        }
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
    public void click_action_battle()
    {
        battle_object.SetActive(true);
        FindObjectOfType<Map_System>().Change_state(Map_System.map_state.fight);
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
    public void click_action_story(int level)
    {
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

    IEnumerator wait_finish(GameObject _ob)
    {
        while(_ob.activeSelf == true)
        {
            yield return new WaitForEndOfFrame();
        }
        map.Go_to_next();
        FindObjectOfType<Map_System>().Change_state(Map_System.map_state.normal);
    }
}
