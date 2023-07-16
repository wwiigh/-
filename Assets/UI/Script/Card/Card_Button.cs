using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Button : MonoBehaviour
{
    [SerializeField] GameObject card_list;
    public void On_Click()
    {
        card_list.SetActive(!card_list.activeSelf);
    }
}
