using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool chaopedra;
    public bool chaofloresta;
    public bool chaoplanice;
    
    // Adicione uma referência para o SpriteRenderer para virar o sprite
    private SpriteRenderer spriteRenderer;

    void Start()
    {
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
        if(FootSteep != null)
        if (horizontalInput != 0)
        {
            FootSteep.SetActive(true);
        } 
        else
        {
            FootSteep.SetActive(false); 
        }

        if (canJump && isgrounded && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
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
}