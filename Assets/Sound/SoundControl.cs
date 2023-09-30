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
        float init = 0.5f;
        bgm.value = PlayerPrefs.GetFloat("BgmVolume", init);
        effectSound.value = PlayerPrefs.GetFloat("EffectSoundVolume", init);;
        audioMixer.SetFloat("BgmVolume",ChangeTodB(bgm.value));
        audioMixer.SetFloat("EffectSoundVolume",ChangeTodB(effectSound.value));
    }
    public void SetMasterVolume(float volume)
    {
        float init = ChangeTodB(volume);
        audioMixer.SetFloat("MasterVolume",init);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetBgmVolume(float volume)
    {
        float init = ChangeTodB(volume);
        audioMixer.SetFloat("BgmVolume",init);
        PlayerPrefs.SetFloat("BgmVolume", volume);
    }
    public void SetEffectSoundVolume(float volume)
    {
        float init = ChangeTodB(volume);
        audioMixer.SetFloat("EffectSoundVolume",init);
        PlayerPrefs.SetFloat("EffectSoundVolume", volume);
    }
    float ChangeTodB(float value)
    {
        if(value<=0)value = 0.0001f;
        return 20*Mathf.Log10(value);
    }
    float ChangeToFloat(float value)
    {
        return Mathf.Pow(10, value / 20);
    }
}
