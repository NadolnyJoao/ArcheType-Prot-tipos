using UnityEngine;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public List<RectTransform> allSlots;

    public void CheckWinCondition()
    {
        UIPuzzlePiece[] pieces = FindObjectsOfType<UIPuzzlePiece>();
        foreach (var piece in pieces)
        {
            if (!piece.IsInCorrectSlot())
                return;
        }

        Debug.Log("🏆 Puzzle completo!");
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
}
