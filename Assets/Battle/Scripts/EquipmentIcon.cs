using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentIcon : MonoBehaviour
{
    [SerializeField] GameObject innerCircle;
    [SerializeField] Image image;
    [SerializeField] Slider slider;
    [SerializeField] TMPro.TextMeshProUGUI uses;
    EquipmentClass equipment;
    float charge;
    int maxCharge;
    int cost;
    public void MouseIn(){
        innerCircle.GetComponent<Image>().color = new Color(255, 255, 0, 255);
    }
    public void MouseOut(){
        innerCircle.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }
    public void MouseClick(){
        StartCoroutine(SmoothUpdate(0.3f, 1));
    }
    public void SetEquipment(EquipmentClass _equipment){
        equipment = _equipment;
        charge = equipment.initialCharge;
        maxCharge = equipment.maxCharge;
        cost = equipment.cost;
        UpdateIcon();
    }
    public void UpdateIcon(){
        image.sprite = equipment.image;
        slider.value = (charge % cost) / cost;
        uses.text = ((int)charge / cost).ToString();
    }
    IEnumerator SmoothUpdate(float speed, int chargeDelta){
        float final = charge + chargeDelta;
        while(final - charge > 0.01){
            charge = Mathf.Lerp(charge, final, speed);
            UpdateIcon();
            yield return new WaitForSeconds(0.016f);
        }
        charge = final;
        UpdateIcon();
    }
}
