using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] EnemyClass[] enemyClass;
    [SerializeField] EquipmentClass[] equipmentClass;
    public GameObject descriptionBox;
    public enum BattleType{
        Normal,
        Elite,
        Boss
    }
    public void EnterBattle(){
        EnterBattle_id(Random.Range(101, 109 + 1));
    }

    // public void EnterBattle(BattleType type){
    //     if (type == BattleType.normal){
    //         EnterBattle_id(Random.Range(101, 109 + 1));
    //     }
    // }
    public void EnterBattle_id(int id){
        InitPlayer();
        int[] enemyID;
        switch(id){
            case 101:
                enemyID = new int[]{0, 0};
                break;
            case 102:
                enemyID = new int[]{3, 3, 3};
                break;
            case 103:
                enemyID = new int[]{4, 5};
                break;
            case 104:
                enemyID = new int[]{1, 2};
                break;
            case 105:
                enemyID = new int[]{0, 1};
                break;
            case 106:
                enemyID = new int[]{4, 3, 3};
                break;
            case 107:
                enemyID = new int[]{1, 3, 3};
                break;
            case 108:
                enemyID = new int[]{2, 2};
                break;
            case 109:
                enemyID = new int[]{2, 5};
                break;
            default:
                Debug.Log("Enter battle: Unknown id " + id.ToString());
                return;
        }
        SpawnEnemies(enemyID);
    }

    void InitPlayer(){
        GameObject player = Instantiate(character, transform);
        player.transform.localPosition = new Vector3(-480, 0, 0);
        player.tag = "Player";
        player.GetComponent<Character>().InitPlayer();
    }

    void InitEnemy(int id, int idx){
        GameObject enemy = Instantiate(character, transform);
        enemy.transform.localPosition = new Vector3(180 + idx*300, 0, 0);
        enemy.tag = "Enemy";
        enemy.GetComponent<Character>().InitEnemy(enemyClass[id]);
    }

    void SpawnEnemies(int[] id){
        for (int i = 0; i < id.Length; i++)
            InitEnemy(id[i], i);
    }

    public EquipmentClass[] GetEquipments(){
        return equipmentClass;
    }
}
