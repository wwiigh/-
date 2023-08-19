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
    public bool vulnerable_buffer = false;
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
        if (GetEnemyID() == 402) hpBar.GetComponent<HPBar>().AdjustLength(0.5f);
    }

    public void UpdateStatus(){
        status.Sort();
        int a = status.Count;
        int b = statusIcons.Count;
        if (b < a)
            for (int i = 0; i < a-b; i++) statusIcons.Add(Instantiate(statusIconTemplate, transform));
        if (b > a)
            for (int i = 0; i < b-a; i++) {
                statusIcons[statusIcons.Count - 1].GetComponent<StatusIcon>().Destroy();
                statusIcons.RemoveAt(statusIcons.Count - 1);
            }

        int idx = 0;
        foreach(var _status in status){
            if (GetEnemyID() == 402) statusIcons[idx].GetComponent<StatusIcon>().UpdateIcon(idx, _status, 3);
            else statusIcons[idx].GetComponent<StatusIcon>().UpdateIcon(idx, _status, 5);
            idx++;
        }
    }

    bool dying = false;
    IEnumerator Die(){
        // Debug.Log("Die() is called");
        if (dying) yield break;
        dying = true;
        // for (int i = 0; i < 10; i++){
        //     transform.GetChild(0).GetComponent<Image>().color -= new Color(0, 0, 0, 0.1f);
        //     yield return new WaitForSeconds(0.1f);
        // }
        // GetComponent<Animator>().Play("die");
        yield return new WaitForSeconds(1.0f);
        if (tag == "Enemy"){
            FindObjectOfType<BattleController>().EnemyDie();
            Destroy(gameObject);
        }
        else{
            FindObjectOfType<BattleController>().PlayerDie();
        }
    }



    public bool GetHit(int damage){
        if (hp == 0) return true;
        
        bool dead = false;
        GameObject dmgText = Instantiate(damageTextTemplate, transform);
        dmgText.GetComponent<DamageText>().Show(damage);
        if (damage >= block + armor + hp){
            dead = true;
            block = 0;
            armor = 0;
            hp = 0;
        }
        else if (damage >= block + armor){
            damage -= block + armor;
            block = 0;
            armor = 0;
            hp -= damage;
        }
        else if (damage >= block){
            damage -= block;
            block = 0;
            armor -= damage;
        }
        else{
            block -= damage;
        }
        hpBar.GetComponent<HPBar>().UpdateHP();
        if (!dead) AnEyeForAnEye(damage);
        if (damage > 0) GetComponent<HitAnimation>().Play(dead);
        if (dead) Debug.Log("GetHit called Die()");
        if (dead) StartCoroutine(Die());

        if (hp != 0 && GetEnemyID() == 305) GetComponent<EnemyMove>().Enemy305_Detect();
        return dead;
    }
    public bool LoseHP(int value){
        if (hp == 0) return true;

        if (hp <= value){
            hp = 0;
            hpBar.GetComponent<HPBar>().UpdateHP();
            Debug.Log("LoseHP called Die()");
            StartCoroutine(Die());
            GetComponent<HitAnimation>().Play(true);
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
        // int final_dmg = BattleController.ComputeDamage(gameObject, target, dmg);
        // if (target.GetComponent<Character>().GetStatus(Status.status.invincible) > 0)
        //     target.GetComponent<Character>().AddStatus(Status.status.invincible, -1);
        // bool target_alive = target.GetComponent<Character>().GetHit(final_dmg);
        // int fireLevel = GetStatus(Status.status.fire_enchantment);
        // if (tag == "Player" && fireLevel > 0 && target_alive) target.GetComponent<Character>().AddStatus(Status.status.burn, fireLevel); 
        // return target_alive;
        return Attack(target, dmg, 1, 1);
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
    public void AddMaxHP(int value){
        maxHP += value;
        Heal(value);
    }
    public int GetHP(){
        return hp;
    }
    public bool IsAlive(){
        return hp > 0;
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
            Deck.Draw();
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
            Deck.Draw();
            AddStatus(Status.status.fortify, -1);
        }
    }



    public void HoverIn(){
        // Debug.Log("HoverIn");
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
        bool dead = LoseHP(level - GetStatus(Status.status.rock_solid));
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
        // var rectT = imageObj.transform as RectTransform;
        // rectT.sizeDelta = enemysize;

        switch(enemy.id){
            case 104:
                AddStatus(Status.status.hard_shell, 1);
                break;
            case 106:
                AddStatus(Status.status.rock_solid, 1);
                break;
            case 201:
                AddStatus(Status.status.taunt, 99);
                AddStatus(Status.status.doom, 1);
                break;
            case 207:
                AddStatus(Status.status.symbioticA, 1);
                AddStatus(Status.status.rage, 4);
                break;
            case 208:
                AddStatus(Status.status.symbioticB, 1);
                AddStatus(Status.status.grief, 30);
                break;
            case 209:
                AddStatus(Status.status.absorb, 1);
                break;
            case 301:
                AddStatus(Status.status.grudge, 2);
                break;
            case 303:
                AddStatus(Status.status.auto_guard, 1);
                AddStatus(Status.status.energy_absorb, 1);
                break;
            case 309:
            case 310:
                AddStatus(Status.status.fluid, 1);
                break;
            case 312:
                AddStatus(Status.status.blink, 1);
                AddStatus(Status.status.oppress, 8);
                break;
            default:
                // Debug.Log("Character.InitEnemy(): Unknown id " + enemy.id.ToString());
                break;
        }

        if (enemy.id == 310) GetComponent<Animator>().Play("309_idle");
        else GetComponent<Animator>().Play(enemy.id.ToString() + "_idle");
    }

    public int GetEnemyID(){
        if (tag == "Player") return -1;
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

    public int GetNegativeStatusCount(){
        int count = 0;
        foreach(var item in status){
            if (Status.IsNegative(item._status)) count++;
        }
        return count;
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
        if (target == Status.status.vulnerable && tag == "Player" && origin_level == 0 && level > 0) vulnerable_buffer = true;
        if (origin_level + level != 0) status.Add((target, origin_level + level));
        status.Sort();
        UpdateStatus();
    }
}
