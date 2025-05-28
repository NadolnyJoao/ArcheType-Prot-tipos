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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && playerContact)
        {
            actions.Invoke();
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
