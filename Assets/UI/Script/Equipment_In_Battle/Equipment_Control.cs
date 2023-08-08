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
        public int use_charge;
        public Sprite image;
        public Equipment_Charge.Charge_Type charge_Type;
        public UI_Show_Text information_Box;
    }
    public GameObject[] equipments;
    public Charge_Control charge_Control;
    public Equipment[] equipments_description;
    List<int> equipments_id;
    int[] now_charge;
    int[] use_charge;
    void OnEnable()
    {
        Enable_Equipment();
    }
    public void Update_Equipment_Charge(Equipment_Charge.Charge_Type charge_Type,int num)
    {
        for(int i=0;i<equipments_id.Count;i++)
        {
            // equipments[i].SetActive(true);
            int id = equipments_id[i];
            Debug.Log("id is "+id);
            // equipments[i].GetComponent<Image>().sprite = equipments_description[id-1].image;
            // equipments[i].GetComponent<UI_Control>().UI_information = equipments_description[id-1].information_Box;
            // equipments[i].GetComponent<UI_Control>().reset_pos();
            // charge_Control.Add_text(i,equipments[i].GetComponent<UI_Control>(), equipments_description[id-1].max_charge);
            if(equipments_description[id-1].charge_Type == charge_Type && charge_Type != Equipment_Charge.Charge_Type.None)
            {
                now_charge[i] += num;
               
                if(now_charge[i] > equipments_description[id-1].max_charge)
                    now_charge[i] = equipments_description[id-1].max_charge;
                charge_Control.Update_text(i,equipments[i].GetComponent<UI_Control>(), equipments_description[id-1].max_charge);
            }
            // equipments[i].GetComponent<UI_Control>().Change_description();
        }
        charge_Control.Set_Charge(now_charge,use_charge);
    }
    public void Use_Equipment_Charge(int id)
    {

    }
    public void Enable_Equipment()
    {
        Bag_System bag_System = FindObjectOfType<Bag_System>();
        List<int> equipment_list = bag_System.Return_Equipment();
        
        // equipment_list.Clear();
        int[] tmp_use = {0,0,0};
        int[] tmp_now = {0,0,0};
        now_charge = tmp_now;
        use_charge = tmp_use;
        // List<int> equipment_list = new List<int>();
        // equipment_list.Add(6);
        // equipment_list.Add(10);
        // equipment_list.Add(28);
        
        for(int i=0;i<equipment_list.Count;i++)
        {
            equipments[i].SetActive(true);
            int id = equipment_list[i];
            equipments[i].GetComponent<Image>().sprite = equipments_description[id-1].image;
            equipments[i].GetComponent<UI_Control>().UI_information = equipments_description[id-1].information_Box;
            equipments[i].GetComponent<UI_Control>().reset_pos();
            // charge_Control.Add_text(i,equipments[i].GetComponent<UI_Control>(), equipments_description[id-1].max_charge);
            tmp_use[i] = equipments_description[id-1].use_charge;
            // equipments[i].GetComponent<UI_Control>().Change_description();
        }
        for(int i=equipment_list.Count;i<3;i++)
        {
            // int id = equipment_list[i];
            equipments[i].SetActive(false);
            // equipments[i].GetComponent<Image>().sprite = equipments_description[id-1].image;
            // equipments[i].GetComponent<UI_Control>().UI_information = equipments_description[id-1].information_Box;
            // equipments[i].GetComponent<UI_Control>().reset_pos();
            // charge_Control.Add_text(i,equipments[i].GetComponent<UI_Control>(), equipments_description[id-1].max_charge);
            // equipments[i].GetComponent<UI_Control>().Change_description();
        }
        charge_Control.Set_Charge(tmp_now,tmp_use);
        for(int i=0;i<equipment_list.Count;i++)
        {
            int id = equipment_list[i];
            charge_Control.Add_text(i,equipments[i].GetComponent<UI_Control>(), equipments_description[id-1].max_charge);
            // equipments[i].GetComponent<UI_Control>().Change_description();
        }
        equipments_id = equipment_list;
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
        now_charge[equipment_index] = now_charge[equipment_index] - use_charge[equipment_index];
        charge_Control.Set_Charge(now_charge,use_charge);
        int id = equipments_id[equipment_index];
        charge_Control.Update_text(equipment_index,equipments[equipment_index].GetComponent<UI_Control>(), equipments_description[id-1].max_charge);
        // charge_Control.Update_text()
        FindObjectOfType<Equipment_Implement>().Use_Equipment(equipments_id[equipment_index]);
    }
}
