using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverSobreposicao : MonoBehaviour
{
   [Header("Objetos a serem desativados quando este painel estiver ativo")]
    public GameObject[] targetsToDisable;

    private bool[] previousStates;

    private void OnEnable()
    {
        if (targetsToDisable == null || targetsToDisable.Length == 0)
            return;

        previousStates = new bool[targetsToDisable.Length];

        // Desativa apenas os objetos selecionados
        for (int i = 0; i < targetsToDisable.Length; i++)
        {
            if (targetsToDisable[i] == null)
                continue;

            previousStates[i] = targetsToDisable[i].activeSelf;
            targetsToDisable[i].SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (previousStates == null || targetsToDisable.Length == 0)
            return;

        // Restaura os estados anteriores
        for (int i = 0; i < targetsToDisable.Length; i++)
        {
            if (targetsToDisable[i] == null)
                continue;

            targetsToDisable[i].SetActive(previousStates[i]);
        }
    }
}
