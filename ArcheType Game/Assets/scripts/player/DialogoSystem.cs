using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Fala
{
    public string name;
    public Color32 colorName;
    public List<string> falas = new List<string>();

    public bool canPass = true;

    private int index = 0;
    private bool finish = false;
    public void ConpriObjetivo()
    {
        canPass = true;
        
    }
    public void nextFala()
    {
        if (!finish)
        { index++; }
        if (index >= falas.Count)
        {
            finish = true;
            index--;
        }
    }
    public bool getFinish() { return finish; }
    public string getFala()
    {
        return falas[index];
    }
    public string getName()
    {
        return name;
    }
    public Color32 getColor()
    {
        return colorName;
    }
}

public class DialogoSystem : MonoBehaviour
{

    public List<Fala> falas = new List<Fala>();
    public int indexFalas = 0;
    public DialogoSystenUI dialogoUI;
    private bool inDialogue = false;

    public void Update()
    {
        //temporario ate unity events
        if (Input.GetKeyDown(KeyCode.W))
        {
            InvokeDialogo();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CumprirObjetivo();
        }
    }
    public void CumprirObjetivo()
    {
        indexFalas++;
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
            }
            else
            {
                falas[indexFalas].nextFala();
                if (falas[indexFalas].getFinish())
                {
                    HiddenFala();
                    inDialogue = false;
                    Debug.Log("Fim do dialogo");
                    //proximo dialogo
                    if (falas[indexFalas].canPass)
                        indexFalas++;
                }
                else
                {
                    ShowFala();
                    Debug.Log("proxima fala");
                }
            }


        }
    }

    public void ShowFala()
    {
        dialogoUI.ShowDialogoBox();
        dialogoUI.SetTextDialogBox(falas[indexFalas].getColor(), falas[indexFalas].getName(), falas[indexFalas].getFala());
    }

    public void HiddenFala()
    {
        dialogoUI.HiddenDialogoBox();
    }

}
