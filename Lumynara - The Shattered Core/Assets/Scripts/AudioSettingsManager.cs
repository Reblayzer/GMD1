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
        Color32 gray = new Color32(142, 142, 142, 255);
        Color32 aqua = new Color32(30, 182, 123, 255);
        Color32 darkAqua = new Color32(11, 100, 65, 255);

        HandleSelectable handleSelectable = handleImage.GetComponent<HandleSelectable>();

        if (isMuted)
        {
            audioMixer.SetFloat("MusicVolume", -80f);
            volumeSlider.interactable = false;

            SetColor(fillImage, darkAqua);

            if (handleSelectable != null)
            {
                handleSelectable.interactable = false;
            }
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volumeSlider.value) * 20);
            volumeSlider.interactable = true;

            SetColor(fillImage, aqua);

            if (handleSelectable != null)
            {
                handleSelectable.interactable = true;
            }
        }
    }


    private void SetColor(Image image, Color32 color)
    {
        if (image != null)
        {
            image.color = color;
        }
    }
}
