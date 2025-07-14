using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public UnityEvent actions;
    public UnityEvent onTrigger;
    public UnityEvent exitTrigger;
    public bool playerContact = false;
    public GameObject Bip_sound;

    private void PlaySound()
    {

            if (Bip_sound != null)
                Bip_sound.SetActive(true);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerContact = true;
            onTrigger.Invoke();

            //passar actions para o player
            PlayerMoviment playerMoviment = other.GetComponent<PlayerMoviment>();
            if (playerMoviment != null && playerContact)
            {
                playerMoviment.setActionInterableContact(actions);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerContact = false;
            exitTrigger.Invoke();
            PlayerMoviment playerMoviment = other.GetComponent<PlayerMoviment>();
            if (playerMoviment != null && playerContact == false)
            {
                playerMoviment.setActionInterableContact(null);
            }
        }
    }
}