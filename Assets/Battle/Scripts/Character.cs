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
    
    int maxHP = 100;
    int hp = 100;
    int armor = 0;
    int block = 0;
    int goldDrop = 0;
    List<(Status.status _status, int level)> status = new List<(Status.status _status, int level)>();
    List<GameObject> statusIcons = new List<GameObject>();
    void Start()
    {
        parent = transform.parent.gameObject;
        Init_HP();
        // selectionMarkObj = Instantiate(selectionMark, transform);
        // selectionMarkObj.GetComponent<SelectionMark>().Init(gameObject);
        status.Add((Status.status.strenth, 1));
        status.Add((Status.status.dexterity, 2));
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

    public int GetMaxHP(){
        return maxHP;
    }

    public int GetHP(){
        return hp;
    }

    public int GetArmor(){
        return armor;
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
    }

    public void InitEnemy(EnemyClass enemy){
        nameText.text = enemy.mobName;
        maxHP = enemy.hp;
        hp = enemy.hp;
        imageObj.GetComponent<Image>().sprite = enemy.image;
        imageObj.transform.localPosition += new Vector3(0, (enemy.sizeY - 300) / 2, 0);
        goldDrop = enemy.goldDrop;
        var rectT = imageObj.transform as RectTransform;
        rectT.sizeDelta = new Vector2(enemy.sizeX, enemy.sizeY);
    }

    public void AddStrenth(int n){
        bool found = false;
        (Status.status _status, int level) pack = (Status.status.strenth, 0);
        foreach(var s in status){
            if (s._status == Status.status.strenth){
                pack = s;
                status.Remove(s);
                found = true;
                break;
            }
        }
        if (found){
            if (pack.level + n != 0) status.Add((pack._status, pack.level + n));
        }
        else{
            if (n != 0)  status.Add((Status.status.strenth, n));
        }
        status.Sort();
        UpdateStatus();
    }

    public void AddDexterity(int n){
        bool found = false;
        (Status.status _status, int level) pack = (Status.status.strenth, 0);
        foreach(var s in status){
            if (s._status == Status.status.dexterity){
                pack = s;
                status.Remove(s);
                found = true;
                break;
            }
        }
        if (found){
            if (pack.level + n != 0) status.Add((pack._status, pack.level + n));
        }
        else{
            if (n != 0)  status.Add((Status.status.dexterity, n));
        }
        status.Sort();
        UpdateStatus();
    }
}
