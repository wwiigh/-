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
        public int start_charge;
        public int cold_time;
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
    int[] now_cold;
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
                if(id == 20)use_charge[i] = now_charge[i];
                charge_Control.Update_text(i,equipments[i].GetComponent<UI_Control>(), equipments_description[id-1].max_charge,now_cold[i]);
            }
            // equipments[i].GetComponent<UI_Control>().Change_description();
        }
        charge_Control.Set_Charge(now_charge,use_charge);
    }
    public void Update_Equipment_Cold()
    {
        for(int i=0;i<equipments_id.Count;i++)
        {
            now_cold[i] = now_cold[i] - 1;
            if(now_cold[i] <= 0)now_cold[i] = 0;
            int id = equipments_id[i];
            charge_Control.Update_text(i,equipments[i].GetComponent<UI_Control>(), 
            equipments_description[id-1].max_charge,now_cold[i]);
        }
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
        int[] tmp_cold = {0,0,0};
        now_charge = tmp_now;
        use_charge = tmp_use;
        now_cold = tmp_cold;
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
            tmp_now[i] = equipments_description[id-1].start_charge;
            if(id == 20)tmp_use[i] = tmp_now[i];
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
        if(now_cold[equipment_index]>0)return;
        if(charge_Control.texts[equipment_index].text == "0")return;
        Debug.Log("now use equipment "+equipments_id[equipment_index]);
        int backup_now_charge = now_charge[equipment_index];
        now_charge[equipment_index] = now_charge[equipment_index] - use_charge[equipment_index];
        charge_Control.Set_Charge(now_charge,use_charge);
        int id = equipments_id[equipment_index];
        now_cold[equipment_index] = equipments_description[id-1].cold_time;
        charge_Control.Update_text(equipment_index,equipments[equipment_index].GetComponent<UI_Control>(), equipments_description[id-1].max_charge,
        now_cold[equipment_index]);
        // charge_Control.Update_text()
        if(id == 6 && backup_now_charge > 0)
        {
            float rv = Random.Range(0.0f,1.0f);
            if(rv <= 1.0f / backup_now_charge || backup_now_charge == 1)
            {
                Debug.Log("SUCCESS USE EQUIPMENT 6");
                now_charge[equipment_index] = 0;
                charge_Control.Set_Charge(now_charge,use_charge);
                charge_Control.Update_text(equipment_index,equipments[equipment_index].GetComponent<UI_Control>(), equipments_description[id-1].max_charge,
                now_cold[equipment_index]);
            }
            else
            {
                Debug.Log("NOT SUCCESS USE EQUIPMENT 6");
                return;
            }
        }
        else if(id == 20)
        {
            now_charge[equipment_index] = 0;
            use_charge[equipment_index] = 0;
            charge_Control.Set_Charge(now_charge,use_charge);
            charge_Control.Update_text(equipment_index,equipments[equipment_index].GetComponent<UI_Control>(), equipments_description[id-1].max_charge,
            now_cold[equipment_index]);
        }
        FindObjectOfType<Equipment_Implement>().Use_Equipment(equipments_id[equipment_index],backup_now_charge);
    }
}
