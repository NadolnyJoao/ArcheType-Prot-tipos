using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoltarMuseu : MonoBehaviour
{
    public string cenaNome;
    public float delay = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadScene", delay);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(cenaNome);
    }
}
