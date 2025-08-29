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
    private DialogoSystem dialogoAtual;
    public static DialogoSystenUI dialogoSystenUI { get; private set; }
    private bool showDialogoBox = false;
    void Awake()
    {
        if (dialogoSystenUI != null)
        {
            Debug.Log("ALGUMA COISAS REFERENTE AO DIALOGOSYSTEMUI");
        }
        dialogoSystenUI = this;
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.W) && !showDialogoBox)
        // {
        //     HiddenDialogoBox();
        // }
    }
    public bool DialogoBoxIsShow()
    {
        return dialogoBox.activeSelf;
    }
 public void SetDialogoAtual(DialogoSystem sistema)
    {
        dialogoAtual = sistema;
    }
    public void DebugTeste()
    {
        Debug.Log("funcionando");
    }
    void Start()
    {
        dialogoBox.SetActive(false);
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
        showDialogoBox = true;
        Invoke("CanHiddenDialogoBox", 1f);
    }
    public void HiddenDialogoBox()
    {
        dialogoBox.SetActive(false);

    }
    private void CanHiddenDialogoBox()
    {
       showDialogoBox = false;
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
