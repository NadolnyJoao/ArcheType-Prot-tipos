using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiaMoviment : MonoBehaviour
{
    private Vector3 pos;
    public Animator anima;
    private SpriteRenderer sprite;

    [Header("Ir ate o quadro")]
    public float minDist;
    public float speed;

    [Header("Perto do player")]
    public Transform transfomrPlayer;

    // Start é chamado uma vez no início
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        // Define a posição inicial como o destino
        pos = transform.position;

        // Obtém o componente Animator
        anima = GetComponent<Animator>();
        if (anima == null)
        {
            Debug.LogError("Componente Animator não encontrado. Verifique se o objeto tem um Animator.");
        }
    }

    // Update é chamado a cada frame
    void Update()
    {
        // Variável de controle para a animação de movimento
        bool isMoving = false;

        // Se o jogador se afastar mais de 10 unidades,
        // o destino do guia se torna a posição do jogador
        if (transfomrPlayer != null)
        {
            float distOfPlayer = Vector3.Distance(transform.position, transfomrPlayer.position);
            if (distOfPlayer > 10)
            {
                pos = transfomrPlayer.position;
                if (transform.position.x > transfomrPlayer.position.x)
            {
                sprite.flipX = true;

            }
            else
            {
                sprite.flipX = false;
            }
            }
        }

        // Calcula a distância até o destino atual
        float distToTarget = Vector3.Distance(transform.position, pos);

        // Se a distância for maior que a distância mínima, o guia se move
        if (distToTarget > minDist)
        {
            // Calcula a direção para o destino
            Vector3 dir = pos - transform.position;
            dir.y = 0; // Zera o movimento no eixo Y para não flutuar
            dir = dir.normalized;

            // Move o guia
            transform.Translate(dir * speed * Time.deltaTime);

            // Indica que o guia está em movimento
            isMoving = true;

            // Se o guia estiver muito perto do destino, trava na posição exata para evitar tremores
            if (distToTarget < 0.1f)
            {
                transform.position = pos;
                isMoving = false; // O guia chegou, então para o movimento
            }


        }
            
        // Atualiza a animação com base no estado de movimento
        if (anima != null)
        {
            anima.SetBool("Speed", isMoving);
        }
    }

    // Método público para definir um novo destino para o guia
    public void SetPosition(Transform trans)
    {
        if (trans != null)
        {
            pos = trans.position;
            pos.y = transform.position.y; // Mantém a altura atual do guia
            Debug.Log("Novo destino definido para o guia.");
        }
    }
}