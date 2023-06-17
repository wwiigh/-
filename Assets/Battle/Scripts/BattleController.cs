using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] EnemyClass[] enemyClass;
    [SerializeField] EquipmentClass[] equipmentClass;
    public GameObject descriptionBox;
    GameObject player;
    public enum BattleType{
        Normal,
        Elite,
        Boss
    }
    public void EnterBattle(){
        EnterBattle_id(Random.Range(101, 107 + 1));
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
                enemyID = new int[]{1, 2};
                break;
            case 104:
                enemyID = new int[]{0, 1};
                break;
            case 105:
                enemyID = new int[]{4, 3, 3};
                break;
            case 106:
                enemyID = new int[]{1, 3, 3};
                break;
            case 107:
                enemyID = new int[]{2, 2};
                break;
            default:
                Debug.Log("Enter battle: Unknown id " + id.ToString());
                return;
        }
        SpawnEnemies(enemyID);
    }

    public void TurnEnd(){

    }

    public GameObject GetPlayer(){
        return player;
    }

    void InitPlayer(){
        // GameObject player = Instantiate(character, transform);
        player = Instantiate(character, transform);
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

    public static int ComputeDamage(GameObject from, GameObject to, float dmg){
        Character from_character = from.GetComponent<Character>();
        Character to_character = to.GetComponent<Character>();
        float multiplier = 1.0f;

        if (to_character.GetStatus(Status.status.invincible) > 0) return 0;

        int strength = from_character.GetStatus(Status.status.strength) + from_character.GetStatus(Status.status.temporary_strength);
        if (from_character.GetStatus(Status.status.weak) > 0) multiplier -= 0.25f;
        if (to_character.GetStatus(Status.status.vulnerable) > 0) multiplier += 0.25f;
        multiplier += from_character.GetStatus(Status.status.damage_adjust) * 0.01f;

        int final_dmg = (int) ((dmg + strength) * multiplier);

        int rock_solid = to_character.GetStatus(Status.status.rock_solid);
        final_dmg -= rock_solid;
        if (to_character.GetStatus(Status.status.hard_shell) > 0){
            if (final_dmg > 1) final_dmg = 1;
        }

        return final_dmg;
    }
}
