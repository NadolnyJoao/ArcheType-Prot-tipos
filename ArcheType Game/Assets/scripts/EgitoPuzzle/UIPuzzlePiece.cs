using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using FMOD.Studio;
using FMODUnity;

public class UIPuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform originalSlot;
    public RectTransform currentSlot;
    public float snapDistance = 100f;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private PuzzleManager puzzleManager;

    private EventReference PuzzleSound;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
       
    }

    void Start()
    {
        currentSlot = (RectTransform)transform.parent;
        puzzleManager = FindObjectOfType<PuzzleManager>();
        PuzzleSound = puzzleManager.PuzzleSound;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform);
        RuntimeManager.PlayOneShot(PuzzleSound, transform.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        RectTransform targetSlot = puzzleManager.emptySlot;
        float distance = Vector2.Distance(rectTransform.position, targetSlot.position);

        if (distance > snapDistance)
        {
            ReturnToCurrentSlot();

            return;
        }

        RectTransform previousSlot = currentSlot;
        transform.SetParent(targetSlot);
        rectTransform.anchoredPosition = Vector2.zero;
        currentSlot = targetSlot;

        puzzleManager.emptySlot = previousSlot;
        //  JONAS pode acionar um som de peça sendo encaixada

        puzzleManager.CheckWinCondition();
    }

    private void ReturnToCurrentSlot()
    {
        transform.SetParent(currentSlot);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public bool IsInCorrectSlot()
    {
        return currentSlot == originalSlot;
    }
}
