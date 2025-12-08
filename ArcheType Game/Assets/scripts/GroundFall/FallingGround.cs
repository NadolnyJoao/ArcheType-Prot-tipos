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

        if (rg == null)
        {
            Debug.LogError("Sem Rigidbody2D no objeto!");
            return;
        }

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
        // Permite que o chão caia
        rg.bodyType = RigidbodyType2D.Dynamic;
        rg.gravityScale = 1;

        // Recria depois
        Invoke(nameof(VoltarChao), timeToBack);

        // Destroi o chão original após o tempo
        Destroy(gameObject, timeToDestroy);
    }

    void VoltarChao()
    {
        GameObject novoChao = Instantiate(groundPrefab, startPosition, startRotation);

        // Garante que o novo chão fique parado
        Rigidbody2D newRg = novoChao.GetComponent<Rigidbody2D>();

        if (newRg != null)
        {
            newRg.bodyType = RigidbodyType2D.Kinematic;
            newRg.gravityScale = 0;
            newRg.velocity = Vector2.zero;
            newRg.angularVelocity = 0;
        }
    }
}