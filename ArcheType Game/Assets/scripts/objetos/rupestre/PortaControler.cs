using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaControler : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Animator animatorPorta; // Referência ao Animator da porta

    [Header("Configurações")]
    [SerializeField] private bool portaStartOpen = false; // Define se a porta começa aberta

    private bool _estaAberta;

    private void Start()
    {
        if (animatorPorta == null)
        {
            animatorPorta = GetComponent<Animator>();
            if (animatorPorta == null)
            {
                Debug.LogError("Animator não encontrado na porta!", this);
            }
        }

        // Configura o estado inicial
        _estaAberta = portaStartOpen;
        animatorPorta.SetBool("Abrir", _estaAberta);
    }

    public void Abrir()
    {
        if (_estaAberta) return; // Já está aberta
        
        _estaAberta = true;
        animatorPorta.SetBool("Abrir", true);
    }


    public void Fechar()
    {
        if (!_estaAberta) return; // Já está fechada
        
        _estaAberta = false;
        animatorPorta.SetBool("Abrir", false);
    }

    /// <summary>

    public void AlternarEstado()
    {
        if (_estaAberta)
            Fechar();
        else
            Abrir();
    }

    // Propriedade para verificar se a porta está aberta
    public bool EstaAberta => _estaAberta;
}
