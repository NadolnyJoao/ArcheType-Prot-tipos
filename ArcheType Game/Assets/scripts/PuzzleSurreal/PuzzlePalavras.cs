using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePalavras : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform originalSlot; // slot correto da palavrinha da silva
    public RectTransform currentSlot;  
    public float snapDistance = 100f;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private PuzzleManagerWords puzzleManager;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        currentSlot = (RectTransform)transform.parent;
        puzzleManager = FindObjectOfType<PuzzleManagerWords>();
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


        if (closestSlot.childCount > 0)
        {
            ReturnToCurrentSlot();
            return;
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
