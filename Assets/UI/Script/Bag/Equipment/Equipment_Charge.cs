using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Charge : MonoBehaviour
{
    public enum Charge_Type
    {
        None,KillEnemy,
        GetArmor,
        Attack,
        UseEnegy,
        GetDemage,
        UseNormalCard,
        TurnStart,
        UseAttackCard,
        UseSkillCard,
        UseCard
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void Update_Equipment_Charge(Charge_Type charge_Type,int num = 1)
    {
        FindObjectOfType<Equipment_Control>().Update_Equipment_Charge(charge_Type,num);
    }
    public void test_charge()
    {
        Equipment_Charge.Update_Equipment_Charge(Charge_Type.UseCard,2);
    }
}
