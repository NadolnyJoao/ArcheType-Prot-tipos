using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;
using FMOD.Studio;

public class ReceberDano : MonoBehaviour
{
    public UnityEvent actionsDeath;
    public UnityEvent actionsDamage;
    public int vida = 2;
    public GameObject gameover;
    public EventReference damageBite;
    private float timeInvencivel = 1f;
    public float timeInvencivelMax = 3f;

    public List<GameObject> hearts = new List<GameObject>();
    void Update()
    {
        timeInvencivel -= Time.deltaTime;
    }

    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (timeInvencivel > 0)
            {
                return;
            }
            RuntimeManager.PlayOneShot(damageBite, transform.position);
            vida--;
            actionsDamage.Invoke();
                hearts[hearts.Count - 1].SetActive(false);
                //remover ultimo item .pop
                hearts.RemoveAt(hearts.Count - 1);
            if (vida <= 0)
            {
                actionsDeath.Invoke();
                Debug.Log("morrer pls");
                //esconder ultimo item do array hearts

            }
            timeInvencivel = timeInvencivelMax;

        }
    }

    public void GameOver()
    {
        gameover.SetActive(true);
    }
}
