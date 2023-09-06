using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundControl : MonoBehaviour
{

    public Slider bgm;
    public Slider effectSound;
    public AudioMixer audioMixer;
    void Start()
    {
        bgm.value = PlayerPrefs.GetFloat("BgmVolume", -20);
        effectSound.value = PlayerPrefs.GetFloat("EffectSoundVolume", -20);;
    }
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume",volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetBgmVolume(float volume)
    {
        audioMixer.SetFloat("BgmVolume",volume);
        PlayerPrefs.SetFloat("BgmVolume", volume);
    }
    public void SetEffectSoundVolume(float volume)
    {
        audioMixer.SetFloat("EffectSoundVolume",volume);
        PlayerPrefs.SetFloat("EffectSoundVolume", volume);
    }
}
