using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    public static int player_max_hp = 100;
    public static int player_hp = 100;
    public static int money = 0;
    public static int sanity = 100;

    public static void AddSan(int value){
        sanity += value;
    }
}
