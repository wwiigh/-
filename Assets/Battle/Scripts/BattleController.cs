using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] GameObject deck_obj;
    [SerializeField] GameObject characters;
    [SerializeField] GameObject character_template;
    [SerializeField] GameObject panel;
    public GameObject descriptionBox;
    [SerializeField] EnemyClass[] enemyClass;
    [SerializeField] EquipmentClass[] equipmentClass;
    GameObject player;
    GameObject caller_saved;
    Deck deck;
    public enum BattleType{
        Normal,
        Elite,
        Boss
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


    private void Start() {
        deck = deck_obj.GetComponent<Deck>();
    }


    public void EnterBattle(){
        for (int i = characters.transform.childCount - 1; i >= 0; i--){
            Destroy(characters.transform.GetChild(i).gameObject);
        }
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
            case 111:
                enemyID = new int[]{5};
                break;
            case 112:
                enemyID = new int[]{6};
                break;
            case 121:
                enemyID = new int[]{7};
                break;
            default:
                Debug.Log("Enter battle: Unknown id " + id.ToString());
                return;
        }
        SpawnEnemies(enemyID);
        Cost.Init();
        deck.Init();
        currentTurn = 0;
        StartCoroutine(_StartTurn());
    }



    public bool PlayedAttackAndSkillThisTurn(){
        return playedAttackThisTurn && playedSkillThisTurn;
    }
    public bool PlayedCardThisTurn(){
        return playedCardThisTurn;
    }
    public int GetCurrentTurn(){
        return currentTurn;
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



    public void PlayedAttack(){
        playedAttackThisTurn = true;
    }
    public void PlayedSkill(){
        playedSkillThisTurn = true;
    }
    public void PlayedCard(Card card){
        playedCardThisTurn = true;
        lastCardPlayed = card;
        if (card.id == 46) played46ThisTurn = true;
    }
    public void Discarded(){
        discardedThisTurn = true;
    }



    public void EndTurn(){
        StartCoroutine(_EndTurn());
    }

    IEnumerator _EndTurn(){
        deck.TurnEnd();
        characters.transform.GetChild(0).GetComponent<Character>().TurnEnd();
        // yield return new WaitForSeconds(0.1f);
        foreach(Transform child in characters.transform){
            if (child.tag == "Enemy"){
                child.GetComponent<Character>().TurnStart();
                child.GetComponent<EnemyMove>().Move();
            }
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(_StartTurn());
    }

    IEnumerator _StartTurn(){
        currentTurn += 1;
        playedAttackThisTurn = false;
        playedSkillThisTurn = false;
        playedCardThisTurn = false;
        discardedThisTurn = false;
        played46ThisTurn = false;
        foreach(Transform child in characters.transform){
            if (child.tag == "Enemy") child.GetComponent<EnemyMove>().SetIntention();
        }
        deck.Draw(5);
        Cost.Refill(0);
        player.GetComponent<Character>().TurnStart();
        yield return new WaitForSeconds(0);
    }



    public BattleState GetState(){
        return battleState;
    }
    void EnterState(BattleState state){
        Debug.Log("battle controller: enter state " + state.ToString());
        battleState = state;
    }



    public void SelectEnemy(GameObject caller){ // to be revised
        caller_saved = caller;
        EnterState(BattleState.SelectEnemy);
    }
    public void SelectEnemy(){ // to be revised
        EnterState(BattleState.SelectEnemy);
    }
    public void EnemySelected(GameObject enemy){
        EnterState(BattleState.Normal);
        CardEffects.EnemySelected(enemy);
        deck.Rearrange();
    }
    public GameObject GetRandomEnemy(){
        List<GameObject> pool = new List<GameObject>();
        foreach(Transform child in characters.transform) 
            if (child.tag == "Enemy") pool.Add(child.gameObject);
        return pool[Random.Range(0, pool.Count)];
    }
    public List<GameObject> GetAllEnemy(){
        List<GameObject> ret = new List<GameObject>();
        foreach(Transform child in characters.transform) 
            if (child.tag == "Enemy") ret.Add(child.gameObject);
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
    List<GameObject> selectedCards = new List<GameObject>();
    public void SelectCard(int count, bool _isEqual, bool _cancellable){
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
        selectedCards.Remove(card);
        panel.GetComponent<Panel>().UpdateAll(selectedCards.Count, targetCardNumber, isEqual, cancellable);
    }
    public void SelectCard_Confirm(){
        if (isEqual && selectedCards.Count == targetCardNumber){
            EnterState(BattleState.Normal);
            CardEffects.CardSelected(selectedCards);
            panel.GetComponent<Panel>().Hide();
        }
        if (!isEqual && selectedCards.Count <= targetCardNumber){
            EnterState(BattleState.Normal);
            CardEffects.CardSelected(selectedCards);
            panel.GetComponent<Panel>().Hide();
        }
    }
    public void SelectCard_Cancel(){
        if (!cancellable) return;
        if (battleState == BattleState.SelectCard){
            EnterState(BattleState.Normal);
            panel.GetComponent<Panel>().Hide();
            deck.ResetHand();
            deck.Rearrange();
        }
    }
    public bool SelectCard_Availible(){
        return selectedCards.Count < targetCardNumber;
    }



    public GameObject GetPlayer(){
        return player;
    }

    void InitPlayer(){
        // GameObject player = Instantiate(character, transform);
        player = Instantiate(character_template, characters.transform);
        player.transform.localPosition = new Vector3(-480, 0, 0);
        player.tag = "Player";
        player.GetComponent<Character>().InitPlayer();
    }

    void InitEnemy(int id, int idx){
        GameObject enemy = Instantiate(character_template, characters.transform);
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
        return ComputeDamage(from, to, dmg, 1, 1);
    }
    public static int ComputeDamage(GameObject from, GameObject to, float dmg, float strength_multiplier, float tmp_strength_multiplier){
        Character from_character = from.GetComponent<Character>();
        Character to_character = to.GetComponent<Character>();
        float multiplier = 1.0f;

        if (to_character.GetStatus(Status.status.invincible) > 0) return 0;

        int strength = (int)(from_character.GetStatus(Status.status.strength) * strength_multiplier) + 
                       (int)(from_character.GetStatus(Status.status.temporary_strength) * tmp_strength_multiplier);
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
    public static int ComputeDamage(GameObject from, float dmg){
        return ComputeDamage(from, dmg, 1, 1);
    }
    public static int ComputeDamage(GameObject from, float dmg, float strength_multiplier, float tmp_strength_multiplier){
        Character from_character = from.GetComponent<Character>();
        float multiplier = 1.0f;

        int strength = (int)(from_character.GetStatus(Status.status.strength) * strength_multiplier) + 
                       (int)(from_character.GetStatus(Status.status.temporary_strength) * tmp_strength_multiplier);
        if (from_character.GetStatus(Status.status.weak) > 0) multiplier -= 0.25f;
        multiplier += from_character.GetStatus(Status.status.damage_adjust) * 0.01f;

        int final_dmg = (int) ((dmg + strength) * multiplier);

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
