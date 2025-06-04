using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu]

public class Fala : ScriptableObject
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
    }    //public List<string> frases = new List<string>();

    public UnityEvent actions;//realiza uma ação no final da fala ( opicional )

    
}
