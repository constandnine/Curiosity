using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("AudioMixer")]
    [SerializeField] private AudioMixer mixer;

    [Header("Slider")]
    [SerializeField] private Slider slider;

    [Header("Volume Key")]
    [Tooltip("Enter the name of the type of volume you are changing with this manager.")]
    [SerializeField] private string volumeKey;

    private void Awake()
    {
        LoadVolume();
    }

    public void MasterVolume()
    {
        float volume = Mathf.Clamp(slider.value, 0.0001f, 1f);
        float volumeInDecibels = Mathf.Log10(volume) * 20;

        mixer.SetFloat("MasterVolume", volumeInDecibels);

        Debug.Log("MasterVolume" + volumeInDecibels, this);
    }

    public void SFXVolume()
    {
        float volume = Mathf.Clamp(slider.value, .0001f, 1f);
        float volumeInDecibels = Mathf.Log10(volume) * 20;

        mixer.SetFloat("SFXVolume", volumeInDecibels);

        Debug.Log("SFXVolume" + volumeInDecibels, this);
    }

    public void MusicVolume()
    {
        float volume = Mathf.Clamp(slider.value, .0001f, 1f);
        float volumeInDecibels = Mathf.Log10(volume) * 20;

        mixer.SetFloat("MusicVolume", volumeInDecibels);

        Debug.Log("MusicVolume" + volumeInDecibels, this);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(volumeKey, slider.value);
        PlayerPrefs.Save();

        Debug.Log($"Saved volume with volume key of {volumeKey}", this);
    }

    private void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(volumeKey, 1f);
        slider.value = savedVolume;

        float volume = Mathf.Clamp(slider.value, .0001f, 1f);
        float volumeInDecibels = Mathf.Log10(volume) * 20;

        mixer.SetFloat("MusicVolume", volumeInDecibels);
    }
}
