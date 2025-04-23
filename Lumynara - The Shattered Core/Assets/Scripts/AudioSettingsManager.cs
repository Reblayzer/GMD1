using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Toggle muteToggle;

    public Image fillImage;
    public Image handleImage;
    public Image checkmarkImage;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);
        muteToggle.onValueChanged.AddListener(ToggleMute);
    }

    public void SetVolume(float volume)
    {
        if (!muteToggle.isOn)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        }
    }

    public void ToggleMute(bool isMuted)
    {
        if (isMuted)
        {
            audioMixer.SetFloat("MusicVolume", -80f);
            volumeSlider.interactable = false;

            SetAlpha(fillImage, 0.4f);
            SetAlpha(handleImage, 0.4f);
            SetAlpha(checkmarkImage, 1f);
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volumeSlider.value) * 20);
            volumeSlider.interactable = true;

            SetAlpha(fillImage, 1f);
            SetAlpha(handleImage, 1f);
            SetAlpha(checkmarkImage, 0.4f);
        }
    }

    private void SetAlpha(Image img, float alpha)
    {
        if (img != null)
        {
            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }
    }
}
