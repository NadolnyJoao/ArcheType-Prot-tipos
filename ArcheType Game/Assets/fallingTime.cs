using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingTime : MonoBehaviour
{
    public float timeToFall = 2f;      // Tempo até ativar o Rigidbody
    public float timeOnGround = 2f;    // Tempo antes de subir novamente
    public float riseSpeed = 3f;       // Velocidade da subida

    private Rigidbody2D rb;
    private Vector3 initialPos;
    private bool isRising = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPos = transform.position;

        rb.isKinematic = true; // Começa parado
        StartCoroutine(Cycle());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

     IEnumerator Cycle()
    {
        while (true)
        {
            // 1. Espera para cair
            yield return new WaitForSeconds(timeToFall);

            // Ativa física para o objeto cair
            rb.isKinematic = false;

            // 2. Espera no chão
            yield return new WaitForSeconds(timeOnGround);

            // Desativa física para subir
            rb.isKinematic = true;

            // 3. Começar a subir
            isRising = true;

            // Espera até terminar de subir
            while (Vector3.Distance(transform.position, initialPos) > 0.01f)
            {
                transform.position = Vector3.Lerp(
                    transform.position, 
                    initialPos, 
                    Time.deltaTime * riseSpeed
                );
                yield return null;
            }

            // Garante que volta exatamente pro ponto inicial
            transform.position = initialPos;

            isRising = false;
        }
    }
}
