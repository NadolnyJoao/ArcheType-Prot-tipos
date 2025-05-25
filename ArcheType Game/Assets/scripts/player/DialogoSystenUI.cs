using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogoSystenUI : MonoBehaviour
{
    public float delayChar = 0.5f;
    public GameObject dialogoBox;
    public TMP_Text nameTextPro;
    public TMP_Text bodyTextPro;
    private string textFromShow;
    void Start()
    {
        dialogoBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AnimationText()
    {
        if (bodyTextPro.text.Length != textFromShow.Length)
        {
            bodyTextPro.text += textFromShow[bodyTextPro.text.Length];
            Invoke("AnimationText", delayChar);
        }
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
        textFromShow = body;
        bodyTextPro.text = "";
        Invoke("AnimationText", delayChar);
            
    }
}
