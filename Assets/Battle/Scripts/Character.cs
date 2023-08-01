using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    BattleController battleController;
    [SerializeField] GameObject imageObj;
    [SerializeField] TMPro.TextMeshProUGUI nameText;
    [SerializeField] GameObject hpBarTemplate;
    [SerializeField] GameObject statusIconTemplate;
    [SerializeField] GameObject damageTextTemplate;
    BattleEffects effects;
    GameObject hpBar;
    EnemyClass enemyClass;
    int maxHP = 100;
    int hp = 100;
    int armor = 0;
    int block = 0;
    List<(Status.status _status, int level)> status = new List<(Status.status _status, int level)>();
    List<GameObject> statusIcons = new List<GameObject>();
    void Start()
    {
        battleController = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        effects = GameObject.FindGameObjectWithTag("BattleEffects").GetComponent<BattleEffects>();
        Init_HP();
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
        if (damage > 0) GetComponent<HitAnimation>().Play();
        GameObject dmgText = Instantiate(damageTextTemplate, transform);
        dmgText.GetComponent<DamageText>().Show(damage);
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
                    AnEyeForAnEye(damage);
                    return true;
                }
            }else{
                armor -= damage;
                hpBar.GetComponent<HPBar>().UpdateHP();
                AnEyeForAnEye(damage);
                return true;
            }
        }else{
            block -= damage;
            hpBar.GetComponent<HPBar>().UpdateHP();
            AnEyeForAnEye(damage);
            return true;
        }
    }
    public bool LoseHP(int value){
        if (hp <= value){
            hp = 0;
            hpBar.GetComponent<HPBar>().UpdateHP();
            return false;
        }
        else{
            hp -= value;
            hpBar.GetComponent<HPBar>().UpdateHP();
            AnEyeForAnEye(value);
            return true;
        }
    }
    void AnEyeForAnEye(int dmgReceived){
        Deck deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        deck.AnEyeForAnEye(dmgReceived);
    }



    public bool Attack(GameObject target, int dmg){
        int final_dmg = BattleController.ComputeDamage(gameObject, target, dmg);
        if (target.GetComponent<Character>().GetStatus(Status.status.invincible) > 0)
            target.GetComponent<Character>().AddStatus(Status.status.invincible, -1);
        bool target_alive = target.GetComponent<Character>().GetHit(final_dmg);
        int fireLevel = GetStatus(Status.status.fire_enchantment);
        if (tag == "Player" && fireLevel > 0 && target_alive) target.GetComponent<Character>().AddStatus(Status.status.burn, fireLevel); 
        return target_alive;
    }
    public bool Attack(GameObject target, int dmg, float strength_multiplier, float tmp_strength_multiplier){
        int final_dmg = BattleController.ComputeDamage(gameObject, target, dmg, strength_multiplier, tmp_strength_multiplier);
        if (target.GetComponent<Character>().GetStatus(Status.status.invincible) > 0)
            target.GetComponent<Character>().AddStatus(Status.status.invincible, -1);
        bool target_alive = target.GetComponent<Character>().GetHit(final_dmg);
        int fireLevel = GetStatus(Status.status.fire_enchantment);
        if (tag == "Player" && fireLevel > 0 && target_alive) target.GetComponent<Character>().AddStatus(Status.status.burn, fireLevel); 
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
        hpBar.GetComponent<HPBar>().UpdateHP();
        effects.Play(gameObject, "heal");
    }



    public int GetArmor(){
        return armor;
    }
    public void AddArmor(int value){
        if (tag == "Player") armor += BattleController.ComputeArmor(value);
        else armor += value;
        hpBar.GetComponent<HPBar>().UpdateHP();
        effects.Play(gameObject, "get_armor");
        if (tag == "Player" && value > 0 && GetStatus(Status.status.fortify) > 0){
            GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>().Draw();
            AddStatus(Status.status.fortify, -1);
        }
    }



    public int GetBlock(){
        return block;
    }
    public void AddBlock(int value){
        if (tag == "Player") block += BattleController.ComputeArmor(value);
        else block += value;
        hpBar.GetComponent<HPBar>().UpdateHP();
        effects.Play(gameObject, "get_armor");
        if (tag == "Player" && value > 0 && GetStatus(Status.status.fortify) > 0){
            GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>().Draw();
            AddStatus(Status.status.fortify, -1);
        }
    }



    public void HoverIn(){
        Debug.Log("HoverIn");
        if (battleController.GetState() == BattleController.BattleState.SelectEnemy && tag == "Enemy"){
            transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 128, 128, 255);
            Debug.Log("change color");
        }
    }
    public void Click(){
        if (battleController.GetState() == BattleController.BattleState.SelectEnemy && tag == "Enemy"){
            battleController.EnemySelected(gameObject);
            transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
    public void HoverOut(){
        if (battleController.GetState() == BattleController.BattleState.SelectEnemy && tag == "Enemy"){
            transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }



    public void TurnStart(){
        if (GetStatus(Status.status.auto_guard) > 0) AddStatus(Status.status.taunt, 1);
        if (GetStatus(Status.status.burn) > 0) TriggerBurn(true);
        if (hpBar != null){
            block = 0;
            hpBar.GetComponent<HPBar>().UpdateHP();
        }
    }
    public void TurnEnd(){
        List<(Status.status _status, int level)> decreaseList = new List<(Status.status _status, int level)>();
        List<(Status.status _status, int level)> clearList = new List<(Status.status _status, int level)>();
        foreach(var pack in status){
            if (Status.DecreaseOnTurnEnd(pack._status)) decreaseList.Add(pack);
            if (Status.ClearOnTurnEnd(pack._status)) clearList.Add(pack);
        }
        foreach(var pack in decreaseList) AddStatus(pack._status, -1);
        foreach(var pack in clearList) AddStatus(pack._status, -pack.level);
        UpdateStatus();
    }



    public bool TriggerBurn(bool decrease){
        int level = GetStatus(Status.status.burn);
        bool dead = LoseHP(level);
        if (decrease){
            if (level % 2 == 1) level = level / 2 + 1;
            else level = level / 2;
            AddStatus(Status.status.burn, -level);
        }
        return dead;
    }

    public void InitPlayer(){
        nameText.text = "玩家";
        maxHP = Global.player_max_hp;
        hp = Global.player_hp;
        GetComponent<Animator>().Play("player_idle");
        // AddStatus(Status.status.absorb, 9);
        // AddStatus(Status.status.accumulation, 9);
        // AddStatus(Status.status.auto_guard, 9);
        // AddStatus(Status.status.bleed, 9);
        // AddStatus(Status.status.blink, 9);
        // AddStatus(Status.status.bounce_back, 9);
        // AddStatus(Status.status.burn, 9);
        // AddStatus(Status.status.compress, 9);
        // AddStatus(Status.status.counter, 9);
        // AddStatus(Status.status.damage_adjust, 9);
        // AddStatus(Status.status.dexterity, 9);
        // AddStatus(Status.status.doom, 9);
        // AddStatus(Status.status.doppelganger, 9);
        // for(int i = 0; i < 15; i++){
        //     AddStatus(Random.Range(0, 53), Random.Range(0, 10));
        // }
    }

    public void InitEnemy(EnemyClass enemy){
        enemyClass = enemy;
        nameText.text = enemy.mobName;
        maxHP = enemy.hp;
        hp = enemy.hp;

        Vector2 enemysize = enemy.size;
        if (enemysize.x == 0) enemysize.x = 300;
        if (enemysize.y == 0) enemysize.y = 300;

        imageObj.GetComponent<Image>().sprite = enemy.image;
        imageObj.transform.localPosition += Vector3.down * (enemysize.y - 300) / 2;
        imageObj.transform.localPosition += (Vector3) enemy.offset;
        var rectT = imageObj.transform as RectTransform;
        rectT.sizeDelta = enemysize;

        switch(enemy.id){
            case 101:
                GetComponent<Animator>().Play("101_idle");
                break;
            case 102:
                GetComponent<Animator>().Play("102_idle");
                break;
            case 103:
                GetComponent<Animator>().Play("103_idle");
                break;
            case 104:
                GetComponent<Animator>().Play("104_idle");
                break;
            case 105:
                GetComponent<Animator>().Play("105_idle");
                break;
            case 106:
                GetComponent<Animator>().Play("106_idle");
                break;
            case 107:
                GetComponent<Animator>().Play("107_idle");
                break;
            case 108:
                GetComponent<Animator>().Play("108_idle");
                break;
            case 201:
                GetComponent<Animator>().Play("201_idle");
                break;
            case 202:
                GetComponent<Animator>().Play("202_idle");
                break;
            default:
                Debug.Log("Character.InitEnemy(): Unknown id " + enemy.id.ToString());
                break;
        }
    }

    public int GetEnemyID(){
        return enemyClass.id;
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
