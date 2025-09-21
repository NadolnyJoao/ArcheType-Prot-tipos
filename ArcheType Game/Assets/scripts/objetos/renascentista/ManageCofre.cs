using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ManageCofre : MonoBehaviour
{
    // Start is called before the first frame update
    string secretCode = "123";
    public List<ManageDigit> digitos= new  List<ManageDigit>();
    public UnityEvent onCofreAberto;
    public bool cofreisOpen = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string currentCode = "";
        foreach (ManageDigit d in digitos)
        {
            currentCode += d.idAtual.ToString();

        }
        if (currentCode == secretCode && cofreisOpen == false)
        {
            Debug.Log("Cofre Aberto");
            onCofreAberto.Invoke();
            cofreisOpen = true;
            //abrir o cofre
        }
    }
}
