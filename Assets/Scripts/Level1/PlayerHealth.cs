using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{

    [Header("Health Settings")]
    public int maxLives = 3;
    public int CurrentLives { get; private set; }

    void Start()
    {
        CurrentLives = maxLives;
    }

    // برخورد فیزیکی (بدن دشمن)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    // برخورد Trigger (چاقو / توپ)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage();
            Destroy(other.gameObject); // پرتابه نابود شه
        }
    }

    void TakeDamage()
    {
        CurrentLives--;
        Debug.Log("جون باقی‌مانده: " + CurrentLives);

        if (CurrentLives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("💀 Game Over!");
        FindObjectOfType<GameManageer>().GameOver();
    }
}

