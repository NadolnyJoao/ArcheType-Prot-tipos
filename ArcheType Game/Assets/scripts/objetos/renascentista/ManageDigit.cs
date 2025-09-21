using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageDigit : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Color32> digitos = new List<Color32>();
    public GameObject showDigit;
    public GameObject nextDown;
    public GameObject nextUp;
    public int idAtual = 0;

    void Start()
    {
        if (digitos.Count > 0)
        {
            showDigit.GetComponent<Image>().color = digitos[idAtual];
            nextUp.GetComponent<Image>().color = digitos[idAtual-1>0?idAtual-1:digitos.Count-1];
            nextDown.GetComponent<Image>().color = digitos[idAtual+1<digitos.Count?idAtual+1:0];
        }
    }

    public void NextUp()
    {
        Debug.Log("Next Up");
        nextUp.GetComponent<Image>().color = digitos[idAtual];
        idAtual++;
        if (idAtual >= digitos.Count)
        {
            idAtual = 0;
        }
        
        showDigit.GetComponent<Image>().color = digitos[idAtual];
        nextDown.GetComponent<Image>().color = digitos[idAtual+1<digitos.Count?idAtual+1:0];
    }
    public void NextDown()
    {
        Debug.Log("Next Down");
        nextDown.GetComponent<Image>().color = digitos[idAtual];
        idAtual--;
        if (idAtual < 0)
        {
            idAtual = digitos.Count - 1;
        }
        showDigit.GetComponent<Image>().color = digitos[idAtual];
        nextUp.GetComponent<Image>().color = digitos[idAtual-1>0?idAtual-1:digitos.Count-1];
    }

    public void SetColors()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
