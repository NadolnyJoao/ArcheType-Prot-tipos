using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cobra : MonoBehaviour
{
    public Transform Player;
    public float speed = 3.0f;
    public Animator ani; 
    public float stopDistance = 10.0f;
    public SpriteRenderer spriteRenderer; 
    private bool oieSybau = false; 
    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null) return;
        if(oieSybau){
        // Distância até o player
        float distance = Vector2.Distance(transform.position, Player.position);

        if (distance > stopDistance)
        {
            // Direção até o player
            // Vector2 direction = (Player.position - transform.position).normalized;

            // Movimento suave em direção ao player
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);

            // Faz a cobra olhar para o player (rotação em 2D)
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           // transform.rotation = Quaternion.Euler(0, 0, angle + 90);
           if (Player.position.x < transform.position.x){
                spriteRenderer.flipX = false;
           } else{
            spriteRenderer.flipX = true; 
           }
        }
        }
    }


    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "ground"){
            ani.SetBool("inground", true);
            Debug.Log("sybau");
            oieSybau = true; 
        }
    }

}
