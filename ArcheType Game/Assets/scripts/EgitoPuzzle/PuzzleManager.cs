using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using Unity.VisualScripting;
using FMODUnity;
using FMOD.Studio;

public class PuzzleManager : MonoBehaviour
{
    public List<RectTransform> allSlots;
    public GameObject puzzle;
    public RectTransform emptySlot;
    public PortaControler portacontroller;
    public bool PuzzleCompleto = false;
    public GameObject Interact;
    public EventReference PuzzleSound;
    public EventReference pieceDrop;
    public UnityEvent finishPuzzleEvent;
    private bool jaShufflePieces = false;
    void Start()
    {
        GameObject porta = GameObject.FindWithTag("portaegito");
        if(porta != null)
        portacontroller = porta.GetComponent<PortaControler>();
    }

    public void CheckWinCondition()
    {
        UIPuzzlePiece[] pieces = FindObjectsOfType<UIPuzzlePiece>();
        foreach (var piece in pieces)
        {
            if (!piece.IsInCorrectSlot())
            {
                RuntimeManager.PlayOneShot(pieceDrop, transform.position);
                return;
            }
        }
        PuzzleCompleto = true;
        if(portacontroller != null)
        portacontroller.Abrir();

        
        if (PuzzleCompleto)
        {
            Debug.Log("🏆 Puzzle completo!");
            ExitPuzzle();
            finishPuzzleEvent.Invoke();
            Object.Destroy(Interact, 0);

        }
    }

    public void AtivarPuzzle()
    {
        if (PuzzleCompleto == false)
        {
            puzzle.SetActive(true);
            if(!jaShufflePieces)
                ShufflePieces();
            
        }

    }

    public void ExitPuzzle()
    {

        puzzle.SetActive(false);
    }

    private void ShufflePieces()
    {
        jaShufflePieces = true;
        List<UIPuzzlePiece> pieces = new List<UIPuzzlePiece>(FindObjectsOfType<UIPuzzlePiece>());
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
            UIPuzzlePiece piece = pieces[i];
            RectTransform newSlot = availableSlots[i];

            piece.transform.SetParent(newSlot);
            piece.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            piece.currentSlot = newSlot;
        }

        emptySlot = availableSlots[pieces.Count];

    }
}
