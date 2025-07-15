using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Mapeamento : MonoBehaviour
{
    public UnityEvent actionsLaterExploration;

    public bool caverna = false;
    public bool floresta = false;
    public bool planice = false;




    public GameObject mapa;
    public GameObject cavernadescoberta;
    public GameObject florestadescoberta;
    public GameObject planicedescoberta;
    public GameObject playerpedra;
    public GameObject playerfloresta;
    public GameObject playerplanice;
    public GameObject mapavazio;

    public GameObject desenhocaverna;
    public GameObject desenhofloresta;
    public GameObject desenhoplanice;
    public GameObject voltaraomuseu;

    private PlayerMoviment playermoviment;
    private bool objetivoCompleto = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playermoviment = player.GetComponent<PlayerMoviment>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((caverna && planice && floresta) && objetivoCompleto == false)
        {
            //explorou tudo 
            actionsLaterExploration.Invoke();
            objetivoCompleto = true;
            Debug.Log("Objetivo de exploração completo!");
        }

        if (desenhocaverna.activeSelf && desenhofloresta.activeSelf && desenhoplanice.activeSelf)
        {
            voltaraomuseu.SetActive(true);
        }

        if (caverna == true)
        {
            cavernadescoberta.SetActive(true);
            mapavazio.SetActive(false);
        }
        if (floresta == true)
        {
            florestadescoberta.SetActive(true);
            mapavazio.SetActive(false);
        }
        if (planice == true)
        {
            planicedescoberta.SetActive(true);
            mapavazio.SetActive(false);
        }

        if (playermoviment.chaopedra == true)
        {
            // playerpedra.SetActive(true);
            caverna = true;
        }
        else
        {
            playerpedra.SetActive(false);
        }

        if (playermoviment.chaofloresta == true)
        {
            // playerfloresta.SetActive(true);
            floresta = true;
        }
        else
        {
            playerfloresta.SetActive(false);
        }

        if (playermoviment.chaoplanice == true)
        {
            // playerplanice.SetActive(true);
            planice = true;
        }
        else
        {
            playerplanice.SetActive(false);
        }
    }

    // public void InteragirCaverna()
    // {
    //     caverna = true;
    // }

    // public void InteragirFloresta()
    // {
    //     floresta = true;
    // }

    // public void InteragirPlanice()
    // {
    //     planice = true;
    // }

    public void FecharMapa()
    {
        mapa.SetActive(false);
    }
    public void AbrirMapa()
    {
        mapa.SetActive(true);
    }
    

    
}
