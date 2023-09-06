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
        GetDamage,
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
        if(FindObjectOfType<Equipment_Control>()==null)return;
        FindObjectOfType<Equipment_Control>().Update_Equipment_Charge(charge_Type,num);
    }
    public static void Update_Equipment_Cold()
    {
        if(FindObjectOfType<Equipment_Control>()==null)return;
        FindObjectOfType<Equipment_Control>().Update_Equipment_Cold();
    }
    public void test_charge()
    {
        Equipment_Charge.Update_Equipment_Charge(Charge_Type.GetDamage, 2);
        Equipment_Charge.Update_Equipment_Cold();

    }
}
