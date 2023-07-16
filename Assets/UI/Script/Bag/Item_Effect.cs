using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_Effect : MonoBehaviour
{
    public enum Type{
        equipment_off,
        equipment_on,
        item,
        relic};
    public Button use;
    public Button close;
    public Button del;
    public Type item_type;
    public int list_index;
    public string _type;
    public string _name;
    public List<int> item_always_use = new List<int>();
    public List<int> item_no_effect = new List<int>();
    Bag_System bag;
    public void show_button()
    {
        if(_type=="relic")
        {
            close_button();
            return;
        }
        del.onClick.RemoveAllListeners();
        use.onClick.RemoveAllListeners();
        close.onClick.RemoveAllListeners();
        use.interactable = true;
        bag.change_button_pos(this.transform.position);
        switch (item_type)
        {
            case Type.equipment_off:
                use.gameObject.SetActive(true);
                close.gameObject.SetActive(true);
                del.gameObject.SetActive(true);
                use.GetComponentInChildren<TMP_Text>().text = "裝備";
                use.onClick.AddListener(delegate {wear_equipment();});
                del.onClick.AddListener(delegate { del_item(); });
                del.GetComponentInChildren<TMP_Text>().text = "移除";
                close.GetComponentInChildren<TMP_Text>().text = "關閉";
                close.onClick.AddListener(delegate {clear_button();});
                break;
            case Type.equipment_on:
                use.gameObject.SetActive(true);
                close.gameObject.SetActive(true);
                use.GetComponentInChildren<TMP_Text>().text = "卸下";
                use.onClick.AddListener(delegate {clear_equipment();});
                close.GetComponentInChildren<TMP_Text>().text = "關閉";
                close.onClick.AddListener(delegate {clear_button();});
                break;
            case Type.item:
                use.gameObject.SetActive(true);
                close.gameObject.SetActive(true);
                del.gameObject.SetActive(true);
                use.GetComponentInChildren<TMP_Text>().text = "使用";
                check_button(int.Parse(_name));
                use.onClick.AddListener(delegate { use_item(int.Parse(_name)); });
                del.GetComponentInChildren<TMP_Text>().text = "移除";
                del.onClick.AddListener(delegate { del_item(); });
                close.GetComponentInChildren<TMP_Text>().text = "關閉";
                close.onClick.AddListener(delegate {clear_button();});
                break;
            default:
                break;
        }
    }
    public void close_button()
    {
        use.gameObject.SetActive(false);
        close.gameObject.SetActive(false);
        del.gameObject.SetActive(false);
    }
    public void use_item(int id)
    {
        bool success = FindObjectOfType<Item_Implement>().Use_Item(id);
        if(success==true)del_item();
    }
    public void del_item()
    {
        // Bag_System bag = FindAnyObjectByType<Bag_System>();
        bag.del_item(list_index);
        clear_button();
    }
    // Start is called before the first frame update
    void Start()
    {
        bag = FindObjectOfType<Bag_System>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void clear_button()
    {
        use.gameObject.SetActive(false);
        close.gameObject.SetActive(false);
        del.gameObject.SetActive(false);
    }

    public bool wear_equipment()
    {
        bool ans =  bag.wear_equipment(list_index);
        clear_button();
        return ans;
    }
    public bool clear_equipment()
    {
        bool ans =  bag.clear_equipment(list_index);
        clear_button();
        return ans;
    }

    public void check_button(int id)
    {
        if(FindObjectOfType<Item_Implement>().use_count==3)use.interactable = false;
        else if(item_always_use.Contains(id))use.interactable = true;
        else if(item_no_effect.Contains(id))use.interactable = false;
        else if(FindObjectOfType<Map_System>().now_state == Map_System.map_state.fight)use.interactable = true;
        else use.interactable = false;
    }
}
