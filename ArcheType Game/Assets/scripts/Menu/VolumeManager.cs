using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class VolumeManager : MonoBehaviour
{
public Slider volumeSlider;
    private Bus masterBus;
    private const string VolumePrefKey = "VolumeLevel";

    void Start()
    {
        masterBus = RuntimeManager.GetBus("bus:/MasterBus");

        // Carrega volume salvo (ou padrão 1.0f)
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1.0f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // Liga o evento de alteração no slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        masterBus.setVolume(volume);
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
    }
    void OnDisable()
{
    PlayerPrefs.SetFloat("volume", volumeSlider.value);
    PlayerPrefs.Save(); // força o salvamento
}
}