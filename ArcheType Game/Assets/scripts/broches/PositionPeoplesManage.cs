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
    string pathFile = "Assets/Saves/Positions.json";

    // Start is called before the first frame update
    void Start()
    {
        // has a position saved?
        //load postitions

        if (PlayerPrefs.HasKey("startgame"))
        {
            if (PlayerPrefs.GetInt("startgame") == 0)
            {
                LoadPositions();
                Debug.Log("carregar as posições");
            }
        }
        else
        {
            
            PlayerPrefs.SetInt("startgame", 0);
            PlayerPrefs.Save();
            Debug.Log("primeira vez na fase menu");
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

    }
}
