using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGround : MonoBehaviour
{
    [Header("Tempos de execução")]
    public float timeToFall = 1.0f;
    public float timeToDestroy = 2.0f;
    public float timeToBack = 3.0f;

    private Rigidbody2D rg;
    public GameObject groundPrefab; 

    private Quaternion startRotation;
    private Vector2 startPosition;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();

        // Se não achar Rigidbody2D, evita erro
        if (rg == null)
        {
            Debug.LogError("Sem RigidBody nesta bomba xd");
            return;
        }

        rg.isKinematic = true; 
        startPosition = transform.position;
        startRotation = transform.rotation;
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
        rg.isKinematic = false;
        Invoke(nameof(VoltarChao), timeToBack); // só instancia após cair
        Destroy(this.gameObject, timeToDestroy); 
    }

    void VoltarChao()
    {
        Instantiate(groundPrefab, startPosition, startRotation);
    }
}