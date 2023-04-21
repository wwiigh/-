using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies")]
public class EnemyClass : ScriptableObject
{
    public int id;
    public string mobName;
    public int hp;
    public int goldDrop;
    public float sizeX;
    public float sizeY;
    public Sprite image;
}
