using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiaHelp : MonoBehaviour
{
    public List<Fala> dicas = new List<Fala>();

    public DialogoSystem dialogoSystem;


    void Start()
    {
        dialogoSystem = GetComponent<DialogoSystem>();
        SetDica(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDica(int id)
    {
        dialogoSystem.falas.Clear();
        dialogoSystem.falas.Add(dicas[id]);

        dialogoSystem.SetIndexFalas(0);
    }
}
