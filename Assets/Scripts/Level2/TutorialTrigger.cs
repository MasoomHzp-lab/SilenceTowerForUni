using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [Header("UI")]
    public GameObject tutorialPanel;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);
    }

    // اینو دکمه ضربدر صدا می‌زنه
    public void ClosePanel()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);

        gameObject.SetActive(false); // دیگه تریگر نشه
    }
    }

