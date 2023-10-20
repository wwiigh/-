using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSound : MonoBehaviour
{
    public enum SoundType{
        bgm,
        enemyAttack,
        playerAttack
    }
    public AudioClip enemyAttack;
    public AudioClip playerAttack;
    public AudioClip bgm;
    public AudioSource sound;
    public void Play(SoundType type){
        if (!sound) sound = GetComponent<AudioSource>();
        switch(type){
            case SoundType.bgm:
                sound.clip = bgm;
                break;
            case SoundType.enemyAttack:
                sound.clip = enemyAttack;
                break;
            case SoundType.playerAttack:
                sound.clip = playerAttack;
                break;
            default:
                Debug.Log("BattleSound: Unknown sound type");
                break;
        }
        sound.Play();
    }
}
