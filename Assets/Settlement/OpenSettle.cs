using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettle : MonoBehaviour
{
    public GameObject Settle;
    public void open(){
        Settle.SetActive(true);
    }
}
