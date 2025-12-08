using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class TakeKey : MonoBehaviour
{
    public bool chavesurreal = false;
    public GameObject chave;
    public BoxCollider2D col;
    public EventReference takeKey;
    public EventReference doorOpen;

    // Start is called before the first frame update
    void Start()
    {
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Takekey()
    {
        Debug.Log("chaveeeeee");
        chave.SetActive(false);
        chavesurreal = true;
        if (chavesurreal)
        {
            col.enabled = true;
            RuntimeManager.PlayOneShot(takeKey, transform.position);
        }
    }
    public void OpenDoorSound()
    {
        RuntimeManager.PlayOneShot(doorOpen, transform.position);
    }
 

}
