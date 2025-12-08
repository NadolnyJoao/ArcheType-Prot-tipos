using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class AudioEventFMOD : MonoBehaviour
{
    // Start is called before the first frame update
    public EventReference soundEvent;

    public void PlayAudio()
    {
        RuntimeManager.PlayOneShot(soundEvent);
    }
}
