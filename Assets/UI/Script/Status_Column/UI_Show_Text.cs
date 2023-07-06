using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New UI_Description", menuName = "UI_Description")]
public class UI_Show_Text : ScriptableObject
{
    [System.Serializable]
    public struct Information_Text
    {
        public Image box;
        [TextArea]
        public string information;
        public int line_count;
    }
    [Header("填入說明文字")]
    public Information_Text[] texts;
    [Header("稀有度 普通(1) < 罕見(2) < 稀有(3) 無稀有度(0)")]
    public int rarity;
}
