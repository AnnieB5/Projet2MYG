using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musiqueSlider;
    [SerializeField] Slider bruitagesSlider;

    const string MIXER_MUSIQUE = "MusiqueVolume";
    const string MIXER_BRUITAGES = "BruitagesVolume";

    void Awake()
    {
        musiqueSlider.onValueChanged.AddListener(SetMusicVolume);
        bruitagesSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(MIXER_MUSIQUE, Mathf.Log10(value) * 20);
    }

    void SetSFXVolume(float value)
    {
        audioMixer.SetFloat(MIXER_BRUITAGES, Mathf.Log10(value) * 20);
    }
}
