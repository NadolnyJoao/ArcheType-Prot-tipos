using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class RespostaQuestion : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEvent acertou;
    public UnityEvent errou;

    public TMP_InputField inputResponse;
    

    public void Responder()
    {
        string resposta = inputResponse.text;
        if (ValidarRespostaEsfinge(resposta))
        {
            acertou.Invoke();
        }
        else
        {
            errou.Invoke();
        }
    }


    static bool ValidarRespostaEsfinge(string resposta)
    {
        string respostaNormalizada = RemoverAcentos(resposta.ToLower());

        // Palavras-chave diretas (aceita "homem", "ser humano", etc.)
        string[] palavrasChave = {
            "homem", "ser humano", "humano", "o homem", "homens",
            "hombre", "man", "anthropos" // outras línguas (opcional)
        };

        // Verificação simples (se contém uma palavra-chave direta)
        foreach (string palavra in palavrasChave)
        {
            if (respostaNormalizada.Contains(palavra))
            {
                return true;
            }
        }

        // Verificação contextual (se descreve o ciclo de vida humano)
        string[] termosCicloVida = {
            "bebe", "crianca", "engatinha", "quatro pes", "4 pes",
            "adulto", "dois pes", "2 pes", "velho", "bengala", "tres pes", "3 pes"
        };

        int termosPresentes = 0;
        foreach (string termo in termosCicloVida)
        {
            if (respostaNormalizada.Contains(termo))
            {
                termosPresentes++;
            }
        }

        // Se pelo menos 3 termos do ciclo de vida estiverem presentes
        return termosPresentes >= 3;
    }

    // Remove acentos manualmente (sem NormalizationForm)
    static string RemoverAcentos(string texto)
    {
        string comAcentos = "áàâãäéèêëíìîïóòôõöúùûüç";
        string semAcentos = "aaaaaeeeeiiiioooooouuuuc";

        for (int i = 0; i < comAcentos.Length; i++)
        {
            texto = texto.Replace(comAcentos[i], semAcentos[i]);
        }
        return texto;
    }
}
