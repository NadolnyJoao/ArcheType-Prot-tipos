using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sikibidi : MonoBehaviour
{
  public Vector3 position;
    private Rigidbody2D rb;
    public bool safada = false; 

    void Start()
    {
        position = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (safada)
        {
               
            LoadingScript.Instance.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

             MonoBehaviour[] scripts = collision.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }

        // Congela física do player (opcional mas recomendado)
       collision.GetComponent<Rigidbody2D>().simulated = false;
       collision.GetComponent<Animator>().enabled = false;
            
            safada = true;  
            Time.timeScale = 0f;   
            // Volta para posição inicial
            transform.position = position;
    
            // Zera velocidade
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;

            // Deixa o rigidbody parado
            rb.bodyType = RigidbodyType2D.Kinematic;

            Debug.Log("aaaaaa");
            
        }
    }
}