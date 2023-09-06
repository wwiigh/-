using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    public void Add_Hp()
    {
        audioSource.Play();
        int hp = (int)(Global.player_max_hp * 0.3f);
        Global.AddHp(hp);
        gameObject.SetActive(false);
    }
    
    public void Add_San()
    {
        audioSource.Play();
        int san = (int)(Global.max_sanity * 0.2f);
        Global.AddSan(san);
        gameObject.SetActive(false);
    }
    public void Up_Card()
    {
        // Global.PlayerDeckInit();
        audioSource.Play();
        List<Card> cards = Global.GetPlayerDeck();
        List<Card> noUpgrade = new List<Card>();
        foreach(Card card in cards)
        {
            if(card.upgraded == false)noUpgrade.Add(card);
        }
        Global.ShowPlayerCards(noUpgrade,Up_Card_Implement,true);
    }
    public void Up_Card_Implement(Card card)
    {
        Global.UpgradeCard(card);
        gameObject.SetActive(false);
    }
}
