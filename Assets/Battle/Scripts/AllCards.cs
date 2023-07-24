using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCards : MonoBehaviour
{
    [SerializeField] List<Card> allCards;
    [SerializeField] List<Card> basicCards;
    [SerializeField] List<Card> specialCards;
    [SerializeField] List<Card> testOnlyCards;
    List<Card> allSkillCards = new List<Card>();
    List<Card> allAttackCards = new List<Card>();

    public int GetCount(){
        return allCards.Count;
    }

    public List<Card> GetAllCards(){
        return allCards;
    }
    public List<Card> GetBasicCards(){
        return basicCards;
    }
    public List<Card> GetSpecialCards(){
        return specialCards;
    }
    public List<Card> GetTestOnlyCards(){
        return testOnlyCards;
    }

    public Card GetCard(int id){
        if (id == 999) return testOnlyCards[0];
        if (id > 200 && id <= specialCards.Count + 200) return specialCards[id - 201];
        if (id >= 101 && id <= 103) return basicCards[id - 101];
        if (id > 0 && id <= allCards.Count) return allCards[id - 1];
        return null;
    }

    public Card GetRandomSkillCard(){
        if (allSkillCards.Count == 0){
            foreach(Card c in allCards){
                if (c.type == Card.Type.skill) allSkillCards.Add(c);
            }
        }

        return allSkillCards[Random.Range(0, allSkillCards.Count)];
    }

    public Card GetRandomAttackCard(){
        if (allAttackCards.Count == 0){
            foreach(Card c in allCards){
                if (c.type == Card.Type.attack) allAttackCards.Add(c);
            }
        }

        return allAttackCards[Random.Range(0, allAttackCards.Count)];
    }
}
