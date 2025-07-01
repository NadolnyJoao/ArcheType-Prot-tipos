using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReceberDano : MonoBehaviour
{
    public UnityEvent actionsDeath;
    public UnityEvent actionsDamage;
    public int vida = 2;
    public GameObject gameover;


    void Update()
    {

    }

    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            vida--;
            actionsDamage.Invoke();
            if (vida <= 0)
            {
                actionsDeath.Invoke();

            }
        }
    }

    public void GameOver()
    {
        gameover.SetActive(true);
    }
}
