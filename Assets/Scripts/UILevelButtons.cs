using UnityEngine;

public class UILevelButtons : MonoBehaviour
{
     public void NextLevel()
    {
        if (GameManageer.Instance != null) GameManageer.Instance.NextLevel();
        else Debug.LogError("GameManageer.Instance is NULL");
    }

    public void Restart()
    {
        if (GameManageer.Instance != null) GameManageer.Instance.RestartGame();
    }

    public void GoMenu()
    {
        if (GameManageer.Instance != null) GameManageer.Instance.LoadMenu();
    }
}
