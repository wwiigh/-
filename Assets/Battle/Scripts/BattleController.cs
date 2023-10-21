using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] List<Sprite> backgroundImages;
    [SerializeField] GameObject deck_obj;
    [SerializeField] GameObject characters;
    [SerializeField] GameObject character_template;
    [SerializeField] GameObject panel;
    [SerializeField] EnemyClass[] enemyClass;
    [SerializeField] EquipmentClass[] equipmentClass;
    GameObject player;
    Deck deck;
    public enum BattleType{
        Normal,
        Elite,
        Boss,
        Special
    }
    public enum BattleState{
        Normal,
        SelectEnemy,
        SelectCard
    }
    BattleState battleState = BattleState.Normal;
    bool playedAttackThisTurn = false;
    bool playedSkillThisTurn = false;
    bool playedCardThisTurn = false;
    bool tookDmgLastTurn = false;
    bool tookDmgThisTurn = false;
    bool discardedThisTurn = false;
    Card lastCardPlayed = null;
    bool played46ThisTurn = false;
    int currentTurn = 0;
    int currentBattleID = 0;
    BattleType currentBattleType;
    Card swallowedCard = null;


    List<int> notEncounterdYet_normal = new List<int>();
    List<int> notEncounterdYet_elite = new List<int>();
    List<int> notEncounterdYet_boss = new List<int>();


    private void Start() {
        // Global.current_level = 1;
        EnterNewLevel();
        deck = deck_obj.GetComponent<Deck>();
    }


    public void EnterNewLevel(){
        // Debug.Log("current level: " + Global.current_level);
        notEncounterdYet_normal.Clear();
        notEncounterdYet_elite.Clear();
        notEncounterdYet_boss.Clear();
        if (Global.current_level == 1){
            for(int i = 0; i < 7; i++) notEncounterdYet_normal.Add(101 + i);
            notEncounterdYet_elite.Add(111);
            notEncounterdYet_elite.Add(112);
            notEncounterdYet_boss.Add(121);
        }
        if (Global.current_level == 2){
            for(int i = 0; i < 7; i++) notEncounterdYet_normal.Add(201 + i);
            notEncounterdYet_elite.Add(211);
            notEncounterdYet_elite.Add(212);
            notEncounterdYet_boss.Add(221);
        }
        if (Global.current_level == 3){
            for(int i = 0; i < 5; i++) notEncounterdYet_normal.Add(301 + i);
            notEncounterdYet_elite.Add(311);
            notEncounterdYet_elite.Add(312);
            notEncounterdYet_boss.Add(321);
        }
    }


    public void EnterBattle(){
        for (int i = characters.transform.childCount - 1; i >= 0; i--){
            Destroy(characters.transform.GetChild(i).gameObject);
        }

        EnterBattle(BattleType.Normal);
    }

    public void EnterBattle(BattleType type){
        int tmp = -1;
        currentBattleType = type;
        if (type == BattleType.Normal){
            if (notEncounterdYet_normal.Count == 0){
                Debug.Log("EnterBattle: no more normal battles");
                return;
            }
            tmp = notEncounterdYet_normal[Random.Range(0, notEncounterdYet_normal.Count)];
            notEncounterdYet_normal.Remove(tmp);
        }
        if (type == BattleType.Elite){
            if (notEncounterdYet_elite.Count == 0){
                Debug.Log("EnterBattle: no more elite battles");
                return;
            }
            tmp = notEncounterdYet_elite[Random.Range(0, notEncounterdYet_elite.Count)];
            notEncounterdYet_elite.Remove(tmp);
        }
        if (type == BattleType.Boss){
            if (notEncounterdYet_boss.Count == 0){
                Debug.Log("EnterBattle: no more boss battles");
                return;
            }
            tmp = notEncounterdYet_boss[Random.Range(0, notEncounterdYet_boss.Count)];
            notEncounterdYet_boss.Remove(tmp);
        }
        if (type == BattleType.Special){
            tmp = 401;
        }
        EnterBattle_id(tmp);
        
        background.GetComponent<Image>().sprite = backgroundImages[Global.current_level - 1];
        background.GetComponent<RectTransform>().offsetMin = new Vector2(0, 390);

        GameObject.Find("SoundPlay").GetComponent<AudioSource>().clip = GetComponent<BattleSound>().bgm;
        GameObject.Find("SoundPlay").GetComponent<AudioSource>().Play();
    }
    


    int enemyCount = 0;
    public int[] GetEnemyIDs(){
        int[] enemyID;
        switch(currentBattleID){
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
            case 111:
                enemyID = new int[]{5};
                break;
            case 112:
                enemyID = new int[]{6};
                break;
            case 121:
                enemyID = new int[]{7};
                break;
            
            case 201:
                enemyID = new int[]{9, 9};
                break;
            case 202:
                enemyID = new int[]{8, 8, 8};
                break;
            case 203:
                enemyID = new int[]{8, 12};
                break;
            case 204:
                enemyID = new int[]{11, 10};
                break;
            case 205:
                enemyID = new int[]{11, 12};
                break;
            case 206:
                enemyID = new int[]{8, 10, 10};
                break;
            case 207:
                enemyID = new int[]{8, 9};
                break;
            case 211:
                enemyID = new int[]{13};
                break;
            case 212:
                enemyID = new int[]{14, 15};
                break;
            case 221:
                enemyID = new int[]{16};
                break;
                
            case 301:
                enemyID = new int[]{17, 17, 17};
                break;
            case 302:
                enemyID = new int[]{19, 20, 18};
                break;
            case 303:
                enemyID = new int[]{21, 22, 23};
                break;
            case 304:
                enemyID = new int[]{24};
                break;
            case 305:
                enemyID = new int[]{25, 26};
                break;
            case 311:
                enemyID = new int[]{27};
                break;
            case 312:
                enemyID = new int[]{28};
                break;
            case 321:
                enemyID = new int[]{29};
                break;

            case 401:
                enemyID = new int[]{32};
                break;
            default:
                Debug.Log("GetEnemyIDs: Unknown id " + currentBattleID.ToString());
                return null;
        }
        return enemyID;
    }
    public void EnterBattle_id(int id){
        currentBattleID = id;
        InitPlayer();
        int[] enemyID = GetEnemyIDs();
        enemyCount = enemyID.Length;
        SpawnEnemies(enemyID);
        Cost.Init();
        deck.Init();
        swallowedCard = null;
        currentTurn = 0;
        StartCoroutine(_StartTurn());
    }



    public void EnemyDie(GameObject deadEnemy){
        enemyCount -= 1;
        EnemyClass.EnemyType enemyType = deadEnemy.GetComponent<Character>().GetEnemyType();
        if (enemyType == EnemyClass.EnemyType.Elite) Global.kill_elites++;
        if (enemyType == EnemyClass.EnemyType.Boss) Global.kill_leaders++;
        Global.kill_enemys++;

        if (enemyCount == 0){
            Debug.Log("battle end: you win");
            ClearDescriptionBox();
            FindObjectOfType<get_booty>().ShowLoot(GetEnemyIDs(), currentBattleType);
            Global.LeaveBattle();
            return;
        }

        Relic_Implement.Handle_Relic_Dead(Relic_Implement.DeadType.Enemy);
        Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.KillEnemy);
        
        if (currentBattleID == 311){
            foreach(Transform child in characters.transform){
                if (child.GetComponent<Character>().GetEnemyID() == 311){
                    child.GetComponent<EnemyMove>().Enemy311_Detect();
                }
            }
        }

        foreach(GameObject enemy in GetAllEnemy()){
            int tmp = enemy.GetComponent<Character>().GetStatus(Status.status.rage);
            if (tmp > 0) enemy.GetComponent<Character>().AddStatus(Status.status.strength, tmp);

            tmp = enemy.GetComponent<Character>().GetStatus(Status.status.grief);
            if (tmp > 0) enemy.GetComponent<Character>().AddArmor(tmp);

            tmp = enemy.GetComponent<Character>().GetStatus(Status.status.absorb);
            if (tmp > 0){
                enemy.GetComponent<Character>().Heal(10);
                enemy.GetComponent<Character>().AddStatus(Status.status.strength, 3);
            }

            if (enemy.GetComponent<Character>().GetStatus(Status.status.symbioticA) > 0)
                enemy.GetComponent<Character>().AddStatus(Status.status.symbioticA, -1);

            if (enemy.GetComponent<Character>().GetStatus(Status.status.symbioticB) > 0)
                enemy.GetComponent<Character>().AddStatus(Status.status.symbioticB, -1);
        }

        int grudgeLevel = deadEnemy.GetComponent<Character>().GetStatus(Status.status.grudge);
        if (grudgeLevel > 0)
            foreach(GameObject enemy in GetAllEnemy())
                enemy.GetComponent<Character>().AddStatus(Status.status.strength, grudgeLevel);
        // else ReorderEnemy();
    }
    public void PlayerDie(){
        Relic_Implement.Handle_Relic_Dead(Relic_Implement.DeadType.Player);
        if (!player.GetComponent<Character>().IsAlive()) 
        {
            Debug.Log("battle end: you lose");
            FindObjectOfType<Map_Generate>().battle_object.SetActive(false);
            FindObjectOfType<Map_Generate>().battle_object_equipment.SetActive(false);
            FindObjectOfType<Map_Generate>().CardPanel.SetActive(false);
            FindObjectOfType<Map_Node_Action>().Dieending();
        }
    }



    public bool PlayedAttackAndSkillThisTurn(){
        return playedAttackThisTurn && playedSkillThisTurn;
    }
    static public bool PlayedCardThisTurn(){
        return FindObjectOfType<BattleController>().playedCardThisTurn;
    }
    static public int GetCurrentTurn(){
        return FindObjectOfType<BattleController>().currentTurn;
    }
    public bool TookDmgLastTurn(){
        return tookDmgLastTurn;
    }
    public bool DiscardedThisTurn(){
        return discardedThisTurn;
    }
    public Card GetLastCardPlayed(){
        return lastCardPlayed;
    }
    public bool Played46ThisTurn(){
        return played46ThisTurn;
    }
    public bool PlayedAttackThisTurn(){
        return playedAttackThisTurn;
    }
    static public int GetEnemyCount(){
        return FindObjectOfType<BattleController>().enemyCount;
    }
    static public bool HasTauntEnemy(){
        foreach(GameObject enemy in GetAllEnemy()){
            if (enemy.GetComponent<Character>().GetStatus(Status.status.taunt) > 0) return true;
        }
        return false;
    }
    static public int GetBattleID(){
        return FindObjectOfType<BattleController>().currentBattleID;
    }



    public void PlayedCard(Card card){
        playedCardThisTurn = true;
        lastCardPlayed = card;
        if (card.rarity == Card.Rarity.common) Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.UseNormalCard);
        if (card.type == Card.Type.attack){
            playedAttackThisTurn = true;
            Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.UseAttackCard);
            Relic_Implement.Update_Relic(Relic_Implement.Type.UseAttackCard);
        }
        if (card.type == Card.Type.skill){
            playedSkillThisTurn = true;
            Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.UseSkillCard);
            Relic_Implement.Update_Relic(Relic_Implement.Type.UseSkillCard);
        }
        if (card.id == 46) played46ThisTurn = true;
        Equipment_Charge.Update_Equipment_Charge(Equipment_Charge.Charge_Type.UseCard);

        if (currentBattleID == 302){
            foreach(Transform child in characters.transform){
                if (child.tag == "Enemy" && child.GetComponent<Character>().GetEnemyID() == 302){
                    child.GetComponent<EnemyMove>().Enemy302_Detect(card);
                }
            }
        }

        if (currentBattleID == 321){
            foreach(Transform child in characters.transform){
                if (child.tag == "Enemy" && child.GetComponent<Character>().GetEnemyID() == 313){
                    child.GetComponent<EnemyMove>().SetIntention();
                }
            }
        }
    }
    public void Discarded(){
        discardedThisTurn = true;
    }



    public void EndTurn(){
        GameObject.Find("End turn button").GetComponent<Button>().interactable = false;
        Character player_character = characters.transform.GetChild(0).GetComponent<Character>();
        if (player_character.GetStatus(Status.status.prepare) > 0 && Deck.GetHand().Count >= 1) PrepareEffect();
        else StartCoroutine(_EndTurn());
    }
    IEnumerator _EndTurn(){
        Character player_character = characters.transform.GetChild(0).GetComponent<Character>();
        deck.TurnEnd();
        player_character.TurnEnd();
        Equipment_Charge.Update_Equipment_Cold();

        List<GameObject> enemylist = new();
        foreach(Transform child in characters.transform)
            if (child.tag == "Enemy" && child.GetComponent<Character>().IsAlive()) enemylist.Add(child.gameObject);
        
        // bubble sort by position.x
        int start_idx = 0;
        int idx;
        while(start_idx < enemylist.Count){
            idx = start_idx;
            while(idx < enemylist.Count - 1){
                if (enemylist[idx].transform.localPosition.x > enemylist[idx+1].transform.localPosition.x)
                    (enemylist[idx], enemylist[idx+1]) = (enemylist[idx+1], enemylist[idx]);
                idx++;
            }
            start_idx++;
        }

        foreach(GameObject chosen in enemylist){
            if (!chosen || !chosen.GetComponent<Character>().IsAlive()) continue;
            chosen.GetComponent<Character>().TurnStart();
            if (chosen.GetComponent<Character>().IsAlive()) chosen.GetComponent<EnemyMove>().Move();
            yield return new WaitForSeconds(1f);
        }
        if(player!=null)
        StartCoroutine(_StartTurn());
    }

    void PrepareEffect(){
        SelectCard(Callback_prepare, 1, false, false);
    }
    public void Callback_prepare(List<GameObject> list){
        if (list.Count > 0) list[0].GetComponent<CardDisplay>().thisCard.keepBeforeUse = true;
        StartCoroutine(_EndTurn());
    }

    IEnumerator _StartTurn(){
        Character player_character = player.GetComponent<Character>();
        currentTurn += 1;
        Relic_Implement.Update_Relic(Relic_Implement.Type.TurnStart);

        int tmp = player_character.GetStatus(Status.status.turtle_stance);
        if (tmp > 0 && !playedAttackThisTurn) player_character.AddStatus(Status.status.temporary_strength, tmp);

        tmp = player_character.GetStatus(Status.status.dragon_stance);
        if (tmp > 0 && !playedSkillThisTurn) player_character.AddStatus(Status.status.temporary_dexterity, tmp);

        playedAttackThisTurn = false;
        playedSkillThisTurn = false;
        playedCardThisTurn = false;
        discardedThisTurn = false;
        played46ThisTurn = false;
        foreach(Transform child in characters.transform){
            if (child.tag == "Enemy"){
                child.GetComponent<EnemyMove>().ChangeState();
                child.GetComponent<EnemyMove>().SetIntention();
            }
        }
        deck.TurnStart();

        tmp = player_character.GetStatus(Status.status.compress);
        if (tmp > 0){
            Cost.Refill(-tmp);
            player_character.AddStatus(Status.status.compress, -tmp);
        }
        else Cost.Refill(0);

        player.GetComponent<Character>().TurnStart();
        yield return new WaitForSeconds(0);
        GameObject.Find("End turn button").GetComponent<Button>().interactable = true;
    }



    public BattleState GetState(){
        return battleState;
    }
    void EnterState(BattleState state){
        Debug.Log("battle controller: enter state " + state.ToString());
        battleState = state;
    }



    public delegate void ReturnEnemy(GameObject enemy);
    ReturnEnemy selectEnemy_callback = null;
    public void SelectEnemy(ReturnEnemy callback){
        selectEnemy_callback = callback;
        EnterState(BattleState.SelectEnemy);
    }
    public void EnemySelected(GameObject enemy){
        EnterState(BattleState.Normal);
        selectEnemy_callback(enemy);
    }
    static public GameObject GetRandomEnemy(){
        List<GameObject> pool = new();
        foreach(Transform child in FindObjectOfType<BattleController>().characters.transform) 
            if (child.tag == "Enemy") pool.Add(child.gameObject);
        return pool[Random.Range(0, pool.Count)];
    }
    static public List<GameObject> GetAllEnemy(){
        List<GameObject> ret = new();
        foreach(Transform child in FindObjectOfType<BattleController>().characters.transform) 
            if (child.tag == "Enemy" && child.GetComponent<Character>().GetHP() > 0) ret.Add(child.gameObject);
        return ret;
    }
    public GameObject GetEnemyWithLowestHP(){
        int lowestHP = 999;
        GameObject ret = null;
        foreach(Transform child in characters.transform){
            if (child.tag == "Enemy" && child.GetComponent<Character>().GetHP() < lowestHP){
                ret = child.gameObject;
                lowestHP = child.GetComponent<Character>().GetHP();
            }
        }
        return ret;
    }



    bool isEqual = true;
    bool cancellable = true;
    int targetCardNumber = 0;
    List<GameObject> selectedCards = new();
    public delegate void ReturnListOfGameObject(List<GameObject> obj);
    ReturnListOfGameObject selectCard_callback = null;
    public void SelectCard(ReturnListOfGameObject callback, int count, bool _isEqual, bool _cancellable){
        selectCard_callback = callback;
        selectedCards.Clear();
        targetCardNumber = count;
        isEqual = _isEqual;
        cancellable = _cancellable;
        panel.GetComponent<Panel>().Show(BattleState.SelectCard, 0, targetCardNumber, isEqual, cancellable);
        EnterState(BattleState.SelectCard);
    }
    public void SelectCard_Add(GameObject card){
        selectedCards.Add(card);
        panel.GetComponent<Panel>().UpdateAll(selectedCards.Count, targetCardNumber, isEqual, cancellable);
    }
    public void SelectCard_Remove(GameObject card){
        if (selectedCards.Contains(card)) selectedCards.Remove(card);
        panel.GetComponent<Panel>().UpdateAll(selectedCards.Count, targetCardNumber, isEqual, cancellable);
    }
    public void SelectCard_Confirm(){
        if (isEqual && selectedCards.Count == targetCardNumber){
            EnterState(BattleState.Normal);
            // CardEffects.CardSelected(selectedCards);
            List<GameObject> selectedCards_copy = new(selectedCards);
            selectCard_callback(selectedCards_copy);
            panel.GetComponent<Panel>().Hide();
            deck.ResetHand(false);
            Deck.Rearrange();
        }
        if (!isEqual && selectedCards.Count <= targetCardNumber){
            EnterState(BattleState.Normal);
            // CardEffects.CardSelected(selectedCards);
            List<GameObject> selectedCards_copy = new(selectedCards);
            selectCard_callback(selectedCards_copy);
            panel.GetComponent<Panel>().Hide();
            deck.ResetHand(false);
            Deck.Rearrange();
        }
    }
    public void SelectCard_Cancel(){
        if (!cancellable) return;
        if (battleState == BattleState.SelectCard){
            EnterState(BattleState.Normal);
            panel.GetComponent<Panel>().Hide();
            deck.ResetHand(true);
            Deck.Rearrange();
        }
    }
    public bool SelectCard_Availible(){
        return selectedCards.Count < targetCardNumber;
    }



    public GameObject GetPlayer(){
        return player;
    }



    public void SetSwallowedCard(Card card){
        swallowedCard = card;
    }
    public Card GetSwallowedCard(){
        return swallowedCard;
    }



    void InitPlayer(){
        // GameObject player = Instantiate(character, transform);
        player = Instantiate(character_template, characters.transform);
        player.transform.localPosition = new Vector3(-350, 0, 0);
        player.tag = "Player";
        player.GetComponent<Character>().InitPlayer();
    }

    void InitEnemy(int id, int idx){
        GameObject enemy = Instantiate(character_template, characters.transform);
        enemy.transform.localPosition = new Vector3(180 + idx*300, 0, 0);
        enemy.tag = "Enemy";
        enemy.GetComponent<Character>().InitEnemy(enemyClass[id]);

        if (id == 17) enemy.GetComponent<EnemyMove>().SetState(idx - 1);
    }

    public GameObject SpawnEnemyAt(int id, Vector2 position){
        enemyCount++;
        GameObject enemy = Instantiate(character_template, characters.transform);
        enemy.transform.localPosition = position;
        enemy.tag = "Enemy";
        enemy.GetComponent<Character>().InitEnemy(enemyClass[id]);
        return enemy;
    }
    void SpawnEnemies(int[] id){
        for (int i = 0; i < id.Length; i++)
            InitEnemy(id[i], i);
        // ReorderEnemy();
    }

    static public float GetEnemyHeight(int id){
        BattleController battleController = FindObjectOfType<BattleController>();
        float ret = -1;
        if (id >= 101 && id <= 108) ret = battleController.enemyClass[id - 101].size.y;
        else if (id >= 201 && id <= 209) ret = battleController.enemyClass[8 + id - 201].size.y;
        else if (id >= 301 && id <= 313) ret = battleController.enemyClass[17 + id - 301].size.y;
        else if (id >= 401 && id <= 403) ret = battleController.enemyClass[30 + id - 401].size.y;
        if (ret == 0) ret = 300;
        return ret;
    }



    void ClearDescriptionBox(){
        DescriptionBox tmp = FindObjectOfType<DescriptionBox>();
        if (tmp == null) return;
        Destroy(tmp.gameObject);
    }

    public void ReorderEnemy(){
        int idx = 0;
        foreach(Transform child in characters.transform){
            if (child.tag != "Enemy" || !child.GetComponent<Character>().IsAlive()) continue;
            StartCoroutine(_Move(child.gameObject, new Vector3(180 + idx*300, 0, 0)));
            // MoveCharacter(child.gameObject, new Vector3(180 + idx*300, 0, 0));
            idx++;
        }
    }

    // void MoveCharacter(GameObject obj, Vector3 destination){

    // }
    IEnumerator _Move(GameObject obj, Vector3 destination){
        Debug.Log("_Move() is called");
        while(obj != null && (obj.transform.localPosition - destination).magnitude > 0.0001f){
            obj.transform.localPosition = Vector3.Lerp(obj.transform.localPosition, destination, 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
        if (obj != null) obj.transform.localPosition = destination;
    }

    public EquipmentClass[] GetEquipments(){
        return equipmentClass;
    }

    public static int ComputeDamage(GameObject from, GameObject to, float dmg){
        return ComputeDamage(from, to, dmg, 1, 1);
    }
    public static int ComputeDamage(GameObject from, GameObject to, float dmg, float strength_multiplier, float tmp_strength_multiplier){
        Character from_character = from.GetComponent<Character>();
        Character to_character = to.GetComponent<Character>();
        float multiplier = 1.0f;

        if (to_character.GetStatus(Status.status.invincible) > 0) return 0;
        if (to_character.GetStatus(Status.status.symbioticA) > 0 && GetCurrentTurn() % 2 == 1) return 0;
        if (to_character.GetStatus(Status.status.symbioticB) > 0 && GetCurrentTurn() % 2 == 0) return 0;

        int strength = (int)(from_character.GetStatus(Status.status.strength) * strength_multiplier) + 
                       (int)(from_character.GetStatus(Status.status.temporary_strength) * tmp_strength_multiplier) +
                       from_character.GetStatus(Status.status.dice20);
        int additional = from_character.GetStatus(Status.status.explosive_force);
        if (from_character.GetStatus(Status.status.weak) > 0) multiplier -= 0.25f;
        if (to_character.GetStatus(Status.status.vulnerable) > 0) multiplier += 0.25f;
        multiplier += from_character.GetStatus(Status.status.damage_adjust) * 0.01f;

        int final_dmg = (int) ((dmg + strength + additional) * multiplier);
        
        if (from_character.GetStatus(Status.status.lock_on) > 0) final_dmg *= 2;
        if (from_character.GetStatus(Status.status.lock_on) > 0) Debug.Log("lock on");

        Debug.Log("all status: ");
        foreach(var x in from_character.GetAllStatus()) Debug.Log("status: " + x._status.ToString() + ", level: " + x.level.ToString());

        int rock_solid = to_character.GetStatus(Status.status.rock_solid);
        final_dmg -= rock_solid;
        if (to_character.GetStatus(Status.status.hard_shell) > 0){
            if (final_dmg > 1) final_dmg = 1;
        }

        return final_dmg;
    }
    public static int ComputeDamage(GameObject from, float dmg){
        return ComputeDamage(from, dmg, 1, 1);
    }
    public static int ComputeDamage(GameObject from, float dmg, float strength_multiplier, float tmp_strength_multiplier){
        Character from_character = from.GetComponent<Character>();
        float multiplier = 1.0f;

        int strength = (int)(from_character.GetStatus(Status.status.strength) * strength_multiplier) + 
                       (int)(from_character.GetStatus(Status.status.temporary_strength) * tmp_strength_multiplier) +
                       from_character.GetStatus(Status.status.dice20);
        int additional = from_character.GetStatus(Status.status.explosive_force);
        if (from_character.GetStatus(Status.status.weak) > 0) multiplier -= 0.25f;
        multiplier += from_character.GetStatus(Status.status.damage_adjust) * 0.01f;

        int final_dmg = (int) ((dmg + strength + additional) * multiplier);

        if (from_character.GetStatus(Status.status.lock_on) > 0) final_dmg *= 2;

        return final_dmg;
    }
    public static int ComputeArmor(float value){
        Character player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        int dexterity = player.GetStatus(Status.status.dexterity) + player.GetStatus(Status.status.temporary_dexterity);
        value += dexterity;

        if (player.GetStatus(Status.status.frail) > 0) value *= 0.75f;

        return (int) value;
    }
}
