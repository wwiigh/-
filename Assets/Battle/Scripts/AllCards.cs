using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCards : MonoBehaviour
{
    [SerializeField] List<Card> allCards;

    public int GetCount(){
        return allCards.Count;
    }

    public List<Card> GetAllCards(){
        return allCards;
    }
}
