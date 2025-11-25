using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerTeste : MonoBehaviour
{
    public BrocheManage brocheteste;
    public PlayerBroche playerbroche;
    public LoadingScript loadingscript;
    public int teste;
    public Image img1;
    public Sprite rupestre;
    public Image img2;
    public Sprite egito;
    public Image img3;
    public Sprite renascentista;
    public Image img4;
    public Sprite surrealista;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerbroche.PlayeAnimation(0);
            img1.sprite = rupestre;
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerbroche.PlayeAnimation(1);
            img2.sprite = egito;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerbroche.PlayeAnimation(2);
            img3.sprite = renascentista;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerbroche.PlayeAnimation(3);
            img4.sprite = surrealista;
        }

        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            loadingscript.LoadScene(11);
        }
        
    }
}
