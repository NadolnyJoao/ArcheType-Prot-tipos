using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cobra : MonoBehaviour
{
     public Transform player;
    public float speed = 3f;
    public float stopDistance = 1f; // Distância mínima para parar
    public float detectionRange = 5f; // Distância para começar a perseguir

    public Animator ani; 
    public SpriteRenderer spriteRenderer;
    private Vector2 randomTarget; // Ponto aleatório de patrulha
    private float changeTargetTime; // Quando trocar de alvo aleatório

    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        PickRandomTarget();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange) 
        {
            // --- MODO PERSEGUIÇÃO ---
            FollowPlayer(distanceToPlayer);
        }
        else
        {
            // --- MODO PATRULHA ---
            Patrol();
        }
    }

    void FollowPlayer(float distance)
    {
        if (distance > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Vira sprite dependendo da posição do player
            if(player.position.x < transform.position.x){
                 spriteRenderer.flipX = false;
            } else{
                spriteRenderer.flipX = true;
            }   
           
        }
    }

    void Patrol()
    {
        // Se já chegou perto do alvo ou passou do tempo, escolhe outro ponto
        if (Vector2.Distance(transform.position, randomTarget) < 0.5f || Time.time > changeTargetTime)
        {
            PickRandomTarget();
        }

        // Anda até o ponto aleatório
        transform.position = Vector2.MoveTowards(transform.position,randomTarget, (speed * 0.5f) * Time.deltaTime); // mais devagar que perseguição


        // Ajusta flip conforme direção
        spriteRenderer.flipX = randomTarget.x < transform.position.x;
    }

    void PickRandomTarget()
    {
        // Pega um ponto aleatório próximo (raio de 5 unidades)
        Vector2 randomCircle = Random.insideUnitCircle * 5f;
        randomTarget = (Vector2)transform.position + randomCircle;

        // Muda de alvo a cada 3 a 6 segundos
        changeTargetTime = Time.time + Random.Range(3f, 6f);
    }
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "ground"){
            ani.SetBool("inground", true); 
        }
    }

}