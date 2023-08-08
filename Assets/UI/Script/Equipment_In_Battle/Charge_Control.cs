using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Charge_Control : MonoBehaviour
{
    public Image[] equipment_images;
    public Image[] button_images;
    public TMP_Text[] texts;
    public int[] equipment_charge_use;
    public int[] equipment_charge_now;
    public float[] button_values;
    

    public void Set_Charge(int[] now_charge, int[] use_charge)
    {
        equipment_charge_use = use_charge;
        equipment_charge_now = now_charge;
        Debug.Log(now_charge[0]+" "+now_charge[1]+" "+now_charge[2]);
        set_equipment_charge(now_charge,use_charge);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // Set_Charge(equipment_charge_now,equipment_charge_total);
        
        set_value(button_images,button_values);
    }
    void set_value(Image[] images,float[] set_values)
    {
        float total = 0;
        for(int i=0;i<images.Length;i++)
        {
            total+=set_percentage(set_values,i);   
            images[i].fillAmount = total;
        }
    }
    void set_equipment_charge(int[] now_charge,int[] use_charge)
    {
        set_equipment_value(equipment_images,now_charge,use_charge);
    }
    void set_equipment_value(Image[] images,int[] now_charge,int[] use_charge)
    {
        float total = 0;
        for(int i=0;i<images.Length;i++)
        {
            if(use_charge[i]==0)
            {
                total = 0;
                texts[i].text = "0";
            }
            else 
            {
                total = set_equipment_percentage(now_charge[i]%use_charge[i],use_charge[i]);   
                images[i].fillAmount = total / 3 + i / 3.0f;
                texts[i].text = (now_charge[i] / use_charge[i]).ToString();
            }
        }
    }

    float set_equipment_percentage(float set_values, float total_charge)
    {
        Debug.Log("total_charge "+total_charge);
        if(Mathf.Abs(total_charge-0)<0.001f)return 0;
        return set_values / total_charge;
    }


    float set_percentage(float[] set_values, int index)
    {
        float total = 0;
        for(int i=0;i<set_values.Length;i++)
        {
            total+=set_values[i];   
        }

        return set_values[index] / total;
    }

    public void Add_text(int id,UI_Control uI_Control,int max_charge)
    {
        uI_Control.Add_text("目前充能 "+equipment_charge_now[id] + "\r\n" + 
        "最大充能 " + max_charge + "\r\n" + "每次消耗 "+equipment_charge_use[id]);
    }
    public void Update_text(int id,UI_Control uI_Control,int max_charge)
    {
        uI_Control.Change_text("目前充能 "+equipment_charge_now[id] + "\r\n" + 
        "最大充能 " + max_charge + "\r\n" + "每次消耗 "+equipment_charge_use[id],2);
    }
}
