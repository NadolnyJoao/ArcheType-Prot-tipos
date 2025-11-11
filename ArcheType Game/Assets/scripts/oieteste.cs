using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oieteste : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sombra; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col){
        Debug.Log("me estupra"); 
    }
}
