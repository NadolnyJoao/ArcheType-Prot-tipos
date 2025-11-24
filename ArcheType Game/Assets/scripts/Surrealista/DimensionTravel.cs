using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class DimensionTravel : MonoBehaviour
{
    public bool mundonormal;
    public bool mundosonho;
    public Transform player;
    public Transform mundonormalobj;
    public Transform mundosonhoobj;
    public Transform salasurreal;
    private PlayerMoviment playermoviment;
    public EventReference mundoNormal;
    public EventReference mundoSurreal;

    public WorldTransitionPlayer transitionPlayer;

    // Start is called before the first frame update
    void Start()
    {
        playermoviment = FindAnyObjectByType<PlayerMoviment>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeWorld()
    {
        if (mundonormal)
        {
            // Vector3 posicaoPlayer = player.position;
            // posicaoPlayer.y = mundosonhoobj.position.y;
            player.position = mundosonhoobj.position;
            mundosonho = true;
            mundonormal = false;
            //RuntimeManager.PlayOneShot(mundoSurreal, transform.position);
            Debug.Log("mudou para mundo do sonho");
            
        }
        else
        if (mundosonho)
        {
            // Vector3 posicaoPlayer = player.position;
            // posicaoPlayer.y = mundonormalobj.position.y;
            player.position = mundonormalobj.position;
            mundonormal = true;
            mundosonho = false;
            //RuntimeManager.PlayOneShot(mundoNormal, transform.position);
            Debug.Log("mudou para mundo do normar");
            
        }
    }
    public void OnAtaque()
    {
        Debug.Log("oie"); 
          if (transitionPlayer != null)
        {
            transitionPlayer.PlayTransition(() =>
            {
                ChangeWorld(); // troca só depois do vídeo
            });
        }
        else
        {
            ChangeWorld(); // fallback caso não tenha vídeo
        }
    }

    public void DestrancarPorta()
    {
        // if (playermoviment.chavesurreal == true)
        // {
        //     Debug.Log("porta destrancada");
        //     player.position = salasurreal.position;
        //     playermoviment.chavesurreal = false;
        // }
    }
}
