using UnityEngine;
using System.Collections.Generic;

public class SequencePuzzleManager : MonoBehaviour
{
   
  

   public int[] correctSequence = { 2, 1, 3 };

    [Header("Wall to Move")]
    public MovingWall wall;

    private readonly List<int> input = new();

    public void Press(int id)
    {
        input.Add(id);

        Debug.Log($"[Puzzle] Input: {string.Join(",", input)}");

        int index = input.Count - 1;

        // چک اشتباه در همان قدم
        if (index < correctSequence.Length && input[index] != correctSequence[index])
        {
            Debug.LogWarning($"[Puzzle] FAIL at step {index}. Expected {correctSequence[index]} but got {id}");
            input.Clear();
            return;
        }

        if (input.Count == correctSequence.Length)
        {
            Debug.Log("[Puzzle] SUCCESS! Opening wall...");
            input.Clear();

            if (wall == null)
            {
                Debug.LogError("[Puzzle] wall is NULL! Assign MovingWall in Inspector.");
                return;
            }

            wall.Open();
        }
    }
}
