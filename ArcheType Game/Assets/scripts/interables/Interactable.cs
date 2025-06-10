using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent actions;
    public UnityEvent onTrigger;
    public UnityEvent exitTrigger;
    public bool playerContact = false;
    public GameObject Bip_sound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && playerContact)
        {
            actions.Invoke();
            Bip_sound.SetActive(true);
        }
        if (Bip_sound == true)
        {
            Bip_sound.SetActive(false);
            Debug.Log("sikidiu");
            } 

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerContact = true;
            onTrigger.Invoke();
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerContact = false;
            exitTrigger.Invoke();
        }
    }
}
