using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoFallingGorund : MonoBehaviour
{
    public float timeToFall = 1.0f;      // tempo até começar a cair
    public float timeToDestroy = 2.0f;   // tempo até destruir depois de cair

    private Rigidbody2D rg;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();

        if (rg == null)
        {
            Debug.LogError("Rigidbody2D não encontrado!");
            return;
        }

        // Começa parado
        rg.bodyType = RigidbodyType2D.Static;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(Cair), timeToFall);
        }
    }

    void Cair()
    {
        // Permite cair
        rg.bodyType = RigidbodyType2D.Dynamic;
        rg.gravityScale = 1;

        // Destrói depois de um tempo
        Destroy(gameObject, timeToDestroy);
    }
}
