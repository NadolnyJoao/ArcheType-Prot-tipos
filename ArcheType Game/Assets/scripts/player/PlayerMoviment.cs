using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public float speed = 100;
    public float forceJump = 100;
    public bool canJump = false;

    private Rigidbody2D rig;
    private float horizontalInput, verticalInput;
    private bool jump = false;
    private bool isgrounded = false;
    public GameObject FootSteep;
    public bool chaopedra;
    public bool chaofloresta;
    public bool chaoplanice;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0)
        {
            FootSteep.SetActive(true);
        } if (horizontalInput == 0)
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

        // -------------

        
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

}
