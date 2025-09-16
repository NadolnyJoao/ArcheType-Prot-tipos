using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DimensionTravel : MonoBehaviour
{
    public bool mundonormal;
    public bool mundosonho;
    public Transform player;
    public Transform mundonormalobj;
    public Transform mundosonhoobj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWorld()
    {
        if (mundonormal)
        {
            // Vector3 posicaoPlayer = player.position;
            // posicaoPlayer.y = mundosonhoobj.position.y;
            player.position = mundosonhoobj.position;
            mundosonho = true;
            mundonormal = false;
            Debug.Log("mudou para mundo do sonho");
        }
        if (mundosonho)
        {
            // Vector3 posicaoPlayer = player.position;
            // posicaoPlayer.y = mundonormalobj.position.y;
            player.position = mundonormalobj.position;
            mundonormal = true;
            mundosonho = false;        
        }
    }
    void OnAtaque()
    {
        ChangeWorld();
    }
}
