using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorPopup : MonoBehaviour
{
    public TMP_Text titulo;
    public TMP_Text texto;

    public void Configurar(string tituloStr, string textoStr)
    {
        titulo.text = tituloStr;
        texto.text = textoStr;
        Destroy(gameObject, 2f); // Destrói o popup após 2 segundos
    }

}