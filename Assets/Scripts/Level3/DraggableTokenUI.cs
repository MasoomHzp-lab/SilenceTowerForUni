using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTokenUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Manager")]
    public LockPuzzleManagerUI manager;

    [Header("Snap Settings")]
    public float snapDistance = 120f;

    public int CurrentNumber { get; private set; } = -1;

    private RectTransform rt;
    private Canvas canvas;
    private Vector2 startAnchoredPos;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startAnchoredPos = rt.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;
        rt.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (manager == null || manager.snapPoints == null || manager.snapPoints.Length == 0)
        {
            Debug.LogWarning($"[{name}] Manager/SnapPoints مشکل دارد.");
            rt.anchoredPosition = startAnchoredPos;
            CurrentNumber = -1;
            return;
        }

        int bestIndex = -1;
        float bestDist = float.MaxValue;

        for (int i = 0; i < manager.snapPoints.Length; i++)
        {
            if (manager.snapPoints[i] == null) continue;

            float d = Vector2.Distance(rt.position, manager.snapPoints[i].position);
            if (d < bestDist)
            {
                bestDist = d;
                bestIndex = i;
            }
        }

        if (bestIndex != -1 && bestDist <= snapDistance)
        {
            rt.position = manager.snapPoints[bestIndex].position;

            // چون snapPoints indexها همون عدد هستن: 0..8
            CurrentNumber = bestIndex;

            Debug.Log($"[{name}] Snapped to {CurrentNumber}");
        }
        else
        {
            rt.anchoredPosition = startAnchoredPos;
            CurrentNumber = -1;
            Debug.Log($"[{name}] Not snapped");
        }

        manager.CheckSolved();
    }
}
