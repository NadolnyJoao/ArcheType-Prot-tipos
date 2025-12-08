using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public Sprite sprite;
    public string titleText;
    public string bodyText;
    private Animator animator;
    [SerializeField] private Image img;
    
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text body;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
     public void SetData(Sprite nsprite, string ntitle, string nbody)
    {
        sprite = nsprite;
        titleText = ntitle;
        bodyText = nbody;
    }
    public void SetData(Notification not)
    {
        sprite = not.sprite;
        titleText = not.titleText;
        bodyText = not.bodyText;
    }
    
    public void StartAnimation()
    {
        DefineUI();
        Debug.Log($"Inicar animação do popup {gameObject.name}");
    }
    public void DefineUI()
    {
        img.sprite = sprite;
        title.text = titleText;
        body.text = bodyText;
    }
}
