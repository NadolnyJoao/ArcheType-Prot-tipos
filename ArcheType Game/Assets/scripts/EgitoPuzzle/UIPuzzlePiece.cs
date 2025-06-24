using UnityEngine;
using UnityEngine.EventSystems;

public class UIPuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform originalSlot;
    public RectTransform currentSlot;
    public float snapDistance = 100f; 

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private PuzzleManager puzzleManager;

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
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        RectTransform closestSlot = puzzleManager.GetClosestSlot(rectTransform, snapDistance);

        if (closestSlot == null)
        {
            ReturnToCurrentSlot();
            return;
        }

        UIPuzzlePiece otherPiece = null;
        if (closestSlot.childCount > 0)
        {
            otherPiece = closestSlot.GetComponentInChildren<UIPuzzlePiece>();
        }

        if (otherPiece != null)
        {
            RectTransform oldSlot = currentSlot;

            otherPiece.transform.SetParent(oldSlot);
            otherPiece.rectTransform.anchoredPosition = Vector2.zero;
            otherPiece.currentSlot = oldSlot;
        }


        transform.SetParent(closestSlot);
        rectTransform.anchoredPosition = Vector2.zero;
        currentSlot = closestSlot;

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
