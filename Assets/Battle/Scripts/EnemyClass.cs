using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies")]
public class EnemyClass : ScriptableObject
{
    public int id;
    public string mobName;
    public int hp;
    public Vector2 size;
    public Vector2 offset;
    public Sprite image;
    public enum EnemyType{
        Normal,
        Elite,
        Boss
    }
    public EnemyType type;
}
