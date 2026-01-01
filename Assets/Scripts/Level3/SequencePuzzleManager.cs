using UnityEngine;
using System.Collections.Generic;

public class SequencePuzzleManager : MonoBehaviour
{
   
     [Header("Correct Order (Button IDs)")]
    public int[] correctSequence = { 1, 2, 3 };

    [Header("Unlock this collider after success")]
    public Collider2D lockedGoalCollider;

    private readonly List<int> input = new();

    private void Start()
    {
        if (lockedGoalCollider != null)
            lockedGoalCollider.enabled = false; // قفل از اول
    }

    public void Press(int id)
    {
        input.Add(id);

        // چک اشتباه از همون قدم
        int index = input.Count - 1;
        if (index < correctSequence.Length && input[index] != correctSequence[index])
        {
            Fail();
            return;
        }

        if (input.Count == correctSequence.Length)
        {
            Success();
        }
    }

    void Success()
    {
        input.Clear();
        if (lockedGoalCollider != null)
            lockedGoalCollider.enabled = true; // باز شدن پایان مرحله
    }

    void Fail()
    {
        input.Clear();
        // فعلاً فقط ریست میشه (بعداً اگر خواستی اینجا دشمن اسپاون کن)
    }
 
}
