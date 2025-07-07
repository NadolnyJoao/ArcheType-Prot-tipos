using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogarPedra : MonoBehaviour
{
    private PlayerMoviment playerMov;
    public GameObject prefabPedra;

    public float countdown = 0.5f; // Tempo de espera entre lançamentos
    private float time = 0;

    public float force;
    // Start is called before the first frame update
    void Start()
    {
        playerMov = GetComponent<PlayerMoviment>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = 0;
        }


        if (Input.GetKeyDown(KeyCode.E) && time == 0)
        {
            LancarPedra();
            time = countdown; // Reinicia o tempo de espera
        }
    }
    public void LancarPedra()
    {
        GameObject pedra = Instantiate(prefabPedra, transform.position + Vector3.right * 2.0f * playerMov.GetDirection(), Quaternion.identity);
        int  direction = playerMov.GetDirection();
        pedra.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f * direction, 1f) * force, ForceMode2D.Impulse);
    }
}
