using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogarPedra : MonoBehaviour
{
    private PlayerMoviment playerMov;
    public GameObject prefabPedra;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        playerMov = GetComponent<PlayerMoviment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LancarPedra();
        }
    }
    public void LancarPedra()
    {
        GameObject pedra = Instantiate(prefabPedra, transform.position, Quaternion.identity);
        int  direction = playerMov.GetDirection();
        pedra.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f * direction, 1f) * force, ForceMode2D.Impulse);
    }
}
