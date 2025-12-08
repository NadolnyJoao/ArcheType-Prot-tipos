using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedraControleDestroy : MonoBehaviour
{

    public void Start(){
        Invoke("DestroyPedra",5.0f);
    }
    void DestroyPedra(){
        Destroy(this.gameObject);
    }

}
