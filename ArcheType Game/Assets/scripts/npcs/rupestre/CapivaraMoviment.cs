using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapivaraMoviment : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Estado { Parado, Andando, Comendo, Fugindo };
    public Estado estadoAtual;

    [Header("tempo")]
    public float time = 0;
    public float distanciaFuga = 10f;
    public float tempoMinParado = 2f;
    public float tempoMaxParado = 5f;
    public float tempoMinAndando = 3f;
    public float tempoMaxAndando = 7f;
    public float tempoComendo = 4f;
    public float speedWalk = 2f;
    public float speedRun = 5f;
    [Header("estados")]

    [Header("movimento")]
    public int directionMov = 1;
    private SpriteRenderer sprite;
    public Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GetComponent<SpriteRenderer>();
        estadoAtual = Estado.Parado;
        sprite.flipX = directionMov == 1;


    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        float distPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distPlayer < distanciaFuga)
        {
            estadoAtual = Estado.Fugindo;
            directionMov = 1;
            sprite.flipX = directionMov == 1;
        }
        else
        {//continue o comportamento normal
            if (time <= 0)
            {
                //mudar estado
                MudarEstado();
            }
        }

        if (estadoAtual == Estado.Fugindo)
        {

            transform.Translate(Vector3.right * directionMov * Time.deltaTime * speedRun);
            if (distPlayer > distanciaFuga * 1.5f)
                estadoAtual = Estado.Andando;
        }
        else if (estadoAtual == Estado.Andando)
        {
            transform.Translate(Vector3.right * directionMov * Time.deltaTime * speedWalk);
        }
    }
    void MudarEstado()
    {
        switch (estadoAtual)
        {
            case Estado.Parado:
                estadoAtual = Estado.Andando;
                time = Random.Range(tempoMinAndando, tempoMaxAndando);
                directionMov = (Random.value > 0.5f) ? -1 : 1;
                sprite.flipX = directionMov == 1;
                break;
            case Estado.Andando:
                if (Random.value > 0.5f)
                {
                    estadoAtual = Estado.Parado;
                    time = Random.Range(tempoMinParado, tempoMaxParado);
                }
                else
                {
                    estadoAtual = Estado.Comendo;
                    time = tempoComendo;
                }
                break;
            case Estado.Comendo:
                if (Random.value > 0.5f)
                {
                    estadoAtual = Estado.Parado;
                    time = Random.Range(tempoMinParado, tempoMaxParado);
                }
                else
                {
                    estadoAtual = Estado.Andando;
                    directionMov = (Random.value > 0.5f) ? -1 : 1;
                    sprite.flipX = directionMov == 1;
                    
                    time = Random.Range(tempoMinAndando, tempoMaxAndando);
                }
                break;

        }
    }
}


