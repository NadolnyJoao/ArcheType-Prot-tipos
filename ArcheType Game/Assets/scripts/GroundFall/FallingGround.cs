using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGround : MonoBehaviour
{   
    public float timeToFall = 1.0f;
    float timeToDestroy = 0.5f;
    private Rigidbody2D rg; 
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>(); 
        rg.isKinematic = true; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Player")){
            Debug.Log("OIe"); 
            Invoke("Cair",timeToFall); 
        }
    }
    void Cair(){
        rg.isKinematic = false;
        Destroy(this.gameObject, timeToDestroy); 
        
    }
}
