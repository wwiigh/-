using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettle : MonoBehaviour
{
    public GameObject Settle;
    public void open_settle(){
        Settle.SetActive(true);
    }
}
