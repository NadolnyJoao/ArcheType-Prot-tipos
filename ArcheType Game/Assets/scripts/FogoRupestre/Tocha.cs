using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


public class Tocha : MonoBehaviour
{
    public EventReference torchSound;
    public EventReference burningSound;
    private EventInstance somInstance;
    public bool teste = false; 

    void Start()
    {
        
    if (torchSound.IsNull)
        {
            Debug.LogError("Evento não atribuído!");
            return;
        }

    somInstance = RuntimeManager.CreateInstance(torchSound);

    var result = somInstance.start();
    Debug.Log("Resultado do start: " + result);

    PLAYBACK_STATE state;
    somInstance.getPlaybackState(out state);
    Debug.Log("Estado do som após start: " + state); 
    }

    void OnEnable()
    {
        somInstance.start();
        Debug.Log("pq meu deus?");
        teste = true; 
    }

    void OnDisable()
    {
        somInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        RuntimeManager.PlayOneShot(burningSound, transform.position);
    }

}
