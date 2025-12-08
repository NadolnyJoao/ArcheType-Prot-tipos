using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


// }

public class DialogoSystem : MonoBehaviour
{
    [System.Serializable]
    public class ActionFala {
        public UnityEvent falaEvent;
        public int id;
    }

    public List<Fala> falas = new List<Fala>();
    public UnityEvent laterActions;
    private int indexFalas = 0;
    // public DialogoSystenUI dialogoUI;
    private bool inDialogue = false;

    public List<ActionFala> actionsLateFala = new List<ActionFala>();


    public void Update()
    {
        // //temporario ate unity events
        // if (Input.GetKeyDown(KeyCode.W))
        // {

        //     InvokeDialogo();
        // }
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     CumprirObjetivo();
        // }
    }
     public bool IsDialogueComplete()
    {
        // Verifica se todas as falas foram usadas ou se o diálogo atual terminou
        return indexFalas >= falas.Count || (falas[indexFalas].getFinish() && inDialogue);
    }
    public void CumprirObjetivo()
    {
        if (!falas[indexFalas].canPass) { indexFalas++; }
    }
    //chamar no unity events
    public void InvokeDialogo()
    {
        if (indexFalas < falas.Count)
        {

            if (inDialogue == false)
            {

                ShowFala();
                inDialogue = true;
                Debug.Log("inicio do dialogo");
                Debug.Log("fala:"+falas[indexFalas]+" index: "+falas[indexFalas].index+"estatus: "+falas[indexFalas].getFinish());

            }
            else
            {
                falas[indexFalas].nextFala();
                if (falas[indexFalas].getFinish())
                {
                    HiddenFala();
                    inDialogue = false;
                    Debug.Log("Fim do dialogo");
                    foreach (var fala in actionsLateFala)
                    {
                        if(indexFalas == fala.id)
                        {
                            fala.falaEvent.Invoke();
                        }
                    }
                    //proximo dialogo
                    if (falas[indexFalas].canPass)
                    {
                        indexFalas++;
                        laterActions.Invoke();
                    }
                }
                else
                {
                    ShowFala();
                    Debug.Log("proxima fala");
                }
            }


        }
    }

     void ShowFala()
    {
        Debug.Log("index da fala "+falas[indexFalas].index);
        DialogoSystenUI.dialogoSystenUI.ShowDialogoBox();
        DialogoSystenUI.dialogoSystenUI.SetTextDialogBox(falas[indexFalas].getColor(), falas[indexFalas].getName(), falas[indexFalas].getFala());
    }

    public void HiddenFala()
    {
        DialogoSystenUI.dialogoSystenUI.HiddenDialogoBox();
    }
    public void SetIndexFalas(int index)
    {
        if (index >= 0 && index < falas.Count)
        {
            indexFalas = index;
        }
        else
        {
            Debug.LogWarning("Index out of range for falas list.");
        }
    }
}
