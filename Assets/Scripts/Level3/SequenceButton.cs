using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class SequenceButton : MonoBehaviour
{
public int buttonID;
    public SequencePuzzleManager manager;

    [Header("Optional - if empty, uses Player tag")]
    public string requiredColliderTag = ""; // مثلا "Foot"

    private int overlapCount = 0; // برای جلوگیری از ریست شدن زودهنگام وقتی چند collider داریم

    private bool IsValid(Collider2D other)
    {
        if (!string.IsNullOrEmpty(requiredColliderTag))
            return other.CompareTag(requiredColliderTag);

        return other.CompareTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsValid(other)) return;

        // اولین ورود معتبر = ثبت دکمه
        if (overlapCount == 0)
        {
            if (manager == null)
            {
                Debug.LogError($"[Button {buttonID}] Manager is NULL! Assign it in Inspector.");
                return;
            }

            Debug.Log($"[Button {buttonID}] Pressed by {other.name}");
            manager.Press(buttonID);   // ✅ این همون متدی هست که تو پروژه‌ت وجود داره
        }

        overlapCount++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsValid(other)) return;

        overlapCount = Mathf.Max(0, overlapCount - 1);
    }

}
