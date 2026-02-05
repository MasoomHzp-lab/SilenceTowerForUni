using UnityEngine;

public class PlayerScore : MonoBehaviour
{
 [Header("Score Settings")]
    public int score = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Star")) // ستاره‌ها Tag = Star
        {
            score++;
            Debug.Log(" امتیاز: " + score);
            if (AudioManager.Instance != null) AudioManager.Instance.SFX_Star();
            // حذف ستاره بعد از برخورد
            Destroy(other.gameObject);
        }
    }
}
