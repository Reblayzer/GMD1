using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioMixer))]
public class AudioSettingsManager : MonoBehaviour
{
    const string kVolumeKey = "MusicVolume";      // stores the slider value 0–1
    const string kMutedKey = "MusicMuted";       // stores 0 or 1

    [Header("Wiring")]
    public AudioMixer audioMixer;  // assign your MusicMixer here
    public Slider volumeSlider; // your SliderLockable
    public Toggle muteToggle;   // your ToggleSelectable

    void Start()
    {
        // 1) register for callbacks
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        muteToggle.onValueChanged.AddListener(OnMuteChanged);

        // 2) load & apply saved settings
        float savedVol = PlayerPrefs.GetFloat(kVolumeKey, 1f);
        bool savedMuted = PlayerPrefs.GetInt(kMutedKey, 0) == 1;

        // order is important: set mute first so slider callback doesn't fight it
        muteToggle.isOn = savedMuted;
        volumeSlider.value = savedVol;
    }

    private void OnVolumeChanged(float v)
    {
        // only actually change mixer if not muted:
        if (!muteToggle.isOn)
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp01(v)) * 20f);

        // persist
        PlayerPrefs.SetFloat(kVolumeKey, v);
        PlayerPrefs.Save();
    }

    private void OnMuteChanged(bool isMuted)
    {
        if (isMuted)
            audioMixer.SetFloat("MusicVolume", -80f);
        else
            // un‐mute back to whatever slider says
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp01(volumeSlider.value)) * 20f);

        // persist
        PlayerPrefs.SetInt(kMutedKey, isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
}
