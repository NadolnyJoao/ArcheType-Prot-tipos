using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
//iclui a biblioteca de UI tmpro da unity
// Required when using Event data.

public class ColorHover : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler//
{

    public GameObject nameHover;
    public TMP_Text textHover;

    // Start is called before the first frame update
    void Start()
    {
        textHover = nameHover.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    public void SetName(string name)
    {
        textHover.text = name;
    }

       public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the color of the GameObject to red when the mouse is over GameObject
        nameHover.SetActive(true);
         Debug.Log("Mouse is over GameObject.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the color of the GameObject back to normal
        nameHover.SetActive(false);
         Debug.Log("Mouse no is over GameObject.");
    }
}
