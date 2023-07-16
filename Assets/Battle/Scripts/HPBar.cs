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
    [SerializeField] Slider armorBar;

    private void Start() {
        character = transform.parent.GetComponent<Character>();
        UpdateHP();
    }
    public void UpdateHP(){
        int hp = character.GetHP();
        int maxHP = character.GetMaxHP();
        int armor = character.GetArmor();
        int block = character.GetBlock();

        if (armor > 0) hpText.text = hp.ToString() + " / " + maxHP.ToString() + " (+" + armor.ToString() + ")";
        else hpText.text = hp.ToString() + " / " + maxHP.ToString();
        transform.GetComponent<Slider>().value = (float) hp / maxHP;
        armorBar.value = (float) armor / maxHP;
        if (armorBar.value > 1) armorBar.value = 1;
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
