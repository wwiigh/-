using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPageControl : MonoBehaviour
{
    public TMP_Text text; 
    public void ChangeToPage2()
    {
        text.pageToDisplay = 2;
        if(text.text.Length<=32)
           text.pageToDisplay = 1;
    }
    public void ChangeToPage1()
    {
        text.pageToDisplay = 1;
    }
}
