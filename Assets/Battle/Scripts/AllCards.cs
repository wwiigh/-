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

    static public int GetCount(){
        return FindObjectOfType<AllCards>().allCards.Count;
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

    static public Card GetCard(int id){
        AllCards allCardsClass = FindObjectOfType<AllCards>();
        if (id >= 900) return allCardsClass.testOnlyCards[id - allCardsClass.testOnlyCards[0].id];
        if (id > 200 && id <= allCardsClass.specialCards.Count + 200) return allCardsClass.specialCards[id - 201];
        if (id >= 101 && id <= 103) return allCardsClass.basicCards[id - 101];
        if (id > 0 && id <= allCardsClass.allCards.Count) return allCardsClass.allCards[id - 1];
        return null;
    }

    static public Card GetRandomSkillCard(){
        AllCards allCardsClass = FindObjectOfType<AllCards>();
        if (allCardsClass.allSkillCards.Count == 0){
            foreach(Card c in allCardsClass.allCards){
                if (c.type == Card.Type.skill) allCardsClass.allSkillCards.Add(c);
            }
        }

        return allCardsClass.allSkillCards[Random.Range(0, allCardsClass.allSkillCards.Count)];
    }

    static public Card GetRandomAttackCard(){
        AllCards allCardsClass = FindObjectOfType<AllCards>();
        if (allCardsClass.allAttackCards.Count == 0){
            foreach(Card c in allCardsClass.allCards){
                if (c.type == Card.Type.attack) allCardsClass.allAttackCards.Add(c);
            }
        }

        return allCardsClass.allAttackCards[Random.Range(0, allCardsClass.allAttackCards.Count)];
    }
}
