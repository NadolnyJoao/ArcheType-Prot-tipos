using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


public class Tocha : MonoBehaviour
{
    public GameObject tochaFogo;
    public EventReference torchSound;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EsperarObjetoAtivar());

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator EsperarObjetoAtivar()
    {
        // Enquanto o objeto NÃO estiver ativo, espera 0.1s e tenta de novo
        while (!tochaFogo.activeInHierarchy)
        {
            yield return new WaitForSeconds(0.1f);

        }
        // toca o som quando o obejto for ativado xD
        RuntimeManager.PlayOneShot(torchSound, transform.position);
        // Quando o objeto estiver ativo, faz algo
        Debug.Log("Objeto ativado!");
    }
}
