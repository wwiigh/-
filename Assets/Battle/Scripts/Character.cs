using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
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
    bool tookDmgLastTurn = false;
    bool tookDmgThisTurn = false;

    List<(Status.status _status, int level)> status = new List<(Status.status _status, int level)>();
    List<GameObject> statusIcons = new List<GameObject>();
    void Start()
    {
        effects = GameObject.FindGameObjectWithTag("BattleEffects").GetComponent<BattleEffects>();
        Init_HP();
        UpdateStatus();
    }

    // void Update()
    // {
    //     if(this.gameObject.tag == "Player")
    //     {
    //         Global.player_hp = GetHP();
    //         UpdateHP();
    //     }
    // }

    public bool HP_Initialized(){
        return hpBar;
    }
    public void Init_HP(){
        if (HP_Initialized()) return;
        hpBar =  Instantiate(hpBarTemplate, transform);
        hpBar.transform.localPosition = hpBar.transform.localPosition + Vector3.down*185;
        if (GetEnemyID() == 402) hpBar.GetComponent<HPBar>().AdjustLength(0.5f);
    }
    void UpdateHP(){
        if (!HP_Initialized()) Init_HP();
        if (tag == "Player") Global.SetHP(hp);
        hpBar.GetComponent<HPBar>().UpdateHP();
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
    public void ClearStatus(){
        foreach(GameObject statusObj in statusIcons) statusObj.GetComponent<StatusIcon>().Destroy();
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
            FindObjectOfType<BattleController>().EnemyDie(gameObject);
            SafeDestroy();
        }
        else{
            FindObjectOfType<BattleController>().PlayerDie();
        }
    }



    public bool GetHit(int damage){
        if (hp == 0) return true;

        int evadeLevel = GetComponent<Character>().GetStatus(Status.status.evade);
        if (evadeLevel > 0)
            if (Random.Range(0, 100) < evadeLevel) damage = 0;

        if (damage > 0) tookDmgThisTurn = true;
        
        int block_diff = armor + block;
        int hp_diff = hp;
        bool dead = false;
        GameObject dmgText = Instantiate(damageTextTemplate, transform);
        dmgText.GetComponent<DamageText>().Show(damage, true);
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
        UpdateHP();
        if (dead && tag == "Player" && Global.Check_Relic_In_Bag(8)){
            damage = 0;
            hp = maxHP;
            dead = false;
            Relic_Implement.Handle_Relic_Dead(Relic_Implement.DeadType.Player);
            UpdateHP();
        }
        if (damage > 0) GetComponent<HitAnimation>().Play(true, dead);
        if (dead){
            Debug.Log("GetHit called Die()");
            StartCoroutine(Die());
            return dead;
        }

        block_diff -= armor + block;
        hp_diff -= hp;

        AnEyeForAnEye(damage);
        PowerCompete(hp_diff);

        if (tag == "Player") Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.GetDamage, damage);

        if (GetComponent<Character>().GetStatus(Status.status.energy_absorb) > 0){
            AddStatus(Status.status.accumulation, block_diff);
            GetComponent<EnemyMove>().Enemy303_Detect();
        }

        if (GetComponent<Character>().GetStatus(Status.status.fluid) > 0){
            AddBlock(hp_diff);
        }

        if (GetEnemyID() == 305) GetComponent<EnemyMove>().Enemy305_Detect();

        if (GetEnemyID() == 313) GetComponent<Animator>().Play("313_hit");

        return dead;
    }
    public bool LoseHP(int value){
        if (hp == 0) return true;

        int evadeLevel = GetComponent<Character>().GetStatus(Status.status.evade);
        if (evadeLevel > 0)
            if (Random.Range(0, 100) < evadeLevel) value = 0;

        if (value > 0) tookDmgThisTurn = true;
        
        GameObject dmgText = Instantiate(damageTextTemplate, transform);
        dmgText.GetComponent<DamageText>().Show(value, false);

        PowerCompete(value);
        
        if (tag == "Player") Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.GetDamage, value);

        if (hp <= value){
            hp = 0;
            UpdateHP();
            Debug.Log("LoseHP called Die()");
            StartCoroutine(Die());
            GetComponent<HitAnimation>().Play(false, true);
            return false;
        }
        else{
            hp -= value;
            UpdateHP();
            AnEyeForAnEye(value);
            GetComponent<HitAnimation>().Play(false, false);
            return true;
        }
    }
    public bool LoseHP(float value){
        if (value % 1 >= 0.5f) return LoseHP((int)(value / 1) + 1);
        return LoseHP((int)(value / 1));
    }
    void AnEyeForAnEye(int dmgReceived){
        if (tag != "Player") return;
        Deck deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        deck.AnEyeForAnEye(dmgReceived);
    }
    void PowerCompete(int hp_diff){
        if (GetStatus(Status.status.power_compete) == 0) return;
        int powerCompeteLevel = GetStatus(Status.status.power_compete);
        if (hp_diff == 0 && powerCompeteLevel == 0) return;

        if (hp_diff >= powerCompeteLevel){
            AddStatus(Status.status.power_compete, -powerCompeteLevel);
            AddStatus(Status.status.vulnerable, 2);
        }
        else AddStatus(Status.status.power_compete, -hp_diff);
    }



    // public bool Attack(GameObject target, int dmg){
    //     // int final_dmg = BattleController.ComputeDamage(gameObject, target, dmg);
    //     // if (target.GetComponent<Character>().GetStatus(Status.status.invincible) > 0)
    //     //     target.GetComponent<Character>().AddStatus(Status.status.invincible, -1);
    //     // bool target_alive = target.GetComponent<Character>().GetHit(final_dmg);
    //     // int fireLevel = GetStatus(Status.status.fire_enchantment);
    //     // if (tag == "Player" && fireLevel > 0 && target_alive) target.GetComponent<Character>().AddStatus(Status.status.burn, fireLevel); 
    //     // return target_alive;
    //     return Attack(target, dmg, 1, 1);
    // }
    public bool Attack(GameObject target, int dmg, float strength_multiplier=1, float tmp_strength_multiplier=1){
        if (tag == "Player") FindObjectOfType<BattleSound>().Play(BattleSound.SoundType.playerAttack);
        if (tag == "Enemy") FindObjectOfType<BattleSound>().Play(BattleSound.SoundType.enemyAttack);
        if(target==null)return true;
        int armor_before = target.GetComponent<Character>().GetArmor() + target.GetComponent<Character>().GetBlock();
        int hp_before = target.GetComponent<Character>().GetHP();

        if (tag == "Player" && GetStatus(Status.status.void_sword) > 0) dmg = 1;

        int final_dmg = BattleController.ComputeDamage(gameObject, target, dmg, strength_multiplier, tmp_strength_multiplier);
        if (target.GetComponent<Character>().GetStatus(Status.status.invincible) > 0)
            target.GetComponent<Character>().AddStatus(Status.status.invincible, -1);
        bool target_dead = target.GetComponent<Character>().GetHit(final_dmg);

        int spike_level = target.GetComponent<Character>().GetStatus(Status.status.spike);
        if (spike_level > 0) GetComponent<Character>().GetHit(spike_level);

        if (target.GetComponent<Character>().GetStatus(Status.status.doom) > 0) GetComponent<Character>().GetHit(final_dmg / 2);

        int counter_level = target.GetComponent<Character>().GetStatus(Status.status.counter);
        if (target.tag == "Player" && counter_level > 0 && target.GetComponent<Character>().GetArmor() == 0 && armor_before > 0){
            GetHit(counter_level);
            AddStatus(Status.status.vulnerable, 1);
        }

        int fireArmor_level = target.GetComponent<Character>().GetStatus(Status.status.fire_armor);
        if (fireArmor_level > 0) AddStatus(Status.status.burn, fireArmor_level);

        int fireLevel = GetStatus(Status.status.fire_enchantment);
        if (tag == "Player" && fireLevel > 0 && !target_dead) target.GetComponent<Character>().AddStatus(Status.status.burn, fireLevel); 
        
        // if (target.GetComponent<Character>().GetStatus(Status.status.bleed) > 0 && target.GetComponent<Character>().GetHP() != GetHP()){
        //     target.GetComponent<Character>().LoseHP(2);
        //     target.GetComponent<Character>().AddStatus(Status.status.bleed, -1);
        // }

        int explosive_force_level = GetStatus(Status.status.explosive_force);
        if (explosive_force_level > 0) AddStatus(Status.status.explosive_force, -explosive_force_level);

        int blood_drain_level = GetStatus(Status.status.blood_drain);
        if (blood_drain_level > 0){
            Heal(final_dmg / 2);
            AddStatus(Status.status.blood_drain, -1);
        }

        if (tag == "Player") Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.Attack);

        return target_dead;
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
    public void SetHP(int value){
        if (value > hp) Heal(value - hp);
        if (value < hp) LoseHP(hp - value);
    }
    public bool IsAlive(){
        return hp > 0;
    }
    public bool IsFullHealth(){
        return hp == maxHP;
    }



    void PlayEffect(string effect_name){
        if (!effects) effects = GameObject.FindGameObjectWithTag("BattleEffects").GetComponent<BattleEffects>();
        effects.Play(gameObject, effect_name);
    }



    public void Heal(int value){
        if (hp + value > maxHP) hp = maxHP;
        else hp += value;
        UpdateHP();
        PlayEffect("heal");
    }



    public int GetArmor(){
        return armor;
    }
    public void AddArmor(int value, bool byCard=true){
        if (tag == "Player" && GetStatus(Status.status.void_sword) > 0 && byCard) value = 1;
        if (tag == "Player") armor += BattleController.ComputeArmor(value);
        else armor += value;
        UpdateHP();
        PlayEffect("get_armor");
        if (tag == "Player" && value > 0 && GetStatus(Status.status.fortify) > 0){
            Deck.Draw();
            AddStatus(Status.status.fortify, -1);
        }

        if (tag == "Player") Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.GetArmor, value);
    }



    public int GetBlock(){
        return block;
    }
    public void AddBlock(int value, bool byCard=true){
        if (tag == "Player" && GetStatus(Status.status.void_sword) > 0 && byCard) value = 1;
        if (tag == "Player") block += BattleController.ComputeArmor(value);
        else block += value;
        UpdateHP();
        if (value > 0) PlayEffect("get_armor");
        if (tag == "Player" && value > 0 && GetStatus(Status.status.fortify) > 0){
            Deck.Draw();
            AddStatus(Status.status.fortify, -1);
        }
        
        if (tag == "Player") Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.GetArmor, value);
    }



    // public void HoverIn(){
    //     // Debug.Log("HoverIn");
    //     if (battleController.GetState() == BattleController.BattleState.SelectEnemy && tag == "Enemy"){
    //         if (!BattleController.HasTauntEnemy() || GetComponent<Character>().GetStatus(Status.status.taunt) > 0){
    //             transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 128, 128, 255);
    //             // Debug.Log("change color");
    //         }
    //     }
    // }
    // public void Click(){
    //     if (battleController.GetState() == BattleController.BattleState.SelectEnemy && tag == "Enemy"){
    //         if (!BattleController.HasTauntEnemy() || GetComponent<Character>().GetStatus(Status.status.taunt) > 0){
    //             battleController.EnemySelected(gameObject);
    //             transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    //         }
    //     }
    // }
    // public void HoverOut(){
    //     if (battleController.GetState() == BattleController.BattleState.SelectEnemy && tag == "Enemy"){
    //         transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    //     }
    // }



    public void TurnStart(){
        tookDmgLastTurn = tookDmgThisTurn;
        tookDmgThisTurn = false;

        if (tag == "Player") Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.TurnStart);

        if (GetEnemyID() == 403) GetComponent<EnemyMove>().list.Add(GetHP());
        if (GetStatus(Status.status.auto_guard) > 0 && !tookDmgLastTurn) AddStatus(Status.status.taunt, 1);
        if (GetStatus(Status.status.burn) > 0) TriggerBurn(true);
        if (GetStatus(Status.status.decay) > 0) LoseHP(GetMaxHP() * GetStatus(Status.status.decay) * 0.01f);
        if (GetStatus(Status.status.lock_on_prepare) > 0){
            AddStatus(Status.status.lock_on_prepare, -1);
            AddStatus(Status.status.lock_on, 1);
        }
        if (GetStatus(Status.status.fire_armor) > 0) AddStatus(Status.status.fire_armor, -GetStatus(Status.status.fire_armor));
        if (hpBar != null){
            block = 0;
            UpdateHP();
        }

        List<(Status.status _status, int level)> decreaseList = new();
        foreach(var pack in status){
            if (pack._status == Status.status.vulnerable && tag == "Player" && vulnerable_buffer){
                vulnerable_buffer = false;
                continue;
            }
            if (Status.DecreaseOnTurnStart(pack._status)) decreaseList.Add(pack);
        }
        foreach(var pack in decreaseList) AddStatus(pack._status, -1);
        UpdateStatus();

        if (tag == "Enemy") Invoke("TurnEnd", 1);
    }
    public void TurnEnd(){
        if (GetStatus(Status.status.remnant) > 0){
            int tmp = GetStatus(Status.status.temporary_strength);
            if (tmp % 2 == 1) tmp++;
            AddStatus(Status.status.temporary_strength, -tmp / 2);

            tmp = GetStatus(Status.status.temporary_dexterity);
            if (tmp % 2 == 1) tmp++;
            AddStatus(Status.status.temporary_dexterity, -tmp / 2);
        }
        else{
            int tmp = GetStatus(Status.status.temporary_strength);
            AddStatus(Status.status.temporary_strength, -tmp);
            
            tmp = GetStatus(Status.status.temporary_dexterity);
            AddStatus(Status.status.temporary_dexterity, -tmp);
        }

        List<(Status.status _status, int level)> decreaseList = new List<(Status.status _status, int level)>();
        List<(Status.status _status, int level)> clearList = new List<(Status.status _status, int level)>();

        int rampartLevel = GetStatus(Status.status.rampart);
        if (rampartLevel > 0) AddBlock(rampartLevel);

        int oppressLevel = GetStatus(Status.status.oppress);
        if (oppressLevel > 0) GameObject.FindWithTag("Player").GetComponent<Character>().GetHit(oppressLevel);

        int mentalWeakLevel = GetStatus(Status.status.mental_weak);
        if (mentalWeakLevel > 0) Global.AddSan(-mentalWeakLevel);

        if (GetStatus(Status.status.bleed) > 0) LoseHP(1);

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
        int tmp = 0;
        if (decrease){
            if (level % 2 == 1) tmp = level / 2 + 1;
            else tmp = level / 2;
            AddStatus(Status.status.burn, -tmp);
        }

        if (GetComponent<Character>().GetStatus(Status.status.invincible) > 0){
            GetComponent<Character>().AddStatus(Status.status.invincible, -1);
            return false;
        }
        if (Status.Is_SymboticA_Active(gameObject) || Status.Is_SymboticB_Active(gameObject)) return false;
        
        int extraDmg = Global.Check_Relic_In_Bag(13)? 3:0;
        bool dead = LoseHP(level - GetStatus(Status.status.rock_solid) + extraDmg);
        return dead;
    }

    public void InitPlayer(){
        nameText.text = "玩家";
        maxHP = Global.player_max_hp;
        hp = Global.player_hp;
        GetComponent<Animator>().Play("player_idle");
        // AddStatus(Status.status.dice20, Random.Range(-4, 5));
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

        
        // AddStatus(Status.status.immobile, 2);
    }

    public int GetEnemyID(){
        if (tag == "Player") return -1;
        return enemyClass.id;
    }
    public EnemyClass.EnemyType GetEnemyType(){
        if (tag == "Player") return 0;
        else return enemyClass.type;
    }
    public Vector3 GetSize(){
        if (!enemyClass) return new Vector3(300, 300);
        return enemyClass.size;
    }
    public Vector3 GetOffset(){
        if (!enemyClass) return new Vector3(0, 0);
        return enemyClass.offset;
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

        if (tag == "Player") FindObjectOfType<Deck>().UpdateHand();
        if (tag == "Enemy") GetComponent<EnemyMove>().SetIntention();
    }

    public void SafeDestroy(){
        Debug.Log("safe destroy");
        GetComponent<EnemyMove>().ClearIntention();
        ClearStatus();
        Destroy(gameObject);
    }
}
