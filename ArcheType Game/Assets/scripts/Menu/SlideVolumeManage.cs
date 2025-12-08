using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;


public class SlideVolumeManage : MonoBehaviour
{

    public Bus busSlide;
    public string nameBus = "bus:/";
    public Slider volumeSlider;
    private const string VolumePrefKey = "VolumeLevel";
    private string namepred;


    void Start()
    {
        volumeSlider = GetComponent<Slider>();
        busSlide = RuntimeManager.GetBus(nameBus);

        Debug.Log(nameBus.Split("bus:/")[0]+","+nameBus.Split("bus:/")[1]);
        namepred = nameBus.Split("bus:/")[1];

        float savedVolume;
        // Carrega volume salvo (ou padrão 1.0f)
        if (PlayerPrefs.HasKey(VolumePrefKey+namepred))
        {
            savedVolume = PlayerPrefs.GetFloat(VolumePrefKey+namepred);
            volumeSlider.value = savedVolume;
            SetVolume(savedVolume);
            Debug.Log("Volume carregado: " + savedVolume);
        }
        else
        {
            volumeSlider.value = 1.0f; // Valor padrão
        }
        // savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1.0f);
        // volumeSlider.value = savedVolume;
        // SetVolume(savedVolume);

        // Liga o evento de alteração no slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        busSlide.setVolume(volume);
        PlayerPrefs.SetFloat(VolumePrefKey+namepred, volume);
        PlayerPrefs.Save(); // Salva o volume atual
        Debug.Log("Volume ajustado para: " + volume);
    }

  
    // Update is called once per frame
    void Update()
    {

    }
}
