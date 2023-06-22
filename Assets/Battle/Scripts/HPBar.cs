using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Character character;
    [SerializeField] Text hpText;
    [SerializeField] Image shieldImg;
    [SerializeField] Text blockText;

    private void Start() {
        character = transform.parent.GetComponent<Character>();
        UpdateHP();
    }
    public void UpdateHP(){
        int hp = character.GetHP();
        int maxHP = character.GetMaxHP();
        int armor = character.GetArmor();
        int block = character.GetBlock();

        hpText.text = hp.ToString() + " / " + maxHP.ToString();
        transform.GetComponent<Slider>().value = (float) hp / maxHP;
        if (block > 0){
            blockText.text = block.ToString();
            foreach (Transform child in transform){
                if (child.gameObject.name == "shield" || child.gameObject.name == "blockText") child.gameObject.SetActive(true);
            }
        }else{
            foreach (Transform child in transform){
                if (child.gameObject.name == "shield" || child.gameObject.name == "blockText") child.gameObject.SetActive(false);
            }
        }
    }
}
