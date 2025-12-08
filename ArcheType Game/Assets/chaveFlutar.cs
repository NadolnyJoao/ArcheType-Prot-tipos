using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaveFlutar : MonoBehaviour
{
  public float amplitude = 0.5f;   // intensidade do sobe-e-desce
    public float speed = 2f;         // velocidade da flutuação

    private float startY;            // posição inicial no eixo Y

    void Start()
    {
        startY = transform.position.y; // guarda a posição inicial
    }

    void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * speed) * amplitude;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
