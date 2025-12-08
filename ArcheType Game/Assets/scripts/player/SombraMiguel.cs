using UnityEngine;
using System.Collections.Generic;

public class SombraMiguel : MonoBehaviour
{
    public Animator papai;
    private Animator myAni;
    private AnimatorOverrideController overrideController;
    
    // Para controlar os triggers que foram acionados no frame atual
    private HashSet<string> triggersThisFrame = new HashSet<string>();

    void Start()
    {
        myAni = GetComponent<Animator>();
        
        // Cria um override controller baseado no controller do pai
        overrideController = new AnimatorOverrideController(papai.runtimeAnimatorController);
        myAni.runtimeAnimatorController = overrideController;
    }

    void Update()
    {
        // Limpa os triggers do frame anterior
        triggersThisFrame.Clear();
        
        // Sincroniza todos os parâmetros
        SyncAnimatorParameters();
    }

    void SyncAnimatorParameters()
    {
        // Obtém todos os parâmetros do animator do pai
        foreach (AnimatorControllerParameter param in papai.parameters)
        {
            switch (param.type)
            {
                case AnimatorControllerParameterType.Float:
                    myAni.SetFloat(param.name, papai.GetFloat(param.name));
                    break;
                case AnimatorControllerParameterType.Int:
                    myAni.SetInteger(param.name, papai.GetInteger(param.name));
                    break;
                case AnimatorControllerParameterType.Bool:
                    myAni.SetBool(param.name, papai.GetBool(param.name));
                    break;
                case AnimatorControllerParameterType.Trigger:
                    // Para triggers, precisamos verificar se foram acionados
                    // SyncTrigger(param.name);
                    // Debug.Log("Trigger param name "+ param.name);
                    break;
            }
        }
    }

    void SyncTrigger(string triggerName)
    {
        // Verifica se o trigger está ativo no animator do pai
        // Usamos IsInTransition para detectar triggers de forma mais confiável
        AnimatorStateInfo stateInfo = papai.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextStateInfo = papai.GetNextAnimatorStateInfo(0);
        // Debug.Log("trigger");
        // Se o trigger foi acionado neste frame, aciona na sombra também
        if (papai.GetCurrentAnimatorStateInfo(0).fullPathHash != myAni.GetCurrentAnimatorStateInfo(0).fullPathHash)
        {
            // Se o estado mudou, pode indicar que um trigger foi acionado
            // Neste caso, tentamos manter os estados sincronizados
            myAni.Play(papai.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, papai.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
    }
}