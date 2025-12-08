using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class StopPlayer : MonoBehaviour
{
    public PlayerMoviment playerMov;
    public GameObject objtst;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(objtst.activeSelf)
        {
            playerMov.enabled = false;
        }
        else
        {
            playerMov.enabled = true;
        }
    }
}
