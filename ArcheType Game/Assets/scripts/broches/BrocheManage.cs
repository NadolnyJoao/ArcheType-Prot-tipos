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
    Surrealista,
    none
};
public class BrocheManage : MonoBehaviour
{
    [Serializable]
    public class BagBroche
    {
        public TypeBoches lastBroche = TypeBoches.none;
        // public Broches coletados;
        public List<string> broches = new List<string>();

    }



    [Serializable]
    public class ImgBoche
    {
        public TypeBoches name;
        public Sprite img;
    }
    public BagBroche myBag = new BagBroche();
    public TypeBoches bocheaddbytype;
    public ImgBoche[] imgBroches;
    public Image[] slotBroches;
    private List<TypeBoches> nameBrochesRenderer = new List<TypeBoches>();

    private string pathFile = "Assets/Saves/Broches/Broches.json";
    // Start is called before the first frame update
    private Sprite imgBroche;

    public UnityEvent Finish;
    private PlayerBroche playerBroche;
    private int numbrochesColeted = 0;

    void Start()
    {
        // Debug.Log(myBag);
        // myBag.coletados = new Broches();
        // ColetarBroche();



        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objPlayer == null)
        {
            Debug.Log("BrocheMAnage naõ achou player");
        }
        playerBroche = objPlayer.GetComponent<PlayerBroche>();
        if (playerBroche == null)
        {
            Debug.Log("BrocheMAnage nçao tem acesso a PLayerBroches");
        }
        LoadBroches();

        // if (myBag.coletados == null) myBag.coletados = new Broches();


    }
    public void AniColect()
    {
        Debug.Log("Invoke AniColect");
        if (myBag.lastBroche != TypeBoches.none && playerBroche != null)
        {
            playerBroche.PlayeAnimation(TypeBrochetoInt(myBag.lastBroche));
            myBag.lastBroche = TypeBoches.none;

            SaveBroches();
        }
    }

    public void ColetarBroche()
    {
        // if (myBag.coletados == null) myBag.coletados = new Broches();
        myBag.broches.Add(TypeBrochetoString(bocheaddbytype));
        myBag.lastBroche = bocheaddbytype;

        SaveBroches();
    }

    // Update is called once per frame
    void SaveBroches()
    {
        BagBroche brochesToSave = myBag;

        string dataJson = JsonUtility.ToJson(brochesToSave, true);
        File.WriteAllText(pathFile, dataJson);
        // Debug.Log("Dados Salvos");
    }
    void LoadBroches()
    {
        if (File.Exists(pathFile))
        {
            string dataJson = File.ReadAllText(pathFile);

            BagBroche loadBroches = JsonUtility.FromJson<BagBroche>(dataJson);

            if (loadBroches == null)
            {
                loadBroches = new BagBroche();

                // NotificationManage.instance.CreateNotification(null, "Sucess", "Arquivo vaziu");
            }
            else
            {

                // NotificationManage.instance.CreateNotification(null, "Sucess", "Achou o arquivo de broches");
            }
            // if (loadBroches.coletados == null) loadBroches.coletados = new Broches();

            myBag = loadBroches;


        }
        else
        {
            //se não achou, cria pasta e arquivo inicial
            Debug.Log("Não achou o arquivo de broches, criando novo");
            myBag = new BagBroche();

            string dir = Path.GetDirectoryName(pathFile);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            string dataJson = JsonUtility.ToJson(myBag, true);
            File.WriteAllText(pathFile, dataJson);

            // NotificationManage.instance.CreateNotification(null, "Erro", "Não achou arquivo de broches - arquivo criado"); 
        }
        RenderBroches();
        Invoke("AniColect", 0.7f);
        Invoke("CheckFinish", 1.7f);
    }
    void CheckFinish()
    {
        if (numbrochesColeted == 4)
        {
            Debug.Log("Temos o fim do jogo");
            string dataJson = JsonUtility.ToJson(new Broches(), true);
            File.WriteAllText(pathFile, dataJson);
            Finish.Invoke();
        }
    }
    void RenderBroches()
    {
        numbrochesColeted = 0;
        Debug.Log("render broches");
        if (myBag.broches.Count > 0)
            foreach (string namejs in myBag.broches)
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
                        Debug.Log("comparação name broche name : " + namejs + " name img.name: " + img.name + " igual? " + (img.name == name));
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
                                    // NotificationManage.instance.CreateNotification(imgBroche, "Broche Renderizado", $"renderizado broche {namejs}");
                                    break;
                                }
                            }

                        }
                    }
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
    int TypeBrochetoInt(TypeBoches type)
    {
        switch (type)
        {
            case TypeBoches.Rupestre:
                return 0;
            case TypeBoches.Egito:
                return 1;
            case TypeBoches.Renascentista:
                return 2;
            case TypeBoches.Surrealista:
                return 3;
            default:
                return 4;
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
                return TypeBoches.none;
        }
    }
}
