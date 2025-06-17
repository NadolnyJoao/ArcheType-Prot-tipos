using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayerMoviment : MonoBehaviour
{
    public float speed = 100;
    public float forceJump = 100;
    public bool canJump = false;
    private int direction = 1; // 1 para direita, -1 para esquerda (valor inicial como direita)
    private Rigidbody2D rig;
    private float horizontalInput, verticalInput;
    private bool jump = false;
    private bool isgrounded = false;
    public GameObject FootSteep;
    public bool isWalking; 
    public bool chaopedra;
    public bool chaofloresta;
    public bool chaoAzulejo;
    public bool chaoplanice;

    public Animator ani;

    [Header("Sons de Passos")]
    [SerializeField] private EventReference passosAzulejo;
    [SerializeField] private EventReference passosFloresta;    
    [SerializeField] private EventReference passosPedra;
    [SerializeField] private EventReference passosGrama;

    private float passoTimer = 0f;
    private float intervaloPasso = 0.58f; 


    // Adicione uma referência para o SpriteRenderer para virar o sprite
    private SpriteRenderer spriteRenderer;

    void Start()
    {      
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obter o componente SpriteRenderer
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Atualizar a direção baseada no input horizontal
        if (horizontalInput > 0)
        {
            direction = 1;
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = false; // Virado para direita
            }
        }
        else if (horizontalInput < 0)
        {
            direction = -1;
            if (spriteRenderer != null) 
            {
                spriteRenderer.flipX = true; // Virado para esquerda
            }
        }
        if (horizontalInput != 0 && isgrounded)
        {
            passoTimer -= Time.deltaTime;
            // se so jogar no update vai ficar repedindo o som varias vezes por isso tenho que colocar um intervalo de tempo para soar mais clean
            // isso vai ser provisório nao me xinga pedro
        }
        if(passoTimer <= 0)
        {
            passoTimer = intervaloPasso; 
             Footsteep();
        }

        if (canJump && isgrounded && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        ani.SetBool("walk",horizontalInput!=0);
    }

    void FixedUpdate()
    {
        float veloy = rig.velocity.y;
        rig.velocity = new Vector2(horizontalInput * speed * Time.fixedDeltaTime, veloy);
        if (jump)
        {
            rig.AddForce(Vector2.up * forceJump * Time.fixedDeltaTime, ForceMode2D.Impulse);
            jump = false;
            isgrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "ground(pedra)")
        {
            chaopedra = true;
        }
        else
        {
            chaopedra = false;
        }

        if (other.gameObject.name == "ground(floresta)")
        {
            chaofloresta = true;
        }
        else
        {
            chaofloresta = false;
        }

        if (other.gameObject.name == "ground(planice)")
        {
            chaoplanice = true;
        }
        else
        {
            chaoplanice = false;
        }
         if (other.gameObject.name == "ground(azulejos)")
        {
            chaoAzulejo = true;
        }
        else
        {
            chaoAzulejo = false; 
        }
        if (other.gameObject.tag == "ground")
        {
            isgrounded = true;
            GroundSong groundSong = other.gameObject.GetComponent<GroundSong>();
            if (groundSong != null)
            {
                Debug.Log("Colider how ground - song " + groundSong.nameSong);
            }
            else
            {
                Debug.Log("Colider how ground - song undefinde");
            }
        }
    }

    // Método público para obter a direção atual (opcional)
    public int GetDirection()
    {
        return direction;

    }

    public void Footsteep()
    {

        if (chaoAzulejo)
        {
            RuntimeManager.PlayOneShot(passosAzulejo, transform.position);
        }

        if (chaopedra)
        {
            RuntimeManager.PlayOneShot(passosPedra, transform.position);
        }

        if (chaofloresta)
        {
            RuntimeManager.PlayOneShot(passosFloresta, transform.position);
        }

        if (chaoplanice)
        {
            RuntimeManager.PlayOneShot(passosGrama, transform.position);
        }
    
    }
}