using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PositionList
{
    public List<Vector3> positions;

    public PositionList(List<Vector3> positions)
    {
        this.positions = positions;
    }
}


public class PositionPeoplesManage : MonoBehaviour
{
    public Transform playerTransform;
    public Transform guiaTransform;
    string pathFile = "Assets/Saves/Position/Positions.json";

    // Start is called before the first frame update
    void Start()
    {
        // has a position saved?
        //load postitions

        if (PlayerPrefs.GetInt("loadPositions", 0) == 1)
        {
            LoadPositions();
        }
        else
        {
            SavePositions();
        }
    }
    public void LoadPositions()
    {
        if (File.Exists(pathFile))
        {
            string dataJson = File.ReadAllText(pathFile);

            PositionList positions = JsonUtility.FromJson<PositionList>(dataJson);

            playerTransform.position = positions.positions[0];
            guiaTransform.position = positions.positions[1];
            Debug.Log("posições caregadas");
            PlayerPrefs.SetInt("loadPositions", 0);
            PlayerPrefs.Save();
        }
        else
        {
            //se não achou, cria pasta e arquivo inicial
            Debug.Log("Não achou o arquivo de broches, criando novo");
            List<Vector3> positions = new List<Vector3>();
            positions.Add(playerTransform.position);
            positions.Add(guiaTransform.position);

            PositionList positionList = new PositionList(positions);
            string dataJson = JsonUtility.ToJson(positionList, true);

            string dir = Path.GetDirectoryName(pathFile);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            NotificationManage.instance.CreateNotification(null, "Erro Position", "Não achou arquivo de broches - arquivo criado"); 
                
            } 

     
            File.WriteAllText(pathFile, dataJson);
            Debug.Log("Dados Salvos");
            PlayerPrefs.SetInt("loadPositions", 1);
            PlayerPrefs.Save();
        }
    }
    public void SavePositions()
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(playerTransform.position);
        positions.Add(guiaTransform.position);

        PositionList positionList = new PositionList(positions);
        string dataJson = JsonUtility.ToJson(positionList, true);
        File.WriteAllText(pathFile, dataJson);
        Debug.Log("Dados Salvos");
        PlayerPrefs.SetInt("loadPositions", 1);
        PlayerPrefs.Save();

    }
}
