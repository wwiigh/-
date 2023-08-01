using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment_Control : MonoBehaviour
{
    [System.Serializable]
    public struct Equipment
    {
        public int id;
        public int max_charge;
        public Sprite image;
        public UI_Show_Text information_Box;
    }
    public GameObject[] equipments;
    public Charge_Control charge_Control;
    public Equipment[] equipments_description;
    List<int> equipments_id;
    void OnEnable()
    {
        // Bag_System bag_System = FindObjectOfType<Bag_System>();
        // List<int> equipment_list = bag_System.Return_Equipment();
        int[] tmp_use = {10,20,30};
        int[] tmp_now = {15,8,99};
        charge_Control.Set_Charge(tmp_now,tmp_use);
        List<int> equipment_list = new List<int>();
        equipment_list.Add(Random.Range(1,5));
        equipment_list.Add(2);
        equipment_list.Add(3);
        
        for(int i=0;i<equipment_list.Count;i++)
        {
            int id = equipment_list[i];
            equipments[i].GetComponent<Image>().sprite = equipments_description[id-1].image;
            equipments[i].GetComponent<UI_Control>().UI_information = equipments_description[id-1].information_Box;
            equipments[i].GetComponent<UI_Control>().Change_description();
            charge_Control.Add_text(i,equipments[i].GetComponent<UI_Control>(), equipments_description[id-1].max_charge);
            // equipments[i].GetComponent<UI_Control>().Change_description();
        }
        
        equipments_id = equipment_list;
        // equipment_list.Clear();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int equipment_index)
    {
        if(charge_Control.texts[equipment_index].text == "0")return;
        Debug.Log("now use equipment "+equipments_id[equipment_index]);
        FindObjectOfType<Equipment_Implement>().Use_Equipment(equipments_id[equipment_index]);
    }
}
