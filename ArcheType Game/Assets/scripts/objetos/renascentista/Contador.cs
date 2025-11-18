using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;
using FMOD.Studio;

public class Contador : MonoBehaviour
{
    public int numColepet = 1;
    public int numclick = 0;
    public UnityEvent eventComplete;
    private bool completed = false;
    public EventReference somCarvao;
    // Start is called before the first frame update
    public void Click()
    {
        numclick++;
        RuntimeManager.PlayOneShot(somCarvao, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(numclick == numColepet && completed == false){
            eventComplete.Invoke();
            completed = true;
        }
    }
}
