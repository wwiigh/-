using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Altar : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] TMP_Text ShowText;
    [SerializeField] GameObject ShowPanel;
    // Start is called before the first frame update
    public void Add_Hp()
    {
        audioSource.Play();
        int hp = (int)(Global.player_max_hp * 0.3f);
        Global.AddHp(hp);
        StartCoroutine(ShowInformation("血量恢復了" + hp));
        // gameObject.SetActive(false);
    }
    
    public void Add_San()
    {
        audioSource.Play();
        int san = (int)(Global.max_sanity * 0.2f);
        Global.AddSan(san);
        StartCoroutine(ShowInformation("理智恢復了" + san));
        // gameObject.SetActive(false);
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
        // gameObject.SetActive(false);;
        StartCoroutine(ShowInformation("升級了" + card.cardName));
    }
    IEnumerator ShowInformation(string text)
    {
        ShowPanel.SetActive(true);
        ShowText.text = text;
        yield return new WaitForSeconds(2);
        ShowPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
