using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    [Header("Tempo antes de trocar de cena")]
    public float tempo = 7f;
    public int scenetocarregar;

    void Start()
    {
        // Inicia a troca de cena depois de X segundos
        Invoke("TrocarCena", tempo);
    }

    void TrocarCena()
    {
        LoadingScript.Instance.LoadScene(scenetocarregar);
    }
}
