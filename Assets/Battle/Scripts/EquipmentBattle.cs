using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBattle : MonoBehaviour
{
    [SerializeField] GameObject equipmentIconObj;
    GameObject battle;
    EquipmentClass[] equipments;
    List<GameObject> equipmentObjs = new List<GameObject>();
    private void Start() {
        battle = transform.parent.gameObject;
        equipments = battle.GetComponent<BattleController>().GetEquipments();
        Init();
    }
    public void Init(){
        int equipment_count = 3;
        for (int i = 0; i < equipment_count; i++){
            GameObject tmp = Instantiate(equipmentIconObj, transform);
            tmp.transform.localPosition = new Vector3(-450 - i * 200, 200, 0);
            tmp.GetComponent<EquipmentIcon>().SetEquipment(equipments[i]);
            equipmentObjs.Add(tmp);
        }
    }
}
