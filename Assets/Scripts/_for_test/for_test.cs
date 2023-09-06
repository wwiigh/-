using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class for_test : MonoBehaviour
{
    public int[] items;
    public int[] equipments;
    public int[] relics;
    // Start is called before the first frame update
    // public GameObject equipment;

    public void Add_TO_Bag()
    {
        FindObjectOfType<Bag_System>().clear();
        foreach (var item in items)
        {
            Global.AddItemToBag(item,"item");
        }
        foreach (var item in equipments)
        {
            Global.AddItemToBag(item,"equipment");
        }
        foreach (var item in relics)
        {
            Global.AddItemToBag(item,"relic");
        }
    }
    public void GoTObattle()
    {
        // Global.init();
        Map_System.now_state = Map_System.map_state.fight;
        Relic_Implement.Handle_Relic_Before_Battle();
        FindObjectOfType<Equipment_Control>().Enable_Equipment();
    }
    public void TestRelic()
    {
        Relic_Implement.Handle_Relic_Dead(Relic_Implement.DeadType.Enemy);
        Relic_Implement.Update_Relic(Relic_Implement.Type.UseAttackCard);
    }
    public void Testequipment()
    {
        // Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.None,1);
        // Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.Attack,1);
        // Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.GetArmor,1);
        // Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.GetDemage,1);
        // Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.KillEnemy,1);
        // Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.TurnStart,1);
        // Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.UseAttackCard,1);
        // Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.UseCard,1);
        // Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.UseEnegy,1);
        // Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.UseNormalCard,1);
        // Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.UseSkillCard,1);
        // Equipment_Charge.Update_Equipment_Cold();
        // Map_System.now_state = Map_System.map_state.normal;
    }
    public void save()
    {
        Global.SaveData();
        Global.AddHp(-10);
    }
    public void Set_false(GameObject ob)
    {
        Global.LeaveBattle();
        // equipment.SetActive(false);
        // this.gameObject.SetActive(false);
    }
    public void load()
    {
        Global.ReadData();
    }
    public void leave()
    {
        Global.LeaveBattle();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
