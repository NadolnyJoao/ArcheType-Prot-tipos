using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PianoKey : MonoBehaviour
{
[Header("FMOD")]
    [SerializeField] private EventReference pianoNoteEvent;

    [Header("Movimento da tecla")]
    public float pressDepth = 0.1f;
    public float pressSpeed = 5f;

    private Vector3 startPos;
    public bool isPressed = false;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPressed)
        {
            isPressed = true;

            // toca a nota apenas uma vez
            RuntimeManager.PlayOneShot(pianoNoteEvent, transform.position);

            // anima a tecla descendo
            StopAllCoroutines();
            StartCoroutine(PressKey());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPressed = false;

            // anima a tecla subindo
            StopAllCoroutines();
            StartCoroutine(ReleaseKey());
        }
    }

    private System.Collections.IEnumerator PressKey()
    {
        Vector3 targetPos = startPos + Vector3.down * pressDepth;
        while (Vector3.Distance(transform.localPosition, targetPos) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * pressSpeed);
            yield return null;
        }
    }

    private System.Collections.IEnumerator ReleaseKey()
    {
        while (Vector3.Distance(transform.localPosition, startPos) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * pressSpeed);
            yield return null;
        }
    }
}
