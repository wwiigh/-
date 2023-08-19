using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hide : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject treasure;
    public GameObject relic;
    public GameObject card;
    public GameObject item;
    public GameObject equipment;
    // Update is called once per frame
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(continue_buttom);
    }
    public void continue_buttom(){
        relic.SetActive(true);
        card.SetActive(true);
        item.SetActive(true);
        equipment.SetActive(true);
        treasure.SetActive(false);
    }

}
