using UnityEngine;
using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.Events;

public class PuzzleManagerWords : MonoBehaviour
{
    public List<RectTransform> allSlots; // todos os slots do puzzlezudo 
    public GameObject puzzle;
    public GameObject portasaida;
    public GameObject portapuzzle;
    public EventReference doorOpen;
    public UnityEvent finish;
    public void CheckWinCondition()
    {
        PuzzlePalavras[] pieces = FindObjectsOfType<PuzzlePalavras>();

        foreach (var piece in pieces)
        {
            if (!piece.IsInCorrectSlot())
                return;
        }
        portapuzzle.SetActive(false);
        portasaida.SetActive(true);
        RuntimeManager.PlayOneShot(doorOpen, transform.position);
        finish.Invoke();
        Debug.Log("parabens voce não é um astrolopietcus");

        ExitPuzzle();
    }

    public RectTransform GetClosestSlot(RectTransform piece, float snapDistance)
    {
        float minDistance = float.MaxValue;
        RectTransform closest = null;

        foreach (RectTransform slot in allSlots)
        {
            float dist = Vector2.Distance(piece.position, slot.position);
            if (dist < snapDistance && dist < minDistance)
            {
                minDistance = dist;
                closest = slot;
            }
        }

        return closest;
    }

    public void AtivarPuzzle()
    {
        puzzle.SetActive(true);
        // ShufflePieces();
    }

    public void ExitPuzzle()
    {
        puzzle.SetActive(false);
    }

    private void ShufflePieces()
    {
        List<PuzzlePalavras> pieces = new List<PuzzlePalavras>(FindObjectsOfType<PuzzlePalavras>());
        List<RectTransform> availableSlots = new List<RectTransform>(allSlots);

        for (int i = 0; i < availableSlots.Count; i++)
        {
            RectTransform temp = availableSlots[i];
            int randomIndex = Random.Range(i, availableSlots.Count);
            availableSlots[i] = availableSlots[randomIndex];
            availableSlots[randomIndex] = temp;
        }

        for (int i = 0; i < pieces.Count; i++)
        {
            PuzzlePalavras piece = pieces[i];
            RectTransform newSlot = availableSlots[i];

            piece.transform.SetParent(newSlot);
            piece.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            piece.currentSlot = newSlot;
        }
    }

}
