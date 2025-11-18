using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum TypeBoches
{
    Rupestre,
    Egito,
    Renascentista,
    Surrealista
};
public class BrocheManage : MonoBehaviour
{

    [Serializable]
    public class ImgBoche
    {
        public TypeBoches name;
        public Sprite img;
    }
    public Broches coletados = new Broches();
    public TypeBoches bocheaddbytype;
    public ImgBoche[] imgBroches;
    public Image[] slotBroches;
    private List<TypeBoches> nameBrochesRenderer = new List<TypeBoches>();

    private string pathFile = "Assets/Saves/Broches/Broches.json";
    // Start is called before the first frame update
    private Sprite imgBroche;

    public UnityEvent Finish;
    void Start()
    {

        LoadBroches();
    }


    public void ColetarBroche()
    {
        coletados.broches.Add(TypeBrochetoString(bocheaddbytype));
        SaveBroches();
    }

    // Update is called once per frame
    void SaveBroches()
    {
        Broches brochesToSave = coletados;

        string dataJson = JsonUtility.ToJson(brochesToSave, true);
        File.WriteAllText(pathFile, dataJson);
        // Debug.Log("Dados Salvos");
    }
    void LoadBroches()
    {
        if (File.Exists(pathFile))
        {
            string dataJson = File.ReadAllText(pathFile);

            Broches loadBroches = JsonUtility.FromJson<Broches>(dataJson);

            coletados = loadBroches;

            // Debug.Log("broches carregados");
            RenderBroches();
        }
    }
    void RenderBroches()
    {
        int numbrochesColeted = 0;
        foreach (string namejs in coletados.broches)
        {
            TypeBoches name = StringtoTypeBoehce(namejs);
            bool noRender = false;
            Debug.Log($"COmparation typebrochew name {name} bochesrender lenghth {nameBrochesRenderer.Count}");
            foreach (TypeBoches namesaved in nameBrochesRenderer)
            {
                Debug.Log($"namesaved {namesaved}  == name {name}");
                if (namesaved == name)
                {
                    noRender = true;
                }
            }
            if (!noRender)
            {
                foreach (ImgBoche img in imgBroches)
                {
                    // Debug.Log("comparação name broche name : " + namejs + " name img.name: " + img.name + " igual? " + (img.name == name));
                    if (img.name == name)
                    {



                        nameBrochesRenderer.Add(name);
                        numbrochesColeted++;
                        imgBroche = img.img;
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
            if (numbrochesColeted == 4)
            {
                Debug.Log("Temos o fim do jogo");
                 string dataJson = JsonUtility.ToJson(new Broches(), true);
                File.WriteAllText(pathFile, dataJson);
                Finish.Invoke();
            }

        }

    }

    string TypeBrochetoString(TypeBoches type)
    {
        switch (type)
        {
            case TypeBoches.Rupestre:
                return "Rupestre";
            case TypeBoches.Egito:
                return "Egito";
            case TypeBoches.Renascentista:
                return "Renascentista";
            case TypeBoches.Surrealista:
                return "Surrealista";
            default:
                return "none";
        }
    }
    TypeBoches StringtoTypeBoehce(string name)
    {
        switch (name)
        {
            case "Rupestre":
                return TypeBoches.Rupestre;
            case "Egito":
                return TypeBoches.Egito;
            case "Renascentista":
                return TypeBoches.Renascentista;
            case "Surrealista":
                return TypeBoches.Surrealista;
            default:
                return TypeBoches.Rupestre;
        }
    }
}
