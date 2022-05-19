using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    public static float MusicVolume { get; set; }
    public static float EffectVolume { get; set; }
    public Slider MusicSlider;
    public Slider EffectSlider;
    void OnEnable()
    {
        MusicSlider.value = MusicVolume;
        EffectSlider.value = EffectVolume;
    }
    public void EffectValueChange()
    {
        EffectVolume = EffectSlider.value;
    }
    public void MusicValueChange()
    {
        MusicVolume = MusicSlider.value;
    }
}
