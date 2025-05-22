using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogoSystenUI : MonoBehaviour
{
    public GameObject dialogoBox;
    public TMP_Text nameTextPro;
    public TMP_Text bodyTextPro;
    void Start()
    {
        dialogoBox.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowDialogoBox()
    {
        dialogoBox.SetActive(true);
    }
    public void HiddenDialogoBox()
    {
        dialogoBox.SetActive(false);

    }
    public void SetTextDialogBox(Color32 color, string name, string body)
    {
        nameTextPro.text = name;
        nameTextPro.faceColor = color;
        bodyTextPro.text = body;
    }
}
