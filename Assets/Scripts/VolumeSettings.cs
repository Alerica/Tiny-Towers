using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSettings : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;

    public void SetMusicVolume()
    {
        float volume = audioSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }
}
