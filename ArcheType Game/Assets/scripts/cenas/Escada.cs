using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escada : MonoBehaviour
{
    public Transform[] portas;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
        Player = GameObject.FindGameObjectWithTag("Player");
    }   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoPorta(int index )
    {
        if (index < 0 || index >= portas.Length)
        {
            Debug.LogError("Index out of range for portas array.");
            return;
        }

        Player.transform.position = portas[index].position;
        Player.transform.rotation = portas[index].rotation;

        // Optional: If you want to reset the player's velocity or other properties, you can do that here.
        Rigidbody playerRigidbody = Player.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
