using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Events")]
public class Event : ScriptableObject
{
    public int id;
    public string eventName;
    public enum Type{
        normal,
        story
    }
    public Type type;
    public Sprite image;
    public string description;
    public List<string> optionText;
}
