using UnityEngine;

public class DeathZone : MonoBehaviour
{
   private GameManageer gm;

    void Start()
    {
        gm = FindObjectOfType<GameManageer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("💀 پلیر افتاد داخل DeathZone!");
            gm.GameOver();
        }
    }
}
