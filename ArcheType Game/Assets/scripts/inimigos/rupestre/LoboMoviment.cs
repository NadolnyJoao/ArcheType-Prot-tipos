using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

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
    public float speedRun = 10f;
    public EventReference latido;
    

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
    private Rigidbody2D rig;
     public Animator anima;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        estadoAtual = Estado.Parado;
        sprite.flipX = direction == 1;

        todosLobos.Add(this);

        rig = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        anima.SetBool("Walking", (estadoAtual == Estado.Andando) || (estadoAtual == Estado.Patrulha) || (estadoAtual == Estado.Atacando));
        if (presaTrans != null )
        {//caçar presa
            direction = presaTrans.transform.position.x > transform.position.x ? 1 : -1;
            sprite.flipX = direction == 1;
            // rig.MovePosition(transform.position + Vector3.right * direction * Time.deltaTime * speedRun);
            rig.velocity = new Vector2(direction * speedRun, rig.velocity.y);
            float distPresa = Vector3.Distance(presaTrans.position, transform.position);
            Debug.Log("I has a presa " + presaTrans.name + " your distance " + distPresa);
            if (distPresa > 15)
            {
                estadoAtual = Estado.Andando;
                presaTrans = null;
                Debug.Log("para de caçar");
            }

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
            // rig.MovePosition(transform.position + (Vector3.right * direction * Time.deltaTime * speedWalk));
            rig.velocity = new Vector2(direction * speedWalk, rig.velocity.y);
            
            
        }
    }
    void MudarEstado()
    {
        switch (estadoAtual)
        {
            case Estado.Parado:
                estadoAtual = Random.value < 0.5f ? Estado.Andando : Estado.Patrulha;
                time = Random.Range(tempoMinAndando, tempoMaxAndando);
                direction = Random.value > 0.4f ? 1 : -1;
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
                rig.velocity = Vector2.zero; // parar o movimento
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
                        direction = (Random.value > 0.4f) ? -1 : 1;
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
        direction = (Random.value > 0.8f) ? -1 : 1;
        sprite.flipX = direction == 1;
    }
    public void setPresa(Transform newPresa)
    {
        presaTrans = newPresa;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.gameObject.name + " is trigger");
        //não podem fazer trigger como um lobo e um interable
        if (other.gameObject.GetComponent<LoboMoviment>() == null && other.gameObject.GetComponent<Interactable>() == null)
        {
            RuntimeManager.PlayOneShot(latido, transform.position);
            setPresa(other.gameObject.transform);
            estadoAtual = Estado.Atacando;
            Debug.Log("começar a caçar " + other.gameObject.name);
        }
    }
}
