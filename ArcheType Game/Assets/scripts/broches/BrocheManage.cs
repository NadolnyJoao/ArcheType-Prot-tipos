using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BrocheManage : MonoBehaviour
{
    public Broches coletados = new Broches();
    private string pathFile = "Assets/Saves/Broches.json";
    // Start is called before the first frame update

    void Start()
    {
        LoadBroches();
    }


    public void ColetarBroche(string name)
    {
        coletados.broches.Add(name);
        SaveBroches();
    }

    // Update is called once per frame
    void SaveBroches()
    {
        Broches brochesToSave = coletados;

        string dataJson = JsonUtility.ToJson(brochesToSave, true);
        File.WriteAllText(pathFile, dataJson);
        Debug.Log("Dados Salvos");
    }
    void LoadBroches()
    {
        if (File.Exists(pathFile))
        {
            string dataJson = File.ReadAllText(pathFile);

            Broches loadBroches = JsonUtility.FromJson<Broches>(dataJson);

            coletados = loadBroches;

            Debug.Log("broches carregados");
        }
    }
}
