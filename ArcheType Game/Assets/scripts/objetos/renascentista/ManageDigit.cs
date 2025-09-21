using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageDigit : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Color32> digitos = new List<Color32>();
    public Image showDigit;
    public Image nextDown;
    public Image nextUp;
    public int idAtual = 0;

    void Start()
    {
        if (digitos.Count > 0)
        {
            SetColors();
        }
    }

  public void SetColors()
{
    int upIndex = (idAtual - 1 + digitos.Count) % digitos.Count;
    int downIndex = (idAtual + 1) % digitos.Count;

    showDigit.color = digitos[idAtual];
    nextUp.color = digitos[upIndex];
    nextDown.color = digitos[downIndex];

    Debug.Log($"SetColors → Atual: {idAtual}, Up: {upIndex}, Down: {downIndex}");
}

    public void NextUp()
    {
        Debug.Log("Next Up");
        // nextUp.color = digitos[idAtual];
        idAtual++;
        if (idAtual >= digitos.Count)
        {
            idAtual = 0;
        }

        // showDigit.color = digitos[idAtual];
        // nextDown.color = digitos[idAtual + 1 < digitos.Count ? idAtual + 1 : 0];
        Invoke("SetColors",1);
    }
    public void NextDown()
    {
        Debug.Log("Next Down");
        // nextDown.color = digitos[idAtual];
        idAtual--;
        if (idAtual < 0)
        {
            idAtual = digitos.Count - 1;
        }
        // showDigit.color = digitos[idAtual];
        // nextUp.color = digitos[idAtual - 1 > 0 ? idAtual - 1 : digitos.Count - 1];
        
        Invoke("SetColors",1);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
