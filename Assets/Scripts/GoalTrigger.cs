using UnityEngine;
using UnityEngine.SceneManagement;


public class GoalTrigger : MonoBehaviour
{
    
void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
{
    Debug.Log("🎉 برنده شدی!");
    FindObjectOfType<GameManageer>().Win();
}

    }

}
