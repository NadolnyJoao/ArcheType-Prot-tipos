using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class MenuManager : MonoBehaviour
{
    [SerializeField] private string nomeDoNivel;
    [SerializeField] private GameObject painelInicial; 
    [SerializeField] private GameObject painelOp;
    public void Jogar()
    {
        SceneManager.LoadScene(nomeDoNivel);
    }
    public void AbrirOp()
    {
        painelInicial.SetActive(false);
        painelOp.SetActive(true);   
    }
    public void FecharOp()
    {
        painelInicial.SetActive(true);
        painelOp.SetActive(false); 
    }
    public void SairGame()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
                Application.Quit(); // Funciona em builds executáveis (Windows, Android, etc.)
        #endif
    
        PlayerPrefs.DeleteAll();
    }
}
