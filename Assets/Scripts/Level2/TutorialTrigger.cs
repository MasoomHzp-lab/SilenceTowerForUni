using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [Header("UI")]
    public GameObject tutorialPanel;

   private void OnTriggerEnter2D(Collider2D other)
{
    if (!other.CompareTag("Player")) return;
    if (tutorialPanel == null) return;
    if (tutorialPanel.activeSelf) return;

    tutorialPanel.SetActive(true);
}


    // اینو دکمه ضربدر صدا می‌زنه
    public void ClosePanel()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }
}
