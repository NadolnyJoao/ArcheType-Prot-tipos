using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class BrocheManage : MonoBehaviour
{
    public Broches coletados = new Broches();
    public string brocheToColetar;

    public Sprite[] imgBroches;
    public Image[] slotBroches;

    private string pathFile = "Assets/Saves/Broches/Broches.json";
    // Start is called before the first frame update
private Sprite imgBroche;
    void Start()
    {

        LoadBroches();
    }


    public void ColetarBroche()
    {
        coletados.broches.Add(brocheToColetar);
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
            RenderBroches();
        }
    }
    void RenderBroches()
    {
        foreach (string name in coletados.broches)
        {


            if (name == "rupestre")
            {
                imgBroche = imgBroches[0];
            }
            else
            {
                imgBroche = imgBroches[1];
            }
            foreach (Image slot in slotBroches)
            {
                if (slot.color != Color.white)
                {
                    slot.sprite = imgBroche;
                    slot.color = Color.white;
                    break;
                }
            }
        }

    }
}
