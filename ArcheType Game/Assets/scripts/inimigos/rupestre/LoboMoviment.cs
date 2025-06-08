using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoboMoviment : MonoBehaviour
{
    public enum Estado { Parado, Andando, Patrulha, Atacando };
    public Estado estadoAtual;
    [Header("tempo")]
    public float time = 0;
    public float tempoPatrulha = 10f;
    public float tempoMinParado = 2f;
    public float tempoMaxParado = 5f;
    public float tempoMinAndando = 3f;
    public float tempoMaxAndando = 7f;
    public float speedWalk = 2f;
    public float speedRun = 5f;

    [Header("Caçar")]
    public float distanciaDetecao = 10;
    public Transform presaTrans;

    [Header("alcateia")]
    public static List<LoboMoviment> todosLobos = new List<LoboMoviment>();

    public List<LoboMoviment> friends = new List<LoboMoviment>();
    public float alcanceComunicacao = 20.0f;
    public LoboMoviment lider;


    [Header("movimento")]
    public int direction = 1;
    private SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        estadoAtual = Estado.Parado;
        sprite.flipX = direction == 1;

        todosLobos.Add(this);

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (presaTrans != null)
        {//caçar presa
            direction = presaTrans.transform.position.x > transform.position.x ? 1 : -1;
            sprite.flipX = direction == 1;
            transform.Translate(Vector3.right * direction * Time.deltaTime * speedRun);

        }
        else
        {
            if (time <= 0)
            {
                MudarEstado();
            }
        }

        if (estadoAtual == Estado.Andando || estadoAtual == Estado.Patrulha)
        {
            if (estadoAtual == Estado.Patrulha && lider != null)
            {//tenho um lider
                direction = lider.transform.position.x > transform.position.x ? 1 : -1;
                sprite.flipX = direction == 1;
            }
            transform.Translate(Vector3.right * direction * Time.deltaTime * speedWalk);
        }
    }
    void MudarEstado()
    {
        switch (estadoAtual)
        {
            case Estado.Parado:
                estadoAtual = Random.value < 0.5f ? Estado.Andando : Estado.Patrulha;
                time = Random.Range(tempoMinAndando, tempoMaxAndando);
                direction = Random.value > 0.5f ? -1 : 1;
                sprite.flipX = direction == 1;
                // criar grupo de patrulha
                if (estadoAtual == Estado.Patrulha)
                {
                    friends.Clear();
                    foreach (LoboMoviment lobo in todosLobos)
                    {
                        if (lobo != this && Vector3.Distance(transform.position, lobo.transform.position) <= alcanceComunicacao)
                        {
                            if ((lobo.estadoAtual == Estado.Parado || lobo.estadoAtual == Estado.Andando) && friends.Count < 2)
                            {
                                lobo.SetLider(this);
                                friends.Add(lobo);
                            }
                        }
                    }
                }
                break;

            case Estado.Andando:
                estadoAtual = Estado.Parado;
                time = Random.Range(tempoMinParado, tempoMaxParado);
                break;
            case Estado.Patrulha:
                if (lider == null)
                {//sou o lider 
                    estadoAtual = Random.value < 0.4f ? Estado.Parado : Estado.Patrulha;
                    if (estadoAtual == Estado.Parado)
                    {
                        estadoAtual = Estado.Parado;
                        time = Random.Range(tempoMinParado, tempoMaxParado);
                        foreach (LoboMoviment lobo in friends)
                        {
                            lobo.EndGrup();
                        }
                        friends.Clear();
                    }else{
                        direction = (Random.value > 0.5f) ? -1 : 1;
                sprite.flipX = direction == 1;
                    }
                }
                else
                {//sou acompanhante
                    if (Random.value < 0.4f)
                    {
                        EndGrup();
                    }
                }
                break;
            default:
                break;
        }
    }
    public void SetLider(LoboMoviment newLider)
    {
        lider = newLider;
        estadoAtual = Estado.Patrulha;
        time = tempoPatrulha;

    }
    public void EndGrup()
    {
        lider = null;
        estadoAtual = Estado.Andando;
        direction = (Random.value > 0.5f) ? -1 : 1;
        sprite.flipX = direction == 1;
    }
    public void setPresa(Transform newPresa)
    {
        presaTrans = newPresa;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + " is trigger");
        if (other.gameObject.GetComponent<LoboMoviment>() == null)
        {
            setPresa(other.gameObject.transform);
            estadoAtual = Estado.Atacando;
        }
    }
}
