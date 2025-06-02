using System.Collections;
using System.Collections.Generic;
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
        Application.Quit();
    }
}
