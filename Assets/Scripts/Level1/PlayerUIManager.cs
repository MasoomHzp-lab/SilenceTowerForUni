using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class PlayerUIManager : MonoBehaviour
{

    [Header("References")]
    public PlayerHealth playerHealth;   // اسکریپت جون
    public PlayerScore playerScore;     // اسکریپت امتیاز

    [Header("UI Elements")]
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (playerHealth != null && livesText != null)
            livesText.text = " Lives: " + playerHealth.CurrentLives;

        if (playerScore != null && scoreText != null)
            scoreText.text = " Score: " + playerScore.score;
    }

}
