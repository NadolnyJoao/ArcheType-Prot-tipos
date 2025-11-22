using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBroche : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerMoviment playerMov;
    private Animator ani;
    void Start()
    {
        playerMov = GetComponent<PlayerMoviment>();
        ani = GetComponent<Animator>();
        if(playerMov == null)
        {
            Debug.Log("PlayerBrohce não tem acesso a PlayerMovimento");
        }
    }

    // Update is called once per frame
    public void PlayeAnimation(int numBroche)
    {
        playerMov.enabled = false;
        ani.SetInteger("numBroche",numBroche);
        ani.SetTrigger("broche");
        Debug.Log($"Plat trigger ani broches how number {numBroche}");

        Invoke("AtivePLayerMOv",1);
    }
    void AtivePLayerMOv()
    {
        Debug.Log("Reativa PlayerMoviment");
        ani.SetInteger("numBroche",4);
        playerMov.enabled = true;
    }
}
