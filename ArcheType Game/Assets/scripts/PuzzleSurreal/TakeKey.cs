using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeKey : MonoBehaviour
{
    public bool chavesurreal = false;
    public GameObject chave;
    public BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Takekey()
    {
        chave.SetActive(false);
        chavesurreal = true;
        if (chavesurreal)
        {
            col.enabled = true;
        }
    }
    

}
