using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Contador : MonoBehaviour
{
    public int numColepet = 1;
    public int numclick = 0;
    public UnityEvent eventComplete;
    private bool completed = false;
    // Start is called before the first frame update
    public void Click()
    {
        numclick++;
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
