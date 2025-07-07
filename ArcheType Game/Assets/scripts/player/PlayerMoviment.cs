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
  
    public Animator ani;

    [Header("Sons de Passos")]
    [SerializeField] private EventReference footSteep;


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
                // spriteRenderer.flipX = false; // Virado para direita
                transform.rotation = Quaternion.Euler(0, 0, 0); // Garante que a rotação esteja zerada
            }
        }
        else if (horizontalInput < 0)
        {
            direction = -1;
            if (spriteRenderer != null)
            {
                // spriteRenderer.flipX = true; // Virado para esquerda
                transform.rotation = Quaternion.Euler(0, 180, 0); // Garante que a rotação esteja virada para a esquerda
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

        // if (canJump && isgrounded && Input.GetButtonDown("Jump"))
        // {
        //     jump = true;
        // }
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
            RuntimeManager.PlayOneShot(footSteep, transform.position);    
    }
}