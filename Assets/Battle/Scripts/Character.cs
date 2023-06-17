using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    GameObject parent;
    // [SerializeField] GameObject GS;
    [SerializeField] GameObject imageObj;
    [SerializeField] TMPro.TextMeshProUGUI nameText;
    [SerializeField] GameObject hpBarTemplate;
    [SerializeField] GameObject statusIconTemplate;
    // [SerializeField] GameObject selectionMark;
    // [SerializeField] public GameObject effect;
    GameObject hpBar;
    // GameObject selectionMarkObj;
    EnemyClass enemyClass;
    int maxHP = 100;
    int hp = 100;
    int armor = 0;
    int block = 0;
    int state = 0;
    List<(Status.status _status, int level)> status = new List<(Status.status _status, int level)>();
    List<GameObject> statusIcons = new List<GameObject>();
    void Start()
    {
        parent = transform.parent.gameObject;
        Init_HP();
        // selectionMarkObj = Instantiate(selectionMark, transform);
        // selectionMarkObj.GetComponent<SelectionMark>().Init(gameObject);
        UpdateStatus();
    }

    public bool HP_Initialized(){
        return hpBar;
    }

    public void Init_HP(){
        if (HP_Initialized()) return;
        hpBar =  Instantiate(hpBarTemplate, transform);
        hpBar.transform.localPosition = hpBar.transform.localPosition + Vector3.down*185;
    }

    public void UpdateStatus(){
        status.Sort();
        int a = status.Count;
        int b = statusIcons.Count;
        if (b < a)
            for (int i = 0; i < a-b; i++) statusIcons.Add(Instantiate(statusIconTemplate, transform));
        if (b > a)
            for (int i = 0; i < b-a; i++) {
                Destroy(statusIcons[statusIcons.Count - 1]);
                statusIcons.RemoveAt(statusIcons.Count - 1);
            }

        int idx = 0;
        foreach(var _status in status){
            statusIcons[idx].GetComponent<StatusIcon>().UpdateIcon(idx, _status);
            idx++;
        }
    }

    // IEnumerator Die(){
    //     GetComponent<Animator>().Play("die");
    //     yield return new WaitForSeconds(1.0f);
    //     if (name == "Enemy"){
    //         GS.GetComponent<GameState>().EnemyDie();
    //         Destroy(gameObject);
    //     }
    //     else{
    //         GS.GetComponent<GameState>().GameOver();
    //     }
    // }

    public bool GetHit(int damage){
        if (block < damage){
            damage -= block;
            block = 0;
            if (armor < damage){
                damage -= armor;
                armor = 0;
                if (hp <= damage){
                    hp = 0;
                    hpBar.GetComponent<HPBar>().UpdateHP();
                    return false;
                }
                else{
                    hp -= damage;
                    hpBar.GetComponent<HPBar>().UpdateHP();
                    return true;
                }
            }else{
                armor -= damage;
                hpBar.GetComponent<HPBar>().UpdateHP();
                return true;
            }
        }else{
            block -= damage;
            hpBar.GetComponent<HPBar>().UpdateHP();
            return true;
        }
    }

    public bool Attack(GameObject target, int dmg){
        int final_dmg = BattleController.ComputeDamage(gameObject, target, dmg);
        if (target.GetComponent<Character>().GetStatus(Status.status.invincible) > 0)
            target.GetComponent<Character>().AddStatus(Status.status.invincible, -1);
        bool target_alive = target.GetComponent<Character>().GetHit(final_dmg);
        return target_alive;
    }

    public int GetMaxHP(){
        return maxHP;
    }

    public int GetHP(){
        return hp;
    }

    public void Heal(int value){
        if (hp + value > maxHP) hp = maxHP;
        else hp += value;
    }

    public int GetArmor(){
        return armor;
    }

    public void AddArmor(int value){
        armor += value;
    }

    public int GetBlock(){
        return block;
    }

    public void HoverIn(){
        // if (GS.GetComponent<GameState>().GetState() == GameState.State.SelectEnemy)
        //     selectionMarkObj.SetActive(true);
    }

    public void HoverOut(){
        // selectionMarkObj.SetActive(false);
    }

    // public void Select(){
    //     if (GS.GetComponent<GameState>().GetState() == GameState.State.SelectEnemy){
    //         GS.GetComponent<Broadcast>().UseCardSelected(gameObject);
    //         HoverOut();
    //     }
    // }

    public void InitPlayer(){
        nameText.text = "玩家";
        maxHP = Global.player_max_hp;
        hp = Global.player_hp;
        for (int i = 0; i < 12; i++){
            GetComponent<Character>().AddStatus((Status.status) Random.Range(0, 38), Random.Range(1, 20));
        }
    }

    public void InitEnemy(EnemyClass enemy){
        enemyClass = enemy;
        nameText.text = enemy.mobName;
        maxHP = enemy.hp;
        hp = enemy.hp;
        imageObj.GetComponent<Image>().sprite = enemy.image;
        imageObj.transform.localPosition += new Vector3(0, (enemy.sizeY - 300) / 2, 0);
        var rectT = imageObj.transform as RectTransform;
        rectT.sizeDelta = new Vector2(enemy.sizeX, enemy.sizeY);
    }

    public int GetEnemyID(){
        return enemyClass.id;
    }

    public int GetState(){
        return state;
    }

    public void SetState(int n){
        state = n;
    }

    public List<(Status.status _status, int level)> GetAllStatus(){
        return status;
    }

    public int GetStatus(Status.status target){
        foreach(var item in status){
            if (item._status == target) return item.level;
        }
        return 0;
    }

    public void AddStatus(Status.status target, int level){
        int origin_level = 0;
        foreach(var item in status){
            if (item._status == target){
                origin_level = item.level;
                status.Remove(item);
                break;
            }
        }
        if (origin_level + level != 0) status.Add((target, origin_level + level));
        status.Sort();
        UpdateStatus();
    }
}
