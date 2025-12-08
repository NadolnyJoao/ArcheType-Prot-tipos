using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class volumeSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var masterBus = FMODUnity.RuntimeManager.GetBus("bus:/MasterBus");
    float volume = PlayerPrefs.GetFloat("VolumeLevel", 1.0f);
    masterBus.setVolume(volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
