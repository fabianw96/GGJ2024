using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SettingsManeger : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public void SetVolume(float volume)
    {
       // AudioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        AudioMixer.SetFloat("Volume", volume);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
