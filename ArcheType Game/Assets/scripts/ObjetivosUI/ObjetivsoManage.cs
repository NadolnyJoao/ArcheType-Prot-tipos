using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjetivsoManage : MonoBehaviour
{

    public TMP_Text textoMainObjt;
    public List<TMP_Text> textsobjetivos = new List<TMP_Text>();
    public List<string> objetivos = new List<string>();
    public Animator ani;
    public void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    public void AddObjetivo(string objt)
    {
        objetivos.Add(objt);
        SetObjtUI();
    }
    private void SetObjtUI()
    {
        textoMainObjt.text = objetivos[objetivos.Count - 1];
        int n = 4;
        if(objetivos.Count < 4)
        {
            n= objetivos.Count;
        }
        for( int i = 0; i <n; i++)
        {

                textsobjetivos[i].text = objetivos[objetivos.Count - 1 - i];
           
        }
    }

    // Update is called once per frame
    public void Expand()
    {
        Debug.Log("expandi objetivo");
        ani.SetTrigger("change");
    }
}
