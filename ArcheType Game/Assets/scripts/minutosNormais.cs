using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minutosNormais : MonoBehaviour
{
    
    public float contadorsegs; 
    public int min = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        contadorsegs += 20 * Time.deltaTime; 

        if (contadorsegs >= 60)
        {
            min = min - 15;
            contadorsegs = 0; 
            transform.rotation = Quaternion.Euler(0,0,0); 
            transform.Rotate(0f, 0f, min);
        }

        

        
    }
}
